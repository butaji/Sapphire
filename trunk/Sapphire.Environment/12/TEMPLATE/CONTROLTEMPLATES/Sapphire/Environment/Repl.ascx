<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Repl.ascx.cs" Inherits="Sapphire.Environment.UI.WebControls.Repl, Sapphire.Environment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa1e57e9c7ebde49" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
  Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<div>
  <asp:TextBox ID="ScriptTextBox" runat="server" Width="100%" TextMode="MultiLine"
    Rows="10" />
</div>
<div>
  <asp:RadioButton ID="PythonRadioButton" runat="server" Checked="true" Text="Python" />
</div>
<div style="text-align: right;">
  <asp:Button ID="RunButton" runat="server" Text="Run" OnClick="RunButton_Click" />
</div>
<div>
  Result:<br />
  <asp:Label ID="ResultLabel" runat="server" />
</div>
