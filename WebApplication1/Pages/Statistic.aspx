<%@ Page Title="סטטיסטיקה" Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="Statistic.aspx.cs" Inherits="WebApplication1.Pages.WebForm6" %>

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

                                <li style: height="70px" ;>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div id="cointainDateDiv">
                                        <div id="divCalendar" z-index:"99999"; class="divCalendar">
                                        <asp:Calendar ID="Calendar2" CssClass="calendar" runat="server" DayNameFormat="FirstLetter" CellPadding="4"  OnSelectionChanged="CalendarFrom_SelectionChanged">
                                            <TodayDayStyle ForeColor="Black" BackColor="#E9E19A"></TodayDayStyle>
                                            <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#E9E19A"></DayHeaderStyle>
                                            <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#D5D900"></SelectedDayStyle>
                                        </asp:Calendar>
                                    </div>

                                    <div id="divCalendar2" class="divCalendar"  z-index:"99999";>
                                        <asp:Calendar ID="CalendarTo" CssClass="calendar" runat="server" DayNameFormat="FirstLetter" CellPadding="4" OnSelectionChanged="CalendarTo_SelectionChanged">
                                            <TodayDayStyle ForeColor="Black" BackColor="#E9E19A"></TodayDayStyle>
                                            <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#E9E19A"></DayHeaderStyle>
                                            <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#D5D900"></SelectedDayStyle>
                                        </asp:Calendar>
                                    </div>

                                    <div id="dateDiv" style="width=100px"">
                                          <h6 style="color: red">מתאריך</h6>
                                    <asp:TextBox ID="TextBoxFromDate" Style="display: inline" runat="server"></asp:TextBox>
                                    <h6 style="color: red">עד תאריך</h6>
                                    <asp:TextBox ID="TextBoxToDate" Style="display: inline" runat="server"></asp:TextBox>

                                    </div>
                                  </div>
                                </li>
                                <li>
                                
                                </li>
                                <li>
                                    <asp:Button ID="BtnTest2" runat="server" OnClick="BtnTest2_Click" CssClass="myButton" Text="test12"></asp:Button>

                                    <asp:Button ID="BtnTest" runat="server" OnClick="test_Click" CssClass="myButton" Text="test11"></asp:Button>
                                </li>
                                <li>
                                    <div>
                                        <asp:Chart ID="cTestChart" runat="server" BackColor="Tomato" BackGradientStyle="Center" Palette="Chocolate" RightToLeft="Yes" Width="600px">
                                            <Series >
                                                <asp:Series Name="Testing"  YValueType="Int32" >
                                                </asp:Series>
                                            </Series>

                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                </asp:ChartArea>

                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>

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
  
        <ul>


        </ul>


    </form>

</asp:Content>
        