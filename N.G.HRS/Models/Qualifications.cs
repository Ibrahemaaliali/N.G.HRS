﻿using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
{
    public class Qualifications
    {
        //يرتبط بجدول (المؤهل) و(التخصص) و(الحامعات) و (الموظفن)
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الحصول علية")]
        public DateOnly ReceivedDate { get; set; }
    }
}
