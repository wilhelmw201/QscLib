using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{
    public class QscEnemyUtils
    {
        public static readonly int[][] EnemyIdByGrade = new int[][]
        {
            new int[] {241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,298,299,307,308,},
            new int[] {260,261,262,263,264,300,309,},
            new int[] {265,266,267,268,269,301,310,},
            new int[] {270,271,272,273,274,302,311,},
            new int[] {275,276,277,278,279,303,312,},
            new int[] {280,281,282,283,284,304,313,},
            new int[] {285,286,287,288,289,305,314,},
            new int[] {290,291,292,293,294,306,315,},
            new int[] {295,296,297,},
            new int[] {295,296,297,},
            new int[] {295,296,297,},

        };
        public static int CreateFittingEnemy(TaiwuEvent ev)
        {
            int progress = QscCoreUtils.GetWorldState(ev);
            if (progress > 9) progress = 9;
            int[] pool = EnemyIdByGrade[progress];
            int id = pool[QscCoreUtils.RandInt32(ev, 0, pool.Length)];
            var charId = EventHelper.CreateNonIntelligentCharacter((short)id);
            return charId;
        }
        
    }
}
