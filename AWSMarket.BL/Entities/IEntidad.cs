using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AWSMarket.BL.Entities
{
    public class IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
