using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponseModels;
using FinalProjectAPBD.Repositories;

namespace FinalProjectAPBD.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;

    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }


    public async Task RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Role = model.Role,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTockenExp = DateTime.Now.AddDays(1)
        };
        await _registrationRepository.RegisterUser(user);
    }

    public async Task<LoginResponceModel> Login(LoginRequest model)
    {
        var response = await _registrationRepository.Login(model);
        return response;
    }

    public async Task<LoginResponceModel> Refresh(RefreshTokenRequest model)
    {
        var responce = await _registrationRepository.Refresh(model);
        return responce;
    }
}