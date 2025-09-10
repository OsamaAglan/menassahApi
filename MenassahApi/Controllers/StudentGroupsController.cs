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
    public class StudentGroupsController : ControllerBase
    {
        private readonly IStudentGroupsRepo _StudentGroupsRepo;

        public StudentGroupsController(IStudentGroupsRepo StudentGroups, IMainHelper mainHelper)
        {
            _StudentGroupsRepo = StudentGroups;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(StudentGroupsDL StudentGroupsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _StudentGroupsRepo.Insert(StudentGroupsDL);
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
        [Route("UpdateStatus")]
        public ActionResult UpdateStatus([FromBody] List<StudentStatusUpdate> updates)
        {
            GeneralResponse response;
            try
            {
                string result = _StudentGroupsRepo.UpdateStatuses(updates);
                if (result == "0")
                {
                    response = new GeneralResponse
                    {
                        ID = "0",
                        Message = "Can't Update Statuses",
                        Success = false
                    };

                    return BadRequest(response);
                }
                else
                {
                    response = new GeneralResponse
                    {
                        ID = result,
                        Message = "✅ تم تحديث الحالات بنجاح",
                        Success = true
                    };

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }











        [HttpDelete]
        [Route("Delete/{StudentGroupID}")]
        public ActionResult Delete(int StudentGroupID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _StudentGroupsRepo.Delete(StudentGroupID);
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

            DataSet ds = _StudentGroupsRepo.GetAll();
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
        [Route("GetByID/{StudentGroupID}")]
        public IActionResult GetByID(int StudentGroupID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _StudentGroupsRepo.GetByID(StudentGroupID);
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
        [Route("GetBystudentID/{StudentID}/{Term}")]
        public IActionResult GetBystudentID(int StudentID, int Term)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _StudentGroupsRepo.GetBystudentID(StudentID, Term);
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
        [Route("GetByGradeID/{GradeID}/{Term}")]
        public IActionResult GetByGradeID(int GradeID, int Term)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _StudentGroupsRepo.GetByGradeID(GradeID, Term);
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
        [Route("GetBySubjectID/{SubjectID}/{Term}")]
        public IActionResult GetBySubjectID(int SubjectID, int Term)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _StudentGroupsRepo.GetBySubjectID(SubjectID, Term);
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