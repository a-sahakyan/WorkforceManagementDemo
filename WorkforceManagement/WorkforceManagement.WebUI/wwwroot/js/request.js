$('#skill-btn').click(function () {
        value = document.getElementById("skill").value;

        if (value == "") {
            alert("Please add skill");
        }
        else {
            $.getJSON("../js/data.json", function (data) {
                var item = data
                $('#bar').append("<div><label class='label label-default' style='margin:10px' id ='l" + item.Count + "'>" + value +"</label><button style='margin:10px' data-toggle='progressbar' data-target='#" + item.Count + "'  data-value='0' class='btn btn-danger'>don't know</button><button style='margin:10px' data-toggle='progressbar' data-target='#" + item.Count + "' data-value='25' class='btn btn-warning'>basic</button><button style='margin:10px' data-toggle='progressbar' data-target='#" + item.Count + "' data-value='50' class='btn btn-info'>normal</button><button style='margin:10px' data-toggle='progressbar' data-target='#" + item.Count + "' data-value='75' class='btn btn-primary'>good</button><button style='margin:10px' data-toggle='progressbar' data-target='#" + item.Count + "' data-value='100' class='btn btn-success'>complete</button><div id='" + item.Count + "' class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' style='width: 0%;'><span id='sr-only" + item.Count + "' class='sr-only'></span></div></div></div>");
                item.Count++;
                var datas = JSON.stringify(item);
                $.ajax({
                    url: "/Home/JsonConfig",
                    type: "POST",
                    data: datas,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error: function (response) {
                        alert(response.responseText);
                        alert("err")
                    },
                    success: function (response) {
                        swal("Good job!", "You have just added a new skill!", "success")
                    }
                });
            })
        }
});

    $("#save-btn").click(function () {
        $.getJSON("../js/data.json", function (data) {
            for (var i = 0; i < data.Count; i++) {
                (function (counter) {
                    console.log("start")
                    var percent = $("#sr-only" + i + "").text();
                    console.log(percent)
                    var skill = $("#l" + i + "").text();
                    var items = {
                        SkillName: "" + skill + "", SkillKnowledge: "" + percent + ""
                    };
                    console.log(items);
                    var data = JSON.stringify(items);

                    $.ajax({
                        url: "/Home/Index",
                        type: "POST",
                        data: data,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error: function (response) {
                            alert(response.responseText);
                            alert("err")
                        },
                        success: function (response) {
                            swal("Good job!", "Your skills have been saved", "success")
                        }
                    });
                })(i);
            }
        })
    });