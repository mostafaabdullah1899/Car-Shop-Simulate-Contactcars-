using Grade_Project_.DTO;
using Grade_Project_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Grade_Project_.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AccountController : ControllerBase
        {
            private readonly Microsoft.AspNetCore.Identity.UserManager<Users> usermanager; 
            private readonly IConfiguration conf;

            public AccountController(UserManager<Users> usermanager, IConfiguration conf) 
            { this.usermanager = usermanager; this.conf = conf; }

            [HttpPost("register")] 
            public async Task<IActionResult> registerUser(registerUserDto _user)
            { 
            
            if (ModelState.IsValid  ) 
            { 
                Users user = new Users();
                user.Full_Name = _user.Full_Name;
                user.UserName = _user.Email; 
                user.Email = _user.Email; 
                user.PasswordHash = _user.PassWord; 
                user.User_Address = _user.Address; 
                user.PhoneNumber = _user.Phone; 
                IdentityResult result = await usermanager.CreateAsync(user, _user.PassWord); 
                if (result.Succeeded) 
                { 
                    return Ok("Account saved"); 
                } 
                else 
                { 
                    return BadRequest(result.Errors); 
                } 
             }
                return BadRequest(ModelState);
        }

            [HttpPost("login")]
            public async Task<IActionResult> login(loginUserDto log)
            {
                if (ModelState.IsValid )
                {
                    Users user = await usermanager.FindByEmailAsync(log.Email);
                if (user != null)
                {

                    bool found = await usermanager.CheckPasswordAsync(user, log.Password);
                    if (found)
                    {

                        if (user.Is_Active == true)
                        {
                            SecurityKey securityKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

                            SigningCredentials signingCredentials =
                                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                            var claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                            var anyRole = await usermanager.GetRolesAsync(user);
                            foreach (var role in anyRole)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }
                            JwtSecurityToken mytoken = new JwtSecurityToken(
                                issuer: conf["JWT:ValidIssuer"],
                                audience: conf["JWT:ValidAudiance"],
                                claims: claims,
                                expires: DateTime.Now.AddHours(3),
                                signingCredentials: signingCredentials
                                );
                            return Ok(new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration = mytoken.ValidTo,
                                user.Id
                            });
                        }
                        return BadRequest("you are not active user");

                    }
                }
                return Unauthorized();

            }
            return Unauthorized();
            }
        }
    }




