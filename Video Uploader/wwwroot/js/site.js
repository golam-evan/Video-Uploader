$(document).ready(function () {

    //Upload section handling
    $("#showUpload").on("click", function () {
        $("#uploadSection").show();
        $("#catalogueSection").hide();
    });

    //Catalogue section handling
    $("#showCatalogue").on("click", function () {
        $("#uploadSection").hide();
        $('#catalogueSection').show();
    });

    // Upload handler
    $("#uploadBtn").click(function () {
        var files = $("#fileInput")[0].files;
        if (files.length === 0) {
            $("#uploadStatus").text("Please select a file.");
            return;
        }

        var formData = new FormData();
        for (var i = 0; i < files.length; i++) {
            formData.append("files", files[i]);
        }

        $.ajax({
            url: "/api/upload",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {                
                location.reload();
            },
            error: function (xhr) {
                $("#uploadStatus").text("Error: " + xhr.responseText);
            }
        });
    });

    // Catalogue click handler
    $("tr[data-video]").click(function () {
        var videoPath = $(this).data("video");
        var player = $("#videoPlayer");
        player.attr("src", videoPath);
        player.show()[0].play();
    });
});