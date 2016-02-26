$(function () {
    $("#QuestionType").change(function () {
        var select = $("#QuestionType option:selected").val();
       
        $("#offForTrueFalse").show();
        $("#offForFillInBlank").show();
        $("#onTrueFalse").hide();
        $("#onForFill").hide();
        if (select == "FillInBlank") {
            $("#offForFillInBlank").hide();
            $("#onForFill").show();
        } else if (select == "TrueFalse") {
            $("#onTrueFalse").show();
            $("#offForTrueFalse").hide();
       
        }
    })
});

$(function () {
    $("#fillin").on('keyup', function () {
        $('#AnswerSubmit').removeClass('hidden');
    })
})
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

$(document).ready(function () {
    var $options = $(".checkbox");

    $options.click(function (e) {
        var $checked = $('div.checkbox-group.required :checkbox:checked').length > 0;
        console.log($checked);
        if ($checked) {
            $('#AssessmentCreate').removeClass('hidden');
        }
        else {
            $('#AssessmentCreate').addClass('hidden');
        }
            

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
$("#AssessSearch").ready( function () {
    var id = '';
    id = $('#user-name').text();
    if (id != '') {
        $("#AssessSearch").val(id)
        $("#AssessSearchBtn").trigger("click");

    }
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