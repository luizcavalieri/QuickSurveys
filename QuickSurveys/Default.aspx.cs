using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace QuickSurveys
{
    public partial class Default : Config.BasePage
    {
        // creating object from class question
        Question currentQuestion = new Question();
        Survey currentSurvey = new Survey();
        SqlConnection myConnection;
        SqlCommand myCommand;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            connectString();

            String queryGetSurvey = @"Select survey_id, survey_description, survey_user_id from surveys";

            myCommand = new SqlCommand(queryGetSurvey, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            if (myReader.Read()) {
                currentSurvey.survey_description = myReader["survey_description"].ToString();
                currentSurvey.survey_description = myReader["survey_description"].ToString();
            }



            myConnection.Close();
            
            int surveyId = 1;
            int questSequence = 8;

            GetQuestion(surveyId, questSequence);


        }

        private void GetQuestion(int surveyId, int questSequence)
        {

            connectString();
            
            String queryQuestionInputType = @"SELECT qt.quest_id, 
                                                     qt.quest_description, 
                                                     qt.quest_survey_id,
                                                     qt.quest_survey_sequence, 
                                                     qt.quest_input_type_id, 
                                                     qt.quest_answer_group_id, 
                                                     it.input_type_desc, 
                                                     srv.survey_description 
                                             FROM questions qt
                                             JOIN input_type it
                                             ON qt.quest_input_type_id = it.input_type_id
                                             Join surveys srv 
                                             ON qt.quest_survey_id = srv.survey_id
                                             where quest_survey_id = " + surveyId + @" and 
                                                   quest_survey_sequence = " + questSequence;


            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryQuestionInputType, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            if (myReader.Read()) 
            {
                currentQuestion.quest_description = myReader["quest_description"].ToString();
                currentQuestion.quest_survey_sequence = Int32.Parse(myReader["quest_survey_sequence"].ToString());
                currentSurvey.survey_description = myReader["survey_description"].ToString();
                //currentQuestion.quest_answer_group_id = Int32.Parse(myReader["quest_answer_group_id"].ToString());
                currentQuestion.quest_answer_group_id = string.IsNullOrEmpty(myReader["quest_answer_group_id"].ToString()) ? 0 : int.Parse(myReader["quest_answer_group_id"].ToString());
                currentQuestion.quest_input_type_id = Int32.Parse(myReader["quest_input_type_id"].ToString());

                int inputType = currentQuestion.quest_input_type_id;
                int answerGroupId = currentQuestion.quest_answer_group_id;

                if (inputType == 1 || inputType == 2 || inputType == 18)
                {
                    FillCheckBox(answerGroupId, inputType);
                }
                else if(inputType == 10)
                {
                    numberBox.Visible = true;
                }
                else if (inputType == 19)
                {
                    textAreaBox.Visible = true;
                }
                else if (inputType == 3)
                {
                    textBox.Visible = true;
                }



            }

            lblQuestionDesc.Text = currentQuestion.quest_description;
            lblQuestSurveySequence.Text = currentQuestion.quest_survey_sequence.ToString();
            lblSurveyDesc.Text = currentSurvey.survey_description;
            //lblTestAnswerGroup.Text = currentQuestion.quest_answer_group_id.ToString();
        
        }



        private void FillCheckBox(int answerGroupId, int inputType)
        {

            connectString();

            String queryAnswerGroupOption = @"SELECT answer_group_option_id, 
                                                     answer_group_option_desc, 
                                                     answer_group_id,
                                                     answer_group_logical_answer 
                                             FROM answer_group_option
                                             where answer_group_id = " + answerGroupId;

            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryAnswerGroupOption, myConnection);

            myConnection.Open();

            
            SqlDataReader readerChbx = myCommand.ExecuteReader();

            while (readerChbx.Read())
            {
                ListItem item = new ListItem();
                item.Text = readerChbx["answer_group_option_desc"].ToString();
                item.Value = readerChbx["answer_group_option_id"].ToString();

                if (inputType == 1)
                {
                    cbxAnswerGroupOpt.Items.Add(item);
                    
                }
                else if (inputType == 2)
                {
                    rdbAnswerGroupOpt.Items.Add(item);
                    
                }
                else if (inputType == 18)
                {
                    ddAnswerGroupOpt.Visible = true;
                    ddAnswerGroupOpt.Items.Add(item);
                }
            }

            myConnection.Close();

        }

        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            //{
            //    //Image img = new Image();

            //if (e.Row.Cells[2].Text.Equals("1"))
            //{
            //    img.ImageUrl = "~/images/pawn.gif";
            //}
            //else if (e.Row.Cells[2].Text.Equals("2"))
            //{
            //    img.ImageUrl = "~/images/knight.gif";
            //}
            //else
            //{
            //    img.ImageUrl = "~/images/king.gif";
            //}
            //e.Row.Cells[2].Controls.Add(img);

            //CheckBox chk = new CheckBox();

            //chk.ID = "selectedId";
            //e.Row.Cells[0].Controls.Add(chk);

            DropDownList ddl = new DropDownList();

            for (int i = 1; i <= Int32.Parse(e.Row.Cells[1].Text); i++)
            {
                ddl.Items.Add(i.ToString());
            }

            //e.Row.Cells[1].Controls.Add(ddl);


        }
    }
}