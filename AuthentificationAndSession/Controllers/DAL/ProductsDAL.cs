using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Controllers.DAL
{
    public class ProductsDAL : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        public ProductsDAL(string connection) :
            base(connection, mappingSource)
        {
        }

        [Function(Name = "[dbo].[Products.SelectAll]")]
        public ISingleResult<ProductsModels> SelectAll()
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())));
            return ((ISingleResult<ProductsModels>)(result.ReturnValue));
        }
    }
}