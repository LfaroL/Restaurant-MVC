$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        // call back function
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target"));
            var $newHtml = $(data); // wrap data in jQuery to run jQuery methods like effect()
            $target.replaceWith($newHtml);  // use replacewWith(), not replace()
            $newHtml.effect("highlight");
        });

        return false;
    }

    // Populate the input form with the item selected
    var submitAutocompleteForm = function (event, ui){
        var $input = $(this);
        $input.val(ui.item.label);

        // find first parent form element
        var $form = $input.parents("form:first");
        $form.submit();
    };


    var createAutoComplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        };

        $input.autocomplete(options);
    };

    // Clicking previous or next page won't load the entire browser
    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });
        return false;
    };

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);

    // Find input element with data-otf-autocomplete attribute, invoke function
    $("input[data-otf-autocomplete]").each(createAutoComplete);

    // Finds click event for pagedList class on main-content
    $(".main-content").on("click", ".pagedList a", getPage);
});