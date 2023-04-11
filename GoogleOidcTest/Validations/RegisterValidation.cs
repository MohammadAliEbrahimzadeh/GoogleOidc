using FluentValidation;
using GoogleOidcTest.DTOs;

namespace GoogleOidcTest.Validations;

public class RegisterValidation : AbstractValidator<RegisterDto>
{
        public RegisterValidation()
        {
                RuleFor(x=>x.Username).NotEmpty().NotNull();
                RuleFor(x=>x.Password).NotEmpty().NotNull();
        }
}
