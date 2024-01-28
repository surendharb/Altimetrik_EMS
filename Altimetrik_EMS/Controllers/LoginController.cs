using Altimetrik_EMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static Altimetrik_EMS.Models.ECIOfficialModal;

namespace Altimetrik_EMS.Controllers
{
    public class LoginController : Controller
    {
        private const string _LoginID = "_LoginID";
        private const string _LoginName = "_LoginName";
        private const string _LoginRole = "_LoginRole";
        private const string _LoginConstituencyID = "_LoginConstituencyID";
        private const string _LoginApproved = "_LoginApproved";
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login_Officials_Load(LoginModal.LoginDetails dataLogin)
        {
            LoginModal loginModal = new LoginModal();
            string msg = string.Empty;

            var eCIOfficials = loginModal.GetAuthenticated_ECIOfficial(dataLogin);

            if(eCIOfficials == null)
            {
                msg = "Invalid Credentials";
            }
            else
            {
                HttpContext.Session.SetInt32(_LoginID, eCIOfficials.Id);
                HttpContext.Session.SetString(_LoginName, eCIOfficials.FirstName + " " + eCIOfficials.LastName);
                HttpContext.Session.SetString(_LoginRole, "ECI Official");
            }

            return Json(msg);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login_Voters_Load(LoginModal.LoginDetails dataLogin)
        {
            LoginModal loginModal = new LoginModal();
            VotersModal votersModal = new VotersModal();
            string msg = string.Empty;

            var voters = loginModal.GetAuthenticated_Voter(dataLogin);

            if (voters == null)
            {
                var voterStatus = votersModal.GetAllVoters().Where(r => r.Username == dataLogin.UserName).FirstOrDefault().IsApproved;

                if(voterStatus == 0)
                {

                    msg = "Your Registration is pending for verification";
                    return Json(msg);
                }

                msg = "Invalid Credentials";
            }
            else
            {
                HttpContext.Session.SetInt32(_LoginID, voters.ID);
                HttpContext.Session.SetInt32(_LoginConstituencyID, voters.ConstituencyID);
                HttpContext.Session.SetInt32(_LoginApproved, voters.IsApproved);
                HttpContext.Session.SetString(_LoginName, voters.FirstName + " " + voters.LastName);
                HttpContext.Session.SetString(_LoginRole, "Voter");
            }
            return Json(msg);
        }
        [HttpPost]
        public JsonResult Load_Voters_Username_Validation_Add(VotersModal.Voters dataVoters)
        {
            VotersModal votersModal = new VotersModal();

            var busername = votersModal.GetVoter_Username_Validation(dataVoters.Username);
 
            return Json(busername);
        }
        [HttpPost]
        public JsonResult Load_Voters_Add(VotersModal.Voters dataVoters)
        {
            VotersModal votersModal = new VotersModal();

            votersModal.Add_New_Voter(dataVoters);

            return Json("");
        }
         
        public IActionResult VotersRegistration()
        {
            return View();
        }
    }
}
