﻿using System.Collections.Generic;
using System.Web.Optimization;

// Install-Package Microsoft.AspNet.Web.Optimization

namespace EasyLOB.Library.Mvc
{
    public class AsIsBundleOrderer : IBundleOrderer
    {
        #region Methods

        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }

        #endregion Methods
    }
}