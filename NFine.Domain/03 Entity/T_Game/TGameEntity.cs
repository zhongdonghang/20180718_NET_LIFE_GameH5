//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:22 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.TGame
{
    /// <summary>
    /// TGame Entity Model
    /// </summary>
    public class TGameEntity : IEntity<TGameEntity>//, ICreationAudited, IDeleteAudited, IModificationAudited
    {
						public  Int32  ID { get; set; }
					public  String  CName { get; set; }
					public  String  EName { get; set; }
					public  Int32?  GameType { get; set; }
					public  DateTime?  CreateTime { get; set; }
					public  String  Setting { get; set; }
		    }
}