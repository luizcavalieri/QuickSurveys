using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys
{
    public class SurveyData
    {
        #region Fields
        
        private int _survey_id;
        private string _survey_description;
        private int _survey_user_id;
        //static List<Survey> surveyList;

        #endregion

        #region Properties

        public int survey_id
        {
            get { return _survey_id; }
            set { _survey_id = value; }
        }

        public string survey_description
        {
            get { return _survey_description; }
            set { _survey_description = value; }
        }
        public int survey_user_id
        {
            get { return _survey_user_id; }
            set { _survey_user_id = value; }
        }
        #endregion

        public SurveyData(string survey_description, int survey_id, int survey_user_id)
        {
            this.survey_description = survey_description;
            this.survey_id = survey_id;
            this.survey_user_id = survey_user_id;
        }
        
    }
}