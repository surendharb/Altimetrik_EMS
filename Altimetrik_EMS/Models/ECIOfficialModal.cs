using Microsoft.EntityFrameworkCore;
using static Altimetrik_EMS.Models.VotersModal;

namespace Altimetrik_EMS.Models
{
    public class ECIOfficialModal : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Database\\Altimetrik_EMS.db");
        }

        public List<ECIOfficials> GetAllECIOfficials()
        {
            using (var context = new Models.ECIOfficialModal())
            {
                return context.ECIOfficial.ToList();
            }
        }

        public ECIOfficials? CheckECIOfficialsCredentials(string Username, string Password)
        {
            string encodedPassword = EncodePasswordToBase64(Password);

            using (var context = new Models.ECIOfficialModal())
            {

                var ECIOfficialsCredentials1 = context.ECIOfficial.ToList();
                var ECIOfficialsCredentials = context.ECIOfficial.Where(r=> r.Username == Username).Where(r=> r.Password == encodedPassword).FirstOrDefault();

                return ECIOfficialsCredentials;
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

        public DbSet<ECIOfficials> ECIOfficial { get; set; }
        public class ECIOfficials
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int Role { get; set; }
            public string Address { get; set; }
        }
    }
}
