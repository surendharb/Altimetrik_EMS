using Microsoft.EntityFrameworkCore;

namespace Altimetrik_EMS.Models
{
    public class ResultModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<ResultDisplay> GetVotingResult(int ConstituencyID)
        {
            List<ResultDisplay> result = new List<ResultDisplay>();
            PartyModal partyModal = new PartyModal();
            using (var context = new Models.ResultModal())
            {
                var candidates = context.candidates.Where(r=> r.ConstituencyID == ConstituencyID).ToList();

                foreach (var item in candidates)
                {
                    ResultDisplay single = new ResultDisplay();

                    single.CandidateName = item.Name;
                    single.PartyName = partyModal.GetParty_ByID(item.PartyID).PartyName;
                    single.TotalVotes = context.voting.Where(r => r.CandidateID == item.ID).Count();

                    result.Add(single);
                }

            }

            var WinningTag = result.OrderByDescending(r=> r.TotalVotes).ToList();

            if(WinningTag != null)
            {
                WinningTag[0].Status = "Win";
            }

            return WinningTag;
        }
        public DbSet<Voting> voting { get; set; }
        public DbSet<Constituency> constituency { get; set; }
        public DbSet<Candidates> candidates { get; set; }
        public class Voting
        {
            public int ID { get; set; }
            public int ElectionID { get; set; }
            public int VoterID { get; set; }
            public int CandidateID { get; set; }
        }
        public class Constituency
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string State { get; set; }
            public int AreaCode { get; set; }
        }
        public class Candidates
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int PartyID { get; set; }
            public int ConstituencyID { get; set; }
        }
        public class ResultDisplay
        {
            public int ID { get; set; }
            public int TotalVotes { get; set; }
            public string CandidateName { get; set; }
            public string PartyName { get; set; }
            public byte[]? PartySymbol { get; set; }
            public string Status { get; set; }


        }
    }
}
