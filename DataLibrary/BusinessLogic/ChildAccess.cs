using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class ChildAccess
    {

        private IDataAccess SqlDataAccess;

        public ChildAccess(IDataAccess _SqlDataAccess)
        {
            SqlDataAccess = _SqlDataAccess;
        }

        public void DeleteRow(int ChildId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", ChildId);
            SqlDataAccess.SaveData("spChild_DeleteChild", parameters);
        }

        public List<ChildModel> GetRows(int ParentId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ParentId",ParentId);
            return SqlDataAccess.LoadData<ChildModel, dynamic>("spChild_GetChildrenByParent",parameters);
        }

        public ChildModel GetRow(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id",Id);
            List<ChildModel> Children = SqlDataAccess.LoadData<ChildModel, dynamic>("spChild_GetChild",parameters);
            return Children[0];
        }

        public int SetRow(ChildModel child)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ParentId",child.ParentId);
            parameters.Add("Name",child.Name);
            parameters.Add("Surname",child.Surname);
            parameters.Add("IDNumber", child.IDNumber);
            return SqlDataAccess.SaveData("spChild_InsertChild",parameters);
        }
    }
}
