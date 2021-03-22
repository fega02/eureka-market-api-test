using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AWSMarket.BL.Entities.Security
{
   
    [Owned]
    public class RefreshToken
    {

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string Token { get; set; }
        [NotMapped]
        public DateTime Expires { get; set; }
        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [NotMapped]
        public DateTime Created { get; set; }
        [NotMapped]
        public string CreatedByIp { get; set; }
        [NotMapped]
        public DateTime? Revoked { get; set; }
        [NotMapped]
        public string RevokedByIp { get; set; }
        [NotMapped]
        public string ReplacedByToken { get; set; }
        [NotMapped]
        public bool IsActive => Revoked == null && !IsExpired;
    }
}