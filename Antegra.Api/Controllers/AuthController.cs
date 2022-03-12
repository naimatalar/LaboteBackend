using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly UserManager<AntegraUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly AntegraContext _context;

        public AuthController(UserManager<AntegraUser> userManager, AntegraContext context, RoleManager<UserRole> roleManager)
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

                if (user==null && userFromMail==null)
                {
                    return Ok(new
                    {
                        token = "",
                        expiration = "",
                        error = true
                    });
                }
                user = user == null ? userFromMail : user;
                if ((user != null || userFromMail!=null) && await _userManager.CheckPasswordAsync(user, model.Password))
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
            try
            {
               
                var userExist = _userManager.Users.Any();

                return Ok(new
                {
                   
                    UserExist = userExist,
                    Auth = User.Identity.IsAuthenticated
                });

            }
            catch (Exception e)
            {


            }

            return Unauthorized();
        }




   

        [AllowAnonymous]
        [HttpPost("UserCreate")]
        public async Task<ActionResult<dynamic>> UserCreate(UserCreateRequestModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                 

                    using (AntegraContext context = new AntegraContext())
                    {
                        using (var transaction = context.Database.BeginTransactionAsync())
                        {
                            AntegraUser user = new AntegraUser();
                            if (_userManager.Users.Count() == 0)
                            {
                                user = new AntegraUser()
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


                    using (AntegraContext context = new AntegraContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            var data = context.MenuModules.ToList();
                            var role = context.UserRoles.FirstOrDefault();

                            foreach (var item in data)
                            {
                                context.UserMenuModules.Add(new UserMenuModule{
                                UserRoleId=role.RoleId,
                                MenuModelId=item.Id
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
