// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace PttSpider.DBContext
{
    public class DellServiceContext : DbContext
    {
        public DellServiceContext()
        {

        }
        public DellServiceContext(DbContextOptions<DellServiceContext> options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
            .ToContainer("account")
            .HasPartitionKey(e => e.Id);
            modelBuilder.Entity<Blog>()
            .ToContainer("PttSpiderCatch")
            .HasPartitionKey(e => e.Id);
            modelBuilder.Entity<SearchRule>()
            .ToContainer("SearchRule")
            .HasPartitionKey(e => e.Id);
        }
        public DbSet<Account>? Account { get; set; }
        public DbSet<Blog>? Blog { get; set; }
        public DbSet<SearchRule>? SearchRule { get; set; }
        public static async Task CheckDatabaseAsync(
            DbContextOptions<DellServiceContext> options
        )
        {
            using var context = new DellServiceContext(options);
            var _ = await context.Database.EnsureCreatedAsync();
        }
    }
}
