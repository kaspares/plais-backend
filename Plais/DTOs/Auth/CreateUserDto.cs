namespace Plais.DTOs.Auth
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
