using day1.DTO;
using day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<AppUser> app, IConfiguration config)
        {
            this.userManager = app;
            this.config = config;
        }
        [HttpPost]
            public async Task<IActionResult> Register(RegisterWithDTO register)
            {
                if (ModelState.IsValid)
            { 
               AppUser user = new AppUser()
               {
                   UserName = register.UserName,
                   Email=register.Email,
                  
               };
                IdentityResult result = await userManager.CreateAsync(user,register.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Create Success");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest("error");
        }
        [HttpPost("login")]//api/accuont/login
        public async Task<IActionResult> Login(LoginWithDTO userFromConsumer)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(userFromConsumer.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userFromConsumer.Password);
                    if (found)
                    {
                        #region Create Token
                        string jti = Guid.NewGuid().ToString();
                        var userRoles = await userManager.GetRolesAsync(user);


                        List<Claim> claim = new List<Claim>();
                        claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claim.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claim.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));
                        if (userRoles != null)
                        {
                            foreach (var role in userRoles)
                            {
                                claim.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }
                        SymmetricSecurityKey signinKey =
                            new(Encoding.UTF8.GetBytes(config["JWT:Key"]));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: config["JWT:Iss"],
                            audience: config["JWT:Aud"],
                            expires: DateTime.Now.AddHours(1),
                            claims: claim,
                            signingCredentials: signingCredentials
                            );

                        return Ok(new
                        {
                            expired = DateTime.Now.AddHours(1),
                            token = new JwtSecurityTokenHandler().WriteToken(Token)
                        });
                        #endregion
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }
    }
}
   