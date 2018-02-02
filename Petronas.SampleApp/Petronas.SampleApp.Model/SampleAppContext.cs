using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.Interfaces;
using Petronas.SampleApp.Model.NotMapping;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Petronas.SampleApp.Model
{
    public class SampleAppContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid,ApplicationUserLogin,ApplicationUserRole,ApplicationUserClaim>
    {
        public SampleAppContext()
            : base("DefaultConnection")
        {

        }

        public virtual DbSet<Company> Companies { get; set; }
        public DbSet<AuditUser> AuditUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Navigation> Navigations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //set schema
            modelBuilder.HasDefaultSchema("dbo");
           
            //call base function
            base.OnModelCreating(modelBuilder);
        }

        public static SampleAppContext Create()
        {
            return new SampleAppContext();
        }

        public override int SaveChanges()
        {
           // SetModifiedInformation();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifiedInformation();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetModifiedInformation()
        {
            var identityName = Thread.CurrentPrincipal.Identity.Name;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IEntity
                            && x.State != EntityState.Unchanged);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IEntity entity)
                {
                    var currentDateTime = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.InsertedBy = identityName;
                        entity.InsertedAt = currentDateTime;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.InsertedBy).IsModified = false;
                        Entry(entity).Property(x => x.InsertedAt).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedAt = currentDateTime;
                }
            }
        }
    }
}