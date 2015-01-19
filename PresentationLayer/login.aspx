<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TheMailClient.Presentation.login" ClientIDMode="static" %>


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
                <div id="video">
                    <div id="vid_wrapper">
                        <object id="flashObj" width="620" height="349" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,47,0">
                            <param name="movie" value="http://c.brightcove.com/services/viewer/federated_f9?isVid=1&isUI=1" />
                            <param name="bgcolor" value="#FFFFFF" />
                            <param name="flashVars" value="videoId=3717655989001&linkBaseURL=http%3A%2F%2Fwww.theatlantic.com%2Fvideo%2Findex%2F375660%2Femail-is-ruining-us-a-simple-solution%2F&playerID=1065729157001&playerKey=AQ~~,AAAABvb_NGE~,DMkZt2E6wO3dFlbHM7HTX1y1bVRDHLp_&domain=embed&dynamicStreaming=true" />
                            <param name="base" value="http://admin.brightcove.com" />
                            <param name="seamlesstabbing" value="false" />
                            <param name="allowFullScreen" value="true" />
                            <param name="swLiveConnect" value="true" />
                            <param name="allowScriptAccess" value="always" />
                            <embed src="http://c.brightcove.com/services/viewer/federated_f9?isVid=1&isUI=1" bgcolor="#FFFFFF" flashvars="videoId=3717655989001&linkBaseURL=http%3A%2F%2Fwww.theatlantic.com%2Fvideo%2Findex%2F375660%2Femail-is-ruining-us-a-simple-solution%2F&playerID=1065729157001&playerKey=AQ~~,AAAABvb_NGE~,DMkZt2E6wO3dFlbHM7HTX1y1bVRDHLp_&domain=embed&dynamicStreaming=true" base="http://admin.brightcove.com" name="flashObj" width="620" height="349" seamlesstabbing="false" type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="always" swliveconnect="true" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"></embed></object>
                    </div>
                </div>
                <div id="login">
                    <div id="login_form">
                        <input id="txtUsername" runat="server" type="text" required="required" meta:resourcekey="txtUsername" />
                        <br />
                        <input id="txtPassword" runat="server" type="password" required="required" meta:resourcekey="txtPassword" />
                        <br />
                        <input id="remember" runat="server" type="checkbox" meta:resourcekey="remember" /><asp:Localize ID="LblRemember" runat="server" meta:resourcekey="LblRemember" />
                        <br />
                        <button id="btnLogin" type="submit" runat="server" value="Login" meta:resourcekey="btnLogin" />
                        <br />
                        <a href="register.aspx" runat="server">
                            <asp:Localize ID="TextRegister" runat="server" meta:resourcekey="LblRegister" /></a><br />
                        <a href="forgot.aspx" runat="server">
                            <asp:Localize ID="Localize1" runat="server" meta:resourcekey="LblForgot" /></a>
                    </div>
                </div>
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
    $('#btnLogin').button({
        label: '<asp:Literal runat="server" Text="<%$ Resources:logintext%>" /> '
    });

    $('#txtUsername').placeholder();
</script>