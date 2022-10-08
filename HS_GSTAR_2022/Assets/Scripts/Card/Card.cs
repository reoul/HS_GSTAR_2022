using TMPro;
using UnityEngine;

public abstract class Card : OverlayBase
{
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract void Use(Dice dice);

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private GameObject _cardShowObj;
    [SerializeField] private Animator _cardEffectAnimator;

    private void Awake()
    {
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
    public void Destroy()
    {
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
}
