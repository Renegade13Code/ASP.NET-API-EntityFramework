using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Domain;
using webAPI.Models.DTO;
using webAPI.Repository;

namespace webAPI.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly IMyTokenHandler myTokenHandler;

        public AuthController(IUserRepository userRepo, IMyTokenHandler myTokenHandler)
        {
            this.userRepo = userRepo;
            this.myTokenHandler = myTokenHandler;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> UserLogin(LoginRequest loginRequest)
        {
            // Authenticate user
            User? user =  await userRepo.Authenticate(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //System.Diagnostics.Debug.WriteLine(user);
                var token = await myTokenHandler.CreateToken(user);
                return Ok(token);

            }

            return BadRequest("Username or password is incorrect");
        }
    }
}
