$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/stream/start",
        success: function (result) {
            getLiveTweets();
        }
    });

    setTimeout(getLiveTweets, 10000);

    $('#reload').on('click', getLiveTweets);
});

function getLiveTweets() {
    $.ajax({
        type: "GET",
        url: "/api/stream/",
        success: function (tweet) {
            if (tweet !== null) {
                $("#stream").text(tweet.text + " - " + tweet.author);
            }
        }
    });
}