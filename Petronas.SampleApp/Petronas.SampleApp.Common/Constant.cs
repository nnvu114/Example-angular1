namespace Petronas.SampleApp.Common
{
    public static class Constants
    {
        public const int DefaultPageSize = 20;
        public const int DefaultPageSizeForNews = 12;
        public const int DefaultNoOfItemForRss = 20;
        public const string ActiveStatus = "Active";
        public const string OrderByDefault = "InsertedAt";
        public const string InactiveStatus = "Inactive";
        public const string DefaultDateFormat = "dd/MM/yyyy";
        public const string LocalIpAddress = "::1";

        public static class Configuration
        {
            public static readonly string ApplicationId = "ApplicationId";
            public static readonly string MockPassword = "MockPass";
            public static readonly string SSOApiBaseUrl = "SSOApiBaseUrl";
            public static readonly string SSOLogout = "SSOLogout";
            public static readonly string SSOWebAuthLogin = "SSOWebAuthLogin";
            public static readonly string LdapPathPetronas = "LdapPathPetronas";
            public static readonly string LdapPathKLCC = "LdapPathKLCC";
            public static readonly string LdapUsername = "LdapUsername";
            public static readonly string LdapPassword = "LdapPassword";
            public static readonly string Domain = "Domain";
            public static readonly string SMTPServer = "SMTPServer";
            public static readonly string SMTPUser = "SMTPUser";
            public static readonly string SMTPPassword = "SMTPPassword";
            public static readonly string SMTPPort = "SMTPPort";
            public static readonly string SMTPEnableSSL = "SMTPEnableSSL";
            public static readonly string SkillWebUrl = "SkillWebUrl";
            public static readonly string KnowledgeDetailPath = "KnowledgeDetailPath";
            public static readonly string EmailFrom = "OR2E@petronas.com.my";
            public static readonly string SkipSendingEmail = "SkipSendingEmail";
            public static readonly string TrendingDaysRange = "TrendingDaysRange";
            public static readonly string IsSendMail = "IsSendMail";
            public static readonly string ApiUrl = "ApiUrl";

            public static readonly int LinkViaEmailLifeTime = 6;
            public static readonly string CaptchaPrivateKey = "CaptchaPrivateKey";
        }

        public static class Global
        {
            public static readonly int DefaultPageSize = 5;
            public static readonly string AttachmentsFolderPath = "Attachments";
            public static readonly string AccessToken = "AccessToken";
            public static readonly string RequestWithSingleParameter = "Request : {0}";
            public static readonly string ResponseWithSingleParameter = "Response : {0}";
            public static readonly string MediaTypeJson = "application/json";

            public static readonly int SessionTimeoutCode = 30000;
        }

        public static class ErrorMessage
        {
            public static readonly string NonConfirmedEmail = "Email has not been confirmed";
            public static readonly string MissedFields = "User Id and activation link are required";
            public static readonly string UserNotFound = "User: {0} does not exists";
            public static readonly string RoleNotExist = "Role does not exist";
            public static readonly string UserCannotRemoveFromRole = "User: {0} could not be removed from role";
            public static readonly string UserCannotAddToRole = "User: {0} could not be added to role";

            public static readonly string SessionTimeoutMessage = "Session is expired";
        }

        public static class EmailMassage
        {
            public static readonly string SubjectResetPassword = "Reset Password";
            public static readonly string BodyResetPassword = "Please click on the following link to reset password: ";
            public static readonly string SubjectConfirmEmail = "Confirm your account";
            public static readonly string BodyConfirmEmail = "Please confirm your account by clicking <a href=\"{0} \">here</a>";
        }

        public static class ResponseMessage
        {
            public static readonly string Success = "Success";
            public static readonly string UnSuccessLogin = "The user name or password is incorrect";
        }

        public static class CompanyMessage
        {
            public static readonly string ExistedUsername = "Username has existed in the database";
            public static readonly string ExistedCompanyNo = "Company's code has existed in the database";
            public static readonly string ExistedEmail = "Company's email has existed in the database";
        }
    }
}
