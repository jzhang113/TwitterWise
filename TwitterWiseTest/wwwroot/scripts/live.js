$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/stream/start",
        success: function (result) {
            getLiveTweets();
        }
    });

    $('#reload').on('click', getLiveTweets);
});

function getLiveTweets() {
    var filter = $('#filtered').is(':checked') ? "filtered" : "";

    $.ajax({
        type: "GET",
        url: "/api/stream/" + filter,
        success: function (tweet) {
            if (tweet !== null) {
                $("#stream").text(tweet.text + " - " + tweet.author);

                var timeout = $("#refreshTime").val() * 1000;
                setTimeout(getLiveTweets, timeout);
            }
        }
    });
}