using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OTCPrice
    {
        public bool Access { get; set; }
        public string[] Data { get; set; }
        public string Msg { get; set; }
    }
}
