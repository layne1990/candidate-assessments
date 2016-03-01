



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


});