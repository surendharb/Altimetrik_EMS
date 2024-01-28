using Microsoft.EntityFrameworkCore;

namespace Altimetrik_EMS.Models
{
    public class SymbolsModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Symbols> GetAllSymbols()
        {
            using (var context = new Models.SymbolsModal())
            {
                return context.symbols.ToList();
            }
        } 
        public void Load_Symbols_Approved(SymbolsApproved SymbolsApproved)
        {
            using (var context = new Models.SymbolsModal())
            {
                var updatedata = context.symbols.Where(r=> r.ID == SymbolsApproved.symbolID).FirstOrDefault();

               
                context.SaveChanges();
            }
        }
        public int GetAllSymbols_Count()
        {
            using (var context = new Models.SymbolsModal())
            {
                return context.symbols.Count();
            }
        }

        public void Add_Symbols(Symbols addSymbols)
        {
            addSymbols.ID = GetAllSymbols_Count() + 1;
            using (var context = new Models.SymbolsModal())
            {
                context.symbols.AddRange(addSymbols);
                context.SaveChanges();
            }
        }

        public DbSet<Symbols> symbols { get; set; }
        public class Symbols
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public byte[]? Symbol { get; set; } 
        }

        public class SymbolsDisplay
        {
            public int Id { get; set; }
            public string Name { get; set; } 
            public byte[]? Symbol { get; set; }
        }
        public class SymbolsApproved
        {
            public int symbolID { get; set; }
            public int partyID { get; set; }

        }

    }
}
