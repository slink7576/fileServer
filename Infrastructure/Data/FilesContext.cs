using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class FilesContext : DbContext
    {
        public DbSet<FileData> Files { get; set; }
        public FilesContext(DbContextOptions<FilesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
