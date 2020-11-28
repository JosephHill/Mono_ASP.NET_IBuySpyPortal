<%@ Control CodeBehind="DesktopPortalBanner.ascx.cs" Language="c#" AutoEventWireup="false" Inherits="ASPNetPortal.DesktopPortalBanner" %>
<%@ Import Namespace="ASPNetPortal" %>

<%--

   The DesktopPortalBanner User Control is responsible for displaying the standard Portal
   banner at the top of each .aspx page.

   The DesktopPortalBanner uses the Portal Configuration System to obtain a list of the
   portal's sitename and tab settings. It then render's this content into the page.

--%>


<table width="100%" cellspacing="0" class="HeadBg" border="0">
    <tr valign="top">
        <td colspan="3" class="SiteLink" background="<%= Request.ApplicationPath %>/images/bars.gif" align="right">
            <asp:label id="WelcomeMessage" forecolor="#eeeeee" runat="server" />
            <a href="<%= Request.ApplicationPath %>" class="SiteLink">Portal Home</a> <span class="Accent">
                |</span> <a href="<%= Request.ApplicationPath %>/Docs/Docs.htm" target="_blank" class="SiteLink">
                Portal Documentation</a>
            <%= LogoffLink %>
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td width="10" rowspan="2">
            &nbsp;
        </td>
        <td height="40">
            <asp:label id="siteName" CssClass="SiteTitle" EnableViewState="false" runat="server" />
        </td>
        <td align="middle" rowspan="2">
            <a href="http://go-mono.org"><img id="logo" src="<%=Request.ApplicationPath%>/images/powered-by-mono.gif" border="0"></a>
        </td>
    </tr>
    <tr>
        <td>
            <asp:datalist id="tabs" cssclass="OtherTabsBg" repeatdirection="horizontal" ItemStyle-Height="25" SelectedItemStyle-CssClass="TabBg" ItemStyle-BorderWidth="1" EnableViewState="false" runat="server">
                <ItemTemplate>
                    &nbsp;<a href='<%= Request.ApplicationPath %>/DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>' class="OtherTabs"><%# ((TabStripDetails) Container.DataItem).TabName %></a>&nbsp;
                </ItemTemplate>
                <SelectedItemTemplate>
                    &nbsp;<span class="SelectedTab"><%# ((TabStripDetails) Container.DataItem).TabName %></span>&nbsp;
                </SelectedItemTemplate>
            </asp:datalist>
        </td>
    </tr>
</table>
