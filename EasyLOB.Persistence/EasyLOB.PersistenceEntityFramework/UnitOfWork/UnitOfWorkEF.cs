﻿using EasyLOB.Data;
using EasyLOB.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

// Install-Package EntityFramework

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkEF : IUnitOfWork
    {
        #region Properties

        public IAuthenticationManager AuthenticationManager { get; }

        private ZDatabaseLogger _databaseLogger;

        public ZDatabaseLogger DatabaseLogger
        {
            get
            {
                return _databaseLogger;
            }
            set
            {
                if (value == ZDatabaseLogger.None)
                {
                    Context.Database.Log = null;
                }
                else if (value != _databaseLogger)
                {
                    Context.Database.Log = log => EntityFrameworkHelper.Log(log, value);
                }

                _databaseLogger = value;
            }
        }

        public ZDBMS DBMS
        {
            get { return Context.GetDBMS(); }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties Entity Framework

        public DbContext Context { get; protected set; }

        public DbContextTransaction Transaction { get; protected set; }

        #endregion Properties Entity Framework

        #region Methods

        public UnitOfWorkEF(DbContext context, IAuthenticationManager authenticationManager)
        {
            Context = context;

            AuthenticationManager = authenticationManager;
            DatabaseLogger = ZDatabaseLogger.None;
            Domain = "";
            Repositories = new Dictionary<Type, object>();
        }

        public virtual bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction == null || Transaction.UnderlyingTransaction.Connection == null)
                    {
                        Transaction = Context.Database.BeginTransaction(isolationLevel);
                    }
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction != null)
                    {
                        Transaction.Commit();
                    }
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public virtual int SQLCommand(string sql)
        {
            return Context.Database.ExecuteSqlCommand(sql);
        }

        public virtual List<T> SQLQuery<T>(string sql)
        {
            return Context.Database.SqlQuery<T>(sql).ToList();
        }

        public IZProfile GetProfile<TEntity>()
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntity>().Profile;
        }

        public virtual IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntity>().Query();
        }

        public virtual IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IZDataBase
        {
            throw new NotImplementedException("abstract class Entity Framework UnitOfWork.GetRepository()");

            //if (!Repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    var repository = new GenericRepository<TEntity>(Context);
            //    Repositories.Add(typeof(TEntity), repository);
            //}

            //return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction != null)
                    {
                        Transaction.Rollback();
                    }
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                foreach (DbEntityValidationResult validationErrors in exception.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in validationErrors.ValidationErrors)
                    {
                        ZOperationError operationError = new ZOperationError("", validationError.ErrorMessage, "", new List<string>() { validationError.PropertyName });

                        operationResult.OperationErrors.Add(operationError);
                    }
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods

        #region Methods IDispose

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = null;
                    }
                }

                disposed = true;
            }
        }

        #endregion Methods IDispose

        #region Triggers

        public virtual bool BeforeCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }
 
        #endregion Triggers
    }
}