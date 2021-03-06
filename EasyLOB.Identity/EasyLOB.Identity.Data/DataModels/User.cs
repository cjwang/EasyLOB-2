using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Identity.Data
{
    public partial class User : ZDataBase
    {
        #region Properties

        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual string UserName { get; set; }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<UserClaim> UserClaims { get; }

        public virtual IList<UserLogin> UserLogins { get; }

        public virtual IList<UserRole> UserRoles { get; }

        #endregion Collections (PK)

        #region Methods

        public User()
        {
            Id = LibraryDefaults.Default_String;
            EmailConfirmed = LibraryDefaults.Default_Boolean;
            PhoneNumberConfirmed = LibraryDefaults.Default_Boolean;
            TwoFactorEnabled = LibraryDefaults.Default_Boolean;
            LockoutEnabled = LibraryDefaults.Default_Boolean;
            AccessFailedCount = LibraryDefaults.Default_Int32;
            UserName = LibraryDefaults.Default_String;
            Email = null;
            PasswordHash = null;
            SecurityStamp = null;
            PhoneNumber = null;
            LockoutEndDateUtc = null;

            UserClaims = new List<UserClaim>();
            UserLogins = new List<UserLogin>();
            UserRoles = new List<UserRole>();

            OnConstructor();
        }

        public User(string id)
            : this()
        {
            Id = id;
        }

        public User(
            string id,
            bool emailConfirmed,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled,
            bool lockoutEnabled,
            int accessFailedCount,
            string userName,
            string email = null,
            string passwordHash = null,
            string securityStamp = null,
            string phoneNumber = null,
            DateTime? lockoutEndDateUtc = null
        )
            : this()
        {
            Id = id;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEndDateUtc = lockoutEndDateUtc;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            UserName = userName;
        }

        public override object[] GetId()
        {
            return new object[] { Id };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                Id = DataHelper.IdToString(ids[0]);
            }
        }

        #endregion Methods
    }
}