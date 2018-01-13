using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys
{
    public class Respondent : User
    {
        int _resp_id;
        int _resp_gender;
        int _resp_state_territory;
        string _resp_email;
        string _resp_home_suburb;
        int _res_home_post_code;
        int _resp_work_post_code;
        string _resp_work_suburb;
        DateTime _resp_date;
        string _resp_ip;
        int _resp_user_id;
        int _resp_survey_id;
    

        public int resp_id
            {
                get { return _resp_id; }
                set { _resp_id = value; }
            }

        public int resp_gender
        {
            get { return _resp_gender; }
            set { _resp_gender = value; }
        }

        
        public int resp_state_territory
        {
            get { return _resp_state_territory; }
            set { _resp_state_territory = value; }
        }

        public string resp_email
        {
            get { return _resp_email; }
            set { _resp_email = value; }
        }

        public string resp_home_suburb
        {
            get { return _resp_home_suburb; }
            set { _resp_home_suburb = value; }
        }

        public int res_home_post_code
        {
            get { return _res_home_post_code; }
            set { _res_home_post_code = value; }
        }

        public int resp_work_post_code
        {
            get { return _resp_work_post_code; }
            set { _resp_work_post_code = value; }
        }

        public string resp_work_suburb
        {
            get { return _resp_work_suburb; }
            set { _resp_work_suburb = value; }
        }

        public DateTime resp_date
        {
            get { return _resp_date; }
            set { _resp_date = value; }
        }

        public string resp_ip
        {
            get { return _resp_ip; }
            set { _resp_ip = value; }
        }

        public int resp_user_id
        {
            get { return _resp_user_id; }
            set { _resp_user_id = value; }
        }

        public int resp_survey_id
        {
            get { return _resp_survey_id; }
            set { _resp_survey_id = value; }
        }

    }
}