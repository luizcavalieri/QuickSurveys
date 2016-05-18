<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuickSurveys.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        
        <asp:MultiView ID="MultiViewMainPage" runat="server">

            <asp:View ID="SurveyBlockView" runat="server" EnableViewState="true" ViewStateMode="Enabled">
             <%-- START SURVEYS BLOCK --%>
             <h1> Choose Your Survey </h1>
                <asp:Repeater ID="RepeaterSurvey" runat="server" >
                    <ItemTemplate>
                            <%--<asp:LinkButton ID="ButtonSurvey" runat="server" Text='<%# Eval("survey_description") %>' CssClass="form-control btn btn-info btn-lg" OnClientClick='return SetSurveyClick(<%# Eval("survey_id") %>)' />--%>
                            <%--<asp:Button ID="SurveyButton" runat="server" Text='<%# Eval("survey_description") %>' onclick="SurveyButton_click" Font-Size="22px" CssClass="form-control btn btn-info btn-lg" ></asp:Button>--%>
                            <asp:Button ID="SurveyButton" runat="server" Text='<%# Eval("survey_description") %>' Font-Size="22px" CssClass="form-control btn btn-info btn-lg" onclick="SurveyButton_Click" CommandArgument='<%# Eval("survey_id") %>' CommandName='btn<%# Eval("survey_id") %>' />
                            
                            <%--<asp:PlaceHolder ID="suveyTbxHolder" runat="server"></asp:PlaceHolder>--%>
                            <%--<asp:TextBox ID="tbxSurveyId" runat="server" Text='<%# Eval("survey_id") %>' Visible="False" AutoPostBack="True"></asp:TextBox>--%>
                            <br / > <br />
                    </ItemTemplate>
                </asp:Repeater>


             <%-- END SURVEYS BLOCK --%>
            </asp:View>
            
            <asp:View ID="QuestionBlockView" runat="server">
            <%-- START QUESTIONS BLOCK --%>
                <div class="col-lg-5">
                <asp:Label ID="lblSurveyDesc" runat="server" Text="Label" CssClass="h2"></asp:Label>
                <asp:Label ID="lblSurveySession" runat="server" Text="Label" CssClass="h2"></asp:Label>
                <br /> <br />
                <asp:Label ID="lblQuestSurveySequence" runat="server" Text="-" CssClass="h4"></asp:Label>
                <asp:Label ID="lblQuestionDesc" runat="server" Text="-" CssClass="h4"></asp:Label>
                <asp:Label ID="lblipAddress" runat="server" Text="-" ></asp:Label><br /> <br />
                <%--<asp:Label ID="lblTestAnswerGroup" runat="server" Text="-" ></asp:Label><br /> <br />--%>
            
                <div style="display:block;">
                    <fieldset class="form-group"> 
                        <div class="checkbox">
                            <asp:CheckBoxList runat="server" ID="cbxAnswerGroupOpt" CssClass="" >
                            </asp:CheckBoxList>
                        </div>   
                    </fieldset>
                    <fieldset class="form-group"> 
                        <div class="radio">
                            <asp:RadioButtonList ID="rdbAnswerGroupOpt" runat="server" CssClass="">
                            </asp:RadioButtonList>
                        </div>
                    </fieldset>

                    <asp:DropDownList ID="ddAnswerGroupOpt" runat="server" Visible="false" CssClass="form-control">
                    </asp:DropDownList>
        
                    <asp:TextBox ID="numberBox" runat="server" Visible="False" MaxLength="4" TextMode="Number" CssClass="form-control"></asp:TextBox>

                    <asp:TextBox ID="textAreaBox" runat="server" Visible="False" MaxLength="350" TextMode="MultiLine" Width="445" CssClass="form-control"></asp:TextBox>

                    <asp:TextBox ID="textBox" runat="server" Visible="False" MaxLength="144" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                </div>
                <br />
                <div class="col-xs-5 ">
                    <asp:Button ID="btnBack" runat="server" Text="<< Previous" 
                        CssClass="btn btn-danger form-control" onclick="PreviousQuestion_Click"/>
                </div>
                <div class="col-xs-7 ">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save and Next >>" CssClass="btn btn-success form-control" onclick="SaveAndNextQuest_Click"/>
                </div>
            
            </div>

            <%-- END QUESTIONS BLOCK --%>

            </asp:View>

            <asp:View ID="ThankYouView" runat="server">
                <div class="row">
                    <h3>Register</h3>
                    <br />
                    <h4 style="color: #999999">Personal Information</h4>
                    <div class="col-xs-3 form-group">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="tbxFirstName" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3 form-group">
                        <asp:Label ID="lblLastName" runat="server" Text="First Name" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="tbxLastName" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3 form-group">
                        <asp:Label ID="Label1" runat="server" Text="Date of Birth" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="tbxDateOfBirth" runat="server" TextMode="SingleLine" CssClass="form-control date"></asp:TextBox>
                        <%--<input type="text" id="datepicker" class="form-control date" />--%>
                    </div>
                </div>
                <div class="row">
                <h4 style="color: #999999">Contact</h4>
                    <div class="col-xs-3 form-group">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" Font-Bold="True" ></asp:Label>
                        <asp:TextBox ID="txbEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3 form-group">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="tbxPhone" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3 form-group">
                        <%--<asp:Label ID="Label4" runat="server" Text="Date of Birth" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>--%>
                    </div>
                </div>
                
            </asp:View>
        </asp:MultiView>
   
    </div>
   
</asp:Content>
