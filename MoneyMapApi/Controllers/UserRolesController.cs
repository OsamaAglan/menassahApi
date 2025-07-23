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
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesRepo _UserRolesRepo;

        public UserRolesController(IUserRolesRepo UserRoles, IMainHelper mainHelper)
        {
            _UserRolesRepo = UserRoles;
            mainHelperRepo = mainHelper;
        }

        private readonly IMainHelper mainHelperRepo;

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(UserRolesDL UserRolesDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _UserRolesRepo.Insert(UserRolesDL);
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
        public ActionResult Update(UserRolesDL UserRolesDL)
        {
            GeneralResponse resonse;
            try
            {
                string ID = _UserRolesRepo.Update(UserRolesDL);
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
        [Route("Delete/{UserRolesID}")]
        public ActionResult Delete(int UserRolesID)
        {
            GeneralResponse resonse;
            try
            {
                string x = _UserRolesRepo.Delete(UserRolesID);
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

            DataSet ds = _UserRolesRepo.GetAll();
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
        [Route("GetByID/{UserRolesID}")]
        public IActionResult GetByID(int UserRolesID)
        {
            var request = Request; //Current

            string Language = "aa";
            //string Language = mainHelperRepo.GetLanguage(request);

            DataSet ds = _UserRolesRepo.GetByID(UserRolesID);
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