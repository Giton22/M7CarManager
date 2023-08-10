using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSocialLogin
{
    public static class Config
    {
        //O365 login settings
        public const string MsClientId = "8589006b-d803-41c3-ac33-201193be1991";
        public const string MsTenant = "common";

        //Facebook login settings
        public const string FbAppId = "443537867618513";

        //Google login settings
        public const string GoogleClientID = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com";
        public const string GoogleClientSecret = "3f6NggMbPtrmIBpgx-MK2xXK";

        //Local login settings
        public const string BaseUrl = "https://localhost:5555";
        public const string Endpont = "auth";
    }
}
