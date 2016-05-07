using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys
{
    public class Survey
    {
        #region Fields

        private int _survey_id;
        private string _survey_description;
        private int _survey_user_id;
        
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

        # region method

        public int showQuestion()
        {

            return 1;
        }

        public int getInputType()
        {
            int inputTypeForm = 0;



            return inputTypeForm;
        }


        #endregion
    }
}