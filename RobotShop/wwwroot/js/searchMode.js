function toggleSearchForm() {
   const toggle = document.getElementById("searchToggle");

   const labelName = document.getElementById("label-name");
   const labelSpec = document.getElementById("label-spec");

   const nameContainer = document.getElementById("nameSearchContainer");
   const specContainer = document.getElementById("specSearchContainer");

   const nameInput = document.getElementById("searchQuery");
   const specInput = specContainer.querySelector("input[name='specQuery']");

   nameContainer.classList.remove("active");
   specContainer.classList.remove("active");

   if (toggle.checked) {
      nameContainer.style.display = "none";
      specContainer.style.display = "block";

      nameInput.disabled = true;
      specInput.disabled = false;

      labelSpec.classList.add("active");
      labelName.classList.remove("active");

      setTimeout(() => specContainer.classList.add("active"), 10);
   } else {
      specContainer.style.display = "none";
      nameContainer.style.display = "block";

      specInput.disabled = true;
      nameInput.disabled = false;

      labelName.classList.add("active");
      labelSpec.classList.remove("active");

      setTimeout(() => nameContainer.classList.add("active"), 10);
   }
}

document.addEventListener("DOMContentLoaded", () => {
   const toggle = document.getElementById("searchToggle");

   toggleSearchForm();

   toggle.addEventListener("change", toggleSearchForm);
});