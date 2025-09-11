using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class NotificationsDAL : SharedCode, INotificationsRepo
    {
        public NotificationsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(NotificationsDL NotificationsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Notifications_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@Title", NotificationsDL.Title));
            cmd.Parameters.Add(GetParameter("@Message", NotificationsDL.Message));
            cmd.Parameters.Add(GetParameter("@NotificationTypeID", NotificationsDL.NotificationTypeID));
            cmd.Parameters.Add(GetParameter("@ToUserID", NotificationsDL.ToUserID));
            cmd.Parameters.Add(GetParameter("@LinkUrl", NotificationsDL.LinkUrl));




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
        public string MarkAsRead(int NotificationID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Notifications_MarkAsRead", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@NotificationId", NotificationID));
 

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
        public string Delete(int NotificationID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Notifications_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@NotificationID", NotificationID));


            SqlParameter pID = GetParameterReturnValue("@BindHdrID");
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
        public DataSet GetByUserID(int UserID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Notifications_GetByUserID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@UserID", UserID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

