using CORE.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Service
{
    public interface IService_Generic<T> 
        where T : class, IBaseModel
    {
        ResultModel<T> Insert(T baseModel);

        ResultModel<T> Update(T baseModel);

        ResultModel Delete(T baseModel);

        ResultModel<T> Get(Expression<Func<T, bool>> filter, params string[] tables);

        ResultModel<IQueryable<T>> Gets(Expression<Func<T, bool>> filter = null, PropertyInfo orderby = null, bool desc = false, int skip = 0, int take = 30, params string[] tables);

        IDbContextTransaction BeginTransaction();

        void CommitTransaction(IDbContextTransaction transaction);

        void RoolbackTransaction(IDbContextTransaction transaction);
    }
}
