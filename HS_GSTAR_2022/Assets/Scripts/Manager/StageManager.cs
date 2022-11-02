using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    /// <summary> 이벤트 스테이지 정보 배열 </summary>
    [SerializeField] private EventStageInfo[] _eventStageInfoList;

    /// <summary> 적 정보 배열 </summary>
    [SerializeField] private EnemyInfo[] _enemyInfoList;

    private Queue<StageType> _stageQueue;
    [SerializeField] private Stage _curStage;

    [SerializeField] private EventStage _eventStage;
    [SerializeField] private BattleStage _battleStage;

    private int _curStageIndex;

    public Transform EventStagePlayerInfoWindowPos;
    public Transform BattleStagePlayerInfoWindowPos;

    public InfoWindow PlayerInfoWindow;

    public StageType NextStageType => _stageQueue.Peek();

    [SerializeField] private MapManager _mapManager;

    private void Awake()
    {
        Debug.Assert(_mapManager != null);
        
        // Resources 폴더에서 이벤트 정보와 적 정보 불러오기
        _eventStageInfoList = Resources.LoadAll<EventStageInfo>("StageInfo/EventInfo");
        _enemyInfoList = Resources.LoadAll<EnemyInfo>("StageInfo/EnemyInfo");

        _stageQueue = new Queue<StageType>();
    }

    private void Start()
    {
        SetRandomStage();
    }

    private void Update()
    {
        _curStage.StageUpdate();
    }

    private void SetRandomStage()
    {
        _mapManager.AddStage(StageType.Event);
        for (int i = 0; i < 5; ++i)
        {
            _stageQueue.Enqueue(StageType.Event);
            _mapManager.AddStage(StageType.Event);
            
            _stageQueue.Enqueue(StageType.Event);
            _mapManager.AddStage(StageType.Event);
            
            _stageQueue.Enqueue(StageType.Event);
            _mapManager.AddStage(StageType.Event);
            
            _stageQueue.Enqueue(StageType.Battle);
            _mapManager.AddStage(StageType.Battle);
        }
    }

    public void SetFadeEvent(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Event:
                SetFadeEventByEventStage();
                break;
            case StageType.Battle:
                SetFadeEventByBattleStage();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stageType), stageType, null);
        }
    }

    /// <summary> 이벤트 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByEventStage()
    {
        int rand = Random.Range(0, _eventStageInfoList.Length);
        EventStageInfo eventStageInfo = _eventStageInfoList[rand];

        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            _battleStage.gameObject.SetActive(false);
            _eventStage.InitEvent(eventStageInfo.Title, eventStageInfo.Description);
            _eventStage.gameObject.SetActive(true);

            PlayerInfoWindow.transform.SetParent(EventStagePlayerInfoWindowPos);
            PlayerInfoWindow.transform.localPosition = Vector3.zero;

            OpenStage(GetNextStage());
        });

        FadeManager.Instance.FadeInFinishEvent.AddListener(() =>
        {
            List<EventCardInfo> infos = new List<EventCardInfo>();
            foreach (EventCardInfo eventCardInfo in eventStageInfo.EventCardInfos)
            {
                infos.Add(eventCardInfo);
            }

            int createdCardCount = CardManager.Instance.CreateCards(infos, Vector3.one, 0.2f);

            DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
        });
    }

    /// <summary> 전투 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByBattleStage()
    {
        Debug.Assert(PlayerInfoWindow != null);

        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            _battleStage.gameObject.SetActive(true);
            int rand = Random.Range(0, _enemyInfoList.Length);
            GameObject enemyObj = Instantiate(_enemyInfoList[rand].Prefab, _battleStage.EnemyCreatePos);
            enemyObj.transform.localPosition = Vector3.zero;
            enemyObj.transform.localScale *= 3;

            EnemyInfo enemyInfo = _enemyInfoList[rand];
            IBattleable enemy = enemyObj.GetComponent<IBattleable>();
            BattleManager.Instance.SetEnemy(enemy);
            enemy.InfoWindow = FindObjectOfType<BattleStage>(true).EnemyInfoWindow;
            enemy.MaxHp = enemyInfo.MaxHp;
            enemy.Hp = enemyInfo.Hp;
            enemy.OffensivePower = enemyInfo.OffensivePower;
            enemy.PiercingDamage = enemyInfo.PiercingDamage;
            enemy.DefensivePower = enemyInfo.DefensivePower;

            PlayerInfoWindow.transform.parent = BattleStagePlayerInfoWindowPos;
            PlayerInfoWindow.transform.localPosition = Vector3.zero;

            OpenStage(GetNextStage());
        });

        FadeManager.Instance.FadeInFinishEvent.AddListener(() => { BattleManager.Instance.StartBattle(); });
    }

    public void OpenStage(Stage stage)
    {
        Debug.Assert(stage != null);

        _curStage?.StageExit();
        
        _mapManager.SubStage();
        
        stage.StageEnter();
        _curStage = stage;
    }

    public Stage GetNextStage()
    {
        StageType nextStageType = _stageQueue.Dequeue();
        return nextStageType == StageType.Battle ? _battleStage : _eventStage;
    }
}
