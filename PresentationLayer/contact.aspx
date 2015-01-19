<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="TheMailClient.Presentation.contact" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title>
        <asp:Localize runat="server" meta:resourcekey="PageTitle"></asp:Localize>
    </title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Contact.css" rel="stylesheet" />

    <script src="Scripts/mustache.js"></script>
    <script src="Scripts/contacts.js"></script>

    <style type="text/css">
        .form {
            float: left;
            width: 60%;
            padding: 2%;
            padding-top: 0px;
            padding-bottom: 0px;
        }

            .form hr {
                width: 95%;
                clear: both;
            }

        .left_cell {
            display: table-cell;
            width: 27%;
            font-weight: bold;
            float: left;
            padding: 1%;
        }

        .right_column {
            overflow: auto;
        }

        .right_cell {
            display: table-cell;
            width: 55%;
            float: left;
        }

            .right_cell input {
                padding: 1%;
                margin: 1%;
                margin-bottom: 0px;
                float: left;
                width: 90%;
            }

        .row {
            width: 95%;
            padding: 1%;
            display: table-row;
            float: left;
        }

            .row textarea {
                width: 80%;
            }

        .buttons {
            float: right;
        }
    </style>
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
                                <asp:Localize ID="new_mail_text" runat="server" Text="<%$Resources:new_contact%>" /></button>
                            <button id="contacts" value="Contacts" type="button" />
                        </div>

                        <script id="tag-template" type="x-tmpl-mustache">
                                {{#array}}
                                <div class="tag"><a href="contacts.aspx?tag={{Name}}" style="text-decoration:none">{{Name}}</a><span class="ui-icon ui-icon-tag" onclick="editTag('{{Id}}','{{Name}}')" /></div>
                                {{/array}}
                        </script>
                        <div id="tag_section" class="ui-widget-content ui-corner-bottom">
                        </div>
                    </div>

                    <%--Template for a thread. Had to be moved out since jQuery UI turned it into content.--%>

                    <div id="right_column" class="ui-corner-all" runat="server">
                        <div class="form">
                            <input type="hidden" id="cont_id" runat="server" />
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize2" meta:resourcekey="lblFirstName" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtFirstName" required="required" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize3" meta:resourcekey="lblMiddleName" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtMiddleName" required="required" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize4" meta:resourcekey="lblLastName" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtLastName" required="required" />
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize5" meta:resourcekey="lblAddress" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtAddress" required="required" />
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize6" meta:resourcekey="lblPhone" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtPhone" required="required" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize7" meta:resourcekey="lblEmail" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtEmail" required="required" />
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize8" meta:resourcekey="lblSPhone" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtSecondaryPhone" required="required" />
                                </div>
                                <div class="buttons">
                                    <button id="AddSP">Add</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_cell">
                                    <header>
                                        <asp:Localize ID="Localize9" meta:resourcekey="lblSEmail" runat="server" />
                                    </header>
                                </div>
                                <div class="right_cell">

                                    <input runat="server" id="txtSecondaryEmail" required="required" />
                                </div>
                                <div class="buttons">
                                    <button id="AddSE">Add</button>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <textarea id="txtNotes" runat="server">Notes...</textarea>
                                <div class="buttons">
                                    <button id="save" type="submit">Save</button>
                                    <br />
                                    <button id="reset" type="reset">Clear</button>
                                </div>
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
    </form>
    <div id="TagEdit">

        <input type="text" id="tag_name_box" /><br />
        <button type="button" id="tag_update" onclick="updateTag()">
            <asp:Localize runat="server" meta:resourcekey="LblTagUpdate" /></button><br />
        <button type="button" id="tag_delete" onclick="deleteTag()">
            <asp:Localize ID="Localize1" runat="server" meta:resourcekey="LblTagDelete" /></button><br />
    </div>
</body>
</html>