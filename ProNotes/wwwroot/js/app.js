
// ---------------------------------------
// Document Ready Alternatives: Javascript
// ---------------------------------------

(function () {
    // self calling function
})();

document.addEventListener("DOMContentLoaded", function (event) {
    // DOMContentLoaded event
});

window.onload = function () {
    // onload function
};

window.onload = () => {
    // onload arrow function
}

// -----------------------------------
// Document Ready Alternatives: Jquery
// -----------------------------------

$(document).ready(function () {
    //
});

$(function () {
    //
});

jQuery(function () {
    // 
});

$(window).on("load", function () {
    //
});

// ----------------
// Jquery Extension
// ----------------

(function ($) {
    "use strict";
    // extend jquery

})(jQuery);