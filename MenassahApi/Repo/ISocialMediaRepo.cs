using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ISocialMediaRepo

    {
        public string Insert(SocialMediaDL SocialMediaDL);
        public string Update(SocialMediaDL SocialMediaDL);
        public string Delete(int SocialID);
        public DataSet GetAll();
        public DataSet GetByID(int SocialID);
        public DataSet GetByTeacherID(int TeacherID);



    }
}
