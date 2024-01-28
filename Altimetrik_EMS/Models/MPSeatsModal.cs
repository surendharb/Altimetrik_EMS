using Microsoft.EntityFrameworkCore;
using static Altimetrik_EMS.Models.BookStore;

namespace Altimetrik_EMS.Models
{
    public class MpSeatsModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Constituency> GetAllMpSeats()
        {
            using (var context = new Models.MpSeatsModal())
            {
                return context.constituency.ToList();
            }
        }
        public int GetTotalMpSeats()
        {
            using (var context = new Models.MpSeatsModal())
            {
                return context.constituency.Count();
            }
        }

        public void Add_MPSeats(Constituency Addconstituency)
        {
            Addconstituency.ID = GetTotalMpSeats() + 1;
            using (var context = new Models.MpSeatsModal())
            {
                context.constituency.AddRange(Addconstituency);
                context.SaveChanges();
            }
        }
         
        public DbSet<Constituency> constituency { get; set; }
        public class Constituency
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string State { get; set; } 
            public int AreaCode { get; set; } 
        }


    }
}
