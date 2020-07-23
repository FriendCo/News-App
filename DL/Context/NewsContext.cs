using DomainClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Context
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options):base(options)
        {

        }

        public DbSet<PagesTB> Pages { get; set; }
        public DbSet<PageGroups> PageGroups { get; set; }
    }
}
