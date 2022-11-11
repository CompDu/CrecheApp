using Dapper;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class ParentAccess
    {
        private IDataAccess _access;
        public ParentAccess(IDataAccess dataAccess)
        {
            _access = dataAccess;
        }

        public ParentModel GetRow(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("Id",id);
            List<ParentModel> TempList = _access.LoadData<ParentModel,dynamic>("dbo.spParent_GetParent",parameter);
            return TempList.FirstOrDefault();
        }

        public List<ParentModel> GetRows()
        {
            DynamicParameters parameter = new DynamicParameters();
            return _access.LoadData<ParentModel, dynamic>("dbo.spParent_GetAllParents", parameter);
        }

       
        public int SetRows(ParentModel parentModel)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("Name",parentModel.Name);
            parameter.Add("Surname",parentModel.Surname);
            parameter.Add("IDNumber",parentModel.IDNumber);
            parameter.Add("Address",parentModel.Address);
            parameter.Add("PhoneNumber", parentModel.PhoneNumber);
            parameter.Add("UserId",parentModel.UserId);

            return _access.SaveData<dynamic>("spParent_InsertParent",parameter);

        }

        public ParentModel GetRow(string UserName)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("UserId", UserName);
            List<ParentModel> TempList = _access.LoadData<ParentModel, dynamic>("dbo.spParent_GetParentByUserId", parameter);
            return TempList.FirstOrDefault();
        }

        public int EditRow(ParentModel parentModel)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("Id", parentModel.Id);
            parameter.Add("Name", parentModel.Name);
            parameter.Add("Surname", parentModel.Surname);
            parameter.Add("IDNumber", parentModel.IDNumber);
            parameter.Add("Address", parentModel.Address);
            parameter.Add("PhoneNumber", parentModel.PhoneNumber);
            parameter.Add("UserId", parentModel.UserId);

            return _access.SaveData<dynamic>("spParent_UpdateParent", parameter);

        }
    }
}
