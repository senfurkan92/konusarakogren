using BLL.Data.Service;
using CORE.Data.Abstract;
using CORE.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Manager
{
    public class Manager_Generic<T, TIDal> : IService_Generic<T>
        where T : class, IBaseModel
        where TIDal : IRepository<T>
    {
        readonly protected TIDal dal;

        public Manager_Generic(TIDal dal)
        {
            this.dal = dal;
        }


        public IDbContextTransaction BeginTransaction()
        {
            return dal.BeginTransaction();
        }

        public void CommitTransaction(IDbContextTransaction transaction)
        {
            dal.CommitTransaction(transaction);
        }

        public ResultModel Delete(T baseModel)
        {
            return dal.Delete(baseModel);
        }

        public ResultModel<T> Get(Expression<Func<T, bool>> filter, params string[] tables)
        {
            return dal.Get(filter, tables);
        }

        public ResultModel<IQueryable<T>> Gets(Expression<Func<T, bool>> filter = null, PropertyInfo orderby = null, bool desc = false, int skip = 0, int take = 30, params string[] tables)
        {
            return dal.Gets(filter, orderby, desc, skip, take, tables);
        }

        public ResultModel<T> Insert(T baseModel)
        {
            return dal.Insert(baseModel);
        }

        public void RoolbackTransaction(IDbContextTransaction transaction)
        {
            dal.RoolbackTransaction(transaction);
        }

        public ResultModel<T> Update(T baseModel)
        {
            return dal.Update(baseModel);
        }
    }
}
