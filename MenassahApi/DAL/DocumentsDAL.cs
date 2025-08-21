using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class DocumentsDAL : SharedCode, DocumentsRepo
    {
        public DocumentsDAL(IConfiguration configuration) : base(configuration)
        {
        }
        public string Update(DocumentsDL DocumentsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Documents_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@documentId", DocumentsDL.documentId));
            cmd.Parameters.Add(GetParameter("@beneficiaryId", DocumentsDL.beneficiaryId));
            cmd.Parameters.Add(GetParameter("@documentTypeId", DocumentsDL.documentTypeId));
            cmd.Parameters.Add(GetParameter("@filePath", DocumentsDL.filePath));



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
        public string Delete(int DocumentID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Documents_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@DocumentID", DocumentID));


            SqlParameter pID = GetParameterReturnValue("@DocumentID");
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

            SqlCommand cmd = GetCommand("spx_tbl_Documents_GetAll", CommandType.StoredProcedure);
            //cmd.Parameters.Add(GetParameter("@fID", ClientID));
            //cmd.Parameters.Add(GetParameter("@LangID", language));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int DocumentID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Documents_GetByID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@DocumentID", DocumentID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByBeneficiaryID(int BeneficiaryID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Documents_GetByBeneficiaryID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@BeneficiaryID", BeneficiaryID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }



        public string Insert(DocumentsDL DocumentsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Documents_Insert", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@beneficiaryId", DocumentsDL.beneficiaryId));
            cmd.Parameters.Add(GetParameter("@documentTypeId", DocumentsDL.documentTypeId));
            cmd.Parameters.Add(GetParameter("@filePath", DocumentsDL.filePath));



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



    }
}

