using Altimetrik_EMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Altimetrik_EMS.Controllers
{
    public class VotersController : Controller
    {
        private const string _LoginID = "_LoginID";
        private const string _LoginName = "_LoginName";
        private const string _LoginRole = "_LoginRole";
        private const string _LoginConstituencyID = "_LoginConstituencyID";
        private const string _LoginApproved = "_LoginApproved";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Overview()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Load_Voters_Overview_Profile_Details()
        {
            VotersModal votersModal = new VotersModal();

            var LoginID = HttpContext.Session.GetInt32(_LoginID);

            var singleVoters =  votersModal.GetVotersByID((int)LoginID);

            return Json(singleVoters);
        }
        [HttpPost]
        public JsonResult Load_Voters_Overview_Poll_Check_Details()
        {
            OverviewModel overviewModel = new OverviewModel();

            var LoginID = HttpContext.Session.GetInt32(_LoginID);

            var singleVoters =  overviewModel.GetAlreadyVoted((int)LoginID);

            return Json(singleVoters);
        }
        [HttpPost]
        public JsonResult Load_Voters_Overview_Pooling_Details()
        {
            OverviewModel overviewModel = new OverviewModel();

            var ConstituencyID = HttpContext.Session.GetInt32(_LoginConstituencyID);
            var LoginID = HttpContext.Session.GetInt32(_LoginID);

            var singlepooling =  overviewModel.GetPoolingDisplay((int)ConstituencyID,(int)LoginID);

            return Json(singlepooling);
        }
        [HttpPost]
        public JsonResult Add_Voters_Overview_Pooling_Details(OverviewModel.Voting dataVoting)
        {
            OverviewModel overviewModel = new OverviewModel();

            var LoginID = HttpContext.Session.GetInt32(_LoginID);
            dataVoting.VoterID = (int)LoginID;
            dataVoting.ElectionID = 1;

            var singlepooling =  overviewModel.InsertVoting(dataVoting);

            return Json(singlepooling);
        }
    }
}
