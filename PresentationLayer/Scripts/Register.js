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

    //http://jqueryui.com/tooltip/#default
    //http://api.jqueryui.com/tooltip/
    $(document).tooltip(
       { position: { tooltipClass: "smalltext", postion: { at: "right" } } }
        );

    fillHeight();
    //https://api.jquery.com/each/
    //https://stackoverflow.com/questions/17403103/how-to-read-json-array-in-jquery
    //http://api.jqueryui.com/theming/icons/
    //https://fortawesome.github.io/Font-Awesome/examples/

    // http://jqueryui.com/accordion/

    $('#header').resizable({
        handles: 's',
        ghost: true,
        //https://api.jquery.com/trigger/
        stop: function (event, ui) { fillHeight(); }
    });

    $.get('privacy_text.aspx', function (privacy) {
        $('#privacy').html(privacy);
    });

    styleButtons();
    $('#form1').validate(
        {
            rules: { txtFirstName: "required" }
        }
        );

    //http://jqueryui.com/dialog/#modal-message

    

}
//http://api.jqueryui.com/dialog/#option-buttons
$(document).ready(function () {
    $('#message').hide();
    if ($('#message').text().length == 0)
        return;

    $('#message').show();
    $('#message').dialog({
        modal: true,
        buttons: [{ text: "Ok", click: function () { $(this).dialog("close"); } }]
    });
});
$(window).bind('onresize', fillHeight());

function styleButtons() {
    // Transform button elements into jqueryUI button widgets.

    //http://jqueryui.com/button/
    //https://api.jquery.com/child-selector/

    $('.delete').button(

       {
           icons: {
               secondary: 'ui-icon-trash'
           },label:"Delete"
       }
       );

    $('.update').button(

    {
        icons: {
            secondary: 'ui-icon-contact'
        }, label: "Update"
    }
    );
    $('.form > input[type="button"],input[type="submit"], input[type="reset"]').button();
   
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
    $('#privacy').height((height * 0.95).toString() + 'px');
}