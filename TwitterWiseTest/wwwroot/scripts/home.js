$(document).ready(function () {
    var index = 0;

    $('#SearchBtn').on('click', function () {

        var searchQuery = $("#SearchInput").val();

        $.ajax({
            type: "GET",
            url: "/api/search/" + searchQuery,
            success: function (result) {
                if (result !== null) {
                    var tweet = result[index++ % result.length];
                    $("#tweet").text(tweet.text + " - " + tweet.author);
                    $("#fav").css("display", "inline-block");
                    $("#fav").css("color", "black");
                } else {
                    $("#fav").hide();
                }
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