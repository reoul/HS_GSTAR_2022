using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private StageInfo _stageinfo;
    [SerializeField] private List<Stage> _stages;
    [SerializeField] private Stage _curStage;

    [SerializeField] private EventStage _eventStage;
    [SerializeField] private BattleStage _battleStage;
    
    private int _curStageIndex;

    public Transform EventStagePlayerInfoWindowPos;
    public Transform BattleStagePlayerInfoWindowPos;

    public InfoWindow PlayerInfoWindow;

    public void OpenStageAddListener(Stage stage)
    {
        if (stage.tag.Equals("BattleStage"))
        {
            FadeManager.Instance.FadeInStartEvent.AddListener(() =>
            {
                _battleStage.gameObject.SetActive(true);
                int rand = Random.Range(0, _stageinfo.EnemyPrefabs.Length);
                GameObject enemyObj = Instantiate(_stageinfo.EnemyPrefabs[rand], _battleStage.EnemyCreatePos);
                enemyObj.transform.localPosition = Vector3.zero;
                enemyObj.transform.localScale *= 3;
                BattleManager.Instance.SetEnemy(enemyObj.GetComponent<IBattleable>());
                enemyObj.GetComponent<IBattleable>().InfoWindow = FindObjectOfType<BattleStage>(true).EnemyInfoWindow;
                IBattleable enemy = enemyObj.GetComponent<IBattleable>();
                enemy.MaxHp = 100;
                enemy.Hp = 100;
                enemy.OffensivePower = 3;
                enemy.PiercingDamage = 2;
                enemy.DefensivePower = 5;
                PlayerInfoWindow.transform.parent = BattleStagePlayerInfoWindowPos;
                PlayerInfoWindow.transform.localPosition = Vector3.zero;
                NextStage();
            });
            FadeManager.Instance.FadeInFinishEvent.AddListener(() =>
            {
                BattleManager.Instance.StartBattle();
            });
        }
        else
        {
            int rand = Random.Range(0, _stageinfo.EventStageInfos.Length);
            EventStageInfo eventStageInfo = _stageinfo.EventStageInfos[rand];
            
            FadeManager.Instance.FadeInStartEvent.AddListener(() =>
            {
                _battleStage.gameObject.SetActive(false);
                _eventStage.InitEvent(eventStageInfo.Title, eventStageInfo.Description);
                _eventStage.gameObject.SetActive(true);
                
                PlayerInfoWindow.transform.parent = EventStagePlayerInfoWindowPos;
                PlayerInfoWindow.transform.localPosition = Vector3.zero;
                NextStage();
            });
            
            FadeManager.Instance.FadeInFinishEvent.AddListener(() =>
            {
                List<EventCardInfo> infos = new List<EventCardInfo>();
                foreach (EventCardInfo eventCardInfo in eventStageInfo.EventCardInfos)
                {
                    infos.Add(eventCardInfo);
                }

                int createdCardCount = CardManager.Instance.CreateCards(infos, null, Vector3.one, 0.2f);
                
                DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
            });
        }
    }
    
    public void OpenStage(Stage stage)
    {
        Debug.Assert(stage != null);
        
        _curStage?.StageExit();
        stage.StageEnter();
        _curStage = stage;
    }

    public Stage GetNextStage()
    {
        return _stages[_curStageIndex];
    }

    public void NextStage()
    {
        if (_curStageIndex == _stages.Count)
        {
            return;
        }
        OpenStage(_stages[_curStageIndex++]);
    }
}
