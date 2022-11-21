using GameData.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiwuModdingLib.Core.Plugin;

namespace Qsc
{
    [PluginConfig("QscLib", "Wilhelmw", "0.1")]
    public class QscLibPlugin : TaiwuRemakePlugin
    {

        Harmony harmony;

        public override void Dispose()
        {
            if (harmony != null)
            {
                AdaptableLog.Info("Disposing Mod: Qsc");
                harmony.UnpatchSelf();
            }

        }

        public override void Initialize()
        {
            AdaptableLog.Info("Loading Mod: Qsc");
            harmony = Harmony.CreateAndPatchAll(typeof(QscLibPlugin));
        }
    }
}
