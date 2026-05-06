function toggleSearchForm() {
	const toggle = document.getElementById("searchToggle");
	const labelName = document.getElementById("label-name");
	const labelSpec = document.getElementById("label-spec");
	const searchForm = document.getElementById("nameSearchContainer");
	const specForm = document.getElementById("specSearchContainer");

	// Ascunde ambele fără flicker
	searchForm.classList.remove("active");
	specForm.classList.remove("active");

	setTimeout(() => {
		if (toggle.checked) {
			searchForm.style.display = "none";
			specForm.style.display = "block";
			specForm.classList.add("active");

			labelSpec.classList.add("active");
			labelName.classList.remove("active");
		} else {
			specForm.style.display = "none";
			searchForm.style.display = "block";
			searchForm.classList.add("active");

			labelName.classList.add("active");
			labelSpec.classList.remove("active");
		}
	}, 50); // Mic delay pentru tranziție
}

document.addEventListener("DOMContentLoaded", () => {
	toggleSearchForm(); // Inițializează corect la load
});