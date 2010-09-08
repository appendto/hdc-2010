<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Task Management!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>My Tasks</h1>
    <div id="allTasks">
    </div>    
    <h1>Add New Tasks</h1>
    <div id="newTasks">
        <form id="newTask" action="/home/create/">
            <label for="Name">Name: </label><input type="text" name="Name" class="required" />
            <label for="Priority">Priority: </label><input type="text" name="Priority" class="autoComplete required" />
            <label for="DueDate">Due Date: </label><input type="text" name="DueDate" class="date required" />
            <label for="StartDate">Start Date: </label><input type="text" name="StartDate" class="date required" />
            <label for="HoursSpent">Hours Spent: </label><input type="text" name="HoursSpent" class="time number required" />
            <label for="HoursRemaining">Hours Remaining: </label><input type="text" name="HoursRemaining" class="time number required" />
            <input type="submit" value="Create New Task" />
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyScripts" runat="server">
    <script src="../../Scripts/jquery.tmpl.js" type="text/javascript"></script>
    <script src="../../Scripts/Home/Index.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.min.js" type="text/javascript"></script>
</asp:Content>
