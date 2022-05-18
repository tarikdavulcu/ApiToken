using ApiToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken.Controllers
{
  [Authorize]
  [ApiController]
  public class City : ControllerBase
  {
    /// <summary>
    /// Token zorunlu..
    /// </summary>
    /// <returns>City</returns>
    [HttpGet]
    [Route("[controller]/GetSecureCityList")]
    
    public IEnumerable<Models.City> SecureCityList([FromHeader(Name = "Authorization")][Required] string token)
    {
      List<Models.City> list = new List<Models.City>();
      Models.City s = new();
      s.Id = 1; s.Name = "Mugla";
      list.Add(s);

      Models.City ss = new Models.City();
      ss.Id = 2; ss.Name = "Antalya";
      list.Add(ss);

      Models.City sss = new Models.City();
      sss.Id = 3; sss.Name = "Balıkesir";
      list.Add(sss);
      return list;
    }


    /// <summary>
    /// Token zorunlu degil.. Herkes kullanabilir.
    /// </summary>
    /// <returns>City</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("[controller]/GetCityList")]
    public IEnumerable<Models.City> CityList()
    {
      using (Db db = new Db())
      {
        return db.Sehirlers.ToList();
      }
    }
  }
}
