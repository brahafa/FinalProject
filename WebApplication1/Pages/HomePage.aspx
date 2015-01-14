<%@ Page Title="דף הבית" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Pages/Site.Master"  CodeBehind="HomePage.aspx.cs" Inherits="WebApplication1.Pages.WebForm2" %>
 <asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">
  

 <form  runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data"> 
    <section id="content">
      <div class="padding">
        <div class="wrapper margin-bot">
          <div class="col-3">
            <div class="indent">
              <h2 class="p0"> רשימת קורסים</h2> 
                <br /><br /><br /><br /><br />
                <div id="new_released_section">
                     <input type="text" id="courseId" style="display: none" runat="server" value="0" />
                    <%
                        int j = 0;
                    %>
            <%  for (int i = 0; i < listCourse.Count; i++)
                { %>
            <div class='new_released_box' id= '<% =listCourse[i].getId() %>'>
                
                <%
                  
                    courseBtn.Text = listCourse[i].getName().Trim();
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
         <div id="buttonAddRemove" class="buttonAddRemove" >
                            <input id="removeCourseBtn" class="myButton" runat="server" name="removeCourseBtn" type="button" value="הסר קורס"/>
                            <asp:Button id="addCourseBtn" runat="server" OnClientClick="return false;" CssClass="myButton" Text="הוסף קורס"  />
              </div>
                 <div id="inputAddRemove" class="buttonAddRemove" style="display:none">
                     <ul>
                         <li>
                             <asp:Button ID="closeButton" runat="server" OnClientClick="return false;" CssClass="closeButton" Text="x"/>
                         </li>
                         <li>
                              <input type="text" id="addOrremove" style="display: none" runat="server" value="0" />
                             <input id="addRemoveBtn" class="myButton" runat="server" name="addRemoveBtn" type="button" value=""/>
                            <input id="courseName" class="RegisterField" runat="server" name="courseName" type="text" />

                             
                            </li>
                         </ul>
                        </div>    
        </div>
             
             <div class="col-4">
            <div class="block-news" id="conectedUser" runat="server"    >
              <h3 class="color-4 p2">:אתה מחובר כ</h3>
                <br/><br/>
               <h3 class="color-4 p2"><label id="UserNameLabel" runat="server"> </label></h3> 
            <br/><br/>
                  <ul class="list-2">
                   <li>  
                     <div   id="profile">
                     <asp:Image runat="server" ID="userImage" CssClass="userImage" />
                   </div>
                </li>   
                      <li>
                           <asp:Button ID="logoutBtn" runat="server" CssClass="myButton" OnClick="logout_click" Text="התנתק"></asp:Button>
                      </li>  
            
              </ul>
                
            </div>
             
          </div>

        </div>
  
      </div>
    </section>


 </form>
</asp:Content>