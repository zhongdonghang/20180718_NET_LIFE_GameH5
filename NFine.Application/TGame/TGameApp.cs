//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:22 
// </copyright>
//-----------------------------------------------------------------------

using NFine.Code;
using NFine.Domain.Entity.TGame;
using NFine.Domain.IRepository.TGame;
using NFine.Repository.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.TGame
{
    public class TGameApp
    {
		private ITGameRepository service = new TGameRepository();

		public List<TGameEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<TGameEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
              //  expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

	    public TGameEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(TGameEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(TGameEntity entity, string keyValue)
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