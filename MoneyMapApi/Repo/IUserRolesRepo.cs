using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IUserRolesRepo

    {
        public string Insert(UserRolesDL IM2OrdersDL);
        public string Update(UserRolesDL IM2OrdersDL);
        public string Delete(int UserRoleID);
        public DataSet GetAll();
        public DataSet GetByID(int UserRoleID);


    }
}
