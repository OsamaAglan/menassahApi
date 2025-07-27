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
    public class TeacherGroupsController : ControllerBase
    {
        private readonly ITeacherGroupsRepo _TeacherGroupsRepo;

        public TeacherGroupsController(ITeacherGroupsRepo TeacherGroups, IMainHelper mainHelper)
        {
            _TeacherGroupsRepo = TeacherGroups;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(TeacherGroupsDL TeacherGroupsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _TeacherGroupsRepo.Insert(TeacherGroupsDL);
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
        public ActionResult Update(TeacherGroupsDL TeacherGroupsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _TeacherGroupsRepo.Update(TeacherGroupsDL);
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
        [Route("Delete/{TeacherGroupID}")]
        public ActionResult Delete(int TeacherGroupID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _TeacherGroupsRepo.Delete(TeacherGroupID);
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

            DataSet ds = _TeacherGroupsRepo.GetAll();
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
        [Route("GetByID/{TeacherGroupID}")]
        public IActionResult GetByID(int TeacherGroupID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _TeacherGroupsRepo.GetByID(TeacherGroupID);
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
        [Route("GetByTeacherID/{TeacherID}")]
        public IActionResult GetByTeacherID(int TeacherID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _TeacherGroupsRepo.GetByTeacherID(TeacherID);
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