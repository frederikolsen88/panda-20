﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Panda_20.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
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
        public bool start_with_windows {
            get {
                return ((bool)(this["start_with_windows"]));
            }
            set {
                this["start_with_windows"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool notifications_play_sound {
            get {
                return ((bool)(this["notifications_play_sound"]));
            }
            set {
                this["notifications_play_sound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool posts_display_notifications {
            get {
                return ((bool)(this["posts_display_notifications"]));
            }
            set {
                this["posts_display_notifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool posts_display_own {
            get {
                return ((bool)(this["posts_display_own"]));
            }
            set {
                this["posts_display_own"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool posts_play_sound {
            get {
                return ((bool)(this["posts_play_sound"]));
            }
            set {
                this["posts_play_sound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int posts_time_limit {
            get {
                return ((int)(this["posts_time_limit"]));
            }
            set {
                this["posts_time_limit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool pm_display_notifications {
            get {
                return ((bool)(this["pm_display_notifications"]));
            }
            set {
                this["pm_display_notifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool pm_play_sound {
            get {
                return ((bool)(this["pm_play_sound"]));
            }
            set {
                this["pm_play_sound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int pm_time_limit {
            get {
                return ((int)(this["pm_time_limit"]));
            }
            set {
                this["pm_time_limit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool comments_display_notifications {
            get {
                return ((bool)(this["comments_display_notifications"]));
            }
            set {
                this["comments_display_notifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool comments_display_own {
            get {
                return ((bool)(this["comments_display_own"]));
            }
            set {
                this["comments_display_own"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool comments_play_sound {
            get {
                return ((bool)(this["comments_play_sound"]));
            }
            set {
                this["comments_play_sound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int comments_time_limit {
            get {
                return ((int)(this["comments_time_limit"]));
            }
            set {
                this["comments_time_limit"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfInt xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <int>5</int>
  <int>10</int>
  <int>15</int>
  <int>20</int>
  <int>25</int>
  <int>30</int>
</ArrayOfInt>")]
        public int[] time_limit_values {
            get {
                return ((int[])(this["time_limit_values"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool comments_display_comments_on_own_post {
            get {
                return ((bool)(this["comments_display_comments_on_own_post"]));
            }
            set {
                this["comments_display_comments_on_own_post"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int comments_time_popdown {
            get {
                return ((int)(this["comments_time_popdown"]));
            }
            set {
                this["comments_time_popdown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool comments_time_popdown_enabled {
            get {
                return ((bool)(this["comments_time_popdown_enabled"]));
            }
            set {
                this["comments_time_popdown_enabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int posts_time_popdown {
            get {
                return ((int)(this["posts_time_popdown"]));
            }
            set {
                this["posts_time_popdown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool posts_time_popdown_enabled {
            get {
                return ((bool)(this["posts_time_popdown_enabled"]));
            }
            set {
                this["posts_time_popdown_enabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int pm_time_popdown {
            get {
                return ((int)(this["pm_time_popdown"]));
            }
            set {
                this["pm_time_popdown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool pm_time_popdown_enabled {
            get {
                return ((bool)(this["pm_time_popdown_enabled"]));
            }
            set {
                this["pm_time_popdown_enabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int notifactions_time_limit {
            get {
                return ((int)(this["notifactions_time_limit"]));
            }
            set {
                this["notifactions_time_limit"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Panda2")]
        public string RegistryValueName {
            get {
                return ((string)(this["RegistryValueName"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public long NumberOfTimesRun {
            get {
                return ((long)(this["NumberOfTimesRun"]));
            }
            set {
                this["NumberOfTimesRun"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool colour_warnings {
            get {
                return ((bool)(this["colour_warnings"]));
            }
            set {
                this["colour_warnings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool pm_display_own {
            get {
                return ((bool)(this["pm_display_own"]));
            }
            set {
                this["pm_display_own"] = value;
            }
        }
    }
}
