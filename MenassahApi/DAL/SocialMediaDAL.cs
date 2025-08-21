using DocumentFormat.OpenXml.Spreadsheet;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class SocialMediaDAL : SharedCode, ISocialMediaRepo
    {
        public SocialMediaDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(SocialMediaDL SocialMediaDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_Insert", CommandType.StoredProcedure);

                         cmd.Parameters.Add(GetParameter("@TeacherID", SocialMediaDL.TeacherID));
                         cmd.Parameters.Add(GetParameter("@PlatformName", SocialMediaDL.PlatformName));
                         cmd.Parameters.Add(GetParameter("@ProfileLink", SocialMediaDL.ProfileLink));
                         cmd.Parameters.Add(GetParameter("@IsMain", SocialMediaDL.IsMain));

                        SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public string Update(SocialMediaDL SocialMediaDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@SocialID", SocialMediaDL.SocialID));
            cmd.Parameters.Add(GetParameter("@TeacherID", SocialMediaDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@PlatformName", SocialMediaDL.PlatformName));
            cmd.Parameters.Add(GetParameter("@ProfileLink", SocialMediaDL.ProfileLink));
            cmd.Parameters.Add(GetParameter("@IsMain", SocialMediaDL.IsMain));



            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public string Delete(int SocialID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@SocialID", SocialID));


            SqlParameter pID = GetParameterReturnValue("@SocialID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public DataSet GetAll()
        {

            SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByTeacherID(int TeacherID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_GetByTeacherID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

                public DataSet GetByID(int SocialID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherSocialMedia_GetByID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@SocialID", SocialID));
            var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

