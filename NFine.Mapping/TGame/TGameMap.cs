//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:23 
// </copyright>
//-----------------------------------------------------------------------

using NFine.Domain.Entity.TGame;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.TGame
{
    public class TGameMap : EntityTypeConfiguration<TGameEntity>
    {
		 public TGameMap()
        {
            this.ToTable("TGame");
            this.HasKey(t => t.ID);
        }
    }
}