using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace QuickSurveys
{
    public class Survey
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

        # region method

        
        //public static IEnumerable<Survey> GetData() 
        //{
            //SqlConnection myConnection;
            //SqlCommand myCommand;
            
            //String myConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            //if (myConnectionString.Equals("prod"))
            //{
            //    myConnectionString = AppConstant.ProdConnectionString;
            //}
            //else if (myConnectionString.Equals("test"))
            //{
            //    myConnectionString = AppConstant.TestConnectionString;
            //}
            //else
            //{
            //    myConnectionString = AppConstant.DevConnectionString;
            //}

            //myConnection = new SqlConnection();
            //myConnection.ConnectionString = myConnectionString;

            //String queryGetSurvey = @"Select survey_id, survey_description, survey_user_id from surveys";
            //myCommand = new SqlCommand(queryGetSurvey, myConnection);

            ////open connectio
            //myConnection.Open();

            ////open the reader
            //SqlDataReader myReader = myCommand.ExecuteReader();
            //surveyList = new List<Survey>();
            //Survey surveyObj;

            //surveyObj = new Survey();
            //int index = 0;

            //while(myReader.Read())
            //{
            //    surveyObj.survey_description = myReader["survey_description"].ToString();
            //    surveyObj.survey_id = Int32.Parse(myReader["survey_id"].ToString());
            //    surveyList.Add(surveyObj);
            //    index++;
            //}


            //return surveyList;
        //}

        

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