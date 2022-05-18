using ApiToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiToken.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class TokenController : ControllerBase
  {
    private IConfiguration _configuration;
    public TokenController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    /// <summary>
    /// Kullanıcı adı ve şifre gönderiniz..
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Token</returns>
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] Token request)
    {
      if (ModelState.IsValid)
      {
        var user = _userService(request.UserName, request.Password);
        if (user == null)
        {
          return Unauthorized();
        }

        var claims = new[]
        {
                    new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

        var token = new JwtSecurityToken
        (
            issuer: _configuration["Issuer"], //appsettings.json içerisinde bulunan issuer değeri
            audience: _configuration["Audience"],//appsettings.json içerisinde bulunan audince değeri
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30), // 30 gün geçerli olacak
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),//appsettings.json içerisinde bulunan signingkey değeri
                    SecurityAlgorithms.HmacSha256)
        );
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
      }
      return BadRequest();
    }


    private User _userService(string userName, string password)
    {
      User user = null;

      using (Db db = new Db())
      {
        if (db.Kullanicilars.Where(k => k.KullaniciAdi == userName && k.Sifre == password).Any()) //kayıt varsa
        {
          user = new User
          {
            UserName = userName,
            Password = password
          };
        }

        return user;
      }

    }
  }

  public class Token
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }

  public class User
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}
