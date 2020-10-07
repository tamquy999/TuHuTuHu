namespace TuHuTuHu.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Msg> Msg { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AvtLink)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.CoverLink)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Pass)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Msg)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.ReceiverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Msg1)
                .WithRequired(e => e.Account1)
                .HasForeignKey(e => e.SenderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Account1)
                .WithMany(e => e.Account2)
                .Map(m => m.ToTable("Follow").MapLeftKey("FollowerID").MapRightKey("UserID"));

            modelBuilder.Entity<Post>()
                .Property(e => e.ImgLink)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Account1)
                .WithMany(e => e.Post1)
                .Map(m => m.ToTable("Love").MapLeftKey("PostID").MapRightKey("UserID"));
        }
    }
}
