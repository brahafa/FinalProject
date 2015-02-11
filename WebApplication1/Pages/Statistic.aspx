<%@ Page Title="סטטיסטיקה" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Pages/Site.Master" CodeBehind="Statistic.aspx.cs" Inherits="WebApplication1.Pages.WebForm6" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="MainContent">


     <form id="Form1" runat="server">

    <div>

     <asp:Calendar ID="Calendar1" runat="server" BackColor="White"  SelectedDate="<%# DateTime.Today %>" />--%>

        <asp:Chart ID="cTestChart" runat="server">
	<Series>
		<asp:Series Name="Testing" YValueType="Int32">

			<Points>
				<asp:DataPoint AxisLabel="Test 1" YValues="10" />
				<asp:DataPoint AxisLabel="Test 2" YValues="50" />

				<asp:DataPoint AxisLabel="Test 3" YValues="30" />
				<asp:DataPoint AxisLabel="Test 4" YValues="40" />

			</Points>
		</asp:Series>
	</Series>
  
	<ChartAreas>
		<asp:ChartArea Name="ChartArea1">
		</asp:ChartArea>

	</ChartAreas>
</asp:Chart>
        

<%--        ngcfhgdhgdhtgdygdtrf--%>
             <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <div id="divCalendar" style="position: absolute; top: 28px; left: 34px; z-index: 99999;">
                        <asp:Calendar ID="Calendar2" runat="server" BorderWidth="2px" BackColor="white" Width="200px"
                            ForeColor="Black" Height="180px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#E4DA85"
                            BorderStyle="Outset" DayNameFormat="FirstLetter" CellPadding="4" OnSelectionChanged="Calendar2_SelectionChanged">
                            <TodayDayStyle ForeColor="Black" BackColor="#E9E19A"></TodayDayStyle>
                            <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#E9E19A"></DayHeaderStyle>
                            <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#D5D900"></SelectedDayStyle>
                        </asp:Calendar>


    </div>
     </form>

</asp:Content>