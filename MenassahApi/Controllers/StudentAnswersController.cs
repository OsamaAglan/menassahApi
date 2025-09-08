using Menassah.Repository;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Menassah
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnswersController : ControllerBase
    {
        private readonly IStudentAnswersRepo _repo;
        private readonly IMainHelper _mainHelper;

        public StudentAnswersController(IStudentAnswersRepo repo, IMainHelper mainHelper)
        {
            _repo = repo;
            _mainHelper = mainHelper;
        }

        // إدخال دفعة من الإجابات (مفضّل)
        [HttpPost]
        [Route("InsertBatch")]
        public IActionResult InsertBatch([FromBody] StudentAnswersBatchRequest request)
        {
            try
            {
                if (request == null || request.Answers == null || request.Answers.Count == 0)
                    return BadRequest(new { Success = false, Message = "لا توجد إجابات" });

                var res = _repo.InsertBatch(request.StudentID, request.Answers);

                return Ok(new { Success = true, Message = "", Data = res });
            }
            catch (Exception ex)
            {
                return BadRequest(_mainHelper.GetException(ex));
            }
        }

        // إدخال إجابة واحدة (اختياري)
        [HttpPost]
        [Route("InsertSingle")]
        public IActionResult InsertSingle([FromBody] StudentAnswerDL request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Success = false, Message = "Bad payload" });

                var res = _repo.InsertSingle(request);
                return Ok(new { Success = true, Message = "", Data = res });
            }
            catch (Exception ex)
            {
                return BadRequest(_mainHelper.GetException(ex));
            }
        }
    }
}
