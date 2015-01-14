<%@ Page Title="מאגר שאלונים" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="StockQuestionnaires.aspx.cs" Inherits="WebApplication1.Pages.c" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">

    <form class="form" runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data">
        <!-- content -->
        <section class="contentClass" id="content">
            

                <div class="padding">
                <div class="indentStock" id="stockQuestionnaire" style="display: inline" runat="server">
                    <h3 class="color-4 p2">מאגר שאלונים: <% =getCourseName() %></h3>
                    <div class="QuestionnaireIndent">
                        <input type="text" id="QuestionnaireId" style="display: none" runat="server" value="0" />
                        <ul class="Questionnaire">
                        
                            <%  for (int i = 0; i < listQuestionnaire.Count; i++)
                                {
                                    NameQuestionnaire.Text = listQuestionnaire[i].getName().Trim();
                                        %>
                            <li id='<% =listQuestionnaire[i].getId() %>'>
                                <div class="liDive">
                                    <input id="classDisplayBtn" type="button" class="buttonStock" value="הצג לכיתה" />
                                    <input id="staticBtn" type="button" class="buttonStock" value="סטטיסטיקה" />
                                    <asp:Button runat="server" id="NameQuestionnaire" OnClientClick="setQuestionnaireId($(this).last().parent().prop('id'));" OnClick ="onClick_Questionnaire" Text=""></asp:Button>
                                </div>
                            </li>

                            <%}%>
                        </ul>
                    </div>
                    <button>הוסף קורס</button>
                    <button>הסר קורס</button>
                </div>
            </div>

            <div class="padding2">
                <div class="indentStock" id="StockQuestion" style="display: none" runat="server">
                    <div class="QuestenClass">
                        <h3 class="color-4 p2">מאגר שאלות: <% =getCourseName() %> </h3>
                        <div class="QuestionnaireIndent">
                            <input type="text" id="QuestionId" style="display: none" runat="server" value="0" />
                            <ul class="Questionnaire">
                                <%  for (int j = 0; j < listQuestion.Count; j++)
                                    {
                                        NameQuestion.Text = listQuestion[j].getQuestion();
                                        %>
                                <li id="Question"+'<% =listQuestion[j].getId() %>'>
                                    <div class="liDive">

                                        <asp:Button runat="server" id="NameQuestion" OnClientClick="setQuestionId($(this).last().parent().prop('id'));"  Text=""></asp:Button>
                                    </div>
                                </li>

                                <%}%>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div id="fade" class="black_overlay" onclick="closeStockQuestion()" runat="server"></div>

        </section>
    </form>
</asp:Content>

