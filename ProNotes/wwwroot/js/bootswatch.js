
$(function () {
	var theme = (localStorage.getItem('theme') == null) ? ("default") : (localStorage.getItem('theme'));
	var link = `${document.baseURI}/css/bootswatch/${theme.toLowerCase()}/bootstrap.min.css`;
	$("link#bootstrap_placeholder").attr("href", link);

});

function ChangeTheme(themeName) {
	localStorage.setItem('theme', themeName);
	location.reload();
}

$(document).ready(function () {

	var theme = (localStorage.getItem('theme') == null) ? ("default") : (localStorage.getItem('theme'));

	$("a[id^=theme-]").each(function (index, value) {
		if (this.id == `theme-${theme}`) {
			$(`#${this.id}`).addClass("active");
		}
	});
});