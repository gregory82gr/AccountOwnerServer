using AccountOwnerServer.Validation;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AccountOwnerServer.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public IConfiguration _configuration;
        private readonly ITokenService _tokenService;



        public TokenController(ILoggerManager logger, 
            IRepositoryWrapper repository, 
            IMapper mapper, 
            IConfiguration config,
            ITokenService tokenService)
            
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _configuration = config;
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        
        [HttpPost("login")]
        public  IActionResult Post(UserInfo _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = _repository.UserInfo.GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                   
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, "Manager"),
                        new Claim(ClaimTypes.Name,user.UserName)

                    };

                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5);

                    _repository.UserInfo.UpdateUser(user);
                    _repository.Save();

                    return Ok(new AuthenticatedResponse 
                    { 
                        AccessToken = accessToken,
                        RefreshToken=refreshToken
                    }); 
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = _repository.UserInfo.GetUserByUserName(username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _repository.UserInfo.UpdateUser(user);
            _repository.Save();
            return Ok(new AuthenticatedResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _repository.UserInfo.GetUserByUserName(username);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _repository.Save();
            return NoContent();
        }

    }
}
