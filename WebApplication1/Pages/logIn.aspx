<%@ Page Title="כניסה" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Pages/Site.Master"  CodeBehind="logIn.aspx.cs" Inherits="WebApplication1.Pages.WebForm4" %>
 <asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">
  

 <form  runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data"> 
         <div class="logInDiv">
            <div class="block-news1" id="existUser"  style="visibility:visible" >
                <br /> <br />
              <h3 class="title1">:כניסת מישתמש</h3><br/>
              <ul class="list-2">             
                <li>
                    <label class="RegisterLabel">*:אימייל</label>
                    <input id="Email" class="RegisterField" runat="server" name="Email" type="text" /> 
                   </li>
                <li>                  
                    <label class="RegisterLabel">*:סיסמה</label>
                    <input id="pass" class="RegisterField" runat="server" name="password" type="password" />   
                </li>  
                  <li>
                     <label id="MissVall" visible="false" runat="server" class="errMesege">יש למלא את השדות סיסמה ודוא"ל*</label>
                </li> 
                <li> <asp:Button runat="server" OnClick="existUser_Click"  Text="היכנס" ID="enterLogInBtn"  class="myButton" Width="100px" Height="35px"/></li>
                <li> <asp:Button runat="server" OnClick="register_Click" Text="חשבון חדש" ID="enterBtn1" class="myButton" Width="100px" Height="35px"/></li>              
                <li><asp:LinkButton id="myid" runat="server" OnClick="ForgotPassword_Click" Text="?שכחת סיסמה" /></li>
              </ul> 
            </div>



          </div>


 </form>
</asp:Content>