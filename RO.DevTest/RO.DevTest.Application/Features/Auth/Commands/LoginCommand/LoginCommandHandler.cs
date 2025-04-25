using MediatR;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RO.DevTest.Domain.Entities;
using System.Text;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse> {
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<User> userManager, IConfiguration configuration) {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null) {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid) {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new[] {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, roles.Count > 0 ? roles[0] : "Customer")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse { Token = tokenString };
    }
}
