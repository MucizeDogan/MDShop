using MDShop.Comment.Entities;
using Microsoft.EntityFrameworkCore;

namespace MDShop.Comment.Context {
    public class CommentContext : DbContext{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=localhost,1442;initial Catalog=MDShopCommentDb;User=sa;Password=123456aA*");
        }

        public DbSet<UserComment> UserComments { get; set; }
    }
}
