﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        public List<Governorate> governoratesList { get; set; }

    }
}
