using System.Collections.Generic;

namespace DataLibrary.BusinessLogic
{
    public interface IForumAccess
    {
        List<ForumModel> GetData<ForumModel>(string procedure,dynamic param);
        int SetData(string procedure,dynamic param);
    }
}