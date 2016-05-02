using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys.Config
{
    public class BasePage : System.Web.UI.Page
    {
        public Boolean IsLogin()
        {
            if (AppSession.UserID.Equals("SESSION_GET_FAIL"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Login(String userName)
        {
            AppSession.UserID = userName;
        }
    }
}