using DocumentFormat.OpenXml.InkML;
using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace Menassah

{
    //[AuthorizeToken]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersRepo _TeachersRepo;

        public TeachersController(ITeachersRepo Teachers, IMainHelper mainHelper)
        {
            _TeachersRepo = Teachers;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(TeachersDL TeachersDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _TeachersRepo.Insert(TeachersDL);
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
        public ActionResult Update(TeachersDL TeachersDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _TeachersRepo.Update(TeachersDL);
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
        [Route("Delete/{TeachersID}")]
        public ActionResult Delete(int TeachersID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _TeachersRepo.Delete(TeachersID);
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

            DataSet ds = _TeachersRepo.GetAll();
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
        [Route("GetDashboard")]
        public IActionResult GetDashboard()
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _TeachersRepo.GetDashboard();

            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                //Data = mainHelperRepo.SerializeFirst(ds.Tables[0])
                Data = new
                {
                    Hdr = mainHelperRepo.Serialize(ds.Tables[0]),
                    Dtls = mainHelperRepo.Serialize(ds.Tables[1])
                }
            };



            return Ok(resonse);
        }
   


        [HttpGet]
        [Route("GetByID/{TeachersID}")]
        public IActionResult GetByID(int TeachersID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _TeachersRepo.GetByID(TeachersID);
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
        [Route("GetGroupGrowth/{TeachersID}")]
        public IActionResult GetGroupGrowth(int TeachersID)
        {
            DataSet ds = _TeachersRepo.GetGroupGrowth(TeachersID);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return Ok(new GeneralResponse
                {
                    ID = "",
                    Message = "No data",
                    Success = false,
                    Data = null
                });
            }

            var table = ds.Tables[0];

            var rawList = table.AsEnumerable()
                .Select(row => new
                {
                    groupName = row["GroupName"].ToString(),
                    joinDate = DateTime.Parse(row["JoinDate"].ToString()).ToString("dd/MM/yyyy"),
                    studentsCount = Convert.ToInt32(row["StudentsCount"])
                })
                .ToList();

            var groupedData = rawList
                .GroupBy(x => x.groupName)
                .Select(g => new
                {
                    groupName = g.Key,
                    data = g.Select(x => new {
                        joinDate = x.joinDate,
                        studentsCount = x.studentsCount
                    }).ToList()
                })
                .ToList();

            return Ok(new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = groupedData
            });
        }


    }
}