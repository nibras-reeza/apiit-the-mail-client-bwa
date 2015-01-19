<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contacts.aspx.cs" Inherits="TheMailClient.Presentation.contacts" ClientIDMode="static" %>

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
        .contactDelete {
            margin-left: 2%;
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
                    <asp:DropDownList ID="lang_selector" runat="server" OnSelectedIndexChanged="lang_selector_SelectedIndexChanged" AutoPostBack="True" />
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

                    <div id="right_column" class="accordion ui-corner-all" runat="server">
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