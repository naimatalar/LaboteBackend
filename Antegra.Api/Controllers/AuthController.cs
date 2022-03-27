using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : LaboteControllerBase
    {
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly LaboteContext _context;

        public AuthController(UserManager<LaboteUser> userManager, LaboteContext context, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Login(LoginRequestModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var userFromMail = await _userManager.FindByEmailAsync(model.UserName);

                if (user == null && userFromMail == null)
                {
                    return Ok(new
                    {
                        token = "",
                        expiration = "",
                        error = true
                    });
                }
                user = user == null ? userFromMail : user;
                if ((user != null || userFromMail != null) && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString()),

                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Enums.SecretKey));
                    var token = new JwtSecurityToken(
                    //  issuer: _configuration[“JWT: ValidIssuer”],
                    //audience: _configuration[“JWT: ValidAudience”],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        error = false
                    });
                }
            }
            catch (Exception e)
            {
            }

            return Ok(new
            {
                token = "",
                expiration = "",
                error = true
            });
        }

        [AllowAnonymous]
        [HttpGet("CheckLogin")]
        public async Task<ActionResult<dynamic>> CheckLogin()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Ok(new
                {

                    UserExist = false,
                    Auth = false
                });

            }
            var userId = User.Identity.UserId();
            var userExist = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (userExist == null)
            {
                return Ok(new
                {

                    UserExist = false,
                    Auth = false
                });
            }

            return Ok(new
            {

                UserExist = userExist,
                Auth = User.Identity.IsAuthenticated
            });



            return Unauthorized();
        }




        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<ActionResult<dynamic>> SignUp(UserCreateRequestModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var userTopic = new UserTopic
                    {
                        Code = Guid.NewGuid().ToString("n"),

                    };
                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransactionAsync())
                        {
                            context.UserTopics.Add(userTopic);
                        }
                    }


                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransactionAsync())
                        {
                            LaboteUser user = new LaboteUser();
                            if (_userManager.Users.Count() == 0)
                            {
                                user = new LaboteUser()
                                {
                                    Email = model.Email,
                                    SecurityStamp = Guid.NewGuid().ToString(),
                                    UserName = model.UserName,
                                    FirstName = model.FirstName,
                                    Lastname = model.LastName,
                                    NotDelete = true

                                };
                                var usr = _userManager.CreateAsync(user, model.Password).Result;
                            }

                            var RoleAdd = _userManager.AddToRoleAsync(user, Enums.Admin).Result;

                        };
                    }


                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            var data = context.MenuModules.ToList();
                            

                            foreach (var item in data)
                            {
                                context.UserMenuModules.Add(new UserMenuModule
                                {
                                    //UserRoleId = role.RoleId,
                                    MenuModelId = item.Id
                                });
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        };
                    }

                }
            }
            catch (Exception e)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Hatalı giriş";
            }

            return PageResponse;


        }

    }
}
