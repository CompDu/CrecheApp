using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public interface IDataAccess
    {
         List<T> LoadData<T,U>( string Procedure,U parameter);
         int SaveData<U>(string Procedure, U parameter );

    }
}
