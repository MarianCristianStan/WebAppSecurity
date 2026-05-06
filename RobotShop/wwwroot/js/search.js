let selectedSuggestionIndex = -1;

function fetchSuggestions(query) {
	if (!query) {
		hideSuggestions();
		return;
	}

	fetch(`/Home/GetSearchSuggestions?term=${encodeURIComponent(query)}`)
		.then(res => res.json())
		.then(data => {
			const container = document.getElementById("suggestions");
			container.innerHTML = "";
			selectedSuggestionIndex = -1;

			data.forEach((item, index) => {
				const div = document.createElement("div");
				div.className = "suggestion-item";
				div.textContent = item;
				div.dataset.index = index;

				div.addEventListener("click", () => {
					document.getElementById("searchQuery").value = item;
					hideSuggestions();
					document.getElementById("searchForm").submit();
				});

				container.appendChild(div);
			});

			if (data.length > 0) {
				container.classList.add("active");
			} else {
				hideSuggestions();
			}
		});
}

function hideSuggestions() {
	const container = document.getElementById("suggestions");
	container.classList.remove("active");
	container.innerHTML = "";
	selectedSuggestionIndex = -1;
}

document.addEventListener("DOMContentLoaded", () => {
	const input = document.getElementById("searchQuery");

	input.addEventListener("keydown", (e) => {
		const suggestions = document.querySelectorAll(".suggestion-item");

		if (suggestions.length === 0) return;

		if (e.key === "ArrowDown") {
			e.preventDefault();
			selectedSuggestionIndex = (selectedSuggestionIndex + 1) % suggestions.length;
		} else if (e.key === "ArrowUp") {
			e.preventDefault();
			selectedSuggestionIndex = (selectedSuggestionIndex - 1 + suggestions.length) % suggestions.length;
		} else if (e.key === "Enter") {
			if (selectedSuggestionIndex >= 0 && selectedSuggestionIndex < suggestions.length) {
				e.preventDefault();
				const selectedText = suggestions[selectedSuggestionIndex].textContent;
				input.value = selectedText;
				hideSuggestions();
				document.getElementById("searchForm").submit();
			}
			return;
		}

		suggestions.forEach((sugg, i) => {
			sugg.style.background = i === selectedSuggestionIndex ? "#fff8dc" : "white";
		});
	});
});
