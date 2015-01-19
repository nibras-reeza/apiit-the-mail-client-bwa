/// <reference path="jquery-ui.min.js" />
/// <reference path="jquery-1.11.1.min.js" />
/*****************************************************************************
    This file contains basic JS for basic page manipulation.

    Author: Nibras Ahamed Reeza (CB004641)
    E-Mail: nibras.ahamed@gmail.com

    Last Modified: 19/08/2014

******************************************************************************/

/*http://www.webmasterworld.com/forum83/187.htm
https://stackoverflow.com/questions/8255870/change-div-height-on-button-click
https://api.jquery.com/jquery.ajax/
*/

window.onload = function () {
    $('#TagEdit').hide();
    fillHeight();
    //https://api.jquery.com/each/
    //https://stackoverflow.com/questions/17403103/how-to-read-json-array-in-jquery
    //http://api.jqueryui.com/theming/icons/
    //https://fortawesome.github.io/Font-Awesome/examples/

    //https://stackoverflow.com/questions/11042775/iterate-over-keyless-array-with-mustache

    // http://jqueryui.com/accordion/

    loadAllThreads();

    enableAutoScrollers();
    // http://jqueryui.com/resizable/#animate
    // https://stackoverflow.com/questions/3628194/how-to-resize-only-horizontally-or-vertically-with-jquery-ui-resizable

    $('#left_column').resizable({
        animate: true,
        handles: 'w, e'
    });

    // http://code.tutsplus.com/tutorials/simple-draggable-element-persistence-with-jquery--net-7474
    // https://stackoverflow.com/questions/5376431/wildcards-in-jquery-selectors
    //http://api.jqueryui.com/draggable/#option-handle
    $('[id$=_column]').draggable({
        axis: 'x',
        distance: 20,
        cursor: 'move',
        containment: '#content',
        scroll: false
    });

    //http://api.jqueryui.com/resizable/#event-resize

    //https://stackoverflow.com/questions/4738595/how-do-i-delay-a-function-call-for-5-seconds

    $('#header').resizable({
        handles: 's',
        ghost: true,
        //https://api.jquery.com/trigger/
        stop: function (event, ui) { fillHeight(); }
    });

    styleButtons();
    loadTags();
};

$(window).bind('onresize', fillHeight());

function styleButtons() {
    // Transform button elements into jqueryUI button widgets.

    //http://jqueryui.com/button/
    $('#new_mail').button();
    $('#contacts').button({
        icons: {
            primary: "ui-icon-mail-closed"
        },
        text: false
    });
    $('#searchButton').bind('click', function () { window.location.href += 'search=' + $('#search_term').val() });
}

function loadTags() {
    var tagTempStr = $('#tag-template').html();
    $.getJSON('api/ContactTags', function (tags) {
        $('#tag_section').html('');

        $('#tag_section').append(Mustache.render(tagTempStr, { array: tags }));
    });
}
function fillHeight() {
    // CSS height:100% doesn't usually fill the entire window. Giving a fixed height would cause us to lose
    // fluidity. Assuming JS, get the available height for content, deduct fixed amount for headers and footers,
    // use the rest for conent.

    var height = window.innerHeight;
    var buffer = 30;

    //https://stackoverflow.com/questions/294250/how-do-i-retrieve-an-html-elements-actual-width-and-height
    buffer += $('#header').height();
    buffer += $('#footer').height();
    height -= buffer;

    height = (height < 200) ? 200 : height; // Minimum monitor resolution is 800 x 600.

    //https://www.google.lk/search?q=jquery+resize&ie=utf-8&oe=utf-8&rls=org.mozilla:en-US:official&client=firefox-a&channel=sb&gws_rd=cr&ei=fRAUVL7UEIHN8gXuyoHQCA
    //https://api.jquery.com/width/#width-value
    $('#body').height(height.toString() + 'px');
}

function enableAutoScrollers() {
    // Overflow is set to scroll by default in CSS. In case JS is disabled, code to enable scrollers will fail.
    // So, hide them first. If we can hide them, we can unhide them as well (JS works!)
    document.getElementById('tag_section').style.overflow = 'hidden';

    document.getElementById('tag_section').onmouseenter = function () {
        document.getElementById('tag_section').style.overflowY = 'scroll';
    }

    document.getElementById('tag_section').onmouseleave = function () {
        document.getElementById('tag_section').style.overflowY = 'hidden';
    }
}

var dialog;
var t_id;
function editTag(id, name) {
    t_id = id;
    $('#TagEdit').show();

    $('#tag_name_box').val(name);

    dialog = $('#TagEdit').dialog({
        modal: true, title
        : 'Update Tag'
    });

    $('#tag_update').button();
    $('#tag_delete').button();
}

//https://stackoverflow.com/questions/2153917/how-to-send-a-put-delete-request-in-jquery

function deleteTag() {
    alert('delete clicked');
    $.ajax({
        url: 'api/ContactTags/' + t_id + '?delete=true',
        type: 'PUT',
        success: function (result) {
            loadTags();
            dialog.dialog('close');
        }
    });
}
function updateTag() {
    $.ajax({
        url: 'api/ContactTags/' + t_id + '?newname=' + $('#tag_name_box').val(),
        type: 'PUT',
        success: function (result) {
            loadTags();
            dialog.dialog('close');
        }
    });
}

function AddTag() {
    var name = window.prompt('Enter name for new tag:');
    $.ajax({
        url: 'api/ContactTags/' + name,
        type: 'POST',
        success: function (result) {
            loadTags();
        }
    });
}

function loadAllThreads() {
    //http://api.jqueryui.com/accordion/
    $('.accordion').accordion({ collapsible: true, animate: true, active: false, heightStyle: "content", icons: { 'header': 'ui-icon-mail-closed', 'activeHeader': 'ui-icon-mail-open' } });

    $('.contactDelete').button({ icons: { primary: "ui-icon-trash" } });

    $('.contactUpdate').button({ icons: { primary: "ui-icon-pencil" } });
    $('.contactAddTag').button({ icons: { primary: "ui-icon-tag" } });
    $('.contactRemoveTag').button({ icons: { primary: "ui-icon-tag" } });
}

function AddTag(id) {
    var tag = window.prompt("Enter tag to add:");
    if (tag == null || tag == "")
        return;
    window.location.href = 'contacts.aspx?add=' + tag + '&contact=' + id;
}

function RemoveTag(id) {
    var tag = window.prompt("Enter tag to add:");
    if (tag == null || tag == "")
        return;
    window.location.href = 'contacts.aspx?remove=' + tag + '&contact=' + id;
}