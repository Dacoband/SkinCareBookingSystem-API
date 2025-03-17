﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComesticStore.Repositories.Models;

[Table("SystemAccount")]
[Index("EmailAddress", Name = "UQ__SystemAc__49A14740E0EEB2DD", IsUnique = true)]
public partial class SystemAccount
{
    [Key]
    [Column("AccountID")]
    public int AccountId { get; set; }

    [Required]
    [StringLength(100)]
    public string AccountPassword { get; set; }

    [StringLength(100)]
    public string EmailAddress { get; set; }

    [Required]
    [StringLength(240)]
    public string AccountNote { get; set; }

    public int? Role { get; set; }
}