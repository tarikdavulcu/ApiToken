using System;
using System.Collections.Generic;

#nullable disable

namespace ApiToken.Models
{
    public partial class Kullanicilar
    {
        public long Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public bool? Durumu { get; set; }
    }
}
