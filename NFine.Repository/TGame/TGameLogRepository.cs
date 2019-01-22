//-----------------------------------------------------------------------
// <copyright file=" TGameLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGameLog.cs
// * history : Created by T4 01/22/2019 15:08:31 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.TGameLog;
using NFine.Domain.IRepository.TGameLog;
//using NFine.Domain.Entity.H5Game;
//using NFine.Domain.IRepository.H5Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.TGameLog
{
    public class TGameLogRepository : RepositoryBase<TGameLogEntity>, ITGameLogRepository
    {

    }
}