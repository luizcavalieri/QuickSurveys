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
           // SqlCommand myCommand2;

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

            int yourIndex = 10;

            String queryQuestionById = "Select * from questions where quest_id =" + yourIndex;

            String queryQuestionInputType = @"SELECT qt.quest_id, qt.quest_description, qt.quest_survey_id, qt.quest_survey_sequence, qt.quest_input_type_id, it.input_type_desc, srv.survey_description
                                             FROM questions qt
                                             JOIN input_type it
                                             ON qt.quest_input_type_id = it.input_type_id
                                             Join surveys srv
                                             ON qt.quest_survey_id = srv.survey_id
                                             where quest_id = " + yourIndex;

            //get the sql script executing on the connection
            myCommand = new SqlCommand(queryQuestionInputType, myConnection);

            //open connectio
            myConnection.Open();

            //open the reader
            SqlDataReader myReader = myCommand.ExecuteReader();

            //for working with DataTable I need to import library 'using System.Data;'
            DataTable dt = new DataTable();



            dt.Columns.Add("Question", System.Type.GetType("System.String"));
            dt.Columns.Add("Question Id", System.Type.GetType("System.String"));
            dt.Columns.Add("Input Type", System.Type.GetType("System.String"));
            dt.Columns.Add("Survey Sequence", System.Type.GetType("System.String"));
            dt.Columns.Add("Survey Number", System.Type.GetType("System.String"));
            dt.Columns.Add("Input Type Desc", System.Type.GetType("System.String"));
            dt.Columns.Add("Survey", System.Type.GetType("System.String"));
            dt.Columns.Add("Question Survey Sequence", System.Type.GetType("System.String"));


            DataRow row;

            while (myReader.Read())
            {
                row = dt.NewRow();
                row["Question"] = myReader["quest_description"].ToString();
                row["Question Id"] = myReader["quest_id"].ToString();
                row["Input Type"] = myReader["quest_input_type_id"].ToString();
                row["Survey Sequence"] = myReader["quest_survey_sequence"].ToString();
                row["Survey Number"] = myReader["quest_survey_sequence"].ToString();
                row["Input Type Desc"] = myReader["input_type_desc"].ToString();
                row["Survey"] = myReader["survey_description"].ToString();
                row["Question Survey Sequence"] = myReader["quest_survey_sequence"].ToString();

                dt.Rows.Add(row);

            }


            lblSurveyDesc.Text = dt.Rows[0]["Survey"].ToString();
            lblQuestSurveySequence.Text = dt.Rows[0]["Question Survey Sequence"].ToString();
            lblQuestionDesc.Text = dt.Rows[0]["Question"].ToString();
            lblInputType.Text = dt.Rows[0]["Input Type"].ToString();


            GridView1.DataSource = dt;
            GridView1.DataBind();

            myConnection.Close();


        }

        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                //Image img = new Image();

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

                e.Row.Cells[1].Controls.Add(ddl);
            }
        }
    }
}