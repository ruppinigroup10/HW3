let mainD = document.getElementById("main");

// Function to get and render all games
function getAllGames() {
  console.log("in getAllGames");
  // const api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games";
  const api = `https://localhost:${PORT}/api/Games`;
  ajaxCall("GET", api, "", renderGames, errorCB);
  console.log("after ajax");
}

// Render games function (same logic as your original forEach)
function renderGames(games) {
  console.log("games recived:", games);
  mainD.innerHTML = ""; // Clear existing content

  games.forEach((game) => {
    //console.log("in game:", game);
    const gameDiv = document.createElement("div");
    gameDiv.classList.add("card");

    gameDiv.innerHTML += `<img src="${game.HeaderImage}">`;
    gameDiv.innerHTML += `<h3>${game.Name}</h3>`;
    gameDiv.innerHTML += `<h4>${game.ReleaseDate}</h4>`;
    gameDiv.innerHTML += `<h4>${game.Publisher}</h4>`;
    gameDiv.innerHTML += `<h4>${game.Price}$</h4>`;
    gameDiv.innerHTML += `<button type="button" id="${game.AppID}">Add to MyGAMES</button>`;

    mainD.appendChild(gameDiv);
  });
}

function errorCB(error) {
  console.log("in error");
  console.error("Error getting games:", error);
  mainD.innerHTML = "<p>Error loading games. Please try again later.</p>";
}

// GAME.forEach((game, index) => {
//   const gameDiv = document.createElement("div");
//   gameDiv.classList.add("card");
//   //combine all to one innerHtml
//   gameDiv.innerHTML += `<img src="${game.Header_image}">`;
//   gameDiv.innerHTML += `<h3>${game.Name}</h3>`;
//   gameDiv.innerHTML += `<h4>${game.Release_date}</h4>`;
//   gameDiv.innerHTML += `<h4>${game.Developers}</h4>`;
//   gameDiv.innerHTML += `<h4>${game.Price}$</h4>`;
//   gameDiv.innerHTML += `<button type="button" id="${game.AppID}">Add to MyGAMES</button>
// `;

//   mainD.appendChild(gameDiv);
// });

function convertDateFormat(dateStr) {
  const date = new Date(dateStr);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`; // DateOnly in C#
}

mainD.addEventListener("click", (e) => {
  if (e.target.tagName === "BUTTON") {
    // GAME.forEach((game) => {
    //   if (game.AppID == e.target.id) {
    //     const GameToPost = {
    //       appID: parseInt(game.AppID),
    //       name: game.Name,
    //       releaseDate: new Date(game.Release_date).toISOString().split("T")[0], // Convert to yyyy-MM-dd
    //       price: parseFloat(game.Price),
    //       description: game.description || "",
    //       headerImage: game.Header_image || "",
    //       website: game.Website || "",
    //       windows: game.Windows === "TRUE",
    //       mac: game.Mac === "TRUE",
    //       linux: game.Linux === "TRUE",
    //       scoreRank: parseInt(game.Score_rank) || 0,
    //       recommendations: game.Recommendations || "",
    //       publisher: game.Developers || "",
    //     };

    //     console.log("GAME Data being sent:", GameToPost); // Log data before sending

    //     const UserToPost = JSON.parse(localStorage.getItem("user"));

    //     console.log("USER Data being sent:", UserToPost); // Log data before sending

    //     // const api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Games";
    //     const api = `https://localhost:${PORT}/api/Games`;

    //     const GameUser = { game: GameToPost, user: UserToPost };
    //     ajaxCall("POST", api, JSON.stringify(GameUser), postSCB, postECB);

    //     function postSCB(result) {
    //       console.log("Success:", result);
    //       Swal.fire({
    //         title: "All done!",
    //         text: "Game added Successfully",
    //         icon: "success",
    //       });
    //     }
    //     function postECB(error) {
    //       console.error("Failed:", error);
    //       if (error.responseJSON) {
    //         Swal.fire({
    //           icon: "error",
    //           title: "Oops...",
    //           text: error.responseJSON.message,
    //           //footer: '<a href="#">Why do I have this issue?</a>',
    //         });
    //       } else {
    //         alert(error.responseText);
    //       }
    //     }
    //   }
    // });

    if (e.target.tagName === "BUTTON") {
      const gameId = e.target.id;
      const gameCard = e.target.closest(".card");
      if (!gameCard) return;

      const GameToPost = {
        appID: parseInt(gameId),
        name: gameCard.querySelector("h3").textContent,
        releaseDate: convertDateFormat(
          gameCard.querySelectorAll("h4")[0].textContent
        ),
        price: parseFloat(
          gameCard.querySelectorAll("h4")[2].textContent.replace("$", "")
        ),
        publisher: gameCard.querySelectorAll("h4")[1].textContent,
        headerImage: gameCard.querySelector("img").src,
      };

      const UserToPost = JSON.parse(localStorage.getItem("user"));
      if (!UserToPost) {
        alert("Please log in first");
        return;
      }

      console.log("Sending data:", { game: GameToPost, user: UserToPost });

      const api = `https://localhost:${PORT}/api/Games`;
      const GameUser = { game: GameToPost, user: UserToPost };

      ajaxCall("POST", api, JSON.stringify(GameUser), postSCB, postECB);
    }

    function postSCB(result) {
      console.log("Success:", result);
      Swal.fire({
        title: "All done!",
        text: "Game added Successfully",
        icon: "success",
      });
    }

    function postECB(error) {
      console.error("Failed:", error);
      if (error.responseJSON) {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: error.responseJSON.message,
        });
      } else {
        alert(error.responseText);
      }
    }
  }
});

console.log("enter to func");
getAllGames();

////////////////////////////////////////////////
// Check if user is logged in and display info//
////////////////////////////////////////////////
const user = JSON.parse(localStorage.getItem("user"));

if (user && user.isLoggedIn) {
  $("#userInfo").html(`
          <div>
              <p>Welcome, ${user.name || user.email}!</p>
              <a href="/Pages/MyGames.html" style="color: white; margin-right: 10px;">My Games</a>
              <button onclick="logout()" class="btn btn-danger">Logout</button>
          </div>
        `);
} else {
  // Show login link for non-logged-in users
  $("#userInfo").html(`
          <div>
              <a href="/Pages/login.html" style="color: white;">Login</a>
          </div>
        `);
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
