function toggleSearchMode() {
   const mode = document.getElementById("searchMode");
   if (mode.value === "products") {
      mode.value = "specs";
      document.getElementById("normalSearchContainer").style.display = "none";
      document.getElementById("specSearchContainer").style.display = "block";
   } else {
      mode.value = "products";
      document.getElementById("normalSearchContainer").style.display = "block";
      document.getElementById("specSearchContainer").style.display = "none";
   }
}