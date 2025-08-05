using MenassahApi.DL;
using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ICitiesRepo

    {
        public string Insert(CitiesDL IM2OrdersDL);
        public string Update(CitiesDL IM2OrdersDL);
        public string Delete(int CityID);
        public DataSet GetAll();
        public DataSet GetByID(int TypeID);


    }
}
