namespace NewsletterAPI.Models
{
    [Serializable]
    public class JwtFeilds
    {
        //base64
        public string token { get; set; }
        public string user_name { get; set; }
        public int expire_time { get; set; }
    }
}