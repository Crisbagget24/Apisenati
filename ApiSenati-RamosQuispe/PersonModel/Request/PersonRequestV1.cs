using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSenati.Models.Request
{
    public class PersonRequestV1
    {
        public int  Id { get; set; }

        public string Nombre { get; set; }

        public string Ciudad { get; set; }

    }
}