﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObject.Shared.Models.BlogImages;
using BusinessObject.Shared.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Shared.Models.Blogs;

[Table("Blog")]
public partial class Blog
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PublishDate { get; set; }

    public int? Views { get; set; }

    [StringLength(255)]
    public string Tags { get; set; }

    public bool IsFeatured { get; set; }

    [StringLength(50)]
    public string AuthorName { get; set; }

    [InverseProperty("Blog")]
    public virtual ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
}