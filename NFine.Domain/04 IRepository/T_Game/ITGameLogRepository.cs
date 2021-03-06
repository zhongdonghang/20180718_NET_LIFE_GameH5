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
//using NFine.Domain.Entity.H5Game;
using NFine.Domain.Entity.TGameLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.TGameLog
{
    public interface ITGameLogRepository : IRepositoryBase<TGameLogEntity>
    {
        double GetMaxScoreByAccount(string lbAccount, int F_CoinType, string eName);

         bool GetGameLogByAccount(string lbAccount, string eName);

         int AddOne(TGameLogEntity entity);
    }
}