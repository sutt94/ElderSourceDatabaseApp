$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize() //might be more data here
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-elderscript-target"));
            $target.replaceWith(data);
        });
        
        return false;
    };

    createAutoComplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-elderscripts-autocomplete")
        };

        $input.autocomplete(options);
    };


    $("form[data-elderscript-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-elderscipt-autocomplete]").each(createAutoComplete)
});