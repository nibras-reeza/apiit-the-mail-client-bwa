/*****************************************************************************
 * This file contains a C# helper class that retreives web site's configurations
 * from the SQL database.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 18/08/2014
 *
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace TheMailClient.Presentation.App_Code.Controller
{
    //https://stackoverflow.com/questions/8046989/access-database-data-from-the-code-behind-using-asp-net-c-sharp
    //http://forums.asp.net/t/1169112.aspx?How+to+access+configuration+appSettings+from+a+class+library+project+

    public class UIConfig
    {
        // Thread-safe singleton pattern
        private static UIConfig instance = new UIConfig();

        private UIConfig()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UIDB"].ConnectionString);
        }

        public static UIConfig getInstance()
        {
            return instance;
        }

        private SqlConnection connection = null;

        // Retrieve a list of links to display in the navigation bar.
        public Dictionary<String, String> getNavBarItems()
        {
            Dictionary<String, String> items = new Dictionary<String, String>();

            //http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand%28v=vs.110%29.aspx

            SqlCommand select = new SqlCommand("getNavBarItems", connection);

            connection.Open();

            try
            {
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(reader[1].ToString(), reader[2].ToString());
                }
            }
            finally
            {
                connection.Close();
            }

            return items;
        }

        //https://stackoverflow.com/questions/9019997/how-to-change-a-asp-webapplication-theme-not-page-programmatically-and-during
        public Dictionary<string, string> getThemes()
        {
            Dictionary<string, string> themes = new Dictionary<string, string>();

            // Get a list of themes
            string[] list = (from d in Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/app_themes"))
                             select Path.GetFileName(d)).ToArray();

            foreach (string s in list)
            {
                string title = s.Replace('-', ' ');
                //http://www.dotnetperls.com/textinfo
                //http://msdn.microsoft.com/en-us/library/system.globalization.textinfo.totitlecase%28v=vs.110%29.aspx
                title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                themes.Add(title, s);
            }

            return themes;
        }

        public string getDefaultTheme()
        {
            return HttpContext.GetGlobalResourceObject("Global", "Theme").ToString();
        }

        public IList<string> GetAvailableCultures(string directoryPath, string fileName)
        {
            // Used in WriteLine to trim output lines.
            int trimLength = directoryPath.Length;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(directoryPath);

            IEnumerable<System.IO.FileInfo> fileList =
              dir.GetFiles(string.Format("{0}.*.resx", fileName),
              System.IO.SearchOption.AllDirectories);

            // Select out the name of culture from the list of resource files
            IList<string> Cultures = fileList
                .Select(x => x.Name.Split('.')[x.Name.Split('.').Length - 2].ToString())
                .Distinct()
                .Where(y => y.Length < 8 && y != "aspx").ToList<string>();

            Cultures.Add("en");

            return Cultures;
        }
    }
}