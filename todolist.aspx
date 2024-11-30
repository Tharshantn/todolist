<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="todolist.aspx.cs" Inherits="Todolist.todolist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvTasks" runat="server" AutoGenerateColumns="false" OnRowCommand="gvTasks_RowCommand" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CommandName="EditTask" CommandArgument='<%# Eval("Id") %>' Text="Edit" />
                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteTask" CommandArgument='<%# Eval("Id") %>' Text="Delete" />
                  
                <asp:Button ID="btnCompleteTask" runat="server" CommandName="CompleteTask" CommandArgument='<%# Eval("Id") %>' Text="Complete Task" />
                <asp:Button ID="btnIncompleteTask" runat="server" CommandName="IncompleteTask" CommandArgument='<%# Eval("Id") %>' Text="Incomplete Task" />
            </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <br />
            <asp:Panel ID="pnlAddTask" runat="server">
                <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnAddTask" runat="server" Text="Add Task" OnClick="btnAddTask_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlEditTask" runat="server" Visible="false">
                <asp:Label ID="lblTaskId" runat="server" Text="Task ID:"></asp:Label>
                <asp:TextBox ID="txtTaskId" runat="server" ReadOnly="true"></asp:TextBox>
                <br />
                <asp:Label ID="lblEditTitle" runat="server" Text="Title:"></asp:Label>
                <asp:TextBox ID="txtEditTitle" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnUpdateTask" runat="server" Text="Update Task" OnClick="btnUpdateTask_Click" />
                <asp:Button ID="btnCancelUpdate" runat="server" Text="Cancel" OnClick="btnCancelUpdate_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
