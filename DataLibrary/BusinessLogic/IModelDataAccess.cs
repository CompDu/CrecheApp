using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public interface IModelDataAccess
    {
        T getRow<T>(int id);
        T setRow<T>(int id, dynamic value);
        List<T> getRows<T>();

    }
}
