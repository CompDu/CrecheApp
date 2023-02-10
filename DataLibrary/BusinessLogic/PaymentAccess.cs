using Dapper;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class PaymentAccess
    {
        private IDataAccess SqlDataAccess;

        public PaymentAccess(IDataAccess _SqlDataAccess)
        {
            SqlDataAccess = _SqlDataAccess;
        }

        public PaymentModel GetRow(int ParentId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ParentId", ParentId);
            List<PaymentModel> TempList = SqlDataAccess.LoadData<PaymentModel, dynamic>("spPayment_GetPaymentByParent", parameters);
            return TempList.FirstOrDefault();
        }

        public List<PaymentModel> GetRows(int ParentId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ParentId", ParentId);
            return SqlDataAccess.LoadData<PaymentModel, dynamic>("spPayment_GetPaymentByParent", parameters);
        }

        public List<PaymentModel> GetRows()
        {
            DynamicParameters parameters = new DynamicParameters();
            return SqlDataAccess.LoadData<PaymentModel, dynamic>("spPayment_GetAllPayments", parameters);
        }

        public int SetRow(PaymentModel payment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ParentId", payment.ParentId);
            parameters.Add("Amount", payment.Amount);
            return SqlDataAccess.SaveData("spPayment_InsertPayment", parameters);
        }
    }
}
