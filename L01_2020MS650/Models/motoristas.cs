﻿using System.ComponentModel.DataAnnotations;
namespace L01_2020MS650.Models
{
    public class motoristas
    {
        [Key]
        public int motoristaId { get; set; }

        public string nombreMotorista { get; set; }

    }
}
