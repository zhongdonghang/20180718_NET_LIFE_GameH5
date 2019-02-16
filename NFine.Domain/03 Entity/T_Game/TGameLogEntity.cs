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
    public class TGameLogEntity : IEntity<TGameLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_LBAccount { get; set; }
        public String F_LogNo { get; set; }
        public string F_GameNo { get; set; }
        public Int32? F_Score { get; set; }
        public Int32? F_GameScore { get; set; }
        public Int32? F_CoinType { get; set; }
        public Int32? F_WinOrLost { get; set; }
        public Int32? F_LogState { get; set; }
        public DateTime? F_LogTime { get; set; }
        public Int32? F_LogType { get; set; }
        public Int32? F_LogFlag { get; set; }
        public String F_Remark { get; set; }
        public DateTime? F_MarkTime { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public Boolean? F_DeleteMark { get; set; }
        public String F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        public double? F_Tax { get; set; }
    }
}