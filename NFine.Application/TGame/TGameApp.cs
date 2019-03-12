//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:22 
// </copyright>
//-----------------------------------------------------------------------

using Maticsoft.DBUtility;
using Newtonsoft.Json.Linq;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameDescEntity;
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
        string PersonGameMaxLoveBird = System.Configuration.ConfigurationManager.AppSettings["PersonGameMaxLoveBird"].ToString();

        #region ��Ϸ����

        private const string GAME_SE_DESC = "��������Ϸ��ɫä�����룡��һ����Ϸ��һ�֣�������������ʱ�����ˣ��벻Ҫգ�۾�Ŷ�������뿪ʼ��ı��ݣ������淨���������óɼ�Ϊ׼�����������߷֣���ΪӮ�ñ����������ȶ�������LoveBird���֣�����۳�LB����.";
        private const string GAME_XXK_DESC = "��������Ϸ����������Ϸͬ���ս�����߷ְɣ������淨���������óɼ�Ϊ׼�����������߷֣���ΪӮ�ñ����������ȶ�������LoveBird���֣�����۳�LB����";
        private const string GAME_SAOLEI_DESC = "����ɨ����Ϸ����windowsϵͳ�Դ�ɨ���������𣬿������߼�������ʱ�����ˣ�����������1�����ܱ���һ���ף�����2�����������ף�������ƣ���չʾ�������ļ��������ֹ�����ʱ99�룬�����ʱ������ɨ���׵ģ������LoveBird���֣��ȵ��׵Ľ��۳����֣�ʱ��Խ�죬���LoveBird����Խ��Ŷ";
        private const string GAME_MSPT_DESC = "ƴͼ����Ϸ���ܹ���10�أ�ÿһ���ؿ����Գɼ����н��㣬ÿ��һ�ؽ������ӦLoveBird���֣�ʧ������۳�LB���֣�׼��������Ready?����������GO!";
        private const string GAME_SST_DESC = "���������⣬��������ķ�ӳ������ÿ����Ĵ���ʱ����1���ӣ����÷ְ�ͨ�������㣬ͨ��Խ�࣬�÷�Խ�ߣ���󽫶һ���LoveBird���֣����û�ﵽ���ֹ���Ҫ�󣬽��۳�LB����Ŷ";

        public DataEntity GetGameInfo(string gameName, string LBAccount)
        {
                DataEntity e = new DataEntity();
                string sqlAllPlayerBestScore = "select max(F_GameScore) from [dbo].[T_GameLog] where F_GameNo='" + gameName + "'";
                string sqlMeBestScore = "select max(F_GameScore) from [dbo].[T_GameLog] where F_GameNo='" + gameName + "' and F_LBAccount='" + LBAccount + "'";
                string sqlAllPlayerCount = "select COUNT(distinct F_LBAccount) as playerCount from dbo.T_GameLog where F_GameNo='" + gameName + "'  Group by F_GameNo";

                object objAllPlayerBestScore = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlAllPlayerBestScore, null);
                e.AllPlayerBestScore = objAllPlayerBestScore == null ? "����" : objAllPlayerBestScore.ToString();

                object objMeBestScore = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlMeBestScore, null);
                e.MeBestScore = objMeBestScore == null ? "����" : objMeBestScore.ToString();

                object objAllPlayerCount = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlAllPlayerCount, null);
                e.AllPlayerCount = objAllPlayerCount == null ? "����" : (int.Parse( objAllPlayerCount.ToString())*100).ToString();

             //   e.AllPlayerCount = (int.Parse(e.AllPlayerCount) * 100).ToString();

                string sqlGame = "select F_Setting from T_Game where F_EName='" + gameName + "'";
                string setting = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlGame, null).ToString();
                JObject json = Json.ToJObject(setting);
                double tax = 0;
                tax = double.Parse(json["Tax"].ToString()) * 100;
                switch (gameName)
                {
                    case "se":
                    e.GameDesc = GAME_SE_DESC+",��ǰ��Ϸÿ��Ӯ��LoveBird�����޶�"+ PersonGameMaxLoveBird + "��.";
                    e.ScoreRuleDesc = string.Format("�������ж�ɫ��Ϸ������Ҫ��{0}LB���ֲ�����Ŷ��һ��LB���ֵ���{1}��Ϸ���֣�Ӯ�ҽ������ٷ�֮{2}��˰����ȡ˰��.��ʼ��Ϸ��Ҫ�۳�{3}LB������Ϊ�������á�"+
                        " ��Ϸ�÷ֹ���:�����ķ����������ʷ��߷ָߣ��ǾͶ����һ���ַ������ո�LB���ֵı����һ��󣬿۳�˰��Ȼ�󻻳ɵȱȵ�LoveBird���ֳ�ֵ����Ǯ���˻���"+
                        " ��Ϸ�۷ֹ��������ķ����������ʷ��߷ֵͣ��Ƿֲ�ְ���LB���ֵĶһ������һ���ֱ�ӿ۳���Ӧ��LB����������", json["LowestPlayLB"], json["LBRatio"], tax,json["PlayLBPay"]);
                        break;
                    case "XXK":
                    e.GameDesc = GAME_XXK_DESC + ",��ǰ��Ϸÿ��Ӯ��LoveBird�����޶�" + PersonGameMaxLoveBird + "��."; 
                    e.ScoreRuleDesc = string.Format("��������Ϸ����͵Ľ���LB�����ż���{0}����,һ��LB���ֵ���{1}��Ϸ���֣�Ӯ�ҽ������ٷ�֮{2}��˰����ȡ˰��.��ʼ����Ϸ��Ҫ�۳�{3}LB������Ϊ�������á�"+
                        " ��Ϸ�÷ֹ���:�����ķ����������ʷ��߷ָߣ��ǾͶ����һ���ַ������ո�LB���ֵı����һ��󣬿۳�˰��Ȼ�󻻳ɵȱȵ�LoveBird���ֳ�ֵ����Ǯ���˻���"+
                        "��Ϸ�۷ֹ��������ķ����������ʷ��߷ֵͣ��Ƿֲ�ְ���LB���ֵĶһ������һ���ֱ�ӿ۳���Ӧ��LB���������� ", json["LowestPlayLB"], json["LBRatio"], tax, json["PlayLBPay"]);
                        break;
                    case "saolei":
                    e.GameDesc = GAME_SAOLEI_DESC + ",��ǰ��Ϸÿ��Ӯ��LoveBird�����޶�" + PersonGameMaxLoveBird + "��.";
                    e.ScoreRuleDesc = string.Format("ɨ����Ϸ����͵Ľ���LB�����ż���{0}����,Ӯ�ҽ������ٷ�֮{1}��˰����ȡ˰�����˽����۳�{2}��LB����,{3}����ɨ�׳ɹ���"+
                        " �����{4}��LoveBird���֣�{5}����ɨ�׳ɹ��������{6}��LoveBird���֣�{7}����ɨ�׳ɹ��������{8}��LoveBird���֡���ʼ����Ϸ��Ҫ�۳�{9}LB������Ϊ�������á�"+
                        " ", json["LowestPlayLB"], tax, json["LostScore"], json["Rule1"]["Times"], json["Rule1"]["Score"], json["Rule2"]["Times"], json["Rule2"]["Score"], json["Rule3"]["Times"], json["Rule3"]["Score"], json["PlayLBPay"]);
                        break;
                    case "mspt":
                    e.GameDesc = GAME_MSPT_DESC + ",��ǰ��Ϸÿ��Ӯ��LoveBird�����޶�" + PersonGameMaxLoveBird + "��.";
                    e.ScoreRuleDesc = string.Format("��Ůƴͼ��Ϸ,����볡��LB�����ż���{0}���֣�Ӯ�ҽ������ٷ�֮{1}��˰����ȡ˰��," +
                            "ȫͨ�ؽ���ȡ{2}��LB����," +
                            " ͨ{3}�ػ��{4}��LoveBird����,���˿۳�{5}��LB����," +
                            " ͨ{6}�ػ��{7}��LoveBird����,���˿۳�{8}��LB����," +
                            " ͨ{9}�ػ��{10}��LoveBird����,���˿۳�{11}��LB����," +
                            " ͨ{12}�ػ��{13}��LoveBird����,���˿۳�{14}��LB����," +
                            " ͨ{15}�ػ��{16}��LoveBird����,���˿۳�{17}��LB����," +
                            " ͨ{18}�ػ��{19}��LoveBird����,���˿۳�{20}��LB����," +
                            " ͨ{21}�ػ��{22}��LoveBird����,���˿۳�{23}��LB����," +
                            " ͨ{24}�ػ��{25}��LoveBird����,���˿۳�{26}��LB����," +
                            " ͨ{27}�ػ��{28}��LoveBird����,���˿۳�{29}��LB����," +
                            " ͨ{30}�ػ��{31}��LoveBird����,���˿۳�{32}��LB���֡���ʼ��Ϸ��Ҫ�۳�{33}LB������Ϊ�������á�",
                            json["LowestPlayLB"], tax, json["ALlInScore"],
                           "1", json["Rule1"]["WinScore"], json["Rule1"]["LostScore"],
                           "2", json["Rule2"]["WinScore"], json["Rule2"]["LostScore"],
                           "3", json["Rule3"]["WinScore"], json["Rule3"]["LostScore"],
                           "4", json["Rule4"]["WinScore"], json["Rule4"]["LostScore"],
                           "5", json["Rule5"]["WinScore"], json["Rule5"]["LostScore"],
                           "6", json["Rule6"]["WinScore"], json["Rule6"]["LostScore"],
                           "7", json["Rule7"]["WinScore"], json["Rule7"]["LostScore"],
                           "8", json["Rule8"]["WinScore"], json["Rule8"]["LostScore"],
                           "9", json["Rule9"]["WinScore"], json["Rule9"]["LostScore"],
                           "10", json["Rule10"]["WinScore"], json["Rule10"]["LostScore"],
                           json["PlayLBPay"]
                            );
                        break;
                    case "sst":
                    e.GameDesc = GAME_SST_DESC + ",��ǰ��Ϸÿ��Ӯ��LoveBird�����޶�" + PersonGameMaxLoveBird + "��.";
                    e.ScoreRuleDesc = string.Format("�����������Ϸ,����볡��LB�����ż���{0}���֣�Ӯ�ҽ������ٷ�֮{1}��˰����ȡ˰��," +
                            "����{2}�⣬����LoveBird����{3}����" +
                            "����{4}�⣬����LoveBird����{5}����" +
                            "������{6}�⣬�۳�LB����{7}����" +
                            "������{8}�⣬�۳�LB����{9}����ʼ��Ϸ��Ҫ�۳�{10}LB������Ϊ��������" +
                            " ",
                            json["LowestPlayLB"], tax,
                            json["WinLevel1"]["WinCount"], json["WinLevel1"]["WinScore"],
                            json["WinLevel2"]["WinCount"], json["WinLevel2"]["WinScore"],
                            json["LostLevel1"]["LostCount"], json["LostLevel1"]["LostScore"],
                            json["LostLevel2"]["LostCount"], json["LostLevel2"]["LostScore"], json["PlayLBPay"]
                            );
                        break;
                }
            return e;
        }

        #endregion



        public List<TGameEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<TGameEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_CName.Contains(keyword));
                expression = expression.Or(t => t.F_EName.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Category == 1);
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

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