﻿using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    public partial class AuditTrailConfigurationDTO : ZDTOBase<AuditTrailConfigurationDTO, AuditTrailConfiguration>
    {
        #region Properties
               
        public virtual int Id { get; set; }
               
        public virtual string Domain { get; set; }
               
        public virtual string Entity { get; set; }
               
        public virtual string LogOperations { get; set; }
               
        public virtual string LogMode { get; set; }

        #endregion Properties

        #region Methods
        
        public AuditTrailConfigurationDTO()
        {
            Id = LibraryDefaults.Default_Int32;
            Domain = LibraryDefaults.Default_String;
            Entity = LibraryDefaults.Default_String;
            LogOperations = null;
            LogMode = null;
            LookupText = null;
        }
        
        public AuditTrailConfigurationDTO(
            int id,
            string domain,
            string entity,
            string logOperations = null,
            string logMode = null
        )
        {
            Id = id;
            Domain = domain;
            Entity = entity;
            LogOperations = logOperations;
            LogMode = logMode;
            LookupText = null;
        }

        public AuditTrailConfigurationDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<AuditTrailConfigurationDTO, AuditTrailConfiguration> GetDataSelector()
        {
            return x => new AuditTrailConfiguration
            (
                x.Id,
                x.Domain,
                x.Entity,
                x.LogOperations,
                x.LogMode
            );
        }

        public override Func<AuditTrailConfiguration, AuditTrailConfigurationDTO> GetDTOSelector()
        {
            return x => new AuditTrailConfigurationDTO
            (
                x.Id,
                x.Domain,
                x.Entity,
                x.LogOperations,
                x.LogMode
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AuditTrailConfiguration auditTrailConfiguration = (AuditTrailConfiguration)data;
                AuditTrailConfigurationDTO dto = (new List<AuditTrailConfiguration> { auditTrailConfiguration })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = auditTrailConfiguration.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<AuditTrailConfigurationDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
