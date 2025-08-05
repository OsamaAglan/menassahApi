using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IUsersRepo

    {
        public string Insert(UsersDL IM2OrdersDL);
        public string Update(UsersDL IM2OrdersDL);
        public string Delete(int UserID);
        public DataSet Login(UsersDL IM2OrdersDL);
        public DataSet GetAll();
        public DataSet GetByID(int UserID);


    }
}
