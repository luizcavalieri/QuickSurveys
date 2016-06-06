using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;

namespace QuickSurveys
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

        public string user_lname
        {
            get { return _user_lname; }
            set { _user_lname = value; }
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


        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } 
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
}