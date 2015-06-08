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
                                        
                                        <h2 class="p0">קורס <% =courseName %></h2>
                                        <br />
                                        <br />
                                        <br />
                                        <h2 id="QuestionnaireTitle" >שאלון <% =courseName %></h2>

                                    </div>
                                </li>
<%--                                <br />
                                <br />
                                <br />--%>

                                <li>
                                    <input runat="server" type="text" id="questTitle" value=""/>
                                </li>

                            </ul>
                            <div id="Americananswer" runat="server" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5 class="ansTytle">:תשובות</h5>
                                    </li>
                                    <%for (int i = 0; i < listQuestion.Count; i++)
                                      {
%>
                                      
                                    <li>
                                          <input type="text"  id='<% =listQuestion[i].getId() %>' class="Question"/>
                                          <input id="check"+'<% =listQuestion[i].getId() %>' type="checkbox"  name="Gender" onclick="cleanCheck1()"  />
                                    </li>
                                   <% } %>
                                    <li>
                                          <input type="text"   id="answer2" placeholder="הכנס תשובה" class="Question"/>
                                        <input id="check2" type="checkbox" onclick="cleanCheck2()"  />
                                    </li>
                                    <li>
                                          <input type="text"  id="answer3" placeholder="הכנס תשובה" class="Question"/>
                                        <input id="check3" type="checkbox" onclick="cleanCheck3()"  />
                                    </li>
                                    <li>
                                          <input type="text"   id="answer4" placeholder="הכנס תשובה" class="Question"/>
                                        <input id="check4" type="checkbox" onclick="cleanCheck4()"/>
                                    </li>
                                    <li>
                                          <input type="text"  id="answer5" placeholder="הכנס תשובה" class="Question"/>
                                        <input id="check5" type="checkbox" onclick="cleanCheck5()" />

                                    </li>
                                    <li>
                                          <input type="text"  id="answer6" placeholder="הכנס תשובה" class="Question"/>
                                        <input id="check6"  class="check1" type="checkbox" onclick="cleanCheck6()"/>
                                    </li>
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
                                <asp:Button ID="nextQuestion" runat="server" OnClientClick="return false;" Width="120px" CssClass="myButton" Text="הבא" />
                                <asp:Button ID="prevQuestion" runat="server"  OnClientClick="return false;"  Width="120px" CssClass="myButton" Text="הקודם" />
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

