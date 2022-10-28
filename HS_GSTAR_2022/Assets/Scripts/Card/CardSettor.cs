using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class CardSettor : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_Text ContextText;
    public GameObject CardShowObj;
    public Animator CardEffectAnimator;

    public string CardCode;

    public void SetCard(string cardCode)
    {
        CardCode = cardCode;
        Type cardType = Type.GetType($"{CardCode},Assembly-CSharp");
        Debug.AssertFormat(cardType != null, gameObject, "{0} 은 존재하지 않는 카드 타입입니다", CardCode);
        gameObject.AddComponent(cardType);
    }
}
