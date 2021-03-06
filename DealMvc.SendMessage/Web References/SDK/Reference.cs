﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.1 版自动生成。
// 
#pragma warning disable 1591

namespace DealMvc.SendMessage.SDK {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://www.82009668.com/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback changePassWordOperationCompleted;
        
        private System.Threading.SendOrPostCallback getBalanceOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendMMSOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendChatOperationCompleted;
        
        private System.Threading.SendOrPostCallback getChatOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::DealMvc.SendMessage.Properties.Settings.Default.DealMvc_SendMobile_SDK_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event changePassWordCompletedEventHandler changePassWordCompleted;
        
        /// <remarks/>
        public event getBalanceCompletedEventHandler getBalanceCompleted;
        
        /// <remarks/>
        public event sendMessageCompletedEventHandler sendMessageCompleted;
        
        /// <remarks/>
        public event sendMMSCompletedEventHandler sendMMSCompleted;
        
        /// <remarks/>
        public event sendChatCompletedEventHandler sendChatCompleted;
        
        /// <remarks/>
        public event getChatCompletedEventHandler getChatCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/changePassWord", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int changePassWord(string username, string password, string newpwd) {
            object[] results = this.Invoke("changePassWord", new object[] {
                        username,
                        password,
                        newpwd});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void changePassWordAsync(string username, string password, string newpwd) {
            this.changePassWordAsync(username, password, newpwd, null);
        }
        
        /// <remarks/>
        public void changePassWordAsync(string username, string password, string newpwd, object userState) {
            if ((this.changePassWordOperationCompleted == null)) {
                this.changePassWordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnchangePassWordOperationCompleted);
            }
            this.InvokeAsync("changePassWord", new object[] {
                        username,
                        password,
                        newpwd}, this.changePassWordOperationCompleted, userState);
        }
        
        private void OnchangePassWordOperationCompleted(object arg) {
            if ((this.changePassWordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.changePassWordCompleted(this, new changePassWordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/getBalance", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getBalance(string username, string password) {
            object[] results = this.Invoke("getBalance", new object[] {
                        username,
                        password});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getBalanceAsync(string username, string password) {
            this.getBalanceAsync(username, password, null);
        }
        
        /// <remarks/>
        public void getBalanceAsync(string username, string password, object userState) {
            if ((this.getBalanceOperationCompleted == null)) {
                this.getBalanceOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetBalanceOperationCompleted);
            }
            this.InvokeAsync("getBalance", new object[] {
                        username,
                        password}, this.getBalanceOperationCompleted, userState);
        }
        
        private void OngetBalanceOperationCompleted(object arg) {
            if ((this.getBalanceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getBalanceCompleted(this, new getBalanceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/sendMessage", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int sendMessage(string username, string pwd, string phones, string contents, string scode, string setTime) {
            object[] results = this.Invoke("sendMessage", new object[] {
                        username,
                        pwd,
                        phones,
                        contents,
                        scode,
                        setTime});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void sendMessageAsync(string username, string pwd, string phones, string contents, string scode, string setTime) {
            this.sendMessageAsync(username, pwd, phones, contents, scode, setTime, null);
        }
        
        /// <remarks/>
        public void sendMessageAsync(string username, string pwd, string phones, string contents, string scode, string setTime, object userState) {
            if ((this.sendMessageOperationCompleted == null)) {
                this.sendMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendMessageOperationCompleted);
            }
            this.InvokeAsync("sendMessage", new object[] {
                        username,
                        pwd,
                        phones,
                        contents,
                        scode,
                        setTime}, this.sendMessageOperationCompleted, userState);
        }
        
        private void OnsendMessageOperationCompleted(object arg) {
            if ((this.sendMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendMessageCompleted(this, new sendMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/sendMMS", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int sendMMS(string username, string pwd, string phones, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] mmsBytes, string scode, string setTime) {
            object[] results = this.Invoke("sendMMS", new object[] {
                        username,
                        pwd,
                        phones,
                        mmsBytes,
                        scode,
                        setTime});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void sendMMSAsync(string username, string pwd, string phones, byte[] mmsBytes, string scode, string setTime) {
            this.sendMMSAsync(username, pwd, phones, mmsBytes, scode, setTime, null);
        }
        
        /// <remarks/>
        public void sendMMSAsync(string username, string pwd, string phones, byte[] mmsBytes, string scode, string setTime, object userState) {
            if ((this.sendMMSOperationCompleted == null)) {
                this.sendMMSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendMMSOperationCompleted);
            }
            this.InvokeAsync("sendMMS", new object[] {
                        username,
                        pwd,
                        phones,
                        mmsBytes,
                        scode,
                        setTime}, this.sendMMSOperationCompleted, userState);
        }
        
        private void OnsendMMSOperationCompleted(object arg) {
            if ((this.sendMMSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendMMSCompleted(this, new sendMMSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/sendChat", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int sendChat(string username, string pwd, string phones, string contents, string scode, string setTime) {
            object[] results = this.Invoke("sendChat", new object[] {
                        username,
                        pwd,
                        phones,
                        contents,
                        scode,
                        setTime});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void sendChatAsync(string username, string pwd, string phones, string contents, string scode, string setTime) {
            this.sendChatAsync(username, pwd, phones, contents, scode, setTime, null);
        }
        
        /// <remarks/>
        public void sendChatAsync(string username, string pwd, string phones, string contents, string scode, string setTime, object userState) {
            if ((this.sendChatOperationCompleted == null)) {
                this.sendChatOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendChatOperationCompleted);
            }
            this.InvokeAsync("sendChat", new object[] {
                        username,
                        pwd,
                        phones,
                        contents,
                        scode,
                        setTime}, this.sendChatOperationCompleted, userState);
        }
        
        private void OnsendChatOperationCompleted(object arg) {
            if ((this.sendChatCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendChatCompleted(this, new sendChatCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.82009668.com/getChat", RequestNamespace="http://www.82009668.com/", ResponseNamespace="http://www.82009668.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getChat(string username, string pwd, string scode) {
            object[] results = this.Invoke("getChat", new object[] {
                        username,
                        pwd,
                        scode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getChatAsync(string username, string pwd, string scode) {
            this.getChatAsync(username, pwd, scode, null);
        }
        
        /// <remarks/>
        public void getChatAsync(string username, string pwd, string scode, object userState) {
            if ((this.getChatOperationCompleted == null)) {
                this.getChatOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetChatOperationCompleted);
            }
            this.InvokeAsync("getChat", new object[] {
                        username,
                        pwd,
                        scode}, this.getChatOperationCompleted, userState);
        }
        
        private void OngetChatOperationCompleted(object arg) {
            if ((this.getChatCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getChatCompleted(this, new getChatCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void changePassWordCompletedEventHandler(object sender, changePassWordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class changePassWordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal changePassWordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getBalanceCompletedEventHandler(object sender, getBalanceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getBalanceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getBalanceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void sendMessageCompletedEventHandler(object sender, sendMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void sendMMSCompletedEventHandler(object sender, sendMMSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendMMSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendMMSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void sendChatCompletedEventHandler(object sender, sendChatCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendChatCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendChatCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getChatCompletedEventHandler(object sender, getChatCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getChatCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getChatCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591