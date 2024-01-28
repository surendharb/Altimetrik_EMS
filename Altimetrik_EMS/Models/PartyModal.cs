using Microsoft.EntityFrameworkCore;
using static Altimetrik_EMS.Models.CandidatesModal;

namespace Altimetrik_EMS.Models
{
    public class PartyModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Party> GetAllParty()
        {
            using (var context = new Models.PartyModal())
            {
                return context.party.ToList();
            }
        }
        public int GetTotalParty()
        {
            using (var context = new Models.PartyModal())
            {
                return context.party.Count();
            }
        }
        public List<PartyDisplay> GetAllPartyDisplay()
        {
            List<Party> parties = new List<Party>();
            List<PartyDisplay> partyDisplays = new List<PartyDisplay>();
            //SymbolModal symbolModal = new SymbolModal();

            using (var context = new Models.PartyModal())
            {
                parties = context.party.ToList();
            }

            foreach (var item in parties)
            {
                PartyDisplay single = new PartyDisplay();
                SymbolsModal symbols = new SymbolsModal();

                single.ID = item.ID;
                single.PartyName = item.PartyName;
                single.ShortName = item.ShortName;

                var symb = symbols.GetAllSymbols().Where(r=> r.ID == item.CurrentSymbolID).FirstOrDefault();
                if(symb != null)
                {
                    single.CurrentSymbol = symb.Symbol;
                }


                partyDisplays.Add(single);
            }

            return partyDisplays;
        }
        public Party GetParty_ByID(int ID)
        {
            using (var context = new Models.PartyModal())
            {
                return context.party.Where(r => r.ID == ID).FirstOrDefault();
            }
        }
        public void Add_Party(Party addParty)
        {
            addParty.ID = GetTotalParty() + 1;
            using (var context = new Models.PartyModal())
            {
                context.party.AddRange(addParty);
                context.SaveChanges();
            }
        }

        public DbSet<Party> party { get; set; }
        public class Party
        {
            public int ID { get; set; }
            public string PartyName { get; set; }
            public string ShortName { get; set; }
            public int CurrentSymbolID { get; set; }
        }

        public class PartyDisplay
        {
            public int ID { get; set; }
            public string PartyName { get; set; }
            public string ShortName { get; set; }
            public byte[]? CurrentSymbol { get; set; }
        }
    }
}
