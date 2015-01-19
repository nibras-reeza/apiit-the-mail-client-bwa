<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TheMailClient.Presentation.manage" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title>
        <asp:Localize ID="HtmlPgeTitle" runat="server" meta:resourcekey="PageTitle" />
    </title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Page.css" rel="stylesheet" />
    <script src="Scripts/mustache.js"></script>

    <script src="Scripts/Register.js"></script>
    <style type="text/css">
        #listAccounts {
            overflow-y: scroll;
            height: 80%;
        }

        #content div {
            padding: 1%;
        }

        .user {
            float: left;
        }

        .delete {
            float: right;
        }

        .update {
            float: right;
        }

        .row {
            float: left;
            width: 97%;
        }

        #AddNew {
            float: right;
            font-size: small;
            margin: 0px;
        }
    </style>
</head>
<body class="ui-widget-content">
    <div id="message" runat="server"></div>
    <form id="form1" runat="server">
        <div id="main_wrapper">

            <%--http://jasonlau.biz/home/jquery/how-to-code-webpage-elements-to-use-jquery-ui-styles--%>
            <div id="header" class="ui-widget-header ui-corner-all">
                <%--https://stackoverflow.com/questions/244807/how-to-use-image-resource-in-asp-net-website--%>
                <div id="logo">
                    <asp:Image ID="logo_image" AlternateText="Logo of The Mail Client" src="<%$Resources: Icons, Logo %>" runat="server" />
                </div>
                <div id="search">
                    <asp:TextBox ID="search_term" Text="<%$Resources: Global, Search %>" runat="server" CssClass="ui-corner-left" /><asp:TextBox ID="searchButton" Text="🔎" runat="server" Enabled="false" CssClass="ui-corner-right" />
                </div>
                <div id="account">
                    <asp:BulletedList ID="account_links" CssClass="horizontal navigation" runat="server" DisplayMode="HyperLink" />
                    <asp:DropDownList ID="lang_selector" runat="server" OnSelectedIndexChanged="lang_selector_SelectedIndexChanged" AutoPostBack="True" />
                </div>
            </div>

            <div id="body">
                <div id="content" class="ui-widget">
                    <div class="ui-widget-header ui-corner-top">
                        <header>
                            <asp:Localize ID="PageTitle" runat="server" meta:resourcekey="PageTitle" />
                            <button id="AddNew" type="button" onclick="window.location.href='update_profile.aspx'" />
                        </header>
                    </div>
                    <div id="listAccounts" class="ui-widget-content ui-corner-bottom" runat="server">
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
</body>
</html>

<script type="text/javascript">

    $('#AddNew').button({
        icons: { primary: "ui-icon-plus" },
        label: '<asp:Localize runat="server" meta:resourcekey="Add" />'
    });
</script>