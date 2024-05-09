using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Data
{
    public class Applicationdbcontext : IdentityDbContext<Applicationuser>
    {

        public DbSet<Applicationuser> Applicationusers { get; set; }
        public DbSet<Applicationsite> Applicationsites { get; set; }
        public DbSet<Applicationtenant> Applicationtenants { get; set; }
        public DbSet<Applicationpage> Applicationpages { get; set; }
        public DbSet<Applicationcontainer> Applicationcontainers { get; set; }
        public DbSet<Applicationcontainercolumn> Applicationcontainercolumns { get; set; }
        public DbSet<Applicationmodulepackage> Applicationmodulepackages { get; set; }
        public DbSet<Applicationmodule> Applicationmodules { get; set; }
        public DbSet<Applicationmodulesettings> Applicationmodulesettings { get; set; }
        public DbSet<Applicationmediafile> Applicationmediafiles { get; set; }

        public Applicationdbcontext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try { base.OnModelCreating(builder); } catch { }
        }

    }
}
