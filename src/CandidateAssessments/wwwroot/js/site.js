


$(function () {



    $("#QuestionType").change(function () {
        var questionType = $("#QuestionType option:selected").val();

        $("[id^='Choice']").val("");
        $("[id$='-error']").text("");

        $("#trueFalseAnswer").hide();
        $("#fillInBlankAnswer").hide();
        $("#multipleChoiceAnswer").hide();

        $("input[type='radio'][name='CorrectAnswer']").prop('checked', false);

        if (questionType == "FillInBlank") {
            $("#fillInBlankAnswer").show();
        } else if (questionType == "TrueFalse") {
            $("#trueFalseAnswer").show();
        } else if (selquestionTypeect == "MultipleChoice") {
            $("#multipleChoiceAnswer").show();
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