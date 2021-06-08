using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;
using AccessDB.DTO;

namespace ModelLogic.Models
{
    public class Destination
    {
        public string Ip { get; }
        public int Type { get; }

        public Destination(DestinationDTO data)
        {
            Ip = data.Ip;
            Type = data.Type;
        }
    }
}
