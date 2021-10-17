using CORE.Data.Abstract;
using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Data.Concrete
{
    public class Repository<T, TContext> : IRepository<T>
        where T : class, IBaseModel
        where TContext : DbContext
    {
        protected readonly TContext context;
        protected readonly DbSet<T> dbset;

        public Repository(TContext context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }

        #region crud
        public ResultModel<T> Insert(T baseModel)
        {
            ResultModel<T> result = null;
            baseModel.IsActive = true;
            baseModel.IsDeleted = false;
            baseModel.InsertDate = DateTime.Now;
            baseModel.UpdateDate = DateTime.Now;
            try
            {
                var queryResult = dbset.Add(baseModel);
                if (context.SaveChanges() > 0 && queryResult.Entity != null)
                {
                    result = new ResultModel<T>(queryResult.Entity, true, "added");
                }
                else
                {
                    result = new ResultModel<T>(queryResult.Entity, false, "not added", new List<string>() { "not added"});
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<T>(null, false, "not added", new List<string>() { ex.GetInnestExp().Message });
            }
            return result;
        }

        public ResultModel<T> Update(T baseModel)
        {
            ResultModel<T> result = null;
            baseModel.UpdateDate = DateTime.Now;
            try
            {
                var queryResult = dbset.Update(baseModel);
                if (context.SaveChanges() > 0 && queryResult.Entity != null)
                {
                    result = new ResultModel<T>(queryResult.Entity, true, "updated");
                }
                else
                {
                    result = new ResultModel<T>(queryResult.Entity, false, "not updated", new List<string>() { "not updated" });
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<T>(null, false, "not updated", new List<string>() { ex.GetInnestExp().Message });
            }
            return result;
        }

        public ResultModel Delete(T baseModel)
        {
            baseModel.DeleteDate = DateTime.Now;
            baseModel.IsDeleted = true;
            baseModel.IsActive = false;
            var result = this.Update(baseModel);
            if (result.Success)
            {
                result.Description = "updated";
            }
            else
            {
                result.Description = "not updated";
            }
            return result;
        }

        public ResultModel<T> Get(Expression<Func<T, bool>> filter, params string[] tables)
        {
            ResultModel<T> result = null;
            try
            {
                var extended = dbset as IQueryable<T>;
                for (int i = 0; i < tables.Length; i++) extended = extended.Include(tables[i]);
                var data = extended.FirstOrDefault(filter);
                var description = data != null ? "found" : "not found";
                result = new ResultModel<T>(data,true,description);
            }
            catch (Exception ex)
            {
                result = new ResultModel<T>(null, false, "not found beacause of error(s)", new List<string>() { ex.GetInnestExp().Message});
            }
            return result;
        }

        public ResultModel<IQueryable<T>> Gets(Expression<Func<T, bool>> filter = null, PropertyInfo orderby = null, bool desc = false, int skip = 0, int take = 30, params string[] tables)
        {
            ResultModel<IQueryable<T>> result = null;
            try
            {
                var extended = dbset as IQueryable<T>;

                // include
                for (int i = 0; i < tables.Length; i++) extended = extended.Include(tables[i]);

                // where
                if (filter != null)
                {
                    extended = extended.Where(filter);
                }

                // order and sort
                extended = this.CustomOrderBy(extended, orderby != null ? orderby : typeof(T).GetProperty("Id"), desc);

                // skip take
                if (take > 0)
                {
                    extended = extended.Skip(skip).Take(take);
                }
                else
                {
                    extended = extended.Skip(skip);
                }

                var description = extended != null && extended.Count()>0 ? "found" : "not found";
                result = new ResultModel<IQueryable<T>>(extended, true, description);   
            }
            catch (Exception ex)
            {
                result = new ResultModel<IQueryable<T>>(null, false, "not found beacause of error(s)", new List<string>() { ex.GetInnestExp().Message });
            }
            return result;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }

        public void RoolbackTransaction(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }
        #endregion

        #region private methods
        private IQueryable<T> CustomOrderBy(IQueryable<T> source, PropertyInfo propertyInfo, bool desc)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var order = desc ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(typeof(Queryable), order, new Type[] { type, propertyInfo.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }
        #endregion
    }
}
