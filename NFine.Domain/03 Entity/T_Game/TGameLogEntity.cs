//-----------------------------------------------------------------------
// <copyright file=" TGameLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGameLog.cs
// * history : Created by T4 01/22/2019 15:08:30 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.TGameLog
{
    /// <summary>
    /// TGameLog Entity Model
    /// </summary>
    public class TGameLogEntity : IEntity<TGameLogEntity>//, ICreationAudited, IDeleteAudited, IModificationAudited
    {
						public  Int32  ID { get; set; }
					public  String  LBAccount { get; set; }
					public  String  LogNo { get; set; }
					public  Int32?  GameNo { get; set; }
					public  Int32?  Score { get; set; }
					public  Int32?  GameScore { get; set; }
					public  Int32?  CoinType { get; set; }
					public  Int32?  WinOrLost { get; set; }
					public  Int32?  LogState { get; set; }
					public  DateTime?  LogTime { get; set; }
					public  Int32?  LogType { get; set; }
					public  Int32?  LogFlag { get; set; }
					public  String  Remark { get; set; }
					public  DateTime?  MarkTime { get; set; }
		    }
}