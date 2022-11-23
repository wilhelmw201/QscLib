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
            new int[] {228,229,230,231,232,233,234,235,236,238,},
            new int[] {237,239,240,241,258,298,307,},
            new int[] {242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,259,260,261,263,264,265,268,273,299,308,},
            new int[] {262,266,269,270,274,278,300,309,},
            new int[] {271,275,279,283,301,310,},
            new int[] {267,276,280,284,288,302,311,},
            new int[] {272,281,285,289,293,303,312,},
            new int[] {277,286,290,294,304,313,},
            new int[] {282,291,295,305,314,},
            new int[] {287,292,296,297,306,315,},

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
