using AccessDB.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLogic.Models
{
    public class SourceType
    {
        public int Type { get; set; }
        public string CommentString { get; set; }

        public SourceType(SourceTypeDTO sourceType)
        {
            Type = sourceType.Type;
            CommentString = sourceType.CommentString;
        }
    }
}
