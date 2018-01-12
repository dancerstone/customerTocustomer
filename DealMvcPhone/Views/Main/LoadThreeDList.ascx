<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Pager<ExperienceHall> _Pager = (Pager<ExperienceHall>)ViewData["Pager"];
    SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
    foreach (var item in _Pager.DataList)
    {
        int? cv = 1;
        string MRL = string.Empty;
        switch (cv)
        {
            case 1:
                MRL =m_site.WebAddress+ item.SingleListPic;
                break;
            case 2:
                MRL = m_site.WebAddress + item.EffectListPic;
                break;
            default:
                break;
        }
%>
<li><a>
    <img src="<%=MRL %>" /></a> </li>
<%} %>