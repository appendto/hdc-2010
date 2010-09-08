/// <reference="../jquery-1.4.1-vsdoc.js" />
$(function () {

    // here we set up our custom event (or subscription)
    // for anytime the tasks need refreshed. this way
    // anytime any code ever needs to instigate a refresh,
    // the code will simply "publish" a refreshTasks message
    // using jQuery custom events.
    $(document).bind("refreshTasks", function () {
        $.ajax({
            url: "/home/index/",
            dataType: "json",
            success: function (data) {

                var $myTasks = $("<div id='accordian'></div>");
                $.each(data, function (idx, elem) {
                    $("<h3><a href='#'>" + elem.Name + "</a></h3>"
                            + "<div class='taskContents' id='" + elem.Id + "'>"
                            + "Priority: <span>" + elem.Priority + "</span>"
                            + "Hours Spent: <span class='time'>" + elem.HoursSpent + "</span>"
                            + "Hours Remaining: <span class='time'>" + elem.HoursRemaining + "</span>"
                            + "Start Date: <span class='date'>" + elem.StartDate + "</span>"
                            + "Due Date: <span class='date'>" + elem.DueDate + "</span>"
                            + "</div>")
                            .appendTo($myTasks);
                });

                $myTasks
                    .appendTo($('#allTasks').empty())
                    .accordion()
                        .find('.taskContents a')
                        .button();
            }
        });
    });

    // here is our 'subscriber' for creating tasks. note
    // that it does not care HOW the user initiated the
    // creation - only that it has been communicated
    // to create a new task.
    $(document).bind("createTask", function (evt, data) {
        var $form = $(data.element);
        $.ajax({
            url: $form.attr('action'),
            dataType: 'text',
            data: $form.serialize(),
            type: 'POST',
            success: function (data) {
                if (data && data.toString() != "False") {
                    $form.find("input:text").val("");
                    $(document).trigger("refreshTasks");
                }
            }
        });
    });

    // map our UI events to our custom events (message topics)
    // it doesn't matter if these messages don't yet exist.
    // we are set up for future functionality.
    // edit not yet in place, but we have our "message"
    // set up
    $(".taskContents a.edit").live("click", function () {
        // advanced: we are using custom events as a message
        $(document).trigger("editTask", [{ element: this}]);
    });

    // canceling edit not yet in place but we have our "message"
    // set up
    $(".taskContents a.cancel").live("click", function () {
        $(document).trigger("cancelEditTask", [{ element: this}]);
    });

    // update edit not yet in place but we have our "message"
    // set up
    $(".taskContents a.update").live("click", function () {
        $(document).trigger("updateTask", [{ element: this}]);
    });

    // creating a new task IS in place, and we are publishing
    // our message whenever a submission occurs
    $("#newTask").live("submit", function () {
        $(document).trigger("createTask", [{ element: this}]);
        return false;
    });

    // 'publish' refreshTasks message now that we are all setup
    $(document).trigger("refreshTasks");

    // add datepicker to any element which ever gains focus 
    // and has the right class
    $("input:text.date").live("focusin", function () {
        $this = $(this);
        if (!$this.is(":data(datepicker)")) {
            $this.datepicker();
        }
    });

    // add autocomplete to any element which ever gains focus and 
    // has the autoComplete class attached
    $("input:text.autoComplete").live("focusin", function () {
        $this = $(this);
        if (!$this.is(":data(autocomplete)")) {
            $this.autocomplete({ source: '/home/Priorities' });
        }
    });

    // here we add our validation plugin too all forms on the page
    // the plugin will look for certain classes on the element
    // to determine the validation rules.
    $('form').validate();

    //skin our submissions with our UI theme.
    $("input:submit").button();
});
