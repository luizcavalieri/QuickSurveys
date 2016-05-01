using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys
{
    public class AppConstant
    {        
        public static String DevConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_9AB8B7_DDA5080;Integrated Security=True";
        public static String TestConnectionString = "Data Source=SQL5025.myWindowsHosting.com;Initial Catalog=DB_9AB8B7_DDA5080;User Id=DB_9AB8B7_DDA5080_admin;Password=hG3Qu25k";
        public static String ProdConnectionString = "Data Source=SQL5006.myWindowsHosting.com;Initial Catalog=DB_9AB8B7_enterprise;User Id=DB_9AB8B7_enterprise_admin;Password=enterprise";
    }
}