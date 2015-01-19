<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update_profile.aspx.cs" Inherits="TheMailClient.Presentation.update_profile" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head runat="server">
    <title>
        <asp:Localize ID="PageTitle" runat="server" meta:resourcekey="PageTitle" /></title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Page.css" rel="stylesheet" />
    <script src="Scripts/mustache.js"></script>
    <script src="Scripts/Register.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
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
                    <asp:TextBox ID="search_term" Text="<%$Resources: Global, Search %>" runat="server" CssClass="ui-corner-left" /><asp:TextBox ID="searchButton" Text="🔎" runat="server" Enabled="false" CssClass="ui-corner-right" />
                </div>
                <div id="account">
                    <asp:BulletedList ID="account_links" CssClass="horizontal navigation" runat="server" DisplayMode="HyperLink" />
                    <asp:DropDownList ID="lang_selector" runat="server" OnSelectedIndexChanged="lang_selector_SelectedIndexChanged" />
                </div>
            </div>

            <div id="body">
                <div id="content" class="ui-widget ui-corner-all">
                    <div id="register_header" class="ui-widget-header ui-corner-top">
                        <header>
                            <asp:Label ID="TitleAddUpdate" runat="server"></asp:Label>
                        </header>
                    </div>
                    <div id="register" class="ui-widget-content ui-corner-bottom">

                        <div class="form">
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblFirstName" runat="server" meta:resourcekey="LblFirstName" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtFirstName" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtFirstName" required="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblLastName" runat="server" meta:resourcekey="LblLastName" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtLastName" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtLastName" required="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblAddress" runat="server" meta:resourcekey="LblAddress" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtAddress" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtAddress" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblPhone" runat="server" meta:resourcekey="LblPhone" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtPhone" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtPhone" TextMode="Phone" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblEmail" runat="server" meta:resourcekey="LblEmail" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtEmail" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtEmail" TextMode="Email" required="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblUsername" runat="server" meta:resourcekey="LblUsername" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtUsername" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtUsername" required="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblPassword" runat="server" meta:resourcekey="LblPassword" />
                                </div>
                                <div class="right_column">
                                    <asp:TextBox ID="txtPassword" CssClass="ui-corner-all" runat="server" meta:resourcekey="txtPassword" TextMode="Password" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblTheme" runat="server" meta:resourcekey="LblTheme" />
                                </div>
                                <div class="right_column">
                                    <asp:DropDownList ID="comboTheme" runat="server" CssClass="ui-corner-all" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblLocale" runat="server" meta:resourcekey="lblLocale" />
                                </div>
                                <div class="right_column">
                                    <asp:DropDownList ID="comboLocale" runat="server" CssClass="ui-corner-all" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="left_column">
                                    <asp:Localize ID="LblType" runat="server" meta:resourcekey="LblType" />
                                </div>
                                <div class="right_column">
                                    <asp:DropDownList ID="comboType" runat="server" CssClass="ui-corner-all">
                                        <asp:ListItem Value="ADMIN" Text="Administrator" />
                                        <asp:ListItem Value="User" Text="User" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">

                                <input type="submit" id="btnSubmit" runat="server" meta:resourcekey="btnSubmit" onclick="" />

                                <input id="btnReset" type="reset" runat="server" meta:resourcekey="btnReset" />
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