function processQuestionType(){
    var questionType = $("#QuestionType option:selected").val();

    $("[id^='Choice']").val("");
    $("[id$='-error']").text("");

    $("#trueFalseAnswer").hide();
    $("#fillInBlankAnswer").hide();
    $("#multipleChoiceAnswer").hide();

    $("input[type='radio'][name='CorrectAnswer']").prop('checked', false);
    $("input[type='radio'][name='CorrectAnswer']").prop('required', true);
    $("input[type='text'][name='CorrectAnswer']").prop('required', false);
    $("input[type='radio'][name='ChoiceA']").prop('required', false);
    $("input[type='radio'][name='ChoiceB']").prop('required', false);
    $("input[type='radio'][name='ChoiceC']").prop('required', false);
    $("input[type='radio'][name='ChoiceD']").prop('required', false);

    if (questionType == "FillInBlank") {
        $("#fillInBlankAnswer").show();
        $("input[type='text'][name='CorrectAnswer']").prop('required', true);
    } else if (questionType == "TrueFalse") {
        $("#trueFalseAnswer").show();
    } else if (questionType == "MultipleChoice") {
        $("#multipleChoiceAnswer").show();
        $("input[type='radio'][name='ChoiceA']").prop('required', true);
        $("input[type='radio'][name='ChoiceB']").prop('required', true);
        $("input[type='radio'][name='ChoiceC']").prop('required', true);
        $("input[type='radio'][name='ChoiceD']").prop('required', true);
    } else if (questionType == "Essay") {
        $("input[type='radio'][name='CorrectAnswer']").prop('required', false);
    }
};


function startTimer(duration, display, url) {
    var start = Date.now(),
        diff,
        minutes,
        seconds;
    function timer() {
        // get the number of seconds that have elapsed since 
        // startTimer() was called
        diff = duration - (((Date.now() - start) / 1000) | 0);

        // does the same job as parseInt truncates the float
        minutes = (diff / 60) | 0;
        seconds = (diff % 60) | 0;

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (diff <= 0) {
            window.location.href = url;
        }
    };
    // we don't want to wait a full second before the timer starts
    timer();
    setInterval(timer, 1000);
}
$(function () {

  

    $("#QuestionType").change(processQuestionType);


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
