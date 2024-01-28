using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations; 
using static Altimetrik_EMS.Models.ECIOfficialModal;
using static Altimetrik_EMS.Models.VotersModal;

namespace Altimetrik_EMS.Models
{
    public class LoginModal
    {
        public ECIOfficials? GetAuthenticated_ECIOfficial(LoginDetails logindetails)
        {
            ECIOfficialModal eCIOfficialModal = new ECIOfficialModal();

            if (logindetails == null)
            {
                return null;
            }

            var authCredentials = eCIOfficialModal.CheckECIOfficialsCredentials(logindetails.UserName, logindetails.Password);

            if (authCredentials == null)
            {
                return null;
            }

            return authCredentials;
        }

        public Voters? GetAuthenticated_Voter(LoginDetails logindetails)
        {
            VotersModal voters = new VotersModal();

            if (logindetails == null)
            {
                return null;
            }

            var authCredentials = voters.CheckVotersCredentials(logindetails.UserName, logindetails.Password);

            if(authCredentials == null)
            {
                return null;
            }

            return authCredentials;
        }

        public class LoginDetails
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Role { get; set; }
            public string LoginError { get; set; }

        }
    }
}
