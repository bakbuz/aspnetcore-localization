$(document).ready(function () {
    $("#culture-selector").on("change", function (e) {
        var selectedCulture = this.value;
        console.log(selectedCulture);
        window.location = "/Home/SetLanguage?culture=" + selectedCulture + "&returnUrl=" + $(this).attr("data-returnUrl")
    });
});