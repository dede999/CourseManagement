namespace Api.Domain.DTOs;

public record SignUpDto(string email, string password, string name)
{
    public void Deconstruct(out string email, out string password, out string name)
    {
        email = this.email;
        password = this.password;
        name = this.name;
    }
}