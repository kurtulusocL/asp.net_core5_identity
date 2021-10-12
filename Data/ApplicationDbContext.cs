using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=.;Database=IdentityCore;user Id=sa;Password=123");
            base.OnConfiguring(optionBuilder);
        }
    }
}
