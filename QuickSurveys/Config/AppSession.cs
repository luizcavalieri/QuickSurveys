using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys.Config
{
    public class AppSession
    {
        public static string UserID
        {
            set { HttpContext.Current.Session["UserId"] = value; }
            get
            {
                if (HttpContext.Current.Session["UserId"] == null || HttpContext.Current.Session["UserId"] == "")
                {
                    return "SESSION_GET_FAIL";
                }
                else
                {
                    return HttpContext.Current.Session["UserId"] as string;
                }
            }
        }
    }
}