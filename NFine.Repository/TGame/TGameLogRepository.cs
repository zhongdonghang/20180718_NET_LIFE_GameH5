
using NFine.Data;
using NFine.Data.Extensions;
using NFine.Domain.Entity.TGameLog;
using NFine.Domain.IRepository.TGameLog;
//using NFine.Domain.Entity.H5Game;
//using NFine.Domain.IRepository.H5Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.TGameLog
{
    public class TGameLogRepository : RepositoryBase<TGameLogEntity>, ITGameLogRepository
    {
        public int AddOne(TGameLogEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool GetGameLogByAccount(string lbAccount)
        {
            string sql = "select count(F_Id) from T_GameLog where F_LBAccount='" + lbAccount + "' ";
            object val  =  DbHelper.ExecuteScalar(sql);
            int count = int.Parse(val.ToString());
            return count>0;
        }

        public double GetMaxScoreByAccount(string lbAccount,int F_CoinType)
        {
            string sqlMaxScore = "select max(F_Score) from T_GameLog where F_WinOrLost=1 and  F_CoinType=" + F_CoinType + " and  F_LBAccount ='" + lbAccount + "'";
            double maxScore = double.Parse(DbHelper.ExecuteScalar(sqlMaxScore).ToString());
            return maxScore;

        }
    }
}