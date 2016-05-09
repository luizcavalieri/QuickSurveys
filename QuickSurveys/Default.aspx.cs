using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;

namespace QuickSurveys
{
    public partial class Default : Config.BasePage
    {
        // creating object from class question
        Question currentQuestion = new Question();
        Survey currentSurvey = new Survey();
        Survey listSurvey = new Survey();
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
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["survey_id"] as string))
                {
                    MultiViewMainPage.ActiveViewIndex = 1;

                    int survSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                    int survId = Int32.Parse(Session["survey_id"].ToString());
                    GetQuestion(survId, survSequence);
                }
                else 
                {
                    MultiViewMainPage.ActiveViewIndex = 0;

                    GetSurveyButtons();
                }

            }

        }

        protected void SurveyButton_Click(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Session["survey_id"] = btn.CommandArgument.ToString();
            Session["quest_survey_sequence"] = 1;

            if (!string.IsNullOrEmpty(Session["survey_id"] as string))
            {
                int surveyId = Int32.Parse(Session["survey_id"].ToString());
                int questSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                GetQuestion(surveyId, questSequence);
                MultiViewMainPage.ActiveViewIndex = 1;

            }
            


            Debug.Write(Session["survey_id"]);

            

        }

        private void GetSurveyButtons()
        {
            connectString();

            String queryGetSurvey = @"Select survey_id, survey_description, survey_user_id from surveys";

            myCommand = new SqlCommand(queryGetSurvey, myConnection);

            LinkButton surveyButton = new LinkButton();

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            int index = 0;
            ArrayList values = new ArrayList();

            while (myReader.Read())
            {
                listSurvey.survey_description = myReader["survey_description"].ToString();
                listSurvey.survey_id = Int32.Parse(myReader["survey_id"].ToString());
                listSurvey.survey_user_id = Int32.Parse(myReader["survey_user_id"].ToString());
                values.Add(new SurveyData(listSurvey.survey_description, listSurvey.survey_id, listSurvey.survey_user_id));

                index++;
            }

            RepeaterSurvey.DataSource = values;
            RepeaterSurvey.DataBind();


            myConnection.Close();
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
                    textAreaBox.Visible = false;
                    textBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                }
                else if (inputType == 19)
                {
                    textAreaBox.Visible = true;
                    numberBox.Visible = false;
                    textBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                }
                else if (inputType == 3)
                {
                    textBox.Visible = true;
                    numberBox.Visible = false;
                    textAreaBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                }
                else
                {
                    textBox.Visible = false;
                    textAreaBox.Visible = false;
                    numberBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                }



            }

            lblQuestionDesc.Text = currentQuestion.quest_description;
            lblQuestSurveySequence.Text = currentQuestion.quest_survey_sequence.ToString();
            lblSurveyDesc.Text = currentSurvey.survey_description;
            lblSurveySession.Text = Session["survey_id"].ToString();
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
                    cbxAnswerGroupOpt.Visible = true;
                    cbxAnswerGroupOpt.Items.Add(item);
                    textBox.Visible = false;
                    textAreaBox.Visible = false;
                    numberBox.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                    
                }
                else if (inputType == 2)
                {
                    rdbAnswerGroupOpt.Visible = true;
                    rdbAnswerGroupOpt.Items.Add(item);
                    textBox.Visible = false;
                    textAreaBox.Visible = false;
                    numberBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    ddAnswerGroupOpt.Visible = false;
                    
                }
                else if (inputType == 18)
                {
                    ddAnswerGroupOpt.Visible = true;
                    ddAnswerGroupOpt.Items.Add(item);
                    textBox.Visible = false;
                    textAreaBox.Visible = false;
                    numberBox.Visible = false;
                    cbxAnswerGroupOpt.Visible = false;
                    rdbAnswerGroupOpt.Visible = false;
                   
                }
            }
            readerChbx.Close();
            myConnection.Close();

        }

        protected void SaveAndNextQuest_Click(object sender, EventArgs e)
        {
            
            Session["quest_survey_sequence_TEMP"] = Int32.Parse(Session["quest_survey_sequence"].ToString()) + 1;

            Session["quest_survey_sequence"] = Int32.Parse(Session["quest_survey_sequence_TEMP"].ToString());

            int survSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
            int survId = Int32.Parse(Session["survey_id"].ToString());
            Response.Redirect("~/Default.aspx");
            //GetQuestion(survId, survSequence);
        }

        
    }
}