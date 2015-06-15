<%@ Page Title="הצגת שאלון" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/SiteStudent.Master" CodeBehind="ShowQuestionnaire.aspx.cs" Inherits="Clicker.Pages.ShowQuestionnaire" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">
    <form runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data">
        <section id="content">
            <div class="padding">
                <div class="wrapper margin-bot">
                    <div class="col-3">
                        <div id="buttonAddRemove">
                            <ul>
                                <li>
                                    <div class="indent">
                                        
                                        <h2 class="p0"> קורס  <% =courseName %></h2>
                                        <br />
                                        <br />
                                        <br />
                                        <h2 id="QuestionnaireTitle" > <% =questionName %> שאלון</h2>
                                        <br />
                                <br />
                                <br />
                                    </div>
                                </li>
                                

                                <li>
             
                                    <asp:Label runat="server" id="questionTitle" Font-Size="Medium"></asp:Label>
                                </li>

                            </ul>
                            <div id="Americananswer" runat="server" class="answer" style="display: none">
                                <ul>
                              <%  
                                  if (listDisplay != null && listDisplay.Count != 0)
                {
                                if (listQuestion[indexQuestion]._Type == 2)//yes no question like american question
                                    {
                                        
                                        Response.Write("<script>document.getElementById('MainContent_Americananswer').style.display = 'inline';</script>");
                                        //yesNoDiv.Style.Add("display", "inline");
                                    }
                                  else if (listQuestion[indexQuestion]._Type == 3)//open question
                                    {
                                       Response.Write("<script>document.getElementById('MainContent_OpenDiv').style.display = 'inline';</script>");
                                       //OpenDiv.Style.Add("display", "inline");
                                    }
                                    else// american question
                                    {
                                        Response.Write("<script>document.getElementById('MainContent_Americananswer').style.display = 'inline';</script>");
                                       //Americananswer.Style.Add("display", "inline");
                                    }

                                  listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[indexQuestion].getId());
                                                                   
%>
                                    <li>
                                        <div id="questionText"><% =listQuestion[indexQuestion].getQuestion() %></div>
                                    </li>

                                      <%
                                    for (int j = 0; j < listAnswer.Count; j++)
                                    {
                                        answerText.Text = listAnswer[j].getAnswer(); 
                                           %>
                                    <li>
                                        
                                         <asp:Label runat="server" id="answerText" Text="" class="Question"></asp:Label>
                                          
                                          <input id="check"+'<% =listQuestion[indexQuestion].getId() %>' type="checkbox"  name="Gender" onclick="cleanCheck()"  />
                                    </li>
                                   <% }
                                      }
                                 %>
                                 
                                </ul>
                            </div>


                            <div id="yesNoDiv" runat="server" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5 class="ansTytle">:תשובות</h5>
                                    </li>
                                    <li>

                                        <%--<asp:TextBox ID="NoTxtBoxId" CssClass="Question" Columns="2" Text="YES" Width="500px" runat="server" />
                                        <input id="checkNo" class="check1" type="checkbox" name="check" value="check1"/>--%>
                                        <label for="c1">לא</label>
                                        <input id="CheckNo" class="check1"  type="checkbox" onclick="cleanCheckNo()"  />


                                    </li>
                                    <li>
                                        <label for="c1">כן</label>
                                        <input id="CheckYes" class="check1"  type="checkbox" onclick="cleanCheckYes()"  />
                                    </li>
                                </ul>
                            </div>

                            <div id="OpenDiv" runat="server" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5 class="ansTytle">:תשובה</h5>
                                    </li>
                                    <li>

                                        <asp:TextBox ID="openAnswerID" CssClass="Question" Columns="2" placeholder="הכנס תשובה" Width="500px" runat="server" />

                                    </li>
                                </ul>
                            </div>
                            <div id="sendDiv">
                                 <input id="err"  type="text" class="errMesegeAddQuest" style="display: none"  />
                                <br />
                                <%--<asp:Button ID="nextQuestionBtn" runat="server" OnClick="displayNextQuestion" Width="120px" CssClass="myButton" Text="הבא" />--%>
                                <input type="button" id="nextQuestionBtn" runat="server" class="myButton" value="שאלה הבאה" />
                               
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="block-news" id="conectedUser" runat="server">
                            <h3 class="color-4 p2">:אתה מחובר כ</h3>
                            <br />
                            <br />
                            <h3 class="color-4 p2">
                                <label id="UserNameLabel" runat="server"></label>
                            </h3>
                            <br />
                            <br />
                            <ul class="list-2">
                                <li>
                                    <div id="profile">
                                        <asp:Image runat="server" ID="userImage" CssClass="userImage" />
                                    </div>
                                </li>
                                <li>
                                    <asp:Button ID="logoutBtn" runat="server" OnClick="logout_click" CssClass="myButton" Text="התנתק"></asp:Button>
                                </li>

                            </ul>

                        </div>

                    </div>
                </div>




            </div>
        </section>


    </form>

</asp:Content>

