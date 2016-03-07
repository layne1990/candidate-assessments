


$(function () {

  

    $("#QuestionType").change(function () {
        var select = $("#QuestionType option:selected").val();

        $('#ChoiceA').val("");
        $('#ChoiceB').val("");
        $('#ChoiceC').val("");
        $('#ChoiceD').val("");

        $("[id^='Choice']").val("");
        $("[id$='-error']").text("");

        $("#offForTrueFalse").show();
        $("#offForFillInBlank").show();
        $("#onTrueFalse").hide();
        $("#onForFill").hide();
        $("input[type='radio'][name='CorrectAnswer']").prop('checked', false);
        if (select == "FillInBlank") {

            $("[id^='Choice']").val("placeholder");

            $("#offForFillInBlank").hide();
            $("#onForFill").show();
        } else if (select == "TrueFalse") {
            $('#ChoiceA').val("True");
            $('#ChoiceB').val("False");
            $('#ChoiceC').val("placeholder");
            $('#ChoiceD').val("placeholder");

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
        $options.hover(
                    function () {
                        $(this).addClass('active-hover');
                    },
                    function () {
                        $(this).removeClass('active-hover');
                    }
                   );
        $options.click(function (e) {
            var $current = $(this);

            $options.removeClass("active-RDA");
            $current.addClass("active-RDA");
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

    $(".hoverBtn").hover(function () {
        $(".card").toggleClass("topic-card");
    }, function () {
        $(".card").toggleClass("topic-card");
    })
});
$.widget("ui.autocomplete", $.ui.autocomplete, {
    _resizeMenu: function () {

        this.menu.element.css('max-height', '300px');
        this.menu.element.css('overflow-y', 'auto');
        this.menu.element.css('overflow-x', 'hidden');
        this._super();

    }

})