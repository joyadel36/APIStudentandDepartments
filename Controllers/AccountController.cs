using API_FinalTask.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_FinalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> userManager,IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        [HttpPost("Register")] //Create User [Identity]
        public async Task<IActionResult> Registaration(RegisterDTO registerUser)
        {
            if(ModelState.IsValid)
            {
                //Save
                IdentityUser user = new IdentityUser();
                user.Email = registerUser.Emil;
                user.UserName = registerUser.UserName;
                IdentityResult res = await _userManager.CreateAsync(user, registerUser.Password);
                if(res.Succeeded)
                {
                    return Ok("Account Added Succefully");
                }
                var ErrList = new List<string>();
                foreach(var err in res.Errors)
                {
                    ErrList.Add(err.Description);
                }
                return BadRequest(ErrList);
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")] //Verify User & Create Token
        public async Task<IActionResult> Login(loginDTO loginUser)
        {
            if(ModelState.IsValid)
            {
                IdentityUser userFromDB =await _userManager.FindByNameAsync(loginUser.UserName);
                if (userFromDB is null)
                {
                    return Unauthorized();
                }
                 bool found = await _userManager.CheckPasswordAsync(userFromDB, loginUser.Password);
                if (found)
                {
                  
                    var Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                    Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    //Check Roles User
                    var Roles = await _userManager.GetRolesAsync(userFromDB);
                    foreach (var role in Roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    

                   
                    SecurityKey SecretKey =
                                  new SymmetricSecurityKey(
                                      Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                    SigningCredentials SignCred =
                        new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                
                    JwtSecurityToken token = new JwtSecurityToken(
                                  issuer: _config["JWT:ValidateAssure"],
                                  audience: _config["JWT:ValidateAudiance"],
                                  claims: Claims,
                                  expires: DateTime.Now.AddDays(14),
                                  signingCredentials: SignCred
                                  ); 
              

                    return Ok(
                        new
                        {
                            token =new JwtSecurityTokenHandler().WriteToken(token),
                            exp=token.ValidTo
                        }
                        );
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
