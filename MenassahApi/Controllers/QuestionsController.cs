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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsRepo _QuestionsRepo;

        public QuestionsController(IQuestionsRepo Questions, IMainHelper mainHelper)
        {
            _QuestionsRepo = Questions;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(QuestionsDL QuestionsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _QuestionsRepo.Insert(QuestionsDL);
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
        public ActionResult Update(QuestionsDL QuestionsDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _QuestionsRepo.Update(QuestionsDL);
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
        [Route("Delete/{QuestionID}")]
        public ActionResult Delete(int QuestionID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _QuestionsRepo.Delete(QuestionID);
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

            DataSet ds = _QuestionsRepo.GetAll();
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
        [Route("GetByID/{QuestionID}")]
        public IActionResult GetByID(int QuestionID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _QuestionsRepo.GetByID(QuestionID);
            var resonse = new GeneralResponse
            {
                ID = "",
                Message = "",
                Success = true,
                Data = mainHelperRepo.Serialize(ds.Tables[0])
            };

            return Ok(resonse);
        }





        //[HttpGet]
        //[Route("GetByGroupID/{GroupID}")]
        //public IActionResult GetByGroupID(int GroupID)
        //{
        //    string Language = "aa";
        //    //string Language = mainHelperRepo.GetLanguage(Request);

        //    DataSet ds = _QuestionsRepo.GetByGroupID(GroupID);

        //    // Serialize الجداول
        //    var questions = mainHelperRepo.Serialize(ds.Tables[0]);
        //    var options = mainHelperRepo.Serialize(ds.Tables[1]);

        //    // Ensure the serialized data is cast to a compatible type
        //    var questionList = questions.Cast<Dictionary<string, object>>().ToList();
        //    var optionList = options.Cast<Dictionary<string, object>>().ToList();

        //    // الربط بين الأسئلة والاختيارات


        //    var mergedData = questionList.Select(q => new
        //    {
        //        questionId = q.ContainsKey("questionId") ? q["questionId"] : null,
        //        teacherGroupId = q.ContainsKey("teacherGroupId") ? q["teacherGroupId"] : null,
        //        groupName = q.ContainsKey("groupName") ? q["groupName"] : null,
        //        term = q.ContainsKey("term") ? q["term"] : null,
        //        gradeId = q.ContainsKey("gradeId") ? q["gradeId"] : null,
        //        gradeName = q.ContainsKey("gradeName") ? q["gradeName"] : null,
        //        subjectId = q.ContainsKey("subjectId") ? q["subjectId"] : null,
        //        subjectName = q.ContainsKey("subjectName") ? q["subjectName"] : null,
        //        questionText = q.ContainsKey("questionText") ? q["questionText"] : null,
        //        score = q.ContainsKey("score") ? q["score"] : null,
        //        createdAt = q.ContainsKey("createdAt") ? q["createdAt"] : null,
        //        questionTypeId = q.ContainsKey("questionTypeId") ? q["questionTypeId"] : null,
        //        typeName = q.ContainsKey("typeName") ? q["typeName"] : null,
        //        askedIn = q.ContainsKey("askedIn") ? q["askedIn"] : null,
        //        options = optionList
        //                    .Where(o => o.ContainsKey("questionId") && o["questionId"].Equals(q["questionId"]))
        //                    .Select(o => new
        //                    {
        //                        optionId = o.ContainsKey("optionId") ? o["optionId"] : null,
        //                        optionText = o.ContainsKey("optionText") ? o["optionText"] : null,
        //                        isCorrect = o.ContainsKey("isCorrect") ? o["isCorrect"] : null
        //                    })
        //                    .ToList()
        //    }).ToList();

        //    var response = new GeneralResponse
        //    {
        //        ID = "",
        //        Message = "",
        //        Success = true,
        //        Data = mergedData
        //    };

        //    return Ok(response);
        //}






        [HttpGet]
        [Route("GetByGroupID/{GroupID}")]
        public IActionResult GetByGroupID(int GroupID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _QuestionsRepo.GetByGroupID(GroupID);
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