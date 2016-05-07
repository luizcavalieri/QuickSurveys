using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickSurveys.BusinessLogic
{
    public class User
    {
        int _user_id;
        string _username;
        string _user_fname;
        string _user_lname;
        string _user_password;
        string _user_role;
        string _user_pref_phone;
        DateTime _user_dob;

        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string user_fname
        {
            get { return _user_fname; }
            set { _user_fname = value; }
        }
        public string user_password
        {
            get { return _user_password; }
            set { _user_password = value; }
        }
        public string user_role
        {
            get { return _user_role; }
            set { _user_role = value; }
        }
        public string user_pref_phone
        {
            get { return _user_pref_phone; }
            set { _user_pref_phone = value; }
        }

        public DateTime user_dob
        {
            get { return _user_dob; }
            set { _user_dob = value; }
        }



    }
}