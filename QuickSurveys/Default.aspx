﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuickSurveys.Default" %>
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

        
        <asp:Label ID="lblQuestSurveySequence" runat="server" Text="-" CssClass="h4"></asp:Label>
        <asp:Label ID="lblQuestionDesc" runat="server" Text="-" CssClass="h4"></asp:Label>
        <asp:Label ID="lblInputType" runat="server" Text="-" ></asp:Label><br /> <br />
                
        <asp:CheckBoxList runat="server" ID="cbxAnswerGroupOpt">
        </asp:CheckBoxList>

        <asp:RadioButtonList ID="rdbAnswerGroupOpt" runat="server">
        </asp:RadioButtonList>

        <asp:DropDownList ID="ddAnswerGroupOpt" runat="server">
        </asp:DropDownList>

        <asp:GridView ID="GridView1" runat="server"  CssClass="table table-hover table-striped">
        </asp:GridView>
    </div>
</asp:Content>
