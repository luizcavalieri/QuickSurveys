using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Data.SqlClient;
using System.Configuration;

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
        SqlConnection myConnection;

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

        public void connectString()
        {
            String myConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            if (myConnectionString.Equals("prod"))
            {
                myConnectionString = AppConstant.ProdConnectionString;
            }
            else if (myConnectionString.Equals("test"))
            {
                myConnectionString = AppConstant.TestConnectionString;
            }
            else
            {
                myConnectionString = AppConstant.DevConnectionString;
            }

            myConnection = new SqlConnection();
            myConnection.ConnectionString = myConnectionString;
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

        // check how many records there are in the database with 
        //the username inserted by the user
        //will return true just when there is only one user with that username
        public bool StaffLoginCheckUser()
        {
            connectString();

            SqlCommand myCommand;

            string getUserQuery = @"Select COUNT(*) from users
                                        where username = @username";

            myCommand = new SqlCommand(getUserQuery, myConnection);

            myConnection.Open();
            myCommand.Parameters.AddWithValue("@username", this.username);

            int numberOfRows = (int)myCommand.ExecuteScalar();

            if (myConnection.State == System.Data.ConnectionState.Open)
                myConnection.Close();

            if (numberOfRows != 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool StaffLogin()
        {
            connectString();
            SqlDataReader myReader;
            SqlCommand myCommand;
            string passwordEntered;

            string getUserQuery = @"Select username, user_password from users
                                        where username = @username";

            myCommand = new SqlCommand(getUserQuery, myConnection);

            myConnection.Open();
            myCommand.Parameters.AddWithValue("@username", this.username);
           
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                passwordEntered = myReader["user_password"].ToString();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                if (this.user_password == passwordEntered)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

    }
}