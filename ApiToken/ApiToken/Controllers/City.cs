using ApiToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken.Controllers
{
    [Authorize]
    [ApiController]
    public class City : ControllerBase
    {
        [HttpGet]
        [Route("[controller]/getsecurecitylist")]
        public IEnumerable<string> SecureCityList()
        {
            List<string> list = new List<string>()
            {
                "Adana","Antep","Urfa"
            };
            return list;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/getcitylist")]
        public IEnumerable<Models.Sehirler> CityList()
        {
            using (Db db = new Db())
            {
                return db.Sehirlers.ToList();
            }
        }
    }
}
