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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            SqlCommand myCommand;
           
            String myConnectionString = ConfigurationManager.ConnectionStrings["MyConnection" ].ConnectionString;

            if (myConnectionString.Equals("prod" ))
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

            myConnection = new SqlConnection ();
            myConnection.ConnectionString = myConnectionString;

            int yourIndex = 10;

            String queryQuestionInputType = @"SELECT qt.quest_id, 
                                                     qt.quest_description, 
                                                     qt.quest_survey_id,
                                                     qt.quest_survey_sequence, 
                                                     qt.quest_input_type_id, 
                                                     qt.quest_answer_group_id, 
                                                     it.input_type_desc, 
                                                     srv.survey_description, 
                                                     ago.answer_group_option_desc
                                             FROM questions qt
                                             JOIN input_type it
                                             ON qt.quest_input_type_id = it.input_type_id
                                             Join surveys srv 
                                             ON qt.quest_survey_id = srv.survey_id
                                             Join answer_group_option ago
                                             on qt.quest_answer_group_id = ago.answer_group_id                                                                  
                                             where quest_id = " + yourIndex;

            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryQuestionInputType, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            // creating object from class question
            Question currentQuestion = new Question();
            Survey currentSurvey = new Survey();

            if (myReader.Read()) 
            {
                currentQuestion.quest_description = myReader["survey_description"].ToString();
                currentQuestion.quest_survey_sequence = Int32.Parse(myReader["quest_survey_sequence"].ToString());
                currentSurvey.survey_description = myReader["survey_description"].ToString();

            }

            lblQuestionDesc.Text = currentQuestion.quest_description;
            lblQuestSurveySequence.Text = currentQuestion.quest_survey_sequence.ToString();
            lblSurveyDesc.Text = currentSurvey.survey_description;
              






            


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