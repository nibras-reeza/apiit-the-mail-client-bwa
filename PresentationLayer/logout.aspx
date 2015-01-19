<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="TheMailClient.Presentation.logout" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title>
        <asp:Literal ID="PageTitle" runat="server" meta:resourcekey="PageTitle" />
    </title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Page.css" rel="stylesheet" />

    <script src="Scripts/mustache.js"></script>
    <script src="Scripts/Register.js"></script>
    <style type="text/css">
        #login {
            width: 30%;
            float: right;
        }

        #video {
            width: 63%;
            float: left;
            padding: 2%;
            padding-bottom: 5%;
            padding-top: 5%;
            text-align: center;
        }

        #vid_wrapper {
            margin-left: auto;
            margin-right: auto;
        }

        #login_form {
            width: 50%;
            margin-left: auto;
            margin-right: auto;
            margin-top: 30%;
        }

            #login_form * {
                padding: 1%;
                margin-top: 3%;
                margin-bottom: 3%;
            }
    </style>
</head>
<body class="ui-widget-content">
    <form id="form1" runat="server">
        <div id="main_wrapper">
            <div id="message" runat="server"></div>
            <%--http://jasonlau.biz/home/jquery/how-to-code-webpage-elements-to-use-jquery-ui-styles--%>
            <div id="header" class="ui-widget-header ui-corner-all">
                <%--https://stackoverflow.com/questions/244807/how-to-use-image-resource-in-asp-net-website--%>
                <div id="logo">
                    <asp:Image ID="logo_image" AlternateText="Logo of The Mail Client" src="<%$Resources: Icons, Logo %>" runat="server" />
                </div>
                <div id="search">
                    <h1>
                        <asp:Literal ID="PageHeader" runat="server" meta:resourcekey="PageHeader" /></h1>
                </div>
                <div id="account">
                    <asp:BulletedList ID="account_links" CssClass="horizontal navigation" runat="server" DisplayMode="HyperLink" />
                    <asp:DropDownList ID="lang_selector" runat="server" OnSelectedIndexChanged="lang_selector_SelectedIndexChanged" />
                </div>
            </div>

            <div id="body">
                <h1>Logging out...</h1>
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
</script>