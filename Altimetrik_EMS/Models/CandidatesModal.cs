using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using static Altimetrik_EMS.Models.BookStore;
using static Altimetrik_EMS.Models.VotersModal;

namespace Altimetrik_EMS.Models
{
    public class CandidatesModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Candidates> GetAllCandidates()
        {
            using (var context = new Models.CandidatesModal())
            {
                return context.candidates.ToList();
            }
        }
        public List<CandidatesDisplay> GetAllCandidatesDisplay()
        {
            List<Candidates> candidates = new List<Candidates>();
            List<CandidatesDisplay> candidatesDisplays = new List<CandidatesDisplay>();
            ConstituencyModal constituencyModal = new ConstituencyModal();
            PartyModal partyModal = new PartyModal();

            using (var context = new Models.CandidatesModal())
            {
                candidates = context.candidates.ToList();
            }

            foreach (var item in candidates)
            {
                CandidatesDisplay single = new CandidatesDisplay();

                single.ID = item.ID;
                single.Name = item.Name; 
                single.Address = item.Address == null ? "" : item.Address;
                single.Party = partyModal.GetParty_ByID(item.PartyID).PartyName;
                single.Constituency = constituencyModal.GetConstituency_ByID(item.ConstituencyID).Name;

                candidatesDisplays.Add(single);
            }

            return candidatesDisplays;
        }
        public int GetTotalCandidates()
        {
            using (var context = new Models.CandidatesModal())
            {
                return context.candidates.Count();
            }
        }

        public void Add_Candidates(Candidates addCandidates)
        {
            addCandidates.ID = GetTotalCandidates() + 1;
            using (var context = new Models.CandidatesModal())
            {
                context.candidates.AddRange(addCandidates);
                context.SaveChanges();
            }
        }
         
        public DbSet<Candidates> candidates { get; set; }
        public class Candidates
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; } 
            public int PartyID { get; set; }
            public int ConstituencyID { get; set; }
        }
        public class CandidatesDisplay
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Party { get; set; }
            public string Constituency { get; set; }
        }

    }
}
