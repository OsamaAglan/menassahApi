using DocumentFormat.OpenXml.Office2010.Word;
using MenassahApi.DL;
using System.Data;

namespace MenassahApi.Repo
{
    public partial interface IUploadsRepo

    {
        public string Insert(UploadsDL  uploadsDL);
        //public string Update(UploadsDL  uploadsDL);
        public string Delete(int  uploadID);
        public DataSet GetAll();
        public DataSet GetByID(int  uploadID);
        public DataSet GetByTeacherID(int TeacherID);
        public DataSet GetByGroupID(int GroupID);


    }
}
