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




$("#AssessSearchBtn").on("click", function () {
    var g = $('#AssessSearch').val().toLowerCase();
    $(".assessments").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.assessments')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});
$("#AssessSearch").on("keyup", function () {
    var g = $(this).val().toLowerCase();
    $(".assessments").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.assessments')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});
$(function () {
    var tags = [];
    $(".CandidateName").each(function () {
        tags.push(this.innerText)
    });
    $(".tNames").each(function () {
        if (tags.indexOf(this.innerText) == -1)
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
    var tags = [];
    $(".TopicNames").each(function () {
        tags.push(this.innerText)
    });
    $("#TopicSearch").autocomplete({
        source: tags
    });
})

$("#TopicSearchBtn").on("click", function () {
    var g = $('#TopicSearch').val().toLowerCase();
    $(".TopicNames").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.TopicItems')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});




$("#QuestionSearch").on("keyup", function () {
    var g = $(this).val().toLowerCase();
    $(".card").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.question')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});
$(function () {
    var tags = [];
    $(".card-text").each(function () {
        tags.push(this.innerText.substring(0, 45))
    });
    $(".cardHeader").each(function () {
        if (tags.indexOf(this.innerText) == -1)
            tags.push(this.innerText)
    });
    $("#QuestionSearch").autocomplete({
        source: tags
    });
})

$("#QuestionSearchBtn").on("click", function () {
    var g = $('#QuestionSearch').val().toLowerCase();
    $(".card").each(function () {
        var s = $(this).text().toLowerCase();
        $(this).closest('.question')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
    });
});