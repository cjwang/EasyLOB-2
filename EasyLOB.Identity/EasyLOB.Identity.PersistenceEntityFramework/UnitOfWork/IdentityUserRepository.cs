﻿using EasyLOB.Identity.Data;
using EasyLOB.Persistence;
using Microsoft.AspNet.Identity;
using System;

namespace EasyLOB.Identity.Persistence
{
    public class IdentityUserRepository : IdentityGenericRepositoryEF<User>
    {
        #region Methods

        public IdentityUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override bool Create(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = new ApplicationUser { UserName = entity.UserName, Email = entity.Email, EmailConfirmed = true };
                IdentityResult identityResult = IdentityHelperEF.UserManager.Create(user, entity.PasswordHash);
                if (!identityResult.Succeeded)
                {
                    (operationResult as ZOperationResult).ParseIdentityResult(identityResult);
                }
                else
                {
                    entity.Id = user.Id;
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public override bool Delete(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = IdentityHelperEF.UserManager.FindById(entity.Id);
                if (user != null)
                {
                    IdentityResult identityResult = IdentityHelperEF.UserManager.Delete(user);
                    if (!identityResult.Succeeded)
                    {
                        (operationResult as ZOperationResult).ParseIdentityResult(identityResult);
                    }
                }
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public override bool Update(ZOperationResult operationResult, User entity)
        //public override async void Update(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = IdentityHelperEF.UserManager.FindById(entity.Id);

                user.Email = entity.Email;
                IdentityResult validEmail = new IdentityResult();
                //validEmail = await IdentityHelperEF.UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    (operationResult as ZOperationResult).ParseIdentityResult(validEmail);
                }

                IdentityResult validPassword = new IdentityResult();
                if (!String.IsNullOrEmpty(entity.PasswordHash))
                {
                    //validPassword = await IdentityHelperEF.UserManager.PasswordValidator.ValidateAsync(entity.PasswordHash);
                    if (validPassword.Succeeded)
                    {
                        user.PasswordHash = IdentityHelperEF.UserManager.PasswordHasher.HashPassword(entity.PasswordHash);
                    }
                    else
                    {
                        (operationResult as ZOperationResult).ParseIdentityResult(validPassword);
                    }
                }

                if (validEmail.Succeeded && validPassword.Succeeded)
                {
                    IdentityResult identityResult = IdentityHelperEF.UserManager.Update(user);
                    if (!identityResult.Succeeded)
                    {
                        (operationResult as ZOperationResult).ParseIdentityResult(identityResult);
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
    }
}