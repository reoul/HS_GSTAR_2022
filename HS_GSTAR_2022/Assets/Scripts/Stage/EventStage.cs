using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventStage : Stage
{
    [SerializeField]
    private TMP_Text _title, _description;
    
    public EventStageInfo EventStageInfo { get; set; }

    public override void StageEnter()
    {
        Debug.Assert(EventStageInfo != null);
        
        // 플레이어 설정
        GameObject playerObj = BattleManager.Instance.PlayerBattleable.OwnerObj;
        playerObj.transform.localPosition = EventStageInfo.PlayerLocation;
        playerObj.SetActive(true);
        Logger.Log("스테이지 오픈");

        // 영역 설정
        GameObject BattleAreaObj = GameObject.Find("BattleAreaImg");
        BattleAreaObj.transform.localPosition = EventStageInfo.BattleAreaPosition;
        BattleAreaObj.transform.localScale = EventStageInfo.BattleAreaScale;
        BattleAreaObj.GetComponent<RectTransform>().sizeDelta = EventStageInfo.BattleAreaRect;
        
        GameObject.Find("CardParent").transform.localPosition = EventStageInfo.CardCreatePosition;
        GameObject.Find("DiceParent").transform.localPosition = EventStageInfo.DiceCreatePosition;

        playerObj.SetActive(true);
    }

    public override void StageExit()
    {
        FindObjectOfType<Player>().gameObject.SetActive(false);
    }

    public override void StageUpdate()
    {
    }

    public void InitEvent(string title, string description)
    {
        _title.text = title;
        _description.text = description;
    }

}
