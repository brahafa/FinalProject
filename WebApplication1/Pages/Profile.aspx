<%@ Page Title="פרטים אישיים" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="Profile.aspx.cs" Inherits="WebApplication1.Pages.WebForm5" %>


 <asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">
  

 <form  runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data"> 
    <section id="content">
      <div class="padding">
        <div class="wrapper margin-bot">
          <div class="col-3">
            <div class="indent">
              <h2>פרטים אישיים</h2>
              <p class="color-4">Quas molestias excepturi sint occaecati cupiditate non provident, similique suntulpa uis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat qui est eligendi optio cumque nihil impedit quo minus idod maxime placeat facere possimus, omnis voluptas officia deserunt molli<br/>
                tia animi, id est laborum et dolorum fuga.</p>
              <div class="wrapper p2">
                <figure class="img-indent"><img src="../images/page4-img1.png" alt="" /></figure>
                <div class="extra-wrap"> Harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus idod maxime placeat facere possimus, omnis alias consequatur aut perferendis doloribus asperiores repellat. Lorem ipsum dolor sit amet, consectetur adipisicing elit voluptas assumenda. Eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaquarum rerum xcepteur sint occaecat cupidatat non proiapiente delectusut reiciendis dent sunt in. </div>
              </div>
              Veniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaquarum rerum xcepteur sint occaecat cupidatat non proident, sunt in culpa qui officia hic tenetur sapiente delectusut reici<br/>
              endis. Voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat. Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliquaenim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. </div>
          </div>
          <div class="col-4">
            <div class="block-news">
              <h3 class="color-4 p2">:הינך מחובר כ</h3>
              <h3 class="color-4 p2"><label id="UserNameLabel" runat="server"> </label></h3> 
              <ul class="list-2">
                   <li>  
                     <div   id="profile">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" Height="12px" Width="14px">
                        <Columns>
                            <asp:BoundField DataField="Text" Visible="false" />
           
                            <asp:ImageField  ControlStyle-Width="110px" ControlStyle-Height="80px" ControlStyle-BorderWidth="1px"   DataImageUrlField="Value"  />
                        </Columns>
                   </asp:GridView>
                   <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
                   <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
                   </div>
                </li>
                <li>
                    <label class="RegisterLabel">*שם מרצה:</label> </li>
                  <li>  <input id="contactName" class="RegisterField" runat="server" name="ContactName" type="text" />     
                </li>
                <li>
                    <input id="ImagePath" type="hidden" runat="server"/>
                    <input id="InputPassword" type="hidden" runat="server"/>
                    <label class="RegisterLabel">*אימייל:</label>
                    <input id="Email" class="RegisterField" runat="server" name="Email" type="text" /> 
                </li>
                <li>                  
                    <label class="RegisterLabel">*סיסמה:</label>
                    <input id="pass" class="RegisterField" runat="server" name="ee" type="text" />
<%--                    <asp:TextBox ID="LectPassword" TextMode="Password" runat="server" />--%>     
                </li>    
                <li><asp:Button runat="server"  Text="שמור שינויים" OnClick="Register_Click" /></li>
              </ul>
            </div>
          </div>
        </div>
  
      </div>
    </section>
 </form>
</asp:Content>
