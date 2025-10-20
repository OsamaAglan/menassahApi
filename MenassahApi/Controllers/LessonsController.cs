using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq; // For LINQ operations on collections    
namespace Menassah

{
    //[AuthorizeToken]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsRepo _LessonsRepo;

        public LessonsController(ILessonsRepo Lessons, IMainHelper mainHelper)
        {
            _LessonsRepo = Lessons;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(LessonsDL LessonsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _LessonsRepo.Insert(LessonsDL);
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
        public ActionResult Update(LessonsDL LessonsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _LessonsRepo.Update(LessonsDL);
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
        [Route("Delete/{LessonID}")]
        public ActionResult Delete(int LessonID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _LessonsRepo.Delete(LessonID);
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
        [Route("GetByID/{LessonID}")]
        public IActionResult GetByID(int LessonID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _LessonsRepo.GetByID(LessonID);
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
           
                Data = new
                {
                    Hdr = mainHelperRepo.Serialize(ds.Tables[0]),
                    Dtls = mainHelperRepo.Serialize(ds.Tables[1])
                }

            };

            return Ok(resonse);
        }






       
        [HttpGet]
        [Route("ContentTypeGetAll")]
        public IActionResult ContentTypeGetAll()
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _LessonsRepo.ContentTypeGetAll();
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
        [Route("GetByGroupID/{GroupID}")]
        public IActionResult GetByGroupID(int GroupID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _LessonsRepo.GetByGroupID(GroupID);
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,

                Data = new
                {
                    Hdr = mainHelperRepo.Serialize(ds.Tables[0]),
                    Dtls = mainHelperRepo.Serialize(ds.Tables[1])
                }

            };

            return Ok(resonse);
        }






    }
}