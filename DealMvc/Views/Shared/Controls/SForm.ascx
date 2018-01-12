<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    function setTValue(key, value) { $("input[name='" + key + "']").attr("value", value); }
    function setRadioValue(key, value) { $("input[name='" + key + "'][value='" + value + "']").attr("checked", 'checked'); }
    function setSelectValue(key, value) { $("select[name='" + key + "'] option:[value='" + value + "']").attr("selected", "selected"); }
    //function setTextAValue(key, value) { $("textarea[name='" + key + "']").html(value); }
    var myValues = new Array();
    var EvalString="";
<%
    try
    {
        NameValueCollection _FormCollection = new NameValueCollection();
        if (ViewData["S_From"] != null)
        { _FormCollection.Add((NameValueCollection)ViewData["S_From"]); }
        if (ViewData["S_From2"] != null)
        { _FormCollection.Add((NameValueCollection)ViewData["S_From2"]); }
        for (int i = 0; i < _FormCollection.Count; i++)
        {
            string key = _FormCollection.GetKey(i).ToString();
            string[] values = _FormCollection.GetValues(i);
            if (values == null || values.Length == 0) { continue; }
            if (values[0].ToString().IndexOf("\r") >= 0 || values[0].ToString().IndexOf("\n") >= 0)
            { continue; }
            for (int u = 0; u < values.Length; u++)
            {
                string C_value = values[u].ToString().Jescape();
//                C_value = C_value.Replace("'","@_@");
//                C_value = C_value.Replace("\"","#_#");
//                C_value = C_value.Replace("\\","&_&");
%>
    EvalString+="myValues.push(new Array('<%=key %>',unescape('<%= C_value %>')));"
<%
    }
        }
    }
    catch (Exception ce) { throw new Exception("保存表单数据出错" + ce.Message); }
%>
       eval(EvalString);
          $(
                function()
                {
                    for (var i = 0; i < myValues.length; i++)
                    {
                        var key = myValues[i][0]; var value = myValues[i][1];
                        //value = value.replace(/@_@/g,"'");value = value.replace(/#_#/g,"\"");value = value.replace(/&_&/g,"\\");
                        try
                        {   
                            if(!$("input[name='" + key + "']").hasClass("noV"))
                            {
                                var type = $("input[name='" + key + "']").attr("type");
                                switch (type.toLowerCase())
                                {
                                    case "text": setTValue(key, value); break;
                                    case "hidden": setTValue(key, value); break;
                                    case "password": setTValue(key, value); break;
                                    case "file": setTValue(key, value); break;
                                    case "radio": setRadioValue(key, value); break;
                                    case "checkbox": setRadioValue(key, value); break;
                                }
                            }
                        }
                        catch (ce)
                        {
                            try{setSelectValue(key, value);}catch (cee) { }
                        }
                    }
                }
            );
</script>
