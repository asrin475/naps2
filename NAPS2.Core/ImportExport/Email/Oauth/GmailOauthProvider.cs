﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAPS2.Config;
using Newtonsoft.Json.Linq;

namespace NAPS2.ImportExport.Email.Oauth
{
    public class GmailOauthProvider : OauthProvider
    {
        private readonly IUserConfigManager userConfigManager;

        private OauthClientCreds creds;

        public GmailOauthProvider(IUserConfigManager userConfigManager)
        {
            this.userConfigManager = userConfigManager;
        }

        #region Authorization

        public override OauthToken Token => userConfigManager.Config.EmailSetup?.GmailToken;

        public override string User => userConfigManager.Config.EmailSetup?.GmailUser;

        protected override OauthClientCreds ClientCreds
        {
            get
            {
                if (creds == null)
                {
                    var credObj = JObject.Parse(Encoding.UTF8.GetString(NAPS2.ClientCreds.google_credentials));
                    var installed = credObj.Value<JObject>("installed");
                    creds = new OauthClientCreds(installed?.Value<string>("client_id"), installed?.Value<string>("client_secret"));
                }
                return creds;
            }
        }

        protected override string Scope => "https://www.googleapis.com/auth/gmail.compose";

        protected override string CodeEndpoint => "https://accounts.google.com/o/oauth2/v2/auth";

        protected override string TokenEndpoint => "https://www.googleapis.com/oauth2/v4/token";

        protected override void SaveToken(OauthToken token)
        {
            userConfigManager.Config.EmailSetup = userConfigManager.Config.EmailSetup ?? new EmailSetup();
            userConfigManager.Config.EmailSetup.GmailToken = token;
            userConfigManager.Config.EmailSetup.GmailUser = GetEmailAddress();
            userConfigManager.Config.EmailSetup.ProviderType = EmailProviderType.Gmail;
            userConfigManager.Save();
        }

        #endregion

        #region Api Methods

        public string GetEmailAddress()
        {
            var resp = GetAuthorized("https://www.googleapis.com/gmail/v1/users/me/profile");
            return resp.Value<string>("emailAddress");
        }

        public string UploadDraft(string messageRaw)
        {
            var resp = PostAuthorized($"https://www.googleapis.com/upload/gmail/v1/users/{User}/drafts?uploadType=multipart", messageRaw, "message/rfc822");
            return resp.Value<JObject>("message").Value<string>("id");
        }

        #endregion
    }
}
