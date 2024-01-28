using Microsoft.EntityFrameworkCore;
using static Altimetrik_EMS.Models.VotersModal;

namespace Altimetrik_EMS.Models
{
    public class ConstituencyModal :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }
        public List<Constituency> GetAllConstituency()
        {
            using (var context = new Models.ConstituencyModal())
            {
                return context.constituency.ToList();
            }
        }

        public Constituency GetConstituency_ByID(int ID)
        {
            using (var context = new Models.ConstituencyModal())
            {
                return context.constituency.Where(r=> r.ID == ID).FirstOrDefault();
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
