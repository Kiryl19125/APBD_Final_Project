
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponseModels;

namespace FinalProjectAPBD.Repositories;

public interface IRegistrationRepository
{
    public Task RegisterUser(AppUser user);
    public Task<LoginResponceModel> Login(LoginRequest model);
    public Task<LoginResponceModel> Refresh(RefreshTokenRequest model);
}