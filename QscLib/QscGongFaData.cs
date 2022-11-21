using GameData.Domains;
using GameData.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{
    public enum GongFaType // 茄子的Enum CombatSkillType有毒，重新给拼音
    {
        NeiGong = 0,
        ShenFa = 1,
        JueJi = 2,
        QuanZhang = 3,
        ZhiFa = 4,
        TuiFa = 5,
        AnQi = 6,
        JianFa = 7,
        DaoFa = 8,
        QiangFa = 9,
        ChuFa = 10,
        Ruanbing = 11,
        YuShe = 12,
        YueQi = 13,
    }
    public class QscGongFaData
    {


        public static readonly GongFaType[] CuiPoTypes =
            Enumerable.Range(3, 10).Select((i) => { return (GongFaType)i; } ).ToArray();
        public static readonly GongFaType[] AllGongFaTypes =
            Enumerable.Range(0, 13).Select((i) => { return (GongFaType)i; }).ToArray();


        public static List<short> GetSkillList(int grade, GongFaType type)
        {
            var tbl = QscGongFaData.GetGongFaTbl();
            return tbl[(int)type][grade];
        }

        public static List<short>[][] GetGongFaTbl()
        {
            if (QscGongFaData.GongFaGradeMap is not null) return QscGongFaData.GongFaGradeMap;

            QscGongFaData.GongFaGradeMap =
                Enumerable.Range(0, 14).Select((x) =>
                Enumerable.Range(0, 9).Select((x) => new List<short> { }).ToArray()
                ).ToArray();


            var Learned = DomainManager.Taiwu.GetTaiwu().GetLearnedCombatSkills();
            foreach (var Skill in Config.CombatSkill.Instance)
            {
                if (Skill.TemplateId > 722) continue;
                if (Learned.Contains(Skill.TemplateId)) continue;
                // AdaptableLog.Info("Adding " + Skill.Name + Skill.TemplateId);
                QscGongFaData.GongFaGradeMap[Skill.Type][Skill.Grade].Add(Skill.TemplateId);

            }

            /*AdaptableLog.Info("GongFaTbl init done.");
            string LogStr = "\n";
            for (int i = 0; i < 14; i++)
            {
                LogStr = LogStr.Concat($"{(GongFaType)i}:").ToString();
                for(int ii = 0; ii < 9; ii++)
                {
                    LogStr = LogStr.Concat($"{GongFaGradeMap[i][ii]},").ToString();
                }
                LogStr = LogStr.Concat($"\n").ToString();
            }
            AdaptableLog.Info(LogStr);*/

            return QscGongFaData.GongFaGradeMap;
        }

        public static void removeFromGongFaTbl(short id)
        {
            var Skill = Config.CombatSkill.Instance.GetItem(id);
            bool result = QscGongFaData.GongFaGradeMap[Skill.Type][Skill.Grade].Remove(id);
            if (! result)
            {
                AdaptableLog.Info("Attempt to remove " + Skill.TemplateId + Skill.Name + " from GongFaTbl but cannot find it.");
            }
        }

        public static int[] GetJueJiList(int grade)
        {
            return QscGongFaData.NewJuejiGradeMap[grade];
        }



        public static List<short>[][] GongFaGradeMap = null;


        public static int[][] NewJuejiGradeMap = new int[][]
        {   // TODO: Use this new Grade Table
            new int[] {206, 219, 231, 244, 250, 277, 280, 288, 313, },
            new int[] {211, 229, 233, 237, 238, 246, 257, 283, },
            new int[] {212, 220, 259, 266, 272, 275, 290, 317, },
            new int[] {204, 222, 240, 251, 253, 267, 271, 294, },
            new int[] {214, 261, 264, 270, 273, 276, 301, 307, },
            new int[] {210, 241, 247, 286, 297, 302, 309, 310, },
            new int[] {201, 207, 235, 260, 318, },
            new int[] {217, 225, 304, 315, },
            new int[] {221, 279, 296, 320, },
        };
    }
}
