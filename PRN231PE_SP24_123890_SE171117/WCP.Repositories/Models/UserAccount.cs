﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WCP.Repositories.Models;

[Table("UserAccount")]
[Index("UserEmail", Name = "UQ__UserAcco__08638DF83F738A26", IsUnique = true)]
public partial class UserAccount
{
    [Key]
    [Column("UserAccountID")]
    public int UserAccountId { get; set; }

    [Required]
    [StringLength(50)]
    public string UserPassword { get; set; }

    [Required]
    [StringLength(70)]
    public string UserFullName { get; set; }

    [StringLength(90)]
    public string UserEmail { get; set; }

    public int? Role { get; set; }
}