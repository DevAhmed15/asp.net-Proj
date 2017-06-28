/*global $*/
$(document).ready(function () {
    'use strict';
    $('.carousel').carousel('pause');
    $('.carousel').carousel({
        interval: 3000
    });
});

//Login And Register form
var Login = document.getElementById('login'),
    Register = document.getElementById('register'),
    formContent = document.getElementById('form-container'),
    logBtn = document.getElementById('loginbtn'),
    logReg = document.getElementById('log-reg');
logBtn.onclick = function () {
    'use strict';
    if (logReg.style.transform !== "translateY(-600px)") {
        logReg.style.transform = "translateY(-600px)";
    } else {
        logReg.style.transform = "translateY(0)";
    }
};
Login.onclick = function () {
    'use strict';
    Login.style.background = "#ddd";
    Register.style.background = "#c4c4c4";
    formContent.innerHTML =
        '<div id="content">' +
        '<input type="text" placeholder="UserName">' +
        '<input type="password" placeholder="Password">' +
        '</div>' +
        '<button id="sub-button">Login</button>';
};
Register.onclick = function () {
    'use strict';
    Login.style.background = "#c4c4c4";
    Register.style.background = "#ddd";
    formContent.innerHTML =
        '<div id="content">' +
        '<input type="text" placeholder="UserName">' +
        '<input type="password" placeholder="Password">' +
        '<input type="password" placeholder="Confirm Password">' +
        '</div>' +
        '<button id="sub-button">Register</button>';
};