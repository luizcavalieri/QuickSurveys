using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys.Config
{
    public class AppUtil
    {
        public void AddAnswer(Answer tempAnswer)
        {
            List<Answer> tempCurrentList = AppSession.AnswerList;

            tempCurrentList.Add(tempAnswer);

            AppSession.AnswerList = tempCurrentList;
        }
    }
}