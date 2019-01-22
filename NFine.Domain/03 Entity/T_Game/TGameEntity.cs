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
    public class TGameEntity : IEntity<TGameEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_CName { get; set; }
        public String F_EName { get; set; }
        public Int32? F_GameType { get; set; }
        public DateTime? F_CreateTime { get; set; }
        public String F_Setting { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public Boolean? F_DeleteMark { get; set; }
        public String F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}