using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Menassah

{
    //[AuthorizeToken]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepo _CitiesRepo;

        public CitiesController(ICitiesRepo Cities, IMainHelper mainHelper)
        {
            _CitiesRepo = Cities;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(CitiesDL CitiesDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _CitiesRepo.Insert(CitiesDL);
                if (ID == "0")
                {
                    resonse = new GeneralResponse
                    {
                        ID = "0",
                        Message = "Can't insert Type",
                        Success = false
                    };

                    return BadRequest(resonse);

                }
                else
                {
                    resonse = new GeneralResponse
                    {
                        ID = ID,
                        Message = "",
                        Success = true
                    };

                    return Ok(resonse);


                }
                //return 0;

            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(CitiesDL CitiesDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _CitiesRepo.Update(CitiesDL);
                if (ID == "0")
                {
                    resonse = new GeneralResponse
                    {
                        ID = "0",
                        Message = "Can't Update Type",
                        Success = false
                    };

                    return BadRequest(resonse);

                }
                else
                {
                    resonse = new GeneralResponse
                    {
                        ID = ID,
                        Message = "",
                        Success = true
                    };

                    return Ok(resonse);


                }
                //return 0;

            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }

        [HttpDelete]
        [Route("Delete/{CityID}")]
        public ActionResult Delete(int CityID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _CitiesRepo.Delete(CityID);
                resonse = new GeneralResponse
                {
                    ID = "",
                    Message = "",
                    Success = true
                };

                return Ok(resonse);

            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _CitiesRepo.GetAll();
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = mainHelperRepo.Serialize(ds.Tables[0])
            };

            return Ok(resonse);
        }

    
        [HttpGet]
        [Route("GetByID/{CityID}")]
        public IActionResult GetByID(int CityID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _CitiesRepo.GetByID(CityID);
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = mainHelperRepo.Serialize(ds.Tables[0])
            };

            return Ok(resonse);
        }

    }
}