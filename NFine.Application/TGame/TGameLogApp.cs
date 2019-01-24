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

        public List<TGameLogEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<TGameLogEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_LBAccount.Contains(keyword));
              //  expression = expression.Or(t => t.F_GameNo.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Category == 1);
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

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

        /// <summary>
        /// 游戏记录表是否存在指定账号的记录
        /// </summary>
        /// <param name="lbAccount"></param>
        /// <returns></returns>
        public bool GetGameLogByAccount(string lbAccount)
        {
            return service.GetGameLogByAccount(lbAccount);
        }

        public TGameLogEntity GetMaxScoreByAccount(string lbAccount)
        {

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