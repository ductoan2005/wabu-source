namespace FW.Common.Helpers
{
    public static class CommonConstants
    {
        #region STATUSCODE
        
        //Success
        public const string STR_ZERO = "0";

        //Error
        public const string STR_ONE = "1";
        public const string STR_MINUS_ONE = "-1";

        //Warning
        public const string STR_TWO = "2";

        public const string STR_THREE = "3"; // SEND EMAIL ERROR

        #endregion 

        public const string HYPHEN = "-";
        public const string PLUS = "+";

        public const int SQL_DUPLICATE_PK_CODE = 1062;
        public const int SQL_ID_IS_USE = 1451;
        public const string SQL_DUPLICATE_PK_PHRASE = "#23000";


        #region JsonMessage

        public const string SEND_EMAIL_SUCCESS = "Gửi email thành công";

        public const string NEED_UPDATE_COMPANY_INFO = "Vui lòng cập nhật thông tin công ty trước khi tạo hồ sơ năng lực";

        public const string NEED_UPDATE_USER_INFO = "Vui lòng cập nhật đầy đủ thông tin tài khoản trước khi tạo hồ sơ năng lực";

        public const string ADD_SUCCESS = "Thêm thành công";

        public const string ADD_ERROR = "Có lỗi xảy ra vui lòng liên hệ admin";

        public const string UPDATE_SUCCESS = "Cập nhật thành công";

        #endregion

        #region ROLE
        public const string ROLE_1_MINUS = "-1";
        public const string ROLE_0 = "0";
        public const string ROLE_1 = "1";
        public const string ROLE_2 = "2";
        public const string ROLE_3 = "3";
        public const string ROLE_1_MINUS_NAME = "GUEST";
        public const string ROLE_0_NAME = "ROOT";
        public const string ROLE_1_NAME = "ADMINISTRATOR";
        public const string ROLE_2_NAME = "INVESTOR";
        public const string ROLE_3_NAME = "CONTRACTOR";
        public const string AUTHORITY_NOT_MATCH = "AUTHORITY_NOT_MATCH";
        #endregion

        #region SCREEN LOGIN
        public const string SCR_LOGIN_WARN_ID_1 = "1";
        public const string SCR_LOGIN_WARN_ID_1_MINUS = "-1";
        public const string SCR_LOGIN_WARN_ID_2 = "2";
        public const string SCR_LOGIN_WARN_ID_5 = "5";
        #endregion

        #region SCREEN NAME
        public const string SCREEN_ACCOUNT = "SCR_ACCOUNT"; //AccountController
        public const string SCREEN_HOME = "SCREEN_HOME"; //HomeController
        public const string SCREEN_BIDDING_NEWS_REGISTRATION = "SCREEN_BIDDING_NEWS_REGISTRATION";
        public const string SCREEN_BID_CONTRACTION_DETAIL = "SCREEN_BID_CONTRACTION_DETAIL";
        public const string SCREEN_CONSTRUCTION = "SCR_CONSTRUCTION";
        public const string SCREEN_FILTER_BIDDINGNEWS = "SCREEN_FILTER_BIDDINGNEWS";
        public const string SCREEN_BIDDING_NEWS_BOOKMARK = "SCREEN_BIDDING_NEWS_BOOKMARK";
        public const string SCREEN_ACCOUNT_PAGECONTRACT = "SCREEN_ACCOUNT_PAGECONTRACT";
        public const string SCREEN_PAGECONTRACTBID = "SCREEN_PAGECONTRACTBID";

        #endregion
    }
}
