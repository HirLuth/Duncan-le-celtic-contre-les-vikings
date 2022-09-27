using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "GlobalStat/ArmesBaseStat",fileName = "Globals Stats")]
    public class ArmesBaseStat : ScriptableObject
    {
        public List<ArmeStat> listBaseStats;
    }
}
