//-----------------------------------------------------------------------
// <copyright file=" TGameLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGameLog.cs
// * history : Created by T4 01/22/2019 15:08:31 
// </copyright>
//-----------------------------------------------------------------------
//using NFine.Domain.Entity.H5Game;
//using NFine.Domain.IRepository.H5Game;
//using NFine.Repository.H5Game;
using NFine.Domain.Entity.TGameLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.TGameLog
{
    public class TGameLogMap : EntityTypeConfiguration<TGameLogEntity>
    {
		 public TGameLogMap()
        {
            this.ToTable("TGameLog");
            this.HasKey(t => t.ID);
        }
    }
}