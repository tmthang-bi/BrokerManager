<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePicker.ascx.cs" Inherits="DatePicker" %>
<table id="tbl_control" cellSpacing="0" cellPadding="0" border="0" style="border-style:none; border-width:0px; white-space: nowrap;">
	<tr>
		<td align="middle" style="border-style:none; border-width:0px; height: 30px;"><asp:textbox id="txt_Date" runat="server" Width="70" OnKeyPress="return checkAllowedKey(event);"></asp:textbox>&nbsp;</td>
		<td style="border-style:none; border-width:0px; height: 30px;"><asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif" EnableViewState="False"></asp:image></td>
	</tr>
</table>