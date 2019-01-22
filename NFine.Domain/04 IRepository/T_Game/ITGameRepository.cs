//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:23 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
//using NFine.Domain.Entity.H5Game;
using NFine.Domain.Entity.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.TGame
{
    public interface ITGameRepository : IRepositoryBase<TGameEntity>
    {
    }
}