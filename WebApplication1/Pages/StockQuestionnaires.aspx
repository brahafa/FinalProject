<%@ Page Title="מאגר שאלונים" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="StockQuestionnaires.aspx.cs" Inherits="Clicker.Pages.c" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">

    <form class="form" runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data">
        <!-- content -->
        <section class="contentClass" id="content">
            

 <div class="padding">
        <div class="wrapper margin-bot">
          <div  class="col-3" id="stockList">

                       <h2 class="p0"> מאגר שאלונים - <% =getCourseName() %> </h2> 
              
              <br />
              <br />
               <br />
              <%if (Request.QueryString["IdCourse"] != null)
                { %>
                  <h2 id="codeCourse" class="p0" >קוד קורס - <% =idCourse %></h2>
              
                      <%} %>     
              
            <%--  <input id="displayMyQuestionnaire" runat="server" class="myButton" type="button" value="הצג את השאלונים שלי" />--%>
              
               
            <div class="indent" id="stockQuestionnaire" style="display: inline" runat="server">
                <br />
                <div class="QuestionnaireIndent">
                        <input type="text" id="idQuestnaire" name="idQuestnaire" style="display: none" runat="server" value="0" />
                        <input type="text" id="QuestionnaireName" style="display: none" runat="server" value="0" />
                        <ul class="Questionnaire">
                        
                            <%  for (int i = 0; i < listQuestionnaire.Count; i++)
                                {
                                    NameQuestionnaire.Text = listQuestionnaire[i].getName().Trim();
                                   
                                        %>
                            <li >
                                <div id="QuestionnaireLiDiv" class="liDive">
                                   
                                 <div id='<% =listQuestionnaire[i].getName()+","+ listQuestionnaire[i].getId()%>'>
                                    <asp:Button runat="server" id="NameQuestionnaire" OnClientClick="setQuestionnaireName($(this).last().parent().prop('id'));" OnClick ="onClick_Questionnaire" Text=""></asp:Button>
                                        </div>
                                </div>
                            </li>

                            <%}
                              
                              
                                %>
                        </ul>
                    </div>
</div>

<div class="indentStock" id="StockQuestion" style="display: none" runat="server">
                           
                    <div class="QuestionClass">
                    <asp:Button ID="closeButtonQuestions" runat="server" OnClientClick="return false;" CssClass="closeButton" Text="x"/>
                        <br />
                         <h3 id="stockQuestionText" class="color-4 p2">מאגר שאלות: <% =QuestionnaireName.Value %> </h3>
                        <div class="QuestionnaireIndent">
                            <input type="text" id="QuestionId" style="display: none" runat="server" value="0" />
                            <ul class="Questionnaire">
                                <%  for (int j = 0; j < listQuestion.Count; j++)
                                    {
                                        NameQuestion.Text = listQuestion[j].getQuestion();
                                        %>
                                <li id="QuestionLiDiv">
                                    
                                    <div id='<% =listQuestion[j].getId() %>' class="liDive">

                                        <asp:Button runat="server" id="NameQuestion" OnClientClick="setQuestionId($(this).last().parent().prop('id'));" OnClick ="onClick_Question" Text=""></asp:Button>
                                   </div>
                                </li>
                                <li>
                                   <% 
                                       
                                        listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[j].getId());
                                        
                                    for(int k=0; k<listAnswer.Count; k++)
                                        {
                                       // answer.Value = listAnswer[k].getAnswer();
                                         %>
                                   <%-- <input id="answer" type="text" runat="server" value="0"/>--%>
                                    <label id="answers"><% =listAnswer[k].getAnswer() %></label>
                                    <br />
                                    <br />
                                    <%}%>
                                </li>

                                <%}%>
                            </ul>
                        </div>
                    </div>

                </div>
         
        </div>
            <div id="buttonCourses">

           
              <div id="buttonAddRemoveQ" class="buttonAddRemove" >
                  <ol>
                         

                      <li>
                              <input id="copyQuestionnaireBtn" class="myButton" runat="server" name="copyQuestionnaireBtn" type="button" value="העתקת שאלון" style="display:none"/>
                        <input id="removeQuestionnaireBtn" class="myButton" runat="server" name="removeQuestionnaireBtn" type="button" value="הסרת שאלון" style="display:none"/> 
                             <input id="deletDisplayQuestFromCourseBtn" runat="server" class="myButton" type="button" value="סיום שאלון" style="display:none" /> 
                             <input id="displayQuestFromCourseBtn" class="myButton" runat="server" name="displayQuestFromCourseBtn" type="button" value="הצג לכיתה" style="display:none"/>  
</li>    
                                                   <li>
                        <input id="removeCourseBtnFromQ" class="myButton" runat="server" name="removeCourseBtn" type="button" value="הסר קורס"/>  
</li>                   
                     <li class="styled-select" id="inputSelectCourse" >
                          <select class="styled-select" id="selectCourse" onchange="selectCourseForCopy()" runat="server" style="display:none">
                                        <option value="-1">:בחר קורס</option>
                                    </select>
                     </li> 
                      </ol>
                 <%-- <div id="inputQtoRemoveOrCopy" style="display:none">
                      <ol>
                      <li>
                             <asp:Button ID="closeButtonRemoveQ" runat="server" OnClientClick="return false;" CssClass="closeButton" Text="x"/>
                         </li>
                         <li>
                              <input id="copyBtn" class="myButton" runat="server" name="copyBtn" type="button" value="העתק" style="display:none"/>
                             <input id="removeBtn" class="myButton" runat="server" name="removeBtn" type="button" value="הסר"/>
                            <input id="removeQname" class="RegisterField" runat="server" name="removeQname" type="text" />
                            
                            </li>
                         <li>
                          <label id="errMesegeEmpty" style="display: none" runat="server" class="errMesege">יש להכניס תחילה את שם השאלון*</label>
                      </li>
                          </ol>
                  </div>--%>
                    </div>
               
               </div>  
           
             <div class="col-4">
           
              </div>
          
             <div class="block-newsHomPage" id="conectedUser" runat="server"    >
              <h3 class="color-4 p2">:אתה מחובר כ</h3>
                <br/><br/>
               <h3 class="color-4 p2"><label id="UserNameLabel" runat="server"> </label></h3> 
            <br/><br/>
                  <ul class="list-2">
                   <li>  
                     <div   id="profile">
                     <asp:Image runat="server" ID="userImage" ImageUrl="~/images/profile.gif" CssClass="userImage" />
                   </div>
                </li>   
                      <li>
                           <asp:Button ID="logoutBtn" runat="server" CssClass="myButton" OnClick="logout_click" Text="התנתק"></asp:Button>
                      </li>  
            
              </ul>
                
            </div>

                 
       </div>
  
      </div>
        </section>
    </form>
</asp:Content>

