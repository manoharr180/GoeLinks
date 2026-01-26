public class LoginOtpVerifyModel
    {
        // either ProfileId or identifier can be used; this mock expects ProfileId
        public int ProfileId { get; set; }
        public string Otp { get; set; }
    }