<%@ Page Title="סטטיסטיקה" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="Statistic.aspx.cs" Inherits="Clicker.Pages.WebForm6" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">


    <form id="Form1" runat="server">
        <section id="content">
            <div class="padding">
                <div class="wrapper margin-bot">
                    <div class="col-3">
                        <div id="buttonAddRemove">
                            <ul>
                                <li>
                                    <div class="indent">
                                        <h2 class="p0">סטטיסטיקה</h2>
                                    </div>
                                </li>
                                  <li class="styled-select">
                                    <br />
                                    <br />
                                    <br />
                                    <br />

                                    <select class="styled-select" id="selected_Questionnaires" onchange="selectQuestion()" runat="server">
                                        <option value="-1">:בחר שאלון</option>
                                    </select>

                                    <select class="styled-select" id="select_Course" onchange="SelectCurseStatistic()" runat="server">
                                        <option value="-1">:בחר קורס</option>
                                    </select>
                                    <asp:TextBox ID="selectTest" Style="display: none" runat="server" ></asp:TextBox>
                                     <asp:TextBox ID="selectQuestion" Style="display: none" runat="server"></asp:TextBox>
                                     </li>
                                <li style: height="70px" ;>
                                    <div id="DivDate"> 
                                       <p  class="color-4 p2" id="labelDate" >
                                      
                                       <asp:Label ID="TextBoxToDate" BorderColor="Red" runat="server" Width="85px" BorderWidth="2px">00/00/00</asp:Label>
                                      :מתאריך
         
                                        <asp:Label ID="TextBoxFromDate"   BorderColor="Red"  runat="server" Width="85px" BorderWidth="2px">00/00/00</asp:Label>
                                       עד תאריך
                                      </p>
                                 </div>


                                    <br /> <br /> <br />



                                        <div id="divCalendar1" class="divCalendar" style="display: none">
                                       <asp:Calendar ID="Calendar1" runat="server"  OnSelectionChanged="Calendar1_SelectionChanged"  SelectionMode="DayWeekMonth" >
                                       </asp:Calendar>
                                       </div>
                                     <div id="divCalendar2" class="divCalendar" style="display: none">
                                       <asp:Calendar ID="Calendar2" runat="server"  OnSelectionChanged="Calendar2_SelectionChanged"  SelectionMode="DayWeekMonth" >
                                       </asp:Calendar>
                                       </div>
                                </li>
                                <li>
                                 <div>
                                         <asp:Table ID="statistictTable" BorderColor="Black" BorderWidth="3px" runat="server" Width="100%">
                                             </asp:Table>
         
                                    </div>

                                </li>
                                <li>
                                    <asp:Button ID="BtnTest2" runat="server" OnClick="BtnselectStatistic_Click" CssClass="myButton" Text="הצג סטטיסטיקה"></asp:Button>
                                                                   <br /> <br /> <br />

                                     </li>
                                <li>
                                   

                                </li>
                            </ul>

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
                                        <asp:Image runat="server" ID="userImage" ImageUrl="~/images/profile.gif" CssClass="userImage" />
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
  
        <ul>


        </ul>


    </form>

</asp:Content>
        