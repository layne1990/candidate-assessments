



$(function () {
    var page = 1;


    $("#QuestionType").change(function () {
        var select = $("#QuestionType option:selected").val();

        $("#offForTrueFalse").show();
        $("#offForFillInBlank").show();
        $("#onTrueFalse").hide();
        $("#onForFill").hide();
        $("input[type='radio'][name='CorrectAnswer']").prop('checked', false);
        if (select == "FillInBlank") {
            $("#offForFillInBlank").hide();
            $("#onForFill").show();
        } else if (select == "TrueFalse") {
            $("input[type='radio'][name='CorrectAnswer']").prop('checked', false);
            $("#onTrueFalse").show();
            $("#offForTrueFalse").hide();

        }
    })


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
    $("#AssessSearch").ready(function () {
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









    $(paging(1))

    $(".pageNumber").click(function () {
        paging(this.innerText * 1)

    })
    $('#leftArrow').click(function () {
        if (page > 1) {
            paging(page - 1);

        }

    });
    $('#rightArrow').click(function () {
        if (page < Math.ceil($('.page-item').length / 10))
            paging(page + 1);
    });

    function paging(start) {
        var max = (start * 10) - 1;
        var min = max - 9;
        page = start;
        $('.pageNumber').removeClass("active");
        $($('.pageNumber')[start - 1]).addClass("active");
        $('.page-item').each(function (i, item) {

            if (i < min || i > max)
                $(item).hide();
            else
                $(item).show();
        })



    }

});