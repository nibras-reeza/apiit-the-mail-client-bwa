<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact_us.aspx.cs" Inherits="TheMailClient.Presentation.contact_us" ClientIDMode="static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="ui-widget">
<head id="Head1" runat="server">
    <title>
        <asp:Localize ID="Localize1" runat="server" meta:resourcekey="LblTitle" /></title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <link href="Styles/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <link href="Styles/Inbox.css" rel="stylesheet" />

    <script src="Scripts/mustache.js"></script>
    <script src="Scripts/basic.js"></script>

    <style type="text/css">
        #text {
            height: 75%;
            right: 450px;
            padding: 2%;
            float: left;
            width: auto;
        }

        #content header {
            padding: 1%;
            font-weight: bold;
            font-size: large;
        }
    </style>
    <%--http://www.mapseasy.com/google-maps-html-generator.php--%>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        ///////////////////////////////////////////////////////////////////
        // Powered By MapsEasy.com Maps Generator
        // Please keep the author information as long as the maps in use.
        // You can find the free service at: http://www.MapsEasy.com
        ///////////////////////////////////////////////////////////////////
        function LoadGmaps() {
            var myLatlng = new google.maps.LatLng(6.9204553, 79.8577746);
            var myOptions = {
                zoom: 16,
                center: myLatlng,
                disableDefaultUI: true,
                panControl: true,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.DEFAULT
                },

                mapTypeControl: true,
                mapTypeControlOptions: {
                    style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR
                },
                streetViewControl: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(document.getElementById("MyGmaps"), myOptions);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: "388, Dr Colvin R de Silva Mawatha, Colombo"
            });
        }
    </script>
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
                            <asp:Localize ID="LblTitle" runat="server" meta:resourcekey="LblTitle" />
                        </header>
                    </div>
                    <div class="ui-accordion-content" id="text">
                        <asp:Localize ID="Localize6" runat="server" meta:resourcekey="LblReach" />
                        <br />
                        <br />
                        <br />
                        <asp:Localize ID="Localize2" runat="server" meta:resourcekey="LblEmail" />nibras@nibrasweb.com
                        <br />
                        <asp:Localize ID="Localize9" runat="server" meta:resourcekey="LblPhone" />+94779966375
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Localize ID="Localize3" runat="server" meta:resourcekey="LblVisit" />
                        <br />
                        <header>
                            <asp:Localize ID="Localize4" runat="server" meta:resourcekey="LblAddressTitle" />
                        </header>
                        <br />
                        <br />
                        <asp:Localize ID="Localize5" runat="server" meta:resourcekey="LblAddress1" />
                        <br />
                        <asp:Localize ID="Localize7" runat="server" meta:resourcekey="LblAddress2" />
                        <br />
                        <asp:Localize ID="Localize8" runat="server" meta:resourcekey="LblAddress3" />
                    </div>
                    <%--http://www.mapseasy.com/google-maps-html-generator.php--%>
                    <div id="MyGmaps" style="width: 400px; height: 300px; border: 1px solid #CECECE; float: right; margin: 3%;"></div>
                    <div>
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
    $('body').bind("onload", LoadGmaps());
    $('body').bind("onunload", GUnload());
</script>