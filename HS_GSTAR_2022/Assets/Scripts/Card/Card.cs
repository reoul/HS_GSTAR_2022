using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Card : OverlayBase
{
    /// <summary> 카드 소유주 게임오브젝트 </summary>
    public GameObject OwnerObj { get; set; }
    
    private TMP_Text _nameText;
    private TMP_Text _contextText;
    private GameObject _cardShowObj;
    private Animator _cardEffectAnimator;
    
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract void Use(Dice dice);


    private void Awake()
    {
        Debug.Assert(GetComponent<CardSettor>() != null);
        _nameText = GetComponent<CardSettor>().NameText;
        _contextText = GetComponent<CardSettor>().ContextText;
        _cardShowObj = GetComponent<CardSettor>().CardShowObj;
        _cardEffectAnimator = GetComponent<CardSettor>().CardEffectAnimator;
        _cardShowObj.SetActive(false);
    }

    private void Start()
    {
        Create();
    }

    /// <summary> 카드 처음 생성될 때 호출 </summary>
    public void Create()
    {
        gameObject.name = GetName();
        _nameText.text = GetName();
        _contextText.text = GetDescription();
        
        _cardEffectAnimator.SetTrigger("Create");   // 생성 애니메이션 호출
    }

    /// <summary> 카드 삭제 될 때 호출 </summary>
    public void StartDestroyAnimation()
    {
        GetComponent<BoxCollider>().enabled = false;
        _cardEffectAnimator.SetTrigger("Destroy");  // 삭제 애니메이션 호출
    }

    protected override void ShowOverlay()
    {
        Logger.Log($"{GetName()}의 오버레이 : {GetDescription()}");
    }
    
    protected override void HideOverlay()
    {
        Logger.Log($"{GetName()}의 오버레이를 숨김");
    }

    /// <summary> 카드 소유주의 IBattleable를 반환 </summary>
    /// <returns>카드 소유주의 IBattleable</returns>
    public IBattleable GetOwnerBattleable()
    {
        Debug.Assert(OwnerObj != null);
        IBattleable ownerBattleable = OwnerObj.GetComponent<IBattleable>();
        Debug.Assert(ownerBattleable != null);
        return ownerBattleable;
    }
}
