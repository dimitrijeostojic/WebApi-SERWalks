using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SERWalks.API.Models.DTO;
using SERWalks.API.Repositories;

namespace SERWalks.API.Controllers
{
    // Kontroler za autentikaciju korisnika
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        // Konstruktor koji prima UserManager za upravljanje korisnicima
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POST: https://localhost:7081/api/Auth/Register
        [HttpPost]
        [Route("Register")]
        // Metoda za registraciju korisnika
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            // Kreiranje IdentityUser objekta sa podacima iz zahteva
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };
            // Kreiranje korisnika u sistemu
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            // Dodavanje uloga korisniku ako je registracija uspešna
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered, please Login");
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }

        // POST: https://localhost:7081/api/Auth/Login
        [HttpPost]
        [Route("Login")]
        // Metoda za prijavu korisnika
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            // Pronalaženje korisnika po email adresi
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                // Provera lozinke
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Kreiranje tokena
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Wrong credentials!");
        }


    }
}
