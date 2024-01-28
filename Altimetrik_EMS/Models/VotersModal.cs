using Microsoft.EntityFrameworkCore;

namespace Altimetrik_EMS.Models
{
    public class VotersModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\\Database\\Altimetrik_EMS.db");
        }

        public List<Voters> GetAllVoters()
        {
            using (var context = new Models.VotersModal())
            {
                return context.voters.ToList();
            }
        }
        public Voters GetVotersByID( int LoginID)
        {
            using (var context = new Models.VotersModal())
            {
                return context.voters.Where(r=> r.ID == LoginID).FirstOrDefault();
            }
        }
        public bool GetVoter_Username_Validation(string username)
        {
            using (var context = new Models.VotersModal())
            {
                var singleVoter =  context.voters.Where(r=> r.Username == username).FirstOrDefault();

                if (singleVoter != null)
                {
                    return true;
                }
                else { 
                    return false;
                }
            }
            return false;
        }
        public List<VotersDisplay> GetAllVotersDisplay()
        {
            List<Voters> voters = new List<Voters>();
            List<VotersDisplay> votersDisplay = new List<VotersDisplay>();
            ConstituencyModal constituencyModal = new ConstituencyModal();

            using (var context = new Models.VotersModal())
            {
                voters = context.voters.ToList();
            }

            foreach (var item in voters)
            {
                VotersDisplay single = new VotersDisplay();

                single.Id = item.ID;
                single.FirstName = item.FirstName;
                single.LastName = item.LastName;
                single.Address = item.Address == null ? "" : item.Address;
                single.IsApproved = item.IsApproved == 1 ? true : false;
                single.Constituency = constituencyModal.GetConstituency_ByID(item.ConstituencyID).Name;

                votersDisplay.Add(single);
            }

            return votersDisplay;
        }

        public void Add_New_Voter(Voters voters)
        {
            using (var context = new Models.VotersModal())
            {
                voters.Password = EncodePasswordToBase64(voters.Password);
                context.voters.Add(voters);
                context.SaveChanges();
            }
        }
        public void Load_Voters_Approved(VotersApproved votersApproved)
        {
            using (var context = new Models.VotersModal())
            {
                var updatedata = context.voters.Where(r=> r.ID == votersApproved.id).FirstOrDefault();

                if (updatedata != null)
                {
                    if (votersApproved.approved == true)
                    {
                        updatedata.IsApproved = 1;
                    }
                    else
                    {
                        updatedata.IsApproved = 0;
                    }
                }
                context.SaveChanges();
            }
        }
        public int GetAllVoters_Count()
        {
            using (var context = new Models.VotersModal())
            {
                return context.voters.Count();
            }
        }

        public Voters? CheckVotersCredentials(string Username, string Password)
        {
            string encodedPassword = EncodePasswordToBase64(Password);

            using (var context = new Models.VotersModal())
            {
                var VotersCredentials = context.voters.Where(r=> r.Username == Username).Where(r=> r.Password == encodedPassword).Where(r=> r.IsApproved == 1).FirstOrDefault();

                return VotersCredentials;
            }
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public DbSet<Voters> voters { get; set; }
        public class Voters
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string? Address { get; set; }
            public byte[]? Photo { get; set; }
            public int ConstituencyID { get; set; }
            public int IsApproved { get; set; }
        }

        public class VotersDisplay
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Address { get; set; }
            public string Constituency { get; set; }
            public bool IsApproved { get; set; }
        }
        public class VotersApproved
        {
            public int id { get; set; }
            public bool approved { get; set; }

        }

    }
}
