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
            return service.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public List<TGameLogEntity> GetList(Pagination pagination, string keyword, string selGame, string selWinOrLost, string selTime)
        {
		    var expression = ExtLinq.True<TGameLogEntity>();
          //  var queryParam = queryJson.ToJObject();
            if (!keyword.IsEmpty())
            {
                //string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_LBAccount.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(selGame))
            {
                if(selGame!="0") expression = expression.And(t => t.F_GameNo.Contains(selGame));
            }
            if (!string.IsNullOrEmpty(selWinOrLost))
            {
                if (selWinOrLost != "0")
                {

                } expression = expression.And(t => t.F_WinOrLost.ToString()==selWinOrLost);
            }
            if (!string.IsNullOrEmpty(selTime))
            {
                if (selTime == "currentDate")
                {
                    string time = DateTime.Now.ToShortDateString();
                    DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格
                    DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

                    expression = expression.And(t => t.F_CreatorTime >= time1 & t.F_CreatorTime <= time2);
                }
                else if (selTime == "currentWeek")
                {
                    string time = DateTime.Now.AddDays(-7).ToShortDateString();
                    DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格
                    DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

                    expression = expression.And(t => t.F_CreatorTime >= time1 & t.F_CreatorTime <= time2);
                }
                else if (selTime == "currentMonth")
                {
                    string time = DateTime.Now.AddDays(-30).ToShortDateString();
                    DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格
                    DateTime time2 = Convert.ToDateTime(time + " 23:59:59");
                    expression = expression.And(t => t.F_CreatorTime >= time1 & t.F_CreatorTime <= time2);
                }
            }
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 游戏记录表是否存在指定账号的记录
        /// </summary>
        /// <param name="lbAccount"></param>
        /// <returns></returns>
        public bool GetGameLogByAccount(string lbAccount,string eName)
        {
            return service.GetGameLogByAccount(lbAccount,eName);
        }

        public double GetMaxScoreByAccount(string lbAccount, int F_CoinType,string eName)
        {
            return service.GetMaxScoreByAccount(lbAccount, F_CoinType, eName);
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