using System.Data;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using MenassahApi.DL;
using MenassahApi.Repo;
namespace Menassah.Shared
{
    public class DashboardDAL :  SharedCode , IDashboardRepo
    {
        public DashboardDAL(IConfiguration configuration) : base(configuration)
        {
        }
            public DataSet GeneralStats()
    {

        SqlCommand cmd = GetCommand("spx_Dashboard_GeneralStats", CommandType.StoredProcedure);
        //cmd.Parameters.Add(GetParameter("@fID", ClientID));
        //cmd.Parameters.Add(GetParameter("@LangID", language));
        var ds = GetDataSet(cmd, 999);
        return ds;

    }

}
}

