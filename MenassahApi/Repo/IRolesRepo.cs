using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IRolesRepo

    {
        public string Insert(RolesDL RolesDL);
        public string Update(RolesDL RolesDL);
        public string Delete(int RoleID);
        public DataSet GetAll();
        public DataSet GetByID(int RoleID);


    }
}
