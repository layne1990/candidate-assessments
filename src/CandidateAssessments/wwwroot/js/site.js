$(function () {
    $("#QuestionType").change(function () {
        var select = $("#QuestionType option:selected").val();
        $('#trueLabel').text('Choice A');
        $('#falseLabel').text('Choice B');
        $("#MCQuestions").show();
        $("#offForTrueFalse").show();

        if (select == "FillInBlank") {
            $("#MCQuestions").hide();
        } else if (select == "TrueFalse") {
            $("#MCQuestions").show();
            $("#offForTrueFalse").hide();
            $('#trueLabel').text('True');
            $('#falseLabel').text('False');
        }
    })
});

$(document).ready(function () {
    var $options = $("label.list-group-item");

    $options.click(function (e) {
        var $current = $(this);
       
        $options.removeClass("active");
        $current.addClass("active");
        $("input", $current).prop("checked", true);
        $('#AnswerSubmit').removeClass('hidden');

    });
});

$("#AssessSearch").on("keyup", function () {
    var g = $(this).val().toLowerCase();
    $(".CanaditeName").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.assessments')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});
$(function () {
    var tags=[];
    $(".CanaditeName").each(function () {
        tags.push(this.innerText)
    });
    $("#AssessSearch").autocomplete({
        source: tags
    });
})
$("#TopicSearch").on("keyup", function () {
    var g = $(this).val().toLowerCase();
    $(".TopicNames").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.TopicItems')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});

$(function () {
    var tags = $(".TopicNames").text().split(" ");
    $("#TopicSearch").autocomplete({
        source: tags
    });
})
