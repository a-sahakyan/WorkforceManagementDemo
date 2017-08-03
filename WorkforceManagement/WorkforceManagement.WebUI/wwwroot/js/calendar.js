$(".btn-calendar").click(function () {
    var lable = $(".btn-calendar").text().trim();

    if (lable == "Hide Calendar") {
        $(".btn-calendar").text("Show Calendar");
        $(".calender-div").hide();
    }
    else {
        $(".btn-calendar").text("Hide Calendar");
        $(".calender-div").show();
    }
});