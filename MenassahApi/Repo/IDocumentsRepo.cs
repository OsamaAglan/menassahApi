using DocumentFormat.OpenXml.Office2010.Word;
using MenassahApi.DL;
using System.Data;

namespace MenassahApi.Repo
{
    public partial interface DocumentsRepo

    {
        public string Insert(DocumentsDL DocumentsDL);
        public string Update(DocumentsDL DocumentsDL);
        public string Delete(int DocumentID);
        public DataSet GetAll();
        public DataSet GetByID(int DocumentID);
        public DataSet GetByBeneficiaryID(int BeneficiaryID);


    }
}
