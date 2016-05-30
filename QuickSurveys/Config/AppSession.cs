using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys
{
    public class AppSession
    {
        public static string UserID
        {
            set { HttpContext.Current.Session["user_id"] = value; }
            get
            {
                if (HttpContext.Current.Session["user_id"] == null || HttpContext.Current.Session["user_id"] == "")
                {
                    return "SESSION_GET_FAIL";
                }
                else
                {
                    return HttpContext.Current.Session["user_id"] as string;
                }
            }
        }

        public static List<Answer> AnswerList
        {
            set 
            { 
                HttpContext.Current.Session["answerlist"] = value; 
            }
            get
            {
                return HttpContext.Current.Session["answerlist"] as List<Answer>;
            }
        }

        public static int? IndexAnswer
        {
            set { HttpContext.Current.Session["indexAnswer"] = value;  }
            get
            {
                return HttpContext.Current.Session["indexAnswer"] as int?;
            }
        }

        public static int? RespondentId
        {
            set { HttpContext.Current.Session["respondentId"] = value; }
            get
            {
                return HttpContext.Current.Session["respondentId"] as int?;
            }
        }

        public static int? SurveyId
        {
            set { HttpContext.Current.Session["surveyId"] = value; }
            get
            {
                return HttpContext.Current.Session["surveyId"] as int?;
            }
        }

        public static bool? EndOfSurvey
        {
            set { HttpContext.Current.Session["endOfSurvey"] = value; }
            get
            {
                return HttpContext.Current.Session["endOfSurvey"] as bool?;
            }
        }
    }
}