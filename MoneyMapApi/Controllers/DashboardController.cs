using Menassah;
using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Menassah

{
    [AuthorizeToken]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepo _DashboardRepo;

        public DashboardController(IDashboardRepo Dashboard, IMainHelper mainHelper)
        {
            _DashboardRepo = Dashboard;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpGet]
        [Route("GeneralStats")]
        public IActionResult GeneralStats()
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _DashboardRepo.GeneralStats();
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = mainHelperRepo.SerializeFirst(ds.Tables[0])
            };

            return Ok(resonse);
        }

      



    }
}