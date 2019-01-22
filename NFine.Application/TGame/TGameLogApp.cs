//-----------------------------------------------------------------------
// <copyright file=" TGameLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGameLog.cs
// * history : Created by T4 01/22/2019 15:08:30 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.TGameLog;
using NFine.Domain.IRepository.TGameLog;
using NFine.Repository.TGameLog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.TGameLog
{
    public class TGameLogApp
    {
		private ITGameLogRepository service = new TGameLogRepository();

		public List<TGameLogEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<TGameLogEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
               // expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public TGameLogEntity GetGameLogByAccount(string lbAccount)
        {
            string sql = "select count(ID) from T_GameLog where LBAccount='"+ lbAccount + "' ";
            //service.FindList(sql);
            return null;
        }


        public TGameLogEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(TGameLogEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(TGameLogEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
    }
}