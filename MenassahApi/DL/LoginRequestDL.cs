namespace MenassahApi.DL
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }


}
