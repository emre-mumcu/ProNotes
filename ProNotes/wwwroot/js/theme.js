function slideToggle(t, e, o) { 0 === t.clientHeight ? j(t, e, o, !0) : j(t, e, o) } function slideUp(t, e, o) { j(t, e, o) } function slideDown(t, e, o) { j(t, e, o, !0) } function j(t, e, o, i) { void 0 === e && (e = 400), void 0 === i && (i = !1), t.style.overflow = "hidden", i && (t.style.display = "block"); var p, l = window.getComputedStyle(t), n = parseFloat(l.getPropertyValue("height")), a = parseFloat(l.getPropertyValue("padding-top")), s = parseFloat(l.getPropertyValue("padding-bottom")), r = parseFloat(l.getPropertyValue("margin-top")), d = parseFloat(l.getPropertyValue("margin-bottom")), g = n / e, y = a / e, m = s / e, u = r / e, h = d / e; window.requestAnimationFrame(function l(x) { void 0 === p && (p = x); var f = x - p; i ? (t.style.height = g * f + "px", t.style.paddingTop = y * f + "px", t.style.paddingBottom = m * f + "px", t.style.marginTop = u * f + "px", t.style.marginBottom = h * f + "px") : (t.style.height = n - g * f + "px", t.style.paddingTop = a - y * f + "px", t.style.paddingBottom = s - m * f + "px", t.style.marginTop = r - u * f + "px", t.style.marginBottom = d - h * f + "px"), f >= e ? (t.style.height = "", t.style.paddingTop = "", t.style.paddingBottom = "", t.style.marginTop = "", t.style.marginBottom = "", t.style.overflow = "", i || (t.style.display = "none"), "function" == typeof o && o()) : window.requestAnimationFrame(l) }) }

let sidebarItems = document.querySelectorAll('.sidebar-item.has-sub');
for (var i = 0; i < sidebarItems.length; i++) {
    let sidebarItem = sidebarItems[i];
    sidebarItems[i].querySelector('.sidebar-link')?.addEventListener('click', function (e) {
        e.preventDefault();

        let submenu = sidebarItem.querySelector('.submenu');
        if (submenu.classList.contains('active')) submenu.style.display = "block"

        if (submenu.style.display == "none") submenu.classList.add('active')
        else submenu.classList.remove('active')
        slideToggle(submenu, 300)
    })
}

window?.addEventListener('DOMContentLoaded', (event) => {
    var w = window.innerWidth;
    if (w < 1200) {
        document.getElementById('sidebar')?.classList.remove('active');
    }
});
window?.addEventListener('resize', (event) => {
    var w = window.innerWidth;
    if (w < 1200) {
        document.getElementById('sidebar')?.classList.remove('active');
    } else {
        document.getElementById('sidebar')?.classList.add('active');
    }
});

document.querySelector('.burger-btn')?.addEventListener('click', () => {
    document.getElementById('sidebar')?.classList.toggle('active');
})
document.querySelector('.sidebar-hide')?.addEventListener('click', () => {
    document.getElementById('sidebar')?.classList.toggle('active');

})


// Perfect Scrollbar Init
if (typeof PerfectScrollbar == 'function') {
    const container = document.querySelector(".sidebar-wrapper");
    const ps = new PerfectScrollbar(container, {
        wheelPropagation: false
    });
}

// Scroll into active sidebar
document.querySelector('.sidebar-item.active')?.scrollIntoView(false);

// Theme Dark - Light
$(document).ready(function () {
    document.getElementById("toggle-dark")?.addEventListener("input", function (e) {
        var selectedTheme = e.target.checked ? "theme-dark" : "theme-light";
        localStorage.setItem('theme-dark-light', selectedTheme);
        ApplyTheme();  
    })
});
document?.addEventListener("DOMContentLoaded", function () {
    ApplyTheme();
});
function ApplyTheme() {
    var selectedTheme = localStorage.getItem('theme-dark-light') ?? "theme-light";
    document.body.className = document.body.className.replace(/\btheme-[a-z0-9]+\b/g, "");    
    document.body.classList.add(selectedTheme);    
    if (document.getElementById("toggle-dark")) document.getElementById("toggle-dark").checked = ("theme-dark" == selectedTheme);

    // prism.js için sitil ayarı
    const prismstyle = document.querySelector('#prismstyle');
    var currentHref = prismstyle.href;
    var targetTheme = selectedTheme == "theme-dark" ? "theme-dark" : "theme-light";

    //if (currentHref.endsWith("prism.css")) {
    //    prismstyle.href = './css/prism-material-light.css';
    //    currentHref = prismstyle.href;
    //    console.log("href changed from default");
    //}
    if (targetTheme == "theme-light" && !currentHref.endsWith("-light.css")) {
        prismstyle.href = './css/prism-material-light.css';
        console.log("href changed to light");
    }
    if (targetTheme == "theme-dark" && !currentHref.endsWith("-dark.css")) {
        prismstyle.href = './css/prism-material-dark.css';
        console.log("href changed to dark");
    } 
    // prism.js için sitil ayarı
}
// Theme Dark - Light