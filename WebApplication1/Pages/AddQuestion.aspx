<%@ Page Title="הוספת שאלה" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="AddQuestion.aspx.cs" Inherits="WebApplication1.Pages.WebForm3" %>

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
                                        <h2 class="p0">הוספת שאלה</h2>
                                    </div>
                                </li>
                                <li class="styled-select">
                                    <br />
                                    <br />
                                    <br />
                                    <br />

                                    <select class="styled-select" id="selected_Questionnaires" onchange="SelectQuestionnaires()" runat="server">
                                        <option value="-1">:בחר שאלון</option>
                                    </select>

                                    <select class="styled-select" id="select_Course" onchange="SelectCurse()" runat="server">
                                        <option value="-1">:בחר קורס</option>
                                    </select>
                                     </li>
                                    <li class="styled-select">
                                    <br />
                                    <input type="text" class="Newquestionn"  id="Newquestionn"  placeholder="הכנס שם שאלון"  style="display: none" />
                                   <select class="styled-select" id="select_permit" runat="server" style="display: none">
                                        <option value="0">פרטי</option>
                                        <option value="1">ציבורי</option>
                                    </select>
                                   
                                     </li>
                                <li>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="openQuestionBtn" runat="server" OnClientClick="return false;" CssClass="myButton" Width="120px" Text="פתוחה" />
                                    <asp:Button ID="americanQuestionBtn" runat="server" OnClientClick="return false;" Width="120px" CssClass="myButton" Text="ריבוי תשובות" />
                                    <asp:Button ID="yesNoQuestionBtn" runat="server" OnClientClick="return false;" Width="120px" CssClass="myButton" Text="כן/לא" />
                                </li>
                                <li>
                                    <h5>:שאלה</h5>
                                </li>
                                <li>
                                    <asp:TextBox ID="question1" CssClass="Question" Columns="2" placeholder="הכנס שאלה" Width="500px" runat="server" />

                                </li>

                            </ul>
                            <div id="Americananswer" class="answer" style="display: inline">
                                <ul>
                                    <li>
                                        <h5>:תשובות</h5>
                                    </li>
                                    <li>
<%--                                        <asp:TextBox ID="dans1" CssClass="Question" placeholder="הכנס תשובה נכונה" runat="server" />--%>
                                          <input type="text"  id="answer1" placeholder="הכנס תשובה" class="Question"/>
                                          <input id="check1" type="checkbox"  name="Gender" onclick="cleanCheck1()"  />
                                    </li>
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


                            <div id="yesNoDiv" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5>:תשובות</h5>
                                    </li>
                                    <li>

                                        <%--<asp:TextBox ID="NoTxtBoxId" CssClass="Question" Columns="2" Text="YES" Width="500px" runat="server" />
                                        <input id="checkNo" class="check1" type="checkbox" name="check" value="check1"/>--%>
                                        <label for="c1">NO</label>
                                        <input id="CheckNo" class="check1"  type="checkbox" onclick="cleanCheckNo()"  />


                                    </li>
                                    <li>
                                        <label for="c1">YES</label>
                                        <input id="CheckYes" class="check1"  type="checkbox" onclick="cleanCheckYes()"  />
                                    </li>
                                </ul>
                            </div>

                            <div id="OpenDiv" class="answer" style="display: none">
                                <ul>
                                    <li>
                                        <h5>:תשובה</h5>
                                    </li>
                                    <li>

                                        <asp:TextBox ID="openAnswerID" CssClass="Question" Columns="2" placeholder="הכנס תשובה" Width="500px" runat="server" />

                                    </li>
                                </ul>
                            </div>
                            <div id="sendDiv">
                                 <input id="err"  type="text" class="errMesegeAddQuest" style="display: none"  />
                                <br />
                                <asp:Button ID="displayClass" runat="server" OnClientClick="return false;" Width="120px" CssClass="myButton" Text="הצג לכיתה" />
                                <asp:Button ID="SaveAnswer" runat="server" OnClientClick="return false;"  Width="120px" CssClass="myButton" Text="שמור" />
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
                                    <asp:Button ID="logoutBtn" runat="server" CssClass="myButton" Text="התנתק"></asp:Button>
                                </li>

                            </ul>

                        </div>

                    </div>
                </div>




            </div>
        </section>


    </form>

</asp:Content>
