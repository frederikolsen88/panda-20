﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Panda_20.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string fb_token {
            get {
                return ((string)(this["fb_token"]));
            }
            set {
                this["fb_token"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string fb_token_expires_in {
            get {
                return ((string)(this["fb_token_expires_in"]));
            }
            set {
                this["fb_token_expires_in"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2.0")]
        public string version {
            get {
                return ((string)(this["version"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.facebook.com/dialog/oauth?client_id=470029853116845&redirect_uri=http" +
            "s://www.facebook.com/connect/login_success.html&scope=read_stream,manage_pages,r" +
            "ead_mailbox,read_page_mailboxes,offline_access&response_type=token")]
        public string fbUrl {
            get {
                return ((string)(this["fbUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool allow_notifications_comments_from_self {
            get {
                return ((bool)(this["allow_notifications_comments_from_self"]));
            }
            set {
                this["allow_notifications_comments_from_self"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool allow_notifications_posts_from_self {
            get {
                return ((bool)(this["allow_notifications_posts_from_self"]));
            }
            set {
                this["allow_notifications_posts_from_self"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_notifications_posts_from_others {
            get {
                return ((bool)(this["allow_notifications_posts_from_others"]));
            }
            set {
                this["allow_notifications_posts_from_others"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_notifications_comments_from_others {
            get {
                return ((bool)(this["allow_notifications_comments_from_others"]));
            }
            set {
                this["allow_notifications_comments_from_others"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public int notifications_max_amount {
            get {
                return ((int)(this["notifications_max_amount"]));
            }
            set {
                this["notifications_max_amount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool allow_notifications_comments_on_self_posts {
            get {
                return ((bool)(this["allow_notifications_comments_on_self_posts"]));
            }
            set {
                this["allow_notifications_comments_on_self_posts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool run_on_startup {
            get {
                return ((bool)(this["run_on_startup"]));
            }
            set {
                this["run_on_startup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_sound_post {
            get {
                return ((bool)(this["allow_sound_post"]));
            }
            set {
                this["allow_sound_post"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_notifications_pm {
            get {
                return ((bool)(this["allow_notifications_pm"]));
            }
            set {
                this["allow_notifications_pm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_sound_pm {
            get {
                return ((bool)(this["allow_sound_pm"]));
            }
            set {
                this["allow_sound_pm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool allow_sound_comments {
            get {
                return ((bool)(this["allow_sound_comments"]));
            }
            set {
                this["allow_sound_comments"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int alert_time_post {
            get {
                return ((int)(this["alert_time_post"]));
            }
            set {
                this["alert_time_post"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int alert_time_pm {
            get {
                return ((int)(this["alert_time_pm"]));
            }
            set {
                this["alert_time_pm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int alert_time_comment {
            get {
                return ((int)(this["alert_time_comment"]));
            }
            set {
                this["alert_time_comment"] = value;
            }
        }
    }
}
