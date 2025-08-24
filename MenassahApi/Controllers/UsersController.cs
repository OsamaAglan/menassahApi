using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRolesRepo _userRolesRepo;
        private readonly ITokenService _tokenService;

        private readonly IUsersRepo _UsersRepo;

        public UsersController(
            IUsersRepo Users,
            IMainHelper mainHelper,
            IUserRolesRepo userRolesRepo,
            ITokenService tokenService)
        {
            _UsersRepo = Users;
            mainHelperRepo = mainHelper;
            _userRolesRepo = userRolesRepo;
            _tokenService = tokenService;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(UsersDL UsersDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _UsersRepo.Insert(UsersDL);
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
        public ActionResult Update(UsersDL UsersDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _UsersRepo.Update(UsersDL);
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
        [Route("Delete/{UsersID}")]
        public ActionResult Delete(int UsersID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _UsersRepo.Delete(UsersID);
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

            DataSet ds = _UsersRepo.GetAll();
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
        [Route("Login")]
        public IActionResult Login(UsersDL UsersDL)
        {
            try
            {
                DataSet ds = _UsersRepo.Login(UsersDL);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {

                    return Unauthorized(new GeneralResponse
                    {
                        ID = "",
                        Message = "بيانات الدخول غير صحيحة",
                        Success = false
                    });
                }

                // استخراج بيانات المستخدم من الجدول
                var row = ds.Tables[0].Rows[0];
                int userId = Convert.ToInt32(row["UserID"]);
                string userName = row["UserName"].ToString();

                int personID = Convert.ToInt32(row["PersonID"]);
                string personName = row["PersonName"]?.ToString() ?? "";
                string avatar = row["avatar"]?.ToString() ?? "";
                string role = row["Role"]?.ToString() ?? "";


                //int? TeacherID = row["TeacherID"] != DBNull.Value ? Convert.ToInt32(row["TeacherID"]) : (int?)null;
                //string TeacherName = row["TeacherName"]?.ToString() ?? "";
                //int? StudentID = row["StudentID"] != DBNull.Value ? Convert.ToInt32(row["StudentID"]) : (int?)null;
                //string StudentName = row["StudentName"]?.ToString() ?? "";

                // جلب الأدوار من UserRolesRepo
                List<string> roles = _userRolesRepo.GetUserRoles(userId);


                // توليد التوكن
                string token = _tokenService.GenerateToken(userId, userName, roles, personID,personName,role, avatar);

                // إرسال النتيجة
                return Ok(new GeneralResponse
                {
                    ID = userId.ToString(),
                    Message = "Login successful",
                    Success = true,
                    Data = new
                    {
                        token,
                        userId,
                        userName,
                        roles,
                        //personID,
                        //personName,
                        //role
                        //TeacherID,
                        //TeacherName,
                        //StudentID,
                        //StudentName
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(mainHelperRepo.GetException(ex));
            }
        }

        [HttpGet]
        [Route("GetByID/{UsersID}")]
        public IActionResult GetByID(int UsersID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _UsersRepo.GetByID(UsersID);
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