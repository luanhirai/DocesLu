namespace DocesLu.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = true;
    }
}
