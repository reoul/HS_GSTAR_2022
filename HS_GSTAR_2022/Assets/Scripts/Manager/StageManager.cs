using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    /// <summary> 이벤트 스테이지 정보 배열 </summary>
    [SerializeField] private EventStageInfo[] _eventStageInfoArray;

    /// <summary> 적 정보 배열 </summary>
    [SerializeField] private EnemyInfo[] _enemyInfoArray;


    private Queue<StageType> _stageQueue;
    [SerializeField] private Stage _curStage;

    public IntroStage IntroStage;
    public EventStage EventStage;
    public BattleStage BattleStage;
    public ShopStage ShopStage;
    public VictoryStage VictoryStage;
    public GameOverStage GameOverStage;

    private int _curStageIndex;

    public Transform EventStagePlayerInfoWindowPos;
    public Transform BattleStagePlayerInfoWindowPos;
    public Transform ShopStagePlayerInfoWindowPos;

    public InfoWindow PlayerInfoWindow;

    public StageType NextStageType => _stageQueue.Peek();

    [SerializeField] private MapManager _mapManager;

    private int _curMonsterIndex = 0;

    private void Awake()
    {
        Debug.Assert(_mapManager != null);

        // Resources 폴더에서 이벤트 정보와 적 정보 불러오기
        _eventStageInfoArray = Resources.LoadAll<EventStageInfo>("StageInfo/EventInfo");
        ShopStage.ItemInfoArray = Resources.LoadAll<ItemInfo>("StageInfo/ItemInfo");

        _stageQueue = new Queue<StageType>();

        // 전투 스테이지 이벤트 등록
        BattleStage.StartBattleEvent = new UnityEvent();
        BattleStage.FinishBattleEvent = new UnityEvent();

        _curMonsterIndex = 0;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SetRandomStage();
        //Time.timeScale = 3;
    }

    private void Update()
    {
        _curStage.StageUpdate();
    }

    private void SetRandomStage()
    {
        _mapManager.AddStage(StageType.Event);

        AddStage(StageType.Event);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);
        AddStage(StageType.Battle);

        AddStage(StageType.Shop);
        AddStage(StageType.Shop);
        AddStage(StageType.Battle);
        AddStage(StageType.Battle);
        AddStage(StageType.Battle);

        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Event);
        AddStage(StageType.Shop);
        AddStage(StageType.Shop);
        AddStage(StageType.Boss);

        AddStage(StageType.Victory);
    }

    private void AddStage(StageType type)
    {
        _stageQueue.Enqueue(type);
        _mapManager.AddStage(type);
    }

    public void SetFadeEvent(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Event:
                SetFadeEventByEventStage();
                break;
            case StageType.Battle:
            case StageType.Boss:
                SetFadeEventByBattleStage();
                break;
            case StageType.Shop:
                SetFadeEventByShopStage();
                break;
            case StageType.Victory:
                SetFadeEventByVictoryStage();
                break;
            case StageType.GameOver:
                SetFadeEventByGameOverStage();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stageType), stageType, null);
        }
    }

    /// <summary> 이벤트 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByEventStage()
    {
        int rand = Random.Range(0, _eventStageInfoArray.Length);
        EventStageInfo eventStageInfo = _eventStageInfoArray[rand];

        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            IntroStage.gameObject.SetActive(false);
            BattleStage.gameObject.SetActive(false);
            ShopStage.gameObject.SetActive(false);

            EventStage.InitEvent(eventStageInfo.Title, eventStageInfo.Description, eventStageInfo.ratingType);
            EventStage.gameObject.SetActive(true);

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

            int createdCardCount = CardManager.Instance.CreateCards(infos, Vector3.one * 1.3f, 0.2f);

            DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
        });
        SoundManager.Instance.BGMChange("BattleSound3");
    }

    /// <summary> 전투 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByBattleStage()
    {
        Debug.Assert(PlayerInfoWindow != null);

        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            IntroStage.gameObject.SetActive(false);
            EventStage.gameObject.SetActive(false);
            ShopStage.gameObject.SetActive(false);

            BattleStage.gameObject.SetActive(true);
            //int rand = Random.Range(0, _enemyInfoArray.Length);
            GameObject enemyObj = Instantiate(_enemyInfoArray[_curMonsterIndex].Prefab, BattleStage.EnemyCreatePos);
            enemyObj.transform.localPosition = Vector3.zero;

            EnemyInfo enemyInfo = _enemyInfoArray[_curMonsterIndex++];
            IBattleable enemy = enemyObj.GetComponent<IBattleable>();
            BattleManager.Instance.SetEnemy(enemy);
            enemy.InfoWindow = FindObjectOfType<BattleStage>(true).EnemyInfoWindow;
            enemy.MaxHp = enemyInfo.MaxHp;
            enemy.Hp = enemyInfo.Hp;
            enemy.OffensivePower.DefaultStatus = enemyInfo.OffensivePower;
            enemy.PiercingDamage.DefaultStatus = enemyInfo.PiercingDamage;
            enemy.DefensivePower.DefaultStatus = enemyInfo.DefensivePower;

            PlayerInfoWindow.transform.parent = BattleStagePlayerInfoWindowPos;
            PlayerInfoWindow.transform.localPosition = Vector3.zero;

            OpenStage(GetNextStage());
        });

        FadeManager.Instance.FadeInFinishEvent.AddListener(() => { BattleManager.Instance.StartBattle(); });
        SoundManager.Instance.BGMChange("Event1");
    }

    /// <summary> 상점 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByShopStage()
    {
        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            IntroStage.gameObject.SetActive(false);
            EventStage.gameObject.SetActive(false);
            BattleStage.gameObject.SetActive(false);

            PlayerInfoWindow.transform.parent = ShopStagePlayerInfoWindowPos;
            PlayerInfoWindow.transform.localPosition = Vector3.zero;

            ShopStage.gameObject.SetActive(true);
            OpenStage(GetNextStage());
        });

        SoundManager.Instance.BGMChange("shop_bgm1");
    }

    /// <summary> 승리 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByVictoryStage()
    {
        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            IntroStage.gameObject.SetActive(false);
            EventStage.gameObject.SetActive(false);
            BattleStage.gameObject.SetActive(false);
            ShopStage.gameObject.SetActive(false);
            VictoryStage.gameObject.SetActive(true);
            OpenStage(GetNextStage());
        });

        //SoundManager.Instance.BGMChange("Event", 1);
    }

    /// <summary> 게임오버 스테이지에 대해 페이드 이벤트 등록 </summary>
    private void SetFadeEventByGameOverStage()
    {
        FadeManager.Instance.FadeInStartEvent.AddListener(() =>
        {
            IntroStage.gameObject.SetActive(false);
            EventStage.gameObject.SetActive(false);
            BattleStage.gameObject.SetActive(false);
            ShopStage.gameObject.SetActive(false);
            GameOverStage.gameObject.SetActive(true);
            OpenStage(GameOverStage);
        });

        //SoundManager.Instance.BGMChange("Event", 1);
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
        switch (nextStageType)
        {
            case StageType.Event:
                return EventStage;
            case StageType.Battle:
            case StageType.Boss:
                return BattleStage;
            case StageType.Shop:
                return ShopStage;
            case StageType.Victory:
                return VictoryStage;
            case StageType.GameOver:
                return GameOverStage;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary> 다음 스테이지 </summary>
    public void NextStage()
    {
        FadeManager.Instance.StartFadeOut();
        SetFadeEvent(NextStageType);
    }

    /// <summary> 타이틀로 가기 </summary>
    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }
}
