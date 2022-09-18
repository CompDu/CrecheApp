using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class ForumAccess : IForumAccess
    {
        private SqlDataAccess sqlDataAccess;
        private readonly IConfiguration configuration;
        public ForumAccess(IConfiguration config)
        {
            configuration = config;
            sqlDataAccess = new SqlDataAccess(configuration);
           
        }

        public List<ForumModel> GetData<ForumModel>()
        {
            var parameters = new DynamicParameters();
            return sqlDataAccess.LoadData<ForumModel, DynamicParameters>("dbo.spForum_GetAll", parameters);

        }

        public int SetData(ForumModel forumModel)
        {
            // not implemented yet
            var parameters = new DynamicParameters();
            parameters.Add("Comment", forumModel.Comment);
            parameters.Add("UserId", forumModel.UserId);
            return sqlDataAccess.SaveData<dynamic>("dbo.spForum_InsertComment", parameters);

        }



    }
}
