// Function to fetch all games
function getGames() {
  // const api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games";
  const api = `https://localhost:${PORT}/api/Games`;
  console.log("Making API call to:", api);
  ajaxCall("GET", api, "", getSCB, getECB);
}

// Success callback for AJAX calls
function getSCB(gamesData) {
  console.log("Games data received:", gamesData);
  if (gamesData) {
    console.log("Rendering games...");
    renderGames(gamesData);
  } else {
    console.error("No games data received");
  }
}

// Error callback for AJAX calls
function getECB(err) {
  console.error("Error occurred:", err);
}

// Main renderGames function to display games
function renderGames(games) {
  const mainDiv = document.getElementById("main");
  if (!mainDiv) {
    console.error("Main div not found");
    return;
  }

  mainDiv.innerHTML = ""; // Clear current content

  games.forEach((game) => {
    //console.log("Game object properties:", Object.keys(game)); // This will show us the actual property names
    //console.log("Full game object:", game); // This will show us the full object
    const gameDiv = document.createElement("div");
    gameDiv.classList.add("card");

    gameDiv.innerHTML = `
            <img src="${game.HeaderImage || "placeholder-image.jpg"}" alt="${
      game.Name
    }">
            <h3>${game.Name || "Untitled Game"}</h3>
            <h4>Release Date: ${
              new Date(game.ReleaseDate).toISOString().split("T")[0] ||
              "Release date not available"
            }</h4>
            <h4>Developer: ${game.Publisher || "Developer unknown"}</h4>
            <h4 id="priceFilter">Price: ${
              game.Price ? game.Price + "$" : 0
            }</h4>
            <h4 id="rankFilter">Rank: ${parseInt(game.ScoreRank) || 0}</h4>
            <button type="button" onclick="deleteGame(${
              game.AppID
            })">Delete Game</button>
        `;

    mainDiv.appendChild(gameDiv);
  });
}

// Function to delete a game
function deleteGame(gameId) {
  Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("Initiating delete for game:", gameId);

      ajaxCall(
        "DELETE",
        // `https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games/${gameId}`,
        `https://localhost:${PORT}/api/Games/${gameId}`,
        null,
        deleteSCB,
        deleteECB
      );
    }
  });

  function deleteSCB(response) {
    if (response.message) {
      Swal.fire({
        title: "Deleted!",
        text: response.message,
        icon: "success",
      });
    }
    getGames(); // Refresh the list after successful deletion
  }

  function deleteECB(error) {
    console.error("Delete failed:", error);
    Swal.fire({
      title: "Error!",
      text: error.responseJSON
        ? error.responseJSON.message
        : "Failed to delete game",
      icon: "error",
    });
  }
}

// Function to filter games by price
function filterByPrice() {
  const minPrice = document.getElementById("priceFilter").value;
  if (!minPrice) {
    console.warn("No minimum price specified");
    Swal.fire({
      title: "No value?",
      text: "Please enter a minimum price to filter games",
      icon: "question",
    });
    return;
  }

  ajaxCall(
    "GET",
    // `https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games/GetByPrice?minPrice=${minPrice}`,
    `https://localhost:${PORT}/api/Games/GetByPrice?minPrice=${minPrice}`,
    "",
    getSCB, // Using the same success callback
    getECB
  );
}

// Function to filter games by rank
function filterByRank() {
  const minScore = document.getElementById("rankFilter").value;
  if (!minScore) {
    console.warn("No minimum score specified");
    Swal.fire({
      title: "No value?",
      text: "Please enter a minimum score to filter games",
      icon: "question",
    });
    return;
  }

  ajaxCall(
    "GET",
    // `https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games/GetByRankScore/minRankScore/${minScore}`,
    `https://localhost:${PORT}/api/Games/GetByRankScore/minRankScore/${minScore}`,
    "",
    getSCB, // Using the same success callback
    getECB
  );
}

$(document).ready(() => {
  getGames();
});

////////////////////////////////////////////////
// Check if user is logged in and display info//
////////////////////////////////////////////////
const user = JSON.parse(localStorage.getItem("user"));

if (user && user.isLoggedIn) {
  $("#userInfo").html(`
            <div>
                <p>Welcome, ${user.name || user.email}!</p>
                <a href="/Pages/index.html" style="color: white; margin-right: 10px;">Home</a>
                <button onclick="logout()" class="btn btn-danger">Logout</button>
            </div>
        `);
} else {
  window.location.replace("/Pages/login.html");
}

////////////////////
// Logout function//
////////////////////
function logout() {
  localStorage.removeItem("user");
  localStorage.removeItem("userCredentials"); // Remove sensitive info too
  Swal.fire({
    title: "Logged Out!",
    text: "You have been successfully logged out",
    icon: "success",
    timer: 1500,
    showConfirmButton: false,
  }).then(() => {
    window.location.replace("/Pages/login.html");
  });
}
