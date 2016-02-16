$(document).ready(function () {
    // Connect to any elements that have 'data-pm-action'
    $("[data-pm-action]").on("click", function (e) {
        e.preventDefault();

        // Fill in hidden fields with action and argument to post back to model
        $("#EventCommand").val($(this).attr("data-pm-action"));
        $("#EventArgument").val($(this).attr("data-pm-val"));

        // Use the switch statement to get and set any special parameters
        switch ($("#EventCommand").val()) {
            case "sort":
                // Store the last sort expression
                $("#LastSortExpression").val($("#SortExpression").val());
                // Get the new sort expression
                $("#SortExpression").val($(this).data("pm-val"));
                // Set the new sort direction
                $("#SortDirectionNew").val($(this).data("pm-direction"));
                break;
        }

        // Submit form with hidden values filled in
        $("form").submit();
    });

    //Allow users to enter numbers only
    $(".numericOnly").bind('keypress', function (e) {
        if (e.keyCode == '9' || e.keyCode == '16') {
            return;
        }
        var code;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (e.which == 46)
            return false;
        if (code == 8 || code == 46)
            return true;
        if (code < 48 || code > 57)
            return false;
    });

    //Disable paste
    $(".numericOnly").bind("paste", function (e) {
        e.preventDefault();
    });

    $(".numericOnly").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val == '0')
            return;
        val = val.replace(/[^0-9]+/g, "");
        $(this).val(val);
    });
});