namespace RestaurantApi
{
    public class Authentication
    {
        public string JwtKey { get; set; }
        public double JwtExpire { get; set; }
        public string JwtIssuer { get; set; }
    }
}
