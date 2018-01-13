using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace QuickSurveys
{
    public class Answer
    {
        #region Fields

        private string _answer_text;
        private int? _answer_numeric;
        private bool? _answer_boolean;
        private int _answer_resp_id;
        private int _answer_question_id;
        private int? _answer_group_option_id;

        #endregion

        #region Properties

        public string answer_text
        {
            get { return _answer_text; }
            set { _answer_text = value; }
        }
        public int? answer_numeric
        {
            get { return _answer_numeric; }
            set { _answer_numeric = value; }
        }
        public bool? answer_boolean
        {
            get { return _answer_boolean; }
            set { _answer_boolean = value; }
        }
        public int answer_resp_id
        {
            get { return _answer_resp_id; }
            set { _answer_resp_id = value; }
        }
        public int answer_question_id
        {
            get { return _answer_question_id; }
            set { _answer_question_id = value; }
        }
        public int? answer_group_option_id
        {
            get { return _answer_group_option_id; }
            set { _answer_group_option_id = value; }
        }

        #endregion

        # region method

        #endregion
    }
}