using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProjectAPBD.Context;
using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalProjectAPBD.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;

    public RegistrationRepository(DBContext context, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task RegisterUser(AppUser user)
    {
        await _context.AppUsers.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<LoginResponceModel> Login(LoginRequest model)
    {
        AppUser user = await _context.AppUsers.Where(u => u.Login == model.Login).FirstOrDefaultAsync();

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(model.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            throw new Exception("Unathorize"); // TODO! make custom exception
        }

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToLower()),

            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            // issuer: "https://localhost:5278", // TODO! load from configuration
            // audience: "https://localhost:5278",
            
            issuer: _configuration["ValidIssuer"],
            audience: _configuration["ValidAudience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTockenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();
        var responce = new LoginResponceModel()
        {
            AccessAToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };

        return responce;
    }

    public async Task<LoginResponceModel> Refresh(RefreshTokenRequest model)
    {
        AppUser user = await _context.AppUsers.Where(u => u.RefreshToken == model.RefreshToken)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTockenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToLower())
            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            // issuer: "https://localhost:5278", // TODO! load from configuration
            // audience: "https://localhost:5278",
            issuer: _configuration["ValidIssuer"],
            audience: _configuration["ValidAudience"],
            
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTockenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();
        var response = new LoginResponceModel()
        {
            AccessAToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = user.RefreshToken
        };

        return response;
    }
}