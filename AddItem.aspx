<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddItem.aspx.cs" Inherits="CloudBarGrillWebApp.AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView ID="LoginView1" runat="server">
        <LoggedInTemplate>
            <p>
                Name:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nameInputBox"
                    ErrorMessage="User name is required."></asp:RequiredFieldValidator>
            </p>
            <input id="nameInputBox" type="text" runat="server" />
            <p>
                Ingredients
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ingredientsInputBox"
                    ErrorMessage="Ingredients are required."></asp:RequiredFieldValidator>
            </p>
            <input id="ingredientsInputBox" type="text" runat="server" />
            <p>
                Price
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="priceInputBox"
                    ErrorMessage="Price is required."></asp:RequiredFieldValidator>
            </p>
            <input id="priceInputBox" type="text" runat="server" />
            <br />
            <br />
            <div class="bottomButtons">
                <asp:Button runat="server" Text="AddItem" ID="addItemButton" OnClick="addItemButton_Click"
                    CssClass="button" />
                <asp:Button runat="server" Text="Back" ID="Button2" CssClass="button" 
                    OnClick="back_Click" CausesValidation="False" />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <h3>
                Please login!!!</h3>
        </AnonymousTemplate>
    </asp:LoginView>
</asp:Content>
