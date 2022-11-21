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
        public static int CreateFittingEnemy(TaiwuEvent ev)
        {
            int progress = QscCoreUtils.GetQscProgress(ev);
            int subprogress = QscCoreUtils.GetQscSubProgress(ev);

            var charId = EventHelper.CreateNonIntelligentCharacter(Config.Character.DefKey.XiangshuMinion0);
            return charId;
        }
    }
}
