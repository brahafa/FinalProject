<%@ Page Title="כניסה" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Pages.WebForm1" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">


    <form runat="server" id="Form1" action="#" method="post" enctype="multipart/form-data">
        <div class="logInDiv">
            <div class="block-news2">
                <br />
                <h3 class="color-4 p2">:הרשמה</h3>
                <br />
                <br />
                <ul class="list-2">
                    <li>
                        <div id="profileRegister">
                            <asp:Image ID="Avatar" runat="server" ImageUrl="~/images/profil.jpg" Width="125px" Height="125px" />
                            <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                        </div>
                    </li>
                    <li>
                        <select class="DropeDownListServer" id="selected_Type" runat="server" onchange="Type()">
                            <option value="-1">:סוג משתמש</option>
                            <option value="0">מרצה</option>
                            <option value="1">סטודנט</option>
                        </select>
                    </li>
                    <li>
                        <label class="RegisterLabel">*שם:</label>

                        <input id="contactName" class="RegisterField" runat="server" name="ContactName" type="text" />
                        <select class="DropeDownListServer" id="selected_Degree" runat="server" onchange="Degree()" style="display: none">
                            <option value="0">מר/ת</option>
                            <option value="1">דוקטור</option>
                            <option value="2">פרופסור</option>
                        </select>
                    </li>
                    <li>
                        <input id="ImagePath" type="hidden" runat="server" />
                        <input id="InputPassword" type="hidden" runat="server" />
                        <label class="RegisterLabel">*אימייל:</label>
                        <input id="Email" class="RegisterField" runat="server" name="Email" type="text" />
                    </li>
                    <li>
                        <label class="RegisterLabel">*סיסמה:</label>
                        <input id="pass" class="RegisterField" runat="server" name="password" type="text" />
                    </li>
                    <li>
                        <label class="RegisterLabel">*אימות סיסמה:</label>
                        <input id="pass1" class="RegisterField" runat="server" name="password" type="text" />
                    </li>
                    <li>
                        <label id="errMesege" visible="false" runat="server" class="errMesege">יש למלא את השדות סיסמה ודוא"ל*</label>
                    </li>
                    <li>
                        <asp:Button runat="server" OnClick="Register_Click" class="myButton" Width="100px" Height="35px" Text="הירשם" /></li>
                </ul>
            </div>



        </div>


    </form>
</asp:Content>
