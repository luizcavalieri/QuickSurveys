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
              
                if (!string.IsNullOrEmpty(Session["survey_id"] as string))
                {
                    MultiViewMainPage.ActiveViewIndex = 2;
                    int survSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                    int survId = Int32.Parse(Session["survey_id"].ToString());
                    GetQuestion(survId, survSequence);
                    
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
            
            Session["survey_id"] = btn.CommandArgument.ToString();
            Session["quest_survey_sequence"] = 1;
            GetIPAddress();

            if (!string.IsNullOrEmpty(Session["survey_id"] as string))
            {
                MultiViewMainPage.ActiveViewIndex = 1;
                FillDropDownState();
                FillDropDownGender();

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
        private void GetQuestion(int surveyId, int questSequence)
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
                                             quest_survey_sequence = " +questSequence;


                //get the sql script executing on the connection
                myCommand = new SqlCommand(queryQuestionInputType, myConnection);

                //open connection
                myConnection.Open();

                //open the reader
                myReader = myCommand.ExecuteReader();

                
                // trying to redirect the page to the thank you page after finishing the survey
                if (CheckLastQuest(myReader) == 0)
                {
                    MultiViewMainPage.ActiveViewIndex = 3;
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
            lblSurveySession.Text = Session["survey_id"].ToString();
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
                                            where qt.quest_survey_id = " + Session["survey_id"];


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
                                            where quest_survey_id = " + Session["survey_id"] + @" and
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
                                                
            int indexAnswerArray = GetIndexAnswerArray();
            bool answerGroupOptionChild = false;

            GetAnswersByInputType(indexAnswerArray, answerGroupOptionChild, answerArray);
            
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
        public void GetAnswersByInputType(int indexAnswerArray, bool answerGroupOptionChild, Answer[] answerArray) 
        {
            
            int inputType = Int32.Parse(Session["input_type"].ToString());

            Answer[] answerArrayTemp = new Answer[GetNumOfOptions()];
            Answer insertAnswer = new Answer();

            answerArrayTemp = (Answer[])Session["answer_array"];


            // if the input type is CHECKBOX
            if (inputType == 1)
            {

                foreach (ListItem myList in cbxAnswerGroupOpt.Items)
                {
                    if (myList.Selected)
                    {
                        answerArray[indexAnswerArray] = new Answer();

                        insertAnswer.answer_group_option_id = Int32.Parse(myList.Value.ToString());
                        insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                        insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());
                        //indexAnswerArray++;
                        
                        InserMultipleAnswerQuestion(Int32.Parse(insertAnswer.answer_group_option_id.ToString()), Int32.Parse(insertAnswer.answer_question_id.ToString()), Int32.Parse(insertAnswer.answer_resp_id.ToString()));
                        
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
                insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());

                int? answerGroupOptionId = int.Parse(insertAnswer.answer_group_option_id.ToString());

                InserMultipleAnswerQuestion(answerGroupOptionId, Int32.Parse(insertAnswer.answer_question_id.ToString()), Int32.Parse(insertAnswer.answer_resp_id.ToString()));

                indexAnswerArray++;
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
                insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());
                InsertTextAnswer(insertAnswer.answer_text, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);
            }
            
            // if the input type is an email, telephone, text, url, or any type that is a string with multiple characters
            else if(inputType == 3 || inputType == 8 || inputType == 13 || inputType == 15 || inputType == 17)
            {
                insertAnswer = new Answer();
                insertAnswer.answer_text = textBox.Text;
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());
                InsertTextAnswer(insertAnswer.answer_text, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);
            }

            // // if the input type is a NUMERIC TEXTBOX
            else if (inputType == 10) 
            {
                insertAnswer = new Answer();
                insertAnswer.answer_numeric = string.IsNullOrEmpty(numberBox.Text.ToString()) ? 0 : Int32.Parse(numberBox.Text.ToString());
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());
                InsertNumericAnswer(insertAnswer.answer_numeric, insertAnswer.answer_question_id, insertAnswer.answer_resp_id);
            }
            
            // if the input type is DROPDOWNLIST
            else if (inputType == 18)
            {
                insertAnswer = new Answer();
                insertAnswer.answer_group_option_id = string.IsNullOrEmpty(ddAnswerGroupOpt.SelectedValue.ToString()) ? 0 : Int32.Parse(ddAnswerGroupOpt.SelectedValue.ToString());
                insertAnswer.answer_question_id = Int32.Parse(Session["current_question"].ToString());
                insertAnswer.answer_resp_id = Int32.Parse(Session["respondent_id"].ToString());
                int? answerGroupOptionId = int.Parse(insertAnswer.answer_group_option_id.ToString());
                
                InserMultipleAnswerQuestion(answerGroupOptionId, Int32.Parse(insertAnswer.answer_question_id.ToString()), Int32.Parse(insertAnswer.answer_resp_id.ToString()));
                
                indexAnswerArray++;
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
            Session["array_answer_index"] = indexAnswerArray;
            //Session["answer_array"] = answerArray;
        }

        // get the Index for adding the values for the answer array 
        public int GetIndexAnswerArray()
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
        }

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

        //Skiping the registration from the respondent
        protected void SkipRegistration_click(object sender, EventArgs e)
        {
            Session["answer_group_option_child"] = false;
            if (!string.IsNullOrEmpty(Session["survey_id"] as string))
            {
            
                connectString();
            
                string ipAddres = GetIPAddress();
                int surveyId = Int32.Parse(Session["survey_id"].ToString());

                String insertAnonymousResp = @"insert into respondents ( resp_gender,    
                                                                         resp_age_range,    
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
                                                output inserted.resp_id values ('', '', '', 'anonymous@anonymous.com', '', 0, 0, '', '" + ipAddres + "', 5, getdate(), " + surveyId  + ");";

            
                //get the sql script executing on the connection
                myCommand = new SqlCommand(insertAnonymousResp, myConnection);

                //open connectio
                myConnection.Open();

                int? modified = (int?)myCommand.ExecuteScalar();

                if (myConnection.State == System.Data.ConnectionState.Open)
                    myConnection.Close();

                Session["respondent_id"] = modified;

            
                //surveyId = Int32.Parse(Session["survey_id"].ToString());
                int questSequence = Int32.Parse(Session["quest_survey_sequence"].ToString());
                GetQuestion(surveyId, questSequence);
                MultiViewMainPage.ActiveViewIndex = 2;
            }
            else
            {
                MultiViewMainPage.ActiveViewIndex = 0;
            }


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

         
    }
}