using System.Collections.Generic;

namespace DataLibrary.BusinessLogic
{
    public interface IForumAccess
    {
        List<ForumModel> GetData<ForumModel>();
        int SetData(ForumModel forumModel);
    }
}