using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfos", menuName = "StageInfo/StageInfos", order = int.MaxValue)]
public class StageInfo : ScriptableObject
{
    public BattleStageInfo[] BattleStageInfos;
    public EventStageInfo[] EventStageInfos;
}
