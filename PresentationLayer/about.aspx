<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="TheMailClient.Presentation.about" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Inbox.css" rel="stylesheet" />

    <script src="Scripts/mustache.js"></script>
    <script src="Scripts/basic.js"></script>
    <style type="text/css">
        #content header {
            font-weight: bold;
            padding: 1%;
            margin: 1%;
        }

        .row {
            float: left;
            padding: 1%;
            margin: 1%;
            height: 45%;
            width: 70%;
        }

            .row img {
                height: 100px;
                width: 150px;
            }

        .big {
            float: left;
            width: 60%;
            line-height: 150%;
            padding: 1%;
            margin: 1%;
        }

        .small {
            text-align: center;
            float: left;
            width: 30%;
            padding: 1%;
            margin: 1%;
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
                <div id="content" class="ui-widget">
                    <div class="ui-widget-header">
                        <header>
                            <asp:Localize meta:resourcekey="AboutUsHeader" runat="server"></asp:Localize>
                        </header>
                    </div>
                    <div class="ui-widget-content">
                        <%--http://pixabay.com/en/at-email-e-mail-fig-silhouette-64056/--%>
                        <div class="row">
                            <div class="big">
                                Email is an integral part of most of our lives. We use it in our social lives. In our work lives.
                                However, email is soon becoming an obsolete platform despite its usefulness and reliability. Most messaging
                               services such as Whatsapp and Facebook offer simple and intuitive messaging services which are soon replacing email.
                                But, what if the simplicity could be bought to email without compromising its extensibility?
                            </div>
                            <div class="small">
                                <img src="Images/note-34686_640.png" alt="Picture of Email" />
                            </div>
                        </div>
                        <%--http://pixabay.com/en/note-thumbtack-reminder-message-34686/--%>
                        <div class="row">
                            <div class="small">
                                <img src="Images/at-64056_640.jpg" alt="Picture of a Person Moving Email" />
                            </div>
                            <div class="big">
                                The Mail Client is a simple web based email client that takes care of the tediousness
                                involved in handling email. It aggregates emails received across multiple services and accounts
                                into a simple web based interface eliminating the need to check multiple services separately. And, unlike
                                traditional clients, it supports tags to label messages and even more awesome is the fact that it groups messages
                                together as threads so that you have less clutter.
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
</body>
</html>