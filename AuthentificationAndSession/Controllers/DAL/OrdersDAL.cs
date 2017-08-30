using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Controllers.DAL
{
    public class OrdersDAL : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        public OrdersDAL(string connection) :
            base(connection, mappingSource)
        {
        }

        [Function(Name = "[dbo].[Orders.Insert]")]
        public Int32 OrdersInsert(
              [Parameter(Name = "@UserName", DbType = "nvarchar(50)")] String userName,
              [Parameter(Name = "@OrdersDate", DbType = "Datetime")] DateTime ordersDate)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), userName, ordersDate);
            return ((Int32)(result.ReturnValue));
        }

        [Function(Name = "[dbo].[OrdersDetails.Insert]")]
        public Int32 OrdersDetailsInsert(
              [Parameter(Name = "@OrdersId", DbType = "int")] Int32 ordersId,
              [Parameter(Name = "@ProductsId", DbType = "int")] Int32 productsId)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), ordersId, productsId);
            return ((Int32)(result.ReturnValue));
        }

        [Function(Name = "[dbo].[Orders.SelectFromUserName]")]
        public ISingleResult<OrdersModels> SelectFromUserName(
              [Parameter(Name = "@UserName", DbType = "nvarchar(50)")] String userName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), userName);
            return ((ISingleResult<OrdersModels>)(result.ReturnValue));
        }

        [Function(Name = "[dbo].[Orders.SelectProductsFromOrdersId]")]
        public ISingleResult<ProductsModels> SelectProductsFromOrdersId(
              [Parameter(Name = "@OrdersId", DbType = "int")] Int32 ordersId)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), ordersId);
            return ((ISingleResult<ProductsModels>)(result.ReturnValue));
        }
    }
}
