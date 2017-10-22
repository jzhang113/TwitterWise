$(document).ready(function () {
    var index = 0;

    $('#SearchBtn').on('click', function () {
        var searchQuery = $("#SearchInput").val();
        var filter = $('#filtered').is(':checked') ? "filtered/" : "";

        $.ajax({
            type: "GET",
            url: "/api/search/" + filtered + searchQuery,
            success: function (result) {
                if (result !== null) {
                    var tweet = result[index++ % result.length];
                    $("#tweet").text(tweet.text + " - " + tweet.author);
                    $("#fav").css("display", "inline-block");
                    $("#fav").css("color", "black");
                } else {
                }
            },
            error: function () {
                $("#tweet").text("No results found");
                $("#fav").hide();
            }
        });
    });

    var fav = $('#fav');
    fav.on('click', function () {
        if (fav.css("color") === "rgb(255, 0, 0)")
            fav.css("color", "black");
        else
            fav.css("color", "red");
    });
});