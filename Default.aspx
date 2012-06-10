<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CloudBarGrillWebApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <link href="Styles/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $().ready(function () {
            $('#dialogContent').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Address for Order",
                width: 450,
                height: 420,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        });

        function openDialog() {
            $('#dialogContent').dialog('open');
            return false;
        }

        function closeDialog() {
            $('#dialogContent').dialog('close');
            $('#addressFieldSet').find('input:text').val('');
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <h2>
        Choose something from our menu!</h2>
    <div id="menuarea">
        <asp:GridView ID="MainMenuGrid" runat="server" ClientIDMode="Static" GridLines="None">
            <AlternatingRowStyle CssClass="alt" />
            <Columns>
                <asp:TemplateField HeaderText="Order">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="bottomButtons">
            <asp:Button ID="Button1" runat="server" Text="Order" OnClientClick="javascript:return openDialog();"
                CssClass="button" />
            <asp:LoginView ID="LoginView1" runat="server">
                <LoggedInTemplate>
                    <asp:Button ID="Button2" runat="server" CssClass="button" 
    OnClick="AddItem_Click" Text="Add Item" CausesValidation="False" />
       <asp:Button ID="Button3" runat="server" CssClass="button" 
      OnClick="GetOrders_Click" Text="Get orders" CausesValidation="False" />
                </LoggedInTemplate>
            </asp:LoginView>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>
    </div>
        <div id="dialogContent">
        <fieldset id="addressFieldSet">
            <p>
                Name:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nameInputBox"
                    ErrorMessage="User name is required."> 
                </asp:RequiredFieldValidator>
            </p>
            <input type="text" id="nameInputBox" runat="server" />
            <p>
                Address:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="addressInputBox"
                    ErrorMessage="User address is required."> 
                </asp:RequiredFieldValidator></p>
            <input type="text" id="addressInputBox" runat="server" />
            <p>
                e-mail:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="emailInputBox"
                    ErrorMessage="User e-mail is required."> 
                </asp:RequiredFieldValidator></p>
            <input type="text" id="emailInputBox" runat="server" />
            <asp:CheckBox ID="sendEmailCheck" runat="server" Text=" Send e-mail" />
            <div class="bottomButtons">
                <asp:Button ID="OrderButton" runat="server" OnClick="OrderButton_Click" Text="Make Order"
                    CssClass="button" />
                <br />
                <br />
                <br />
                <asp:Button ID="CloseButton" runat="server" OnClientClick="closeDialog()" Text="Cancel"
                    CssClass="button" />
            </div>
        </fieldset>
    </div>
</asp:Content>
