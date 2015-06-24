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

                                        <h2>קורס  <% =courseName %></h2>
                                        <br />
                                        <br />
                                        <br />
                                        <h2 id="QuestionnaireTitle" ><% =questionName %></h2>
                                        
                                        <br />
                                        <br />
                                        <br />
                                    </div>
                                </li>


                                <li>

                                    <asp:Label runat="server" ID="questionTitle" Font-Size="Medium"></asp:Label>
                                </li>
                                <% if(listQuestion.Count != 0)
                                           { %>
                                <li>
                                    <div>
                                        

                                        <label id="questionText"><% =listQuestion[indexQuestion].getQuestion() %></label>
                                        
                                    </div>
                                    <%--<asp:Label  ></asp:Label>--%>
                                    </li>
                            </ul>


                            <div id="Americananswer" runat="server" class="answer" style="display: none">
                                <ul>
                                    <%  
                                        if (listQuestion != null && listQuestion.Count != 0)
                                        {

                                            listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[indexQuestion].getId());
                                            numAns.Value = listAnswer.Count.ToString();

                                            if (listQuestion[indexQuestion]._Type == 2)//yes no question like american question
                                            {
                                                Response.Write("<script>document.getElementById('MainContent_Americananswer').style.display = 'inline';</script>");
                                                //yesNoDiv.Style.Add("display", "inline");
                                            }
                                            else if (listQuestion[indexQuestion]._Type == 3)//open question
                                            {
                                                listAnswer = null;
                                                Response.Write("<script>document.getElementById('MainContent_OpenDiv').style.display = 'inline';</script>");
                                                //OpenDiv.Style.Add("display", "inline");
                                            }
                                            else if (listQuestion[indexQuestion]._Type == 1)// american question
                                            {

                                                Response.Write("<script>document.getElementById('MainContent_Americananswer').style.display = 'inline';</script>");
                                                //Americananswer.Style.Add("display", "inline");
                                            }

                                        }
                                    %>
                                    <li>
                                    <input type="text" id="numAns" style="display: none" runat="server" value="0" />
                                        </li>


                                    <%
                    if (listAnswer.Count > 0)
                    { %>
                                    <li>


                                        <label id="answerText1" class="Question"><%= listAnswer[0].getAnswer().ToString() %></label>
                                        <input id="checkbox1" type="checkbox" name="Gender" onclick="cleanCheckBox1()" style="display: inline" />
                                    </li>
                                    <%}
                                        if (listAnswer.Count > 1)
                                        {%>
                                    <li>


                                        <label id="answerText2" class="Question"><%= listAnswer[1].getAnswer().ToString() %></label>
                                        <input id="checkbox2" type="checkbox" name="Gender" onclick="cleanCheckBox2()" style="display: inline" />
                                    </li>
                                    <%}
                                      if (listAnswer.Count > 2)
                                      {%>
                                    <li>


                                        <label id="answerText3" class="Question"><%= listAnswer[2].getAnswer().ToString() %></label>
                                        <input id="checkbox3" type="checkbox" name="Gender" onclick="cleanCheckBox3()" style="display: inline" />
                                    </li>
                                    <%}
                                      if (listAnswer.Count > 3)
                                      {%>
                                    <li>


                                        <label id="answerText4" class="Question"><%= listAnswer[3].getAnswer().ToString() %></label>
                                        <input id="checkbox4" type="checkbox" name="Gender" onclick="cleanCheckBox4()" style="display: inline" />
                                    </li>
                                    <%}
                                      if (listAnswer.Count > 4)
                                      {%>
                                    <li>


                                        <label id="answerText5" class="Question"><%= listAnswer[4].getAnswer().ToString() %></label>
                                        <input id="checkbox5" type="checkbox" name="Gender" onclick="cleanCheckBox5()" style="display: inline" />
                                    </li>
                                    <%}
                                      if (listAnswer.Count > 5)
                                      {%>
                                    <li>


                                        <label id="answerText6" class="Question"><%= listAnswer[5].getAnswer().ToString() %></label>
                                        <input id="checkbox6" type="checkbox" name="Gender" onclick="cleanCheckBox6()" style="display: inline" />
                                    </li>
                                    <%} %>
                                    <% 
               
                                 %>
                                </ul>
                            </div>

                           
                            <div id="OpenDiv" runat="server" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5 class="ansTytle">:תשובה</h5>
                                    </li>
                                    <li>

                                        <asp:TextBox ID="openAnswer" CssClass="Question" Columns="2" placeholder="הכנס תשובה" Width="500px" runat="server" />
                                        <%--<input type="text" name="checking" id="checking" onclick="checkTextField(this);" />--%>
                                    </li>
                                </ul>
                            </div>
                             <%} //listQuestin.count != 0 %>
                         
                             
                            <div id="sendDiv">
                                <input id="err" type="text" class="errMesegeAddQuest" style="display: none" />
                                <br />
                                <%--<asp:Button ID="nextQuestionBtn" runat="server" OnClick="displayNextQuestion" Width="120px" CssClass="myButton" Text="הבא" />--%>
                                <input type="button" id="nextQuestionBtn" runat="server" class="myButton" value="שאלה הבאה" style="display: none" />

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

