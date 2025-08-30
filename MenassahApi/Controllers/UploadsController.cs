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
    public class  UploadsController : ControllerBase
    {
        private readonly  IUploadsRepo _UploadsRepo;

        public  UploadsController( IUploadsRepo  uploads, IMainHelper mainHelper)
        {
            _UploadsRepo =  uploads;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;


        [HttpPut]
        [Route("Update")]
        public ActionResult Update(UploadsDL  uploadsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _UploadsRepo.Update( uploadsDL);
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
        [Route("Delete/{UploadID}")]
        public ActionResult Delete(int UploadID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _UploadsRepo.Delete(UploadID);
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

            DataSet ds = _UploadsRepo.GetAll();
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
        [Route("GetByID/{UploadID}")]
        public IActionResult GetByID(int UploadID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _UploadsRepo.GetByID(UploadID);
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

            DataSet ds = _UploadsRepo.GetByTeacherID(TeacherID);
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

            DataSet ds = _UploadsRepo.GetByGroupID(GroupID);
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = mainHelperRepo.Serialize(ds.Tables[0])
            };

            return Ok(resonse);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert([FromForm] UploadsDL model)
        {
            try
            {
                var file = model.File;
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new GeneralResponse
                    {
                        ID = "0",
                        Message = "الملف غير موجود",
                        Success = false
                    });
                }

                string UploadPath = model.uploadType switch
                {
                    "docs" or "videos" or "images" =>
                        $"uploads/teachers/{model.teacherId}/groups/{model.groupId}/{model.uploadType}",

                    "profile" =>
                        $"uploads/teachers/{model.teacherId}/{model.uploadType}",

                    _ => $"uploads/teachers/{model.teacherId}"
                };

                // حفظ الملف على السيرفر
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", UploadPath);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // جهز الكائن
                var doc = new UploadsDL
                {
                    uploadType = model.uploadType,
                    teacherId = model.teacherId,
                    groupId = model.groupId,
                    filePath = "/" + UploadPath + "/" + uniqueFileName
                };

                string ID = _UploadsRepo.Insert(doc);
                if (ID == "0")
                {
                    // ⚠️ امسح الملف اللي اتحفظ علشان ميبقاش orphan
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    return BadRequest(new GeneralResponse
                    {
                        ID = "0",
                        Message = "Can't insert uploads",
                        Success = false
                    });
                }

                return Ok(new GeneralResponse
                {
                    ID = ID,
                    Message = "تم رفع الملف بنجاح",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }



    }
}