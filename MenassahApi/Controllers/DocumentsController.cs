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
    [AuthorizeToken]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsRepo _DocumentsRepo;

        public DocumentsController(DocumentsRepo Documents, IMainHelper mainHelper)
        {
            _DocumentsRepo = Documents;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;


        [HttpPut]
        [Route("Update")]
        public ActionResult Update(DocumentsDL DocumentsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _DocumentsRepo.Update(DocumentsDL);
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
        [Route("Delete/{DocumentID}")]
        public ActionResult Delete(int DocumentID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _DocumentsRepo.Delete(DocumentID);
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

            DataSet ds = _DocumentsRepo.GetAll();
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
        [Route("GetByID/{DocumentID}")]
        public IActionResult GetByID(int DocumentID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _DocumentsRepo.GetByID(DocumentID);
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
        [Route("GetByBeneficiaryID/{BeneficiaryID}")]
        public IActionResult GetByBeneficiaryID(int BeneficiaryID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _DocumentsRepo.GetByBeneficiaryID(BeneficiaryID);
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
        public async Task<IActionResult> Insert1([FromForm] DocumentsDL model)
        {
            GeneralResponse response;

            try
            {
                // احفظ الملف أولاً
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

                // حفظ الملف على السيرفر
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // جهز كائن DocumentsDL وأرسله للمخزن
                var doc = new DocumentsDL
                {
                    beneficiaryId = model.beneficiaryId,
                    documentTypeId = model.documentTypeId,
                    filePath = "/uploads/" + uniqueFileName,
                };

                string ID = _DocumentsRepo.Insert(doc);
                if (ID == "0")
                {
                    response = new GeneralResponse
                    {
                        ID = "0",
                        Message = "Can't insert Documents",
                        Success = false
                    };

                    return BadRequest(response);
                }
                else
                {
                    response = new GeneralResponse
                    {
                        ID = ID,
                        Message = "",
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



    }
}