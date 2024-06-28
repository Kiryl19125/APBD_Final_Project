using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponseModels;

namespace FinalProjectAPBD.Services;

public interface IRegistrationService
{
    
    public Task RegisterUser(RegisterRequest model);
    public Task<LoginResponceModel> Login(LoginRequest model);

    public Task<LoginResponceModel> Refresh(RefreshTokenRequest model);
}