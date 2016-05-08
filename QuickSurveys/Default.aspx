<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuickSurveys.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Label ID="lblSurveyDesc" runat="server" Text="Label" 
            CssClass="h2"></asp:Label>
        
        <br /> 
        <br />
        
   
        <asp:GridView ID="GridView2" runat="server">
           
        </asp:GridView>

       

        <asp:MultiView ID="MultiView1" runat="server">

            <asp:View ID="View1" runat="server" EnableViewState="true" ViewStateMode="Enabled">
             <%-- START SURVEYS BLOCK --%>
                
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
                </asp:DetailsView>

             <%-- END SURVEYS BLOCK --%>
            </asp:View>

        </asp:MultiView>

        


        <%-- START QUESTIONS BLOCK --%>
        
        <asp:Label ID="lblQuestSurveySequence" runat="server" Text="-" CssClass="h4"></asp:Label>
        <asp:Label ID="lblQuestionDesc" runat="server" Text="-" CssClass="h4"></asp:Label>
        <%--<asp:Label ID="lblInputType" runat="server" Text="-" ></asp:Label><br /> <br />--%>
        <%--<asp:Label ID="lblTestAnswerGroup" runat="server" Text="-" ></asp:Label><br /> <br />--%>
        <br /><br />
                
        <asp:CheckBoxList runat="server" ID="cbxAnswerGroupOpt">
        </asp:CheckBoxList>

        <asp:RadioButtonList ID="rdbAnswerGroupOpt" runat="server">
        </asp:RadioButtonList>

        <asp:DropDownList ID="ddAnswerGroupOpt" runat="server" Visible="false" >
        </asp:DropDownList>
        
        <asp:TextBox ID="numberBox" runat="server" Visible="False" MaxLength="4" TextMode="Number"></asp:TextBox>

        <asp:TextBox ID="textAreaBox" runat="server" Visible="False" MaxLength="350" TextMode="MultiLine" Width="350"></asp:TextBox>

        <asp:TextBox ID="textBox" runat="server" Visible="False" MaxLength="144" TextMode="SingleLine"></asp:TextBox>

        <br /><br />

        <asp:Button ID="btnSubmit" runat="server" Text="Next Question >>" CssClass="btn btn-success"/>

        <%-- END QUESTIONS BLOCK --%>

        <asp:GridView ID="GridView1" runat="server"  CssClass="table table-hover table-striped">
        </asp:GridView>
    </div>
</asp:Content>
