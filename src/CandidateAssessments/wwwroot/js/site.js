



$(function () {
  


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