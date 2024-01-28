using Altimetrik_EMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System.Xml.Linq;
using static Altimetrik_EMS.Models.DashboardModal;

namespace Altimetrik_EMS.Controllers
{
    public class ECIOfficialsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Load_Dashboard_Panel()
        {
            DashboardPanel dashboardPanel = new DashboardPanel();
            MpSeatsModal mpSeatsModal = new MpSeatsModal();
            VotersModal votersModal = new VotersModal();
            PartyModal partyModal = new PartyModal();


            dashboardPanel.TotalVoters = votersModal.GetAllVotersDisplay().Count();
            dashboardPanel.TotalConstitutions = mpSeatsModal.GetAllMpSeats().Count();
            dashboardPanel.RegisteredParty = partyModal.GetAllPartyDisplay().Count();
            return Json(dashboardPanel);
        }


        public IActionResult Voters()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Load_Voters_Panel()
        {
            VotersModal votersModal = new VotersModal();

            var listVoters =  votersModal.GetAllVotersDisplay();

            return Json(listVoters);
        }

        [HttpPost]
        public JsonResult Load_Voters_Approved_Panel(VotersModal.VotersApproved votersApproved)
        {
            VotersModal votersModal = new VotersModal();

            votersModal.Load_Voters_Approved(votersApproved);

            return Json(votersModal);
        }

        public IActionResult MPSeats()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Load_MPSeats_Panel()
        {
            MpSeatsModal mpSeatsModal = new MpSeatsModal();

            var listVoters =  mpSeatsModal.GetAllMpSeats();

            return Json(listVoters);
        }

        [HttpPost]
        public JsonResult Load_MPSeat_Add_Panel(MpSeatsModal.Constituency dataConstituency)
        {
            MpSeatsModal mpSeatsModal = new MpSeatsModal();

            mpSeatsModal.Add_MPSeats(dataConstituency);

            return Json(mpSeatsModal);
        }

        public IActionResult Candidates()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Load_Candidates_Panel()
        {
            CandidatesModal candidatesModal = new CandidatesModal();

            var candidates =  candidatesModal.GetAllCandidatesDisplay();

            return Json(candidates);
        }

        [HttpPost]
        public JsonResult Load_Candidates_Add_Panel(CandidatesModal.Candidates dataCandidates)
        {
            CandidatesModal candidatesModal = new CandidatesModal();

            candidatesModal.Add_Candidates(dataCandidates);

            return Json(candidatesModal);
        }

        [HttpPost]
        public JsonResult Load_Candidates_Party_DropDown_Panel()
        {
            PartyModal partyModal = new PartyModal();

            var party =  partyModal.GetAllParty();

            return Json(party);
        }
        [HttpPost]
        public JsonResult Load_Candidates_Constituency_DropDown_Panel()
        {
            ConstituencyModal constituencyModal = new ConstituencyModal();

            var constituency =  constituencyModal.GetAllConstituency();

            return Json(constituency);
        }

        public IActionResult Party()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Load_Party_Panel()
        {
            PartyModal partyModal = new PartyModal();

            var partyDisplays =  partyModal.GetAllPartyDisplay();

            return Json(partyDisplays);
        }

        [HttpPost]
        public JsonResult Load_Party_Add_Panel(PartyModal.Party dataParty)
        {
            PartyModal partyModal = new PartyModal();

            partyModal.Add_Party(dataParty);

            return Json(partyModal);
        }

        public IActionResult Symbols()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Load_Symbols_Panel()
        {
            SymbolsModal symbolsModal = new SymbolsModal();

            var symbols =  symbolsModal.GetAllSymbols();

            return Json(symbols);
        }

        [HttpPost]
        public IActionResult Load_Symbols_Add_Panel(string name)
        {
            try
            {
                SymbolsModal symbolsModal = new SymbolsModal();

                SymbolsModal.Symbols dataSymbols = new SymbolsModal.Symbols(); 

                var photoFile = Request.Form.Files[0];

                using (var ms = new MemoryStream())
                {
                    photoFile.CopyTo(ms);
                    dataSymbols.Symbol = ms.ToArray(); 
                }

                dataSymbols.Name = name;

                symbolsModal.Add_Symbols(dataSymbols);

                return Content("Symbol added and saved successfully!");
                 
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }


        public IActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Load_Result_Panel(ConstituencyModal.Constituency datdConstituency)
        {
            ResultModal resultModal = new ResultModal();

            var result = resultModal.GetVotingResult(datdConstituency.ID);
            return Json(result);
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YourAction(string name)
        {
            try
            {
                // Create a unique filename for the photo
                string photoFileName = Guid.NewGuid().ToString() + ".jpg";

                // Save the photo to the server
                var photoPath = Path.Combine("YourUploadDirectory", photoFileName);

                using (var stream = new FileStream(photoPath, FileMode.Create))
                {
                    var photoFile = Request.Form.Files[0];
                    photoFile.CopyTo(stream);
                }

                // Save data to SQLite database
                using (var connection = new SqliteConnection("Data Source=YourDatabase.db"))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO YourTable (Name, PhotoFileName) VALUES (@Name, @PhotoFileName)";
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhotoFileName", photoFileName);
                        command.ExecuteNonQuery();
                    }
                }

                return Content("Data received and saved successfully!");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }
    }
}
