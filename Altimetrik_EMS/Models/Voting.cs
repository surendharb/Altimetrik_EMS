using Microsoft.EntityFrameworkCore;

namespace Altimetrik_EMS.Models
{
    public class VotingModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Voting> GetAllVoting()
        {
            using (var context = new Models.VotingModal())
            {
                return context.voting.ToList();
            }
        }
        public List<Voting> GetPooling()
        {
            using (var context = new Models.VotingModal())
            {
                return context.voting.ToList();
            }
        }
        public Voting GetAllVotingByID( int LoginID)
        {
            using (var context = new Models.VotingModal())
            {
                return context.voting.Where(r=> r.ID == LoginID).FirstOrDefault();
            }
        }
        public List<Voting> GetAllResult()
        {
            using (var context = new Models.VotingModal())
            {
                return context.voting.ToList();
            }
        }
        public DbSet<Voting> voting { get; set; }
        public class Voting
        {
            public int ID { get; set; }
            public int ElectionID { get; set; }
            public int VoterID { get; set; }
            public int CandidateID { get; set; } 
        }         

    }
}
