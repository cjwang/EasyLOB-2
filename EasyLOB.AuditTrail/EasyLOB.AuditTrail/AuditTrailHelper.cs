﻿using EasyLOB.Library;

namespace EasyLOB.AuditTrail
{
    public static partial class AuditTrailHelper
    {
        #region Properties

        public static bool IsAuditTrail
        {
            get
            {
                return ConfigurationHelper.AppSettings<bool>("EasyLOB.AuditTrail");
            }
        }

        #endregion Properties

    }
}