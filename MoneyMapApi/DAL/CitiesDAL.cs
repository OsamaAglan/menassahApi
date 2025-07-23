using DocumentFormat.OpenXml.Bibliography;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class CitiesDAL : SharedCode, ICitiesRepo
    {
        public CitiesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(CitiesDL CitiesDL)
        {
            SqlCommand cmd = GetCommand("tbl_Cities_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@CityName", CitiesDL.CityName));



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
        public string Update(CitiesDL CitiesDL)
        {
            SqlCommand cmd = GetCommand("tbl_Cities_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@CityID", CitiesDL.CityID));
            cmd.Parameters.Add(GetParameter("@CityName", CitiesDL.CityName));


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
        public string Delete(int CityID)
        {
            SqlCommand cmd = GetCommand("tbl_Cities_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@CityID", CityID));


            SqlParameter pID = GetParameterReturnValue("@CityID");
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

            SqlCommand cmd = GetCommand("tbl_Cities_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int CityID)
{

    SqlCommand cmd = GetCommand("tbl_Cities_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@CityID", CityID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

