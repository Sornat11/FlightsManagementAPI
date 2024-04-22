using FlightsManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightsManagementAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(UserDto request);
        Task<string> Login(UserDto request);
    }
}