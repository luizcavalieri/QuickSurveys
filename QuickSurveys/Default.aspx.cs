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
        Respondent currentRespondent = new Respondent();
        SqlConnection myConnection;
        SqlCommand myCommand;
        Answer currentAnswer = new Answer();
        List<Answer[]> answerList = new List<Answer[]>();
        //int answerIndexArray;
        AppSession appSession = new AppSession();
        User currentUser = new User();
        User staffCurrentUser = new User();
        
        
        // open the connection with the database.
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

        // page_load when the application is loaded -- the first function to happen
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (AppSession.StaffSession != null && AppSession.StaffSession == true && AppSession.StaffLogged != null && AppSession.StaffLogged == true)
                {
                    MultiViewMainPage.ActiveViewIndex = 5;
                    Response.Write("<script language=javascript>alert('Welcome!!');</script>");
                }
                else if ((AppSession.StaffSession != null && AppSession.StaffSession == true) && (AppSession.StaffLogged == null || AppSession.StaffLogged == false))
                {
                    MultiViewMainPage.ActiveViewIndex = 4;

                }
                else
                {
                    if (AppSession.SurveyId != null)
                    {
                        if (AppSession.EndOfSurvey == null)
                        {
                            AppSession.EndOfSurvey = false;
                        }


                        if (AppSession.EndOfSurvey == true)
                        {
                            FillDropDownState();
                            FillDropDownGender();
                            MultiViewMainPage.ActiveViewIndex = 2;
                        }
                        else
                        {
                            MultiViewMainPage.ActiveViewIndex = 1;
                            int survSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                            int? survId = AppSession.SurveyId;
                            GetQuestion(survId, survSequence);
                        }

                    }
                    else
                    {
                        MultiViewMainPage.ActiveViewIndex = 0;
                        Session["answer_group_option_child"] = false;
                        Session["current_question"] = 0;
                        GetSurveyButtons();
                    }

                }
 
            }

        }

        private void FillDropDownState()
        {
            connectString();

            String queryAnswerGroupOption = @"SELECT answer_group_option_id, 
                                                     answer_group_option_desc, 
                                                     answer_group_id,
                                                     answer_group_logical_answer 
                                             FROM answer_group_option
                                             where answer_group_id = 6
                                             Order by answer_group_option_desc";

            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryAnswerGroupOption, myConnection);

            myConnection.Open();

            
            SqlDataReader readerChbx = myCommand.ExecuteReader();

            while (readerChbx.Read())
            {
                ListItem item = new ListItem();
                item.Text = readerChbx["answer_group_option_desc"].ToString();
                item.Value = readerChbx["answer_group_option_id"].ToString();
                ddAnswerGroupOpt.Visible = true;
                ddState.Items.Add(item);
            }

            myConnection.Close();

        }

        private void FillDropDownGender()
        {
            connectString();

            String queryAnswerGroupOption = @"SELECT answer_group_option_id, 
                                                     answer_group_option_desc, 
                                                     answer_group_id,
                                                     answer_group_logical_answer 
                                             FROM answer_group_option
                                             where answer_group_id = 8
                                             Order by answer_group_option_desc";

            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryAnswerGroupOption, myConnection);

            myConnection.Open();


            SqlDataReader readerChbx = myCommand.ExecuteReader();

            while (readerChbx.Read())
            {
                ListItem item = new ListItem();
                item.Text = readerChbx["answer_group_option_desc"].ToString();
                item.Value = readerChbx["answer_group_option_id"].ToString();
                ddRespGender.Items.Add(item);
                
            }

            myConnection.Close();

        }

        // respondent chooses the survey that he wants to answer
        protected void SurveyButton_Click(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            AppSession.SurveyId = Int32.Parse(btn.CommandArgument.ToString());
            Session["quest_survey_sequence"] = 1;
            GetIPAddress();

            if (AppSession.SurveyId != null)
            {
                MultiViewMainPage.ActiveViewIndex = 1;
                int questSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                GetQuestion(AppSession.SurveyId, questSequence);

            }
            
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            return context.Request.ServerVariables["REMOTE_ADDR"];

        }

        // show survey buttons
        private void GetSurveyButtons()
        {
            connectString();

            //get list of surveys
            String queryGetSurvey = @"Select survey_id, survey_description, survey_user_id from surveys";

            myCommand = new SqlCommand(queryGetSurvey, myConnection);

            LinkButton surveyButton = new LinkButton();

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            
            ArrayList values = new ArrayList();

            while (myReader.Read())
            {
                listSurvey.survey_description = myReader["survey_description"].ToString();
                listSurvey.survey_id = Int32.Parse(myReader["survey_id"].ToString());
                listSurvey.survey_user_id = Int32.Parse(myReader["survey_user_id"].ToString());
                values.Add(new SurveyData(listSurvey.survey_description, listSurvey.survey_id, listSurvey.survey_user_id));
            }

            RepeaterSurvey.DataSource = values;
            RepeaterSurvey.DataBind();


            myConnection.Close();
        }

        // show questions by the survey and sequence
        private void GetQuestion(int? surveyId, int questSequence)
        {

            if (Session["answer_group_option_child"] == null)
            {
                Session["answer_group_option_child"] = false;
            }
                
            if (AppSession.SurveyId != null)
            {
                //hidding the back button in case this is the first question.
                if (questSequence == 1)
                {
                    btnBack.Visible = false;
                }
                else
                {
                    btnBack.Visible = true;
                }

                int? previousQuest;
                bool answerGroupChildFlag;
                connectString();

                SqlDataReader myReader;

                // if current question is null it means that this is not the first question 
                //so the system can start the check if there is any answer that is flagged for child question.

                if (Session["current_question"] != null)
                {
                    previousQuest = Int32.Parse(Session["current_question"].ToString());
                    answerGroupChildFlag = bool.Parse(Session["answer_group_option_child"].ToString());
                }
                else
                {
                    previousQuest = 0;
                    answerGroupChildFlag = false;
                }

                // Check if respondent answerede any question with an answer flagged
                if (answerGroupChildFlag)
                {
                    String queryQuestionInputType = @"SELECT qt.quest_id, 
                                                         qt.quest_description, 
                                                         qt.quest_survey_id,
                                                         qt.quest_main_id,
                                                         qt.quest_child_sequence,
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
                                                 where quest_main_id = " + previousQuest;

                    //get the sql script executing on the connection
                    myCommand = new SqlCommand(queryQuestionInputType, myConnection);

                    int surveyTemp = Int32.Parse(Session["quest_survey_sequence"].ToString()) - 1;
                    Session["quest_survey_sequence"] = surveyTemp;

                }
                else
                {
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

                    //open connection
                    myConnection.Open();

                    //open the reader
                    myReader = myCommand.ExecuteReader();


                    // redirect the page to the registration page after finishing the survey
                    if (CheckLastQuest(myReader) == 0)
                    {
                        AppSession.EndOfSurvey = true;
                        Response.Redirect("~/Default.aspx");
                    }

                    myConnection.Close();
                }

                //open connection
                myConnection.Open();

                //open the reader
                myReader = myCommand.ExecuteReader();

                // populate the answer option objects from the database
                PopulateMultiAnswer(myReader);
            }

        
        }

        private void InsertQuestsToDatabase(int? respondentId)
        {
            
            for (int i = 0; i < AppSession.AnswerList.Count; i++)
            {
                int? answer_numeric = AppSession.AnswerList[i].answer_numeric;
                string answer_text = AppSession.AnswerList[i].answer_text;
                bool? answer_boolean = AppSession.AnswerList[i].answer_boolean;
                int? answer_resp_id = respondentId;
                int? answer_quest_id = AppSession.AnswerList[i].answer_question_id;
                int? answer_group_option_id = AppSession.AnswerList[i].answer_group_option_id;
                connectString();

                string insertAnswers = @"INSERT INTO answers
                                            (answer_numeric ,answer_text ,answer_boolean ,answer_resp_id ,answer_question_id ,answer_group_option_id)
                                          values
                                            (@answer_numeric, @answer_text, @answer_boolean, @answer_resp_id, @answer_quest_id, @answer_group_option_id)";

                
               // //get the sql script executing on the connection
               myCommand = new SqlCommand(insertAnswers, myConnection);

               myConnection.Open();
               myCommand.Parameters.AddWithValue("@answer_numeric", GetDataValue(answer_numeric));
               myCommand.Parameters.AddWithValue("@answer_text", GetDataValue(answer_text));
               myCommand.Parameters.AddWithValue("@answer_boolean", GetDataValue(answer_boolean));
               myCommand.Parameters.AddWithValue("@answer_resp_id", GetDataValue(answer_resp_id));
               myCommand.Parameters.AddWithValue("@answer_quest_id", GetDataValue(answer_quest_id));
               myCommand.Parameters.AddWithValue("@answer_group_option_id", GetDataValue(answer_group_option_id));


               int success = myCommand.ExecuteNonQuery();

               //if (success != 1)
               //{
               //    ("It didn't insert shit:" + query);
               //}

               if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

               //int? checkIfInsert = modified;

            }
        }
        
        // handling the "NULL" values to insert in the database
        public static object GetDataValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        // populate the objects from the data in the database
        private void PopulateMultiAnswer(SqlDataReader myReader)
        {
            if (myReader.Read())
            {
                int questSurveySequence = (myReader["quest_survey_sequence"] != DBNull.Value) ? Convert.ToInt32(myReader["quest_survey_sequence"]) : 0;
                currentQuestion.quest_description = myReader["quest_description"].ToString();

                if (questSurveySequence != 0)
                {
                    currentQuestion.quest_survey_sequence = questSurveySequence;
                }
                else
                {
                    currentQuestion.quest_survey_sequence = Int32.Parse(myReader["quest_child_sequence"].ToString());
                }

                currentSurvey.survey_description = myReader["survey_description"].ToString();
                currentQuestion.quest_answer_group_id = string.IsNullOrEmpty(myReader["quest_answer_group_id"].ToString()) ? 0 : int.Parse(myReader["quest_answer_group_id"].ToString());
                currentQuestion.quest_input_type_id = Int32.Parse(myReader["quest_input_type_id"].ToString());
                currentQuestion.quest_id = Int32.Parse(myReader["quest_id"].ToString());
                Session["current_question"] = currentQuestion.quest_id;
                Session["input_type"] = currentQuestion.quest_input_type_id;

                int inputType = currentQuestion.quest_input_type_id;
                int answerGroupId = currentQuestion.quest_answer_group_id;

                if (inputType == 1 || inputType == 2 || inputType == 18)
                {
                    FillCheckBox(answerGroupId, inputType);
                }
                else if (inputType == 10)
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
            lblSurveySession.Text = AppSession.SurveyId.ToString();
            //lblTestAnswerGroup.Text = currentQuestion.quest_answer_group_id.ToString();
        }

        private int CheckLastQuest(SqlDataReader myReader)
        {
            DataTable dt = new DataTable();
            dt.Load(myReader);
            int countQuestions = dt.Rows.Count;

            return countQuestions;
        }

        // fill the checkboxes, radiobuttons and dropdown lists with the answer options.
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

        // get the number of options of answers to use in the array object answer 
        private int GetNumOfOptions()
        {
            int countGroupOption;
            
            connectString();
            
            String queryCountGroupOption = @"select qt.quest_answer_group_id, 
		                                            qt.quest_id, 
		                                            qt.quest_multi_answer, 
		                                            qt.quest_description,
		                                            ago.answer_group_logical_answer,
		                                            ago.answer_group_option_desc		 
                                            from questions qt
                                            Join answer_group_option ago
                                            on qt.quest_answer_group_id = ago.answer_group_id
                                            where qt.quest_survey_id = " + AppSession.SurveyId;


            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryCountGroupOption, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader readerCountGroupOption = myCommand.ExecuteReader();

            DataTable dtCountGroupOption = new DataTable();

            dtCountGroupOption.Load(readerCountGroupOption);
            countGroupOption = dtCountGroupOption.Rows.Count;

            myConnection.Close();


            int countSingleAnswer;

            connectString();

            String queryCountSingleAnswer = @"select quest_answer_group_id, 
		                                            quest_id, 
		                                            quest_multi_answer, 
		                                            quest_description
		                                    from questions qt
                                            where quest_survey_id = " + AppSession.SurveyId + @" and
                                            quest_multi_answer = 0";


            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryCountSingleAnswer, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader readerCountSingleAnswer = myCommand.ExecuteReader();

            DataTable dtCountSingleAnswer = new DataTable();

            dtCountSingleAnswer.Load(readerCountSingleAnswer);
            countSingleAnswer = dtCountSingleAnswer.Rows.Count;

            myConnection.Close();


            int countTotalAnswer = countSingleAnswer + countGroupOption;

            return countTotalAnswer;
        }

        // clicking on the next button in the question
        protected void SaveAndNextQuest_Click(object sender, EventArgs e)
        {
            //Object to keep the current answers 
            Answer[] answerArray = new Answer[GetNumOfOptions()];
           
            //increasing one to follow the sequence of questions in the survey.
            //in case this is a child question will increase as well, but will be decreased in the function 
            int surveyTemp = Int32.Parse(Session["quest_survey_sequence"].ToString()) + 1;
            Session["quest_survey_sequence"] = surveyTemp;
                                                
            //int indexAnswerArray = GetIndexAnswerArray();
            bool answerGroupOptionChild = false;

            GetAnswersByInputType(answerGroupOptionChild, answerArray);
            
            Response.Redirect("~/Default.aspx");
            //GetQuestion(survId, survSequence);
        }

        // function for inserting values in the database that are with multiple values
        protected void InserMultipleAnswerQuestion(int? answer_option_group_id, int answer_quest_id, int answer_resp_id)
        {
            if (answer_option_group_id != 0)
            {
                connectString();

                String insertMultipleAnswerQuestion = @"insert into answers (  answer_resp_id,    
                                                                  answer_question_id,
                                                                  answer_group_option_id )   
                                                output inserted.answer_id values (" + answer_resp_id + ", " + answer_quest_id + ", " + answer_option_group_id + ");";


                //get the sql script executing on the connection
                myCommand = new SqlCommand(insertMultipleAnswerQuestion, myConnection);

                //open connectio
                myConnection.Open();

                int? modified = (int?)myCommand.ExecuteScalar();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                int? checkIfInsert = modified;
            }
         
        }

        protected void InsertNumericAnswer(int? answer_numeric, int answer_quest_id, int answer_resp_id)
        {
            if (answer_numeric != null)
            {
                connectString();

                String insertMultipleAnswerQuestion = @"insert into answers (  answer_resp_id,    
                                                                  answer_question_id,
                                                                  answer_numeric )   
                                                output inserted.answer_id values (" + answer_resp_id + ", " + answer_quest_id + ", " + answer_numeric + ");";


                //get the sql script executing on the connection
                myCommand = new SqlCommand(insertMultipleAnswerQuestion, myConnection);

                //open connectio
                myConnection.Open();

                int? modified = (int?)myCommand.ExecuteScalar();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                int? checkIfInsert = modified;
            }

        }

        protected void InsertTextAnswer(string answer_text, int answer_quest_id, int answer_resp_id)
        {
            if (answer_text != "")
            {
                connectString();

                String insertTextAnswer = @"insert into answers (  answer_resp_id,    
                                                                  answer_question_id,
                                                                  answer_text )   
                                                output inserted.answer_id values (" + answer_resp_id + ", " + answer_quest_id + ", '" + answer_text + "');";


                //get the sql script executing on the connection
                myCommand = new SqlCommand(insertTextAnswer, myConnection);

                //open connectio
                myConnection.Open();

                //int? modified = (int?)myCommand.ExecuteScalar();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                //int? checkIfInsert = modified;
            }

        }

        /* getting the answer depending on the input type
         input types treated RadioButton, CheckBox, TextBox, DropDownList, NumericAnswer, */
        public void GetAnswersByInputType(bool answerGroupOptionChild, Answer[] answerArray) 
        {
            int inputType = Int32.Parse(Session["input_type"].ToString());
            Answer insertAnswer = new Answer();

            if (AppSession.AnswerList == null)
            {
                AppSession.AnswerList = new List<Answer>();
                AppSession.IndexAnswer = new int();
            }
            
            // if the input type is CHECKBOX
            if (inputType == 1)
            {

                foreach (ListItem myList in cbxAnswerGroupOpt.Items)
                {
                    if (myList.Selected)
                    {
                        insertAnswer.answer_group_option_id = Int32.Parse(myList.Value.ToString());
                        insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                        //insertAnswer.answer_resp_id = Int32.Parse(AppSession.RespondentId.ToString());
                        AppSession.IndexAnswer++;

                        //int indexAnswerList = Int32.Parse(AppSession.IndexAnswer.ToString());

                        AppSession.AnswerList.Add(insertAnswer);
                        //InserMultipleAnswerQuestion(Int32.Parse(insertAnswer.answer_group_option_id.ToString()), Int32.Parse(insertAnswer.answer_question_id.ToString()), Int32.Parse(insertAnswer.answer_resp_id.ToString()));
                        
                        int answerGroupOptionId = Int32.Parse(myList.Value.ToString());

                        if (GetLogicalAnswer(answerGroupOptionId))
                        {
                            answerGroupOptionChild = true;
                        }
                    }
                }
            }
            
            // if the input type is RADIOBUTTON
            else if (inputType == 2)
            {
                insertAnswer = new Answer();
                insertAnswer.answer_group_option_id = string.IsNullOrEmpty(rdbAnswerGroupOpt.SelectedValue.ToString()) ? 0 : int.Parse(rdbAnswerGroupOpt.SelectedValue.ToString());
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                //insertAnswer.answer_resp_id = Int32.Parse(AppSession.RespondentId.ToString());

                AppSession.IndexAnswer++;

                AppSession.AnswerList.Add(insertAnswer);

                int? answerGroupOptionId = int.Parse(insertAnswer.answer_group_option_id.ToString());

                //InserMultipleAnswerQuestion(answerGroupOptionId, Int32.Parse(insertAnswer.answer_question_id.ToString()), Int32.Parse(insertAnswer.answer_resp_id.ToString()));

                
                if (!string.IsNullOrEmpty(rdbAnswerGroupOpt.SelectedValue.ToString())) 
                {
                    if (GetLogicalAnswer(answerGroupOptionId))
                    {
                        answerGroupOptionChild = true;
                    }
                }
            }
            
            // if the input type is TEXT BOX
            else if (inputType == 19)
            {
                insertAnswer = new Answer();
                insertAnswer.answer_text = textAreaBox.Text;
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                //insertAnswer.answer_resp_id = Int32.Parse(AppSession.RespondentId.ToString());
               // InsertTextAnswer(insertAnswer.answer_text, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);

                AppSession.IndexAnswer++;
                AppSession.AnswerList.Add(insertAnswer);

            }
            
            // if the input type is an email, telephone, text, url, or any type that is a string with multiple characters
            else if(inputType == 3 || inputType == 8 || inputType == 13 || inputType == 15 || inputType == 17)
            {
                insertAnswer = new Answer();
                insertAnswer.answer_text = textBox.Text;
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                //insertAnswer.answer_resp_id = Int32.Parse(AppSession.RespondentId.ToString());
               // InsertTextAnswer(insertAnswer.answer_text, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);

                AppSession.IndexAnswer++;
                AppSession.AnswerList.Add(insertAnswer);

            }

            // // if the input type is a NUMERIC TEXTBOX
            else if (inputType == 10) 
            {
                insertAnswer = new Answer();
                insertAnswer.answer_numeric = string.IsNullOrEmpty(numberBox.Text.ToString()) ? 0 : Int32.Parse(numberBox.Text.ToString());
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
               // insertAnswer.answer_resp_id = Int32.Parse(AppSession.RespondentId.ToString());
               // InsertNumericAnswer(insertAnswer.answer_numeric, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);

                AppSession.IndexAnswer++;
                AppSession.AnswerList.Add(insertAnswer);
            }
            
            // if the input type is DROPDOWNLIST
            else if (inputType == 18)
            {
                insertAnswer.answer_group_option_id = string.IsNullOrEmpty(ddAnswerGroupOpt.SelectedValue.ToString()) ? 0 : Int32.Parse(ddAnswerGroupOpt.SelectedValue.ToString());
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                int? answerGroupOptionId = int.Parse(insertAnswer.answer_group_option_id.ToString());
              
                AppSession.IndexAnswer++;
                AppSession.AnswerList.Add(insertAnswer);
                
                // check if the question has a child
                if (!string.IsNullOrEmpty(rdbAnswerGroupOpt.SelectedValue.ToString()))
                {
                    if (GetLogicalAnswer(answerGroupOptionId))
                    {
                        answerGroupOptionChild = true;
                        
                    }
                }
            }

            // adding the values collected in the answer array to the session
            Session["answer_group_option_child"] = answerGroupOptionChild;
            
        }

         //get the Index for adding the values for the answer array 
        /*public int GetIndexAnswerArray()
        {
            int indexAnswerArray;

            if (string.IsNullOrEmpty(Session["array_answer_index"] as string))
            {
                indexAnswerArray = 0;
            }
            else
            {
                indexAnswerArray = Int32.Parse(Session["array_answer_index"].ToString());
            }

            return indexAnswerArray;
        }*/
        
        // check if the answer has a child question
        private bool GetLogicalAnswer(int? answerGroupOptionChild)
        {
            connectString();

            String queryAnswerLogical = @"Select answer_group_logical_answer, answer_group_option_id from answer_group_option
                                        where answer_group_option_id = " + answerGroupOptionChild;


            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryAnswerLogical, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader readerCountGroupOption = myCommand.ExecuteReader();

            Boolean questionChild;

            if (readerCountGroupOption.Read())
            {
                questionChild = bool.Parse(readerCountGroupOption["answer_group_logical_answer"].ToString());
            }
            else
            {
                questionChild = false;
            }


            myConnection.Close();

            if (questionChild)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected int InsertUser(string username, string user_fname, string user_lname, string user_password, string user_pref_phone, DateTime user_dob, string user_role)
        {
            int user_id = 0;

            String insertUser = @"insert into users (username, user_fname, user_lname, user_password, user_role, user_pref_phone, user_dob)   
                                    output inserted.user_id values (@username, @user_fname, @user_lname, @user_password, @user_role, @user_pref_phone, @user_dob);";

            myCommand = new SqlCommand(insertUser, myConnection);

            myConnection.Open();
            myCommand.Parameters.AddWithValue("@username", GetDataValue(username));
            myCommand.Parameters.AddWithValue("@user_fname", GetDataValue(user_fname));
            myCommand.Parameters.AddWithValue("@user_lname", GetDataValue(user_lname));
            myCommand.Parameters.AddWithValue("@user_password", GetDataValue(user_password));
            myCommand.Parameters.AddWithValue("@user_role", GetDataValue(user_role));
            myCommand.Parameters.AddWithValue("@user_pref_phone", GetDataValue(user_pref_phone));
            myCommand.Parameters.AddWithValue("@user_dob", GetDataValue(user_dob));

            user_id = (int)myCommand.ExecuteScalar();

            if (myConnection.State == System.Data.ConnectionState.Open)
                myConnection.Close();

            return user_id;
        }

        protected string TypedPassword
        {
            get
            {
                if (ViewState["TypedPassword"] != null)
                {
                    return Convert.ToString(ViewState["TypedPassword"]);
                }
                return null;
            }
            set
            {
                ViewState["TypedPassword"] = value;
            }
        }
        
        protected void RespondentRegistration_Click(object sender, EventArgs e)
        {
            connectString();
            
            currentUser.username = tbxEmail.Text.ToString();
            currentUser.user_fname = tbxFirstName.Text.ToString();
            currentUser.user_lname = tbxLastName.Text.ToString();
            currentUser.user_dob = DateTime.Parse(tbxDateOfBirth.Text.ToString());
            currentUser.user_role = "respondent";
            currentUser.user_pref_phone = tbxPhone.Text.ToString();
            currentUser.user_password = currentUser.EncodePasswordToBase64(tbxPasswordRegistration.Text);
            currentRespondent.resp_ip = GetIPAddress();
            currentRespondent.resp_survey_id = Int32.Parse(AppSession.SurveyId.ToString());
            currentRespondent.resp_gender = Int32.Parse(ddRespGender.SelectedValue.ToString());
            currentRespondent.resp_state_territory = Int32.Parse(ddState.SelectedValue.ToString());
            currentRespondent.resp_email = tbxEmail.Text.ToString();
            currentRespondent.resp_home_suburb = tbxSuburbHome.Text.ToString();
            currentRespondent.resp_work_suburb = tbxSuburbWork.Text.ToString();
            currentRespondent.res_home_post_code = Int32.Parse(tbxPostCodeHome.Text.ToString());
            currentRespondent.resp_work_post_code = Int32.Parse(tbxPostCodeWork.Text.ToString());
            currentRespondent.resp_user_id = Int32.Parse(InsertUser(currentUser.username, currentUser.user_fname, currentUser.user_lname, currentUser.user_password, currentUser.user_pref_phone, currentUser.user_dob, currentUser.user_role).ToString());


            String insertRespondent = @"insert into respondents ( resp_gender, resp_state_territory, resp_email, resp_home_suburb, res_home_post_code, resp_work_post_code, resp_work_suburb, resp_IP, resp_user_id, resp_date, resp_survey_id)   
                                    output inserted.resp_id values (@resp_gender, @resp_state_territory, @resp_email, @resp_home_suburb, @res_home_post_code, @resp_work_post_code, @resp_work_suburb, @resp_IP, @resp_user_id, getdate(), @resp_survey_id);";


            myCommand = new SqlCommand(insertRespondent, myConnection);

            myConnection.Open();
            myCommand.Parameters.AddWithValue("@resp_gender", GetDataValue(currentRespondent.resp_gender));
            myCommand.Parameters.AddWithValue("@resp_state_territory", GetDataValue(currentRespondent.resp_state_territory));
            myCommand.Parameters.AddWithValue("@resp_email", GetDataValue(currentRespondent.resp_email));
            myCommand.Parameters.AddWithValue("@resp_home_suburb", GetDataValue(currentRespondent.resp_home_suburb));
            myCommand.Parameters.AddWithValue("@res_home_post_code", GetDataValue(currentRespondent.res_home_post_code));
            myCommand.Parameters.AddWithValue("@resp_work_post_code", GetDataValue(currentRespondent.resp_work_post_code));
            myCommand.Parameters.AddWithValue("@resp_work_suburb", GetDataValue(currentRespondent.resp_work_suburb));
            myCommand.Parameters.AddWithValue("@resp_IP", GetDataValue(currentRespondent.resp_ip));
            myCommand.Parameters.AddWithValue("@resp_user_id", GetDataValue(currentRespondent.resp_user_id));
            myCommand.Parameters.AddWithValue("@resp_survey_id", GetDataValue(currentRespondent.resp_survey_id));

            ////get the sql script executing on the connection
            //myCommand = new SqlCommand(insertAnonymousResp, myConnection);

            ////open connectio
            //myConnection.Open();

            int? modified = (int?)myCommand.ExecuteScalar();

            if (myConnection.State == System.Data.ConnectionState.Open)
                myConnection.Close();

            AppSession.RespondentId = modified;

            InsertQuestsToDatabase(AppSession.RespondentId);

            //surveyId = Int32.Parse(AppSession.SurveyId.ToString());
            MultiViewMainPage.ActiveViewIndex = 3;
        }

        //Skiping the registration from the respondent
        protected void SkipRegistration_click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(AppSession.SurveyId as string))
            //{
            
                connectString();
            
                string ipAddres = GetIPAddress();
                int surveyId = Int32.Parse(AppSession.SurveyId.ToString());

                String insertAnonymousResp = @"insert into respondents ( resp_gender,     
                                                                         resp_state_territory,    
                                                                         resp_email,
                                                                         resp_home_suburb,    
                                                                         res_home_post_code,    
                                                                         resp_work_post_code,    
                                                                         resp_work_suburb,      
                                                                         resp_IP,    
                                                                         resp_user_id,
                                                                         resp_date,    
                                                                         resp_survey_id)   
                                                output inserted.resp_id values ('', '', 'anonymous@anonymous.com', '', 0, 0, '', '" + ipAddres + "', 5, getdate(), " + surveyId  + ");";

            
                //get the sql script executing on the connection
                myCommand = new SqlCommand(insertAnonymousResp, myConnection);

                //open connectio
                myConnection.Open();

                int? modified = (int?)myCommand.ExecuteScalar();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                AppSession.RespondentId = modified;

                InsertQuestsToDatabase(AppSession.RespondentId);
                                
                //surveyId = Int32.Parse(AppSession.SurveyId.ToString());
                MultiViewMainPage.ActiveViewIndex = 3;

            //}
            //else
            //{
            //    MultiViewMainPage.ActiveViewIndex = 0;
            //}


        }

        //Go back previous question COMMENTED
        protected void PreviousQuestion_Click(object sender, EventArgs e)
        {

            if (bool.Parse(Session["answer_group_option_child"].ToString()))
            {
                Session["answer_group_option_child"] = false;
            }
            else
            {
                int surveyTemp = Int32.Parse(Session["quest_survey_sequence"].ToString()) - 1;
                Session["quest_survey_sequence"] = surveyTemp;
            }


            Response.Redirect("~/Default.aspx");
        }

        protected void btnStaffSession_Click(object sender, EventArgs e)
        {
            AppSession.StaffSession = true;
            
            Response.Redirect("~/Default.aspx");
        }

        protected void btnBackToSurvey_click(object sender, EventArgs e)
        {
            AppSession.StaffSession = false;

            Response.Redirect("~/Default.aspx");
        }

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {
            staffCurrentUser.username = tbxStaffUsername.Text;
            staffCurrentUser.user_password = tbxStaffPassword.Text;

            if (staffCurrentUser.StaffLoginCheckUser())
            {

                if (staffCurrentUser.StaffLogin())
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Please verify if the username and password, are correct!');</script>");
                }
                
            }
            else
            {
                Response.Write("<script language=javascript>alert('Please verify if the username and password, are correct!');</script>");
            }


        }

        protected void btnStaffLogout_Click(object sender, EventArgs e)
        {
            staffCurrentUser.StaffLogout();

            Response.Redirect("~/Default.aspx");
        }

         
    }
}