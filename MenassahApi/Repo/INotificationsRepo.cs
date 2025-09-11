using DocumentFormat.OpenXml.Office2010.PowerPoint;
using MenassahApi.DL;
using System.Data;

namespace MenassahApi.Repo
{
    public partial interface INotificationsRepo

    {
        public string Insert(NotificationsDL NotificationsDL);
        public string MarkAsRead(int NotificationID);
        public string Delete(int NotificationID);
         public DataSet GetByUserID(int UserID);


    }
}
