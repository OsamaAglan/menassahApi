using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IRolesRepo

    {
        public string Insert(RolesDL IM2OrdersDL);
        public string Update(RolesDL IM2OrdersDL);
        public string Delete(int RoleID);
        public DataSet GetAll();
        public DataSet GetByID(int RoleID);


    }
}
