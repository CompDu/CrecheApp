using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CrecheApp.Models;

namespace CrecheApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CrecheApp.Models.ForumReplyModel>? ForumReplyModel { get; set; }
        public DbSet<CrecheApp.Models.ForumModel>? ForumModel { get; set; }
        public DbSet<CrecheApp.Models.ChildModel> ChildModel { get; set; }
        public DbSet<CrecheApp.Models.ParentModel> ParentModel { get; set; }
    }
}