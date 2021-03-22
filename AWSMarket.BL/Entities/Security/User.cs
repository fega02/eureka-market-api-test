using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AWSMarket.BL.Entities.Security
{
    public class User : IEntity
    {
        
        public string Name { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }

        public string Email { get; set; }
               
        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    
    }
}
