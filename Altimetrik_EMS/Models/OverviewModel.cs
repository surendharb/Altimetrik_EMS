using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Composition.Convention;
using static Altimetrik_EMS.Models.BookStore;
using static Altimetrik_EMS.Models.VotersModal;

namespace Altimetrik_EMS.Models
{
    public class OverviewModel : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Voting> GetAllPooling()
        {
            using (var context = new Models.OverviewModel())
            {
                return context.voting.ToList();
            }
        }
        public bool GetAlreadyVoted(int LoginID)
        {
            using (var context = new Models.OverviewModel())
            {
               var count = context.voting.Where(r=> r.VoterID == LoginID).Count();

                if(count > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool InsertVoting(Voting voting)
        {
            using (var context = new Models.OverviewModel())
            { 
                context.voting.Add(voting);
                context.SaveChanges();
            }
            return true;
        }

        public PoolingDisplay GetPoolingDisplay(int ConstituencyID, int LoginID)
        {
            PoolingDisplay poolingDisplay = new PoolingDisplay();
            
            using (var context = new Models.OverviewModel())
            {
                poolingDisplay.ConstituencyName = context.constituency.Where(r=> r.ID == ConstituencyID).FirstOrDefault().Name;

                poolingDisplay.CanVote = GetAlreadyVoted(LoginID);
                poolingDisplay.poolingParties = new List<PoolingParty>();

                var listCandidates = context.candidates.Where(r=> r.ConstituencyID == ConstituencyID);

                foreach (var item in listCandidates)
                {
                    PoolingParty poolingParty = new PoolingParty();

                    poolingParty.CandidateName = item.Name;
                    poolingParty.CandidateID = item.ID;
                    poolingParty.PartyName = context.party.Where(r => r.ID == item.PartyID).FirstOrDefault().PartyName;
                    poolingParty.PartySymbol = context.symbols.Where(r => r.ID == context.party.Where(r => r.ID == item.PartyID).FirstOrDefault().CurrentSymbolID).FirstOrDefault().Symbol;
                    poolingDisplay.poolingParties.Add(poolingParty);
                }
            }
            return poolingDisplay;
        }
        public DbSet<Constituency> constituency { get; set; }
        public DbSet<Voting> voting { get; set; }
        public DbSet<Candidates> candidates { get; set; }
        public DbSet<Party> party { get; set; }
        public DbSet<Symbols> symbols { get; set; }
        public class Voting
        {
            public int ID { get; set; } 
            public int ElectionID { get; set; }
            public int CandidateID { get; set; }
            public int VoterID { get; set; }
        }
        public class Candidates
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int PartyID { get; set; }
            public int ConstituencyID { get; set; }
        }
        public class Party
        {
            public int ID { get; set; }
            public string PartyName { get; set; }
            public string ShortName { get; set; }
            public int CurrentSymbolID { get; set; }
        }
        public class Symbols
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public byte[]? Symbol { get; set; }
        }
        public class Constituency
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string State { get; set; }
            public int AreaCode { get; set; }
        }
        public class PoolingDisplay
        { 
            public string ConstituencyName { get; set; }             
            public List<PoolingParty> poolingParties { get; set; }
            public bool CanVote { get; set; }
        }
        public class PoolingParty
        {
            public int CandidateID { get; set; }
            public string CandidateName { get; set; }
            public string PartyName { get; set; }
            public byte[]? PartySymbol { get; set; }
        }

    }
}
