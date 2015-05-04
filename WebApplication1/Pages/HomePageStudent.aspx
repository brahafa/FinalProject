<%@ Page Title="דף הבית"  Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/SiteStudent.Master"  CodeBehind="HomePageStudent.aspx.cs" Inherits="WebApplication1.Pages.HomePageStudent" %>
 

<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">
  
 <form  runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data"> 
    <section id="content">
      <div class="padding">
        <div class="wrapper margin-bot">
          <div  class="col-3">
            <div class="indent">
              <h2 class="p0"> רשימת קורסים</h2> 
                <br /><br /><br /><br /><br />
                <div id="new_released_section">
                     <input type="text" id="courseId" style="display: none" runat="server" value="0" />
                    <%
                        int j = 0;
                    %>
            <%  for (int i = 0; i < listCourses.Count; i++)
                { %>
            <div class='new_released_box' id= '<% =listCourses[i].getId() %>'>
                
                <%
                  
                    courseBtn.Text = listCourses[i].getName().Trim();
                    courseBtn.BackColor = colorCourses[j];
                    //courseInputBtn.Value = listCourse[i].getName().Trim();
                  j++;
                  if (j == 9)
                  {
                      j = 0;
                  }
                  %> 
                <asp:Button runat="server" class="coursesBtn" OnClick="goStock_Click" OnClientClick="setCourseID($(this).last().parent().prop('id'));" id="courseBtn" BackColor=""  Text=""/>
               <%--<input type="button" runat="server" class="coursesBtn" id="courseInputBtn" value="" style="background-color:red"/>--%>
            </div>
            <%}%>
        </div>
</div>
         
        </div>
            <div id="buttonCourses">

           
              <div id="buttonAddRemove" class="buttonAddRemove" >
                  <ul>
                         <li>
                            <input id="removeCourseBtn" class="myButton" runat="server"  name="removeCourseBtn" type="button" value="הסר קורס"/>
                            <input id="addCourseBtn" class="myButton" runat="server"  name="addCourseBtn" type="button" value="הוסף קורס"/>
                           <%--  <asp:Button id="" runat="server" OnClientClick="return false;" CssClass="myButton" Text="הוסף קורס"  />--%>
              </li>
                      
                         </ul>    
                </div>
                
                 <div id="inputAddRemove" class="buttonAddRemove" style="display:none">
                     <ol>
                         <li>
                             <asp:Button ID="closeButton" runat="server" OnClientClick="return false;" CssClass="closeButton" Text="x"/>
                         </li>
                         <li>
                              <input type="text" id="addOrremove" style="display: none" runat="server" value="0" />
                             <input id="addRemoveBtnStudent" class="myButton" runat="server" name="addRemoveBtn" type="button" value=""/>
<%--                             <asp:Button ID="addRemoveBtn" runat="server" OnClientClick="return false;" CssClass="myButton" Text="" />--%>
                            <input id="courseName" class="RegisterField" runat="server" name="courseName" type="text" />

                             
                            </li>
                         <li>
                          <label id="errMesegeEmpty" style="display: none" runat="server" class="errMesege">יש להכניס תחילה את שם הקורס*</label>
                      </li>
                         </ol>
                        </div> 
                 </div>  
             <div class="col-4">
           
             
          </div>
             <div class="block-newsHomPage" id="conectedUser" runat="server"    >
              <h3 class="color-4 p2">:אתה מחובר כ</h3>
                <br/><br/>
               <h3 class="color-4 p2"><label id="UserNameLabels" runat="server"> </label></h3> 
            <br/><br/>
                  <ul class="list-2">
                   <li>  
                     <div   id="profile">
                     <asp:Image runat="server" ID="userImages" CssClass="userImage" />
                   </div>
                </li>   
                      <li>
                           <asp:Button ID="logoutBtns" runat="server" CssClass="myButton" OnClick="logout_click" Text="התנתק"></asp:Button>
                      </li>  
            
              </ul>
                
            </div>
        </div>
  
      </div>
    </section>


 </form>
</asp:Content>
