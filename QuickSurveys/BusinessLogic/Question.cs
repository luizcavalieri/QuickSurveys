using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QuickSurveys
{
    public class Question
    {
        #region Fields

        private int _quest_id;
        private string _quest_description;
        private int _quest_answer_group_option_id;
        private int _quest_required;
        private int _quest_input_type_id;
        private int _quest_survey_id;
        private int _quest_survey_sequence;
        private int _quest_main_id;
        private int _quest_child_sequence;

        #endregion

        #region Properties

        public int quest_id
        {
            get { return _quest_id;  }
            set { _quest_id = value; }
        }

        public string quest_description
        {
            get { return _quest_description; }
            set { _quest_description = value; }
        }
        public int quest_answer_group_option_id
        {
            get { return _quest_answer_group_option_id; }
            set { _quest_answer_group_option_id = value; }
        }
        public int quest_required
        {
            get { return _quest_required; }
            set { _quest_required = value; }
        }
        public int quest_input_type_id
        {
            get { return _quest_input_type_id; }
            set { _quest_input_type_id = value; }
        }
        public int quest_survey_id
        {
            get { return _quest_survey_id; }
            set { _quest_survey_id = value; }
        }
        public int quest_survey_sequence
        {
            get { return _quest_survey_sequence; }
            set { _quest_survey_sequence = value; }
        }

        public int quest_main_id
        {
            get { return _quest_main_id; }
            set { _quest_main_id = value; }
        }

        public int quest_child_sequence
        {
            get { return _quest_child_sequence; }
            set { _quest_child_sequence = value; }
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