<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compose.aspx.cs" Inherits="TheMailClient.Presentation.compose" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title>
        <asp:Localize runat="server" meta:resourcekey="PageTitle"></asp:Localize>
    </title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Inbox.css" rel="stylesheet" />

    <script src="Scripts/mustache.js"></script>

    <script src="Scripts/basic.js"></script>

    <style type="text/css">
        #left_section {
            width: 70%;
            float: left;
        }

            #left_section hr {
                padding: 0px;
                margin: 1%;
            }

        #right_section * {
            padding: 1%;
            margin: 1%;
        }

        #right_section hr {
            padding: 0px;
            margin: 1%;
        }

        #right_section {
            text-align: center;
            width: 25%;
            float: right;
        }

        #DropdownFrom {
            padding: 1%;
            margin: 1%;
            width: 25%;
            margin-right: 3%;
        }

        #To {
            width: 56%;
            padding: 1%;
            margin: 1%;
        }

        #draft_body {
            margin-top: 3%;
            margin: 1%;
            width: 95%;
            height: 90%;
        }

        #txtSubject {
            padding: 1%;
            margin: 1%;
            width: 75%;
            margin-right: 3%;
        }

        #send, #draft, #add {
            width: 50%;
            height: 5%;
            margin: 3%;
            padding: 1%;
        }
    </style>
    <script src="//cdn.ckeditor.com/4.4.4/standard/ckeditor.js"></script>
</head>
<body class="ui-widget-content">
    <form id="form1" runat="server">
        <div id="main_wrapper">

            <%--http://jasonlau.biz/home/jquery/how-to-code-webpage-elements-to-use-jquery-ui-styles--%>
            <div id="header" class="ui-widget-header ui-corner-all">
                <%--https://stackoverflow.com/questions/244807/how-to-use-image-resource-in-asp-net-website--%>
                <div id="logo">
                    <asp:Image ID="logo_image" AlternateText="Logo of The Mail Client" src="<%$Resources: Icons, Logo %>" runat="server" />
                </div>
                <div id="search">
                    <asp:TextBox ID="search_term" Text="<%$Resources: Global, Search %>" runat="server" CssClass="ui-corner-left" /><asp:TextBox ID="searchButton" Text="🔎" runat="server" Enabled="false" CssClass="ui-corner-right" /><asp:Button ID="reload" CssClass="ui-widget-header" meta:resourcekey="reload" runat="server" />
                </div>
                <div id="account">
                    <asp:BulletedList ID="account_links" CssClass="horizontal navigation" runat="server" DisplayMode="HyperLink" />
                    <asp:DropDownList ID="lang_selector" runat="server" OnSelectedIndexChanged="lang_selector_SelectedIndexChanged" />
                </div>
            </div>

            <div id="body">
                <div id="content">

                    <%--https://stackoverflow.com/questions/3628194/how-to-resize-only-horizontally-or-vertically-with-jquery-ui-resizable --%>
                    <div id="left_column" class="ui-widget ui-corner-all">
                        <%--http://api.jqueryui.com/button/--%>
                        <%--http://jqueryui.com/button/--%>
                        <div id="new" class="ui-widget-header ui-corner-top">
                            <button id="new_mail" value="New Mail" type="button">
                                <asp:Localize ID="new_mail_text" runat="server" Text="<%$Resources:new_mail%>" /></button>
                            <button id="contacts" value="Contacts" type="button" />
                        </div>

                        <script id="tag-template" type="x-tmpl-mustache">
                                {{#array}}
                                <div class="tag"><a href="index.aspx?tag={{Name}}" style="text-decoration:none">{{Name}}</a><span class="ui-icon ui-icon-tag" onclick="editTag('{{Id}}','{{Name}}')" /></div>
                                {{/array}}
                        </script>
                        <div id="tag_section" class="ui-widget-content ui-corner-bottom">
                        </div>
                    </div>

                    <%--Template for a thread. Had to be moved out since jQuery UI turned it into content.--%>

                    <div id="right_column" class="ui-corner-all" runat="server">
                        <div id="left_section">
                            From:
                            <asp:DropDownList ID="DropdownFrom" runat="server"></asp:DropDownList>To:<asp:TextBox ID="To" runat="server"></asp:TextBox>
                            <br />
                            Subject:
                            <asp:TextBox ID="txtSubject" runat="server" />
                            <a href="#">CC/BCC</a>
                            <hr />
                            <%--https://github.com/xing/wysihtml5/wiki/Getting-Started--%>
                            <textarea id="draft_body" placeholder="Enter your text ..." autofocus runat="server"></textarea>
                        </div>
                        <div id="right_section">

                            <button id="send" type="button" runat="server">Send</button><br />
                            <button id="draft" type="button" runat="server">Draft</button>
                            <hr />
                            <div id="attachment">
                                <div id="attachment_previews" runat="server"></div>
                                <button id="add" type="button" runat="server" onclick="showUpload()">Add</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="promo"></div>
            </div>

            <div id="footer">
                <asp:BulletedList ID="navigation_bar" CssClass="horizontal navigation" runat="server" DisplayMode="HyperLink" />

                <asp:Localize ID="copyright" runat="server" Text="<%$ Resources:Global, Copyright %>" />
                <!--
                    http://www.thebookdesigner.com/2010/01/copyright-page-samples-you-can-copy-and-paste-into-your-book/

                    http://www.ascii.cl/htmlcodes.htm
                    -->
            </div>
        </div>
        <%--http://asp.net-tutorials.com/controls/file-upload-control/--%>
        <div id="file_upload">
            <asp:FileUpload ID="FileUploadControl" runat="server" />
        </div>
        <input type="hidden" id="action" runat="server" />
    </form>

    <script>
        $('#file_upload').hide();
        function showUpload() {
            $('#file_upload').show();
            $('#file_upload').dialog({
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Upload": function () {
                        $('#action').val('upload');
                        $('#form1').submit();
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        $('#send').button({

        });

        $('#draft').button({

        });

        $('#add').button({

        });

        $('#send').bind("click", function () {

            $('#action').val('send');
            $('#form1').submit();

        });

        $('#draft').bind("click", function () {
            $('#action').val('save');
            $('#form1').submit();

        });
        // Replace the <textarea id="editor1"> with a CKEditor
        // instance, using default configuration.

        CKEDITOR.replace('draft_body', { height: '250px' });
    </script>
</body>
</html>