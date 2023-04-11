namespace GoogleOidcTest.DTOs;

public class LoginDto
{
        public string? Password { get; set; }
        public string? Username { get; set; }

        public bool RememberMe { get; set; }
}
