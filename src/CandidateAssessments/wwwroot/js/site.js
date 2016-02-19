$(function () {
    $("#QuestionType").change(function() {
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

$(document).ready(function() {
    var $options = $("label.list-group-item");

    $options.click(function(e) {
        var $current = $(this);
        e.preventDefault();
        e.stopPropagation();
        $options.removeClass("active");
        $current.addClass("active");
        $("input", $current).prop("checked", true);
    });
});

