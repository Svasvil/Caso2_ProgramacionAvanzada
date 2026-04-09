using Caso2.API.Models;
using Caso2.API.Models.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Caso2.API.DataBases
{
    public class ObjContexto : DbContext
    {
        public ObjContexto(DbContextOptions<ObjContexto> opts) : base(opts) { }

        public DbSet<TicketModel> Ticket { get; set; } = null!;
        public DbSet<UserModel> Users { get; set; } = null!;


        //keys 
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<TicketModel>(entity =>
            {
                entity.HasKey(o => o.Id);


                entity.HasOne(b => b.AsignadoA)
                      .WithMany()
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

