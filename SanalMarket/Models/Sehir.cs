﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanalMarket.Models
{
    [Table("Sehirler")]

    public class Sehir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }

        [Required]
        [StringLength(50)]

        public string SehirAd { get; set; }

        public virtual  List<Siparis> Siparisler { get; set; }

    }
}