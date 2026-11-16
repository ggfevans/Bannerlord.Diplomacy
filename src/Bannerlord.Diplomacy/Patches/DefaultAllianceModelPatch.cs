using Diplomacy.Models;
using Diplomacy.PatchTools;

using System.Collections.Generic;

using TaleWorlds.CampaignSystem.GameComponents;

namespace Diplomacy.Patches
{
    /// <summary>
    /// <see cref="DiplomacyAllianceModel"/> answers for the alliance limit when the game asks the registered
    /// model, but the default model can also read its own property directly. Patching the getter keeps both
    /// in agreement with the "Maximum Alliances per Kingdom" setting.
    /// </summary>
    internal sealed class DefaultAllianceModelPatch : PatchClass<DefaultAllianceModelPatch, DefaultAllianceModel>
    {
        protected override IEnumerable<Patch> Prepare() => new Patch[]
        {
            new Postfix(nameof(MaxNumberOfAlliancesPostfix), new Reflect.Getter<DefaultAllianceModel>(nameof(DefaultAllianceModel.MaxNumberOfAlliances)).MethodInfo),
        };

        private static void MaxNumberOfAlliancesPostfix(ref int __result)
        {
            if (DiplomacyAllianceModel.ConfiguredMaxNumberOfAlliances is { } maxAlliances)
                __result = maxAlliances;
        }
    }
}
