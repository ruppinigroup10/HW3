//////////////////////////////////
// Password toggle functionality//
//////////////////////////////////
$(".password-toggle").click(function () {
  const passwordField = $(this).siblings("input");
  if (passwordField.attr("type") === "password") {
    passwordField.attr("type", "text");
    $(this).removeClass("fa-eye").addClass("fa-eye-slash");
  } else {
    passwordField.attr("type", "password");
    $(this).removeClass("fa-eye-slash").addClass("fa-eye");
  }
});
////////////////////////////////
// Tab switching functionality//
////////////////////////////////
$(".tab-button").click(function () {
  $(".tab-button").removeClass("active");
  $(this).addClass("active");
  const formId = $(this).data("form");
  $(".form_con").hide();
  $(`#${formId}`).fadeIn();
});

//////////////////////////
// Show login by default//
//////////////////////////
$('.tab-button[data-form="login"]').click();

////////////////////////
// Login functionality//
////////////////////////
$("#login").submit(function () {
  user = {
    email: $("#log_email").val(),
    password: $("#log_password").val(),
  };
  //api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Users/Login";
  api = `https://localhost:${PORT}/api/Users/Login`;

  console.log("Attempting login with:", user);

  ajaxCall("POST", api, JSON.stringify(user), lscb, lecb);
  return false;
});

function lscb(result) {
  console.log("Login success:", result);

  const userEmail = $("#log_email").val();
  //const api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Users";
  const api = `https://localhost:${PORT}/api/Users`;

  ajaxCall(
    "GET",
    api,
    "",
    function (usersData) {
      console.log("Users data received:", usersData);

      const currentUser = usersData.find((user) => user.email === userEmail);

      if (currentUser) {
        // Store sensitive info separately
        localStorage.setItem(
          "userCredentials",
          JSON.stringify({
            email: currentUser.email,
            password: $("#log_password").val(),
          })
        );

        // Store display info without password
        localStorage.setItem(
          "user",
          JSON.stringify({
            id: currentUser.id,
            name: currentUser.name,
            email: currentUser.email,
            isLoggedIn: true,
          })
        );
      } else {
        localStorage.setItem(
          "user",
          JSON.stringify({
            email: userEmail,
            isLoggedIn: true,
          })
        );
      }

      Swal.fire({
        title: "Success!",
        text:
          typeof result === "string"
            ? result
            : result.message || "Logged in successfully",
        icon: "success",
        timer: 1500,
        showConfirmButton: false,
      }).then(() => {
        window.location.replace("/Pages/index.html");
      });
    },
    function (err) {
      console.log("GET request error details:", err);
      localStorage.setItem(
        "user",
        JSON.stringify({
          email: userEmail,
          isLoggedIn: true,
        })
      );

      Swal.fire({
        title: "Success!",
        text:
          typeof result === "string"
            ? result
            : result.message || "Logged in successfully",
        icon: "success",
        timer: 1500,
        showConfirmButton: false,
      }).then(() => {
        window.location.replace("/Pages/index.html");
      });
    }
  );
}

function lecb(err) {
  console.log(err);
  if (err.responseJSON) {
    Swal.fire({
      title: "Error!",
      text: err.responseJSON.message,
      icon: "error",
      confirmButtonText: "OK",
    });
  } else {
    Swal.fire({
      title: "Error!",
      text: err.responseText,
      icon: "error",
      confirmButtonText: "OK",
    });
  }
}

///////////////////////////
// Register functionality//
///////////////////////////
$("#Register").submit(function () {
  //alert("in register submit");
  user = {
    name: $("#reg_name").val(),
    email: $("#reg_email").val(),
    password: $("#reg_password").val(),
  };
  //api ="https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Users/Register";
  api = `https://localhost:${PORT}/api/Users/Register`;

  ajaxCall("POST", api, JSON.stringify(user), rscb, recb);
  return false;
});

function rscb(result) {
  console.log("Register success:", result);

  const userEmail = $("#reg_email").val();
  //const api = "https://proj.ruppin.ac.il/igroup10/test2/tar1/api/Users";
  const api = `https://localhost:${PORT}/api/Users`;

  ajaxCall(
    "GET",
    api,
    "",
    function (usersData) {
      console.log("Users data received:", usersData);

      const currentUser = usersData.find((user) => user.email === userEmail);

      if (currentUser) {
        // Store sensitive info separately
        localStorage.setItem(
          "userCredentials",
          JSON.stringify({
            email: currentUser.email,
            password: $("#reg_password").val(),
          })
        );

        // Store display info without password
        localStorage.setItem(
          "user",
          JSON.stringify({
            id: currentUser.id,
            name: currentUser.name,
            email: currentUser.email,
            isLoggedIn: true,
          })
        );
      } else {
        localStorage.setItem(
          "user",
          JSON.stringify({
            email: userEmail,
            isLoggedIn: true,
          })
        );
      }

      Swal.fire({
        title: "Success!",
        text:
          typeof result === "string"
            ? result
            : result.message || "Registered successfully",
        icon: "success",
        timer: 1500,
        showConfirmButton: false,
      }).then(() => {
        window.location.replace("/Pages/index.html");
      });
    },
    function (err) {
      console.log("GET request error details:", err);
      localStorage.setItem(
        "user",
        JSON.stringify({
          email: userEmail,
          isLoggedIn: true,
        })
      );

      Swal.fire({
        title: "Success!",
        text:
          typeof result === "string"
            ? result
            : result.message || "Registered successfully",
        icon: "success",
        timer: 1500,
        showConfirmButton: false,
      }).then(() => {
        window.location.replace("/Pages/index.html");
      });
    }
  );
}

function recb(err) {
  console.log(err);
  if (err.responseJSON) {
    Swal.fire({
      title: "Error!",
      text: err.responseJSON.message,
      icon: "error",
      confirmButtonText: "Try Again",
      confirmButtonColor: "#d33",
      showClass: {
        popup: "animate__animated animate__fadeIn",
      },
    });
  } else {
    Swal.fire({
      title: "Error!",
      text: err.responseText,
      icon: "error",
      confirmButtonText: "Try Again",
      confirmButtonColor: "#d33",
      showClass: {
        popup: "animate__animated animate__fadeIn",
      },
    });
  }
}
