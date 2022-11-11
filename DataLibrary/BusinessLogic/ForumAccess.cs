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
    public class ForumAccess
    {
        private IDataAccess sqlDataAccess;
       // private readonly IConfiguration configuration;
        public ForumAccess(IDataAccess _sqlDataAccess)
        {

            sqlDataAccess = _sqlDataAccess;
           
        }
        public int Create(ForumModel forumModel)
        {
            var Parameters = new DynamicParameters();
            Parameters.Add("@DateCreated", forumModel.CreatedDate);
            Parameters.Add("@Comment", forumModel.Comment);
            Parameters.Add("@UserId", forumModel.UserId);
            return sqlDataAccess.SaveData<dynamic>("dbo.spForum_InsertComment", Parameters);
        }

        public int Update(ForumModel forumModel)
        {
            var Parameters = new DynamicParameters();
            Parameters.Add("@CommentId", forumModel.CommentId);
            Parameters.Add("@Comment", forumModel.Comment);
            return sqlDataAccess.SaveData<dynamic>("dbo.spForum_UpdateComment", Parameters);
        }
        public ForumModel GetRow(int id)
        {
            var arguments = new DynamicParameters();
            arguments.Add("@CommentId", id);
            List<ForumModel> CommentsList = sqlDataAccess.LoadData<ForumModel,dynamic>("dbo.spForum_GetRecord",arguments);
            return CommentsList.FirstOrDefault<ForumModel>();
        }

        public List<ForumModel> GetRows()
        {
            var parameters = new DynamicParameters();
            return sqlDataAccess.LoadData<ForumModel, DynamicParameters>("dbo.spForum_GetAll", parameters);
        }

        public int SetRows(ForumModel forumModel)
        {
            var Parameters = new DynamicParameters();
            Parameters.Add("@DateCreated",forumModel.CreatedDate);
            Parameters.Add("@Comment",forumModel.Comment);
            Parameters.Add("@UserId",forumModel.UserId);
            return sqlDataAccess.SaveData<dynamic>("dbo.spForum_InsertComment", Parameters);
        }

        public List<ForumModel> GetData<ForumModel>(string Procedure,dynamic par)
        {
            var parameters = par;
            return sqlDataAccess.LoadData<ForumModel, DynamicParameters>(Procedure, parameters);

        }

        public int SetData(string procedure,dynamic par)
        {
            // not implemented yet
            
            return sqlDataAccess.SaveData<dynamic>(procedure, par);

        }



    }
}
