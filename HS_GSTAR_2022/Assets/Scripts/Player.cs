using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : MonoBehaviour, IBattleable
{
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int Shield { get; private set; }

    private void Awake()
    {
        MaxHp = 100;
        Hp = MaxHp;
        Shield = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int createdCardCount = CardManager.Instance.CreateCards(GetComponent<IUseCard>());
            DiceManager.Instance.CreateDices(createdCardCount);
        }
    }

    public void ToDamage(int damage)
    {
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void SetShield(int shield)
    {
        Shield = shield;
        Logger.Log($"플레이어 실드 {Shield} 설정", gameObject);
    }

    public void ToShield(int shield)
    {
        Shield += shield;
        Logger.Log($"플레이어 실드 {shield} 증가. 현재 실드량 : {Shield}", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }

    public List<string> GetCardCodes()
    {
        List<string> codes = new List<string> {"001", "003"};

#if USE_DEBUG
        foreach (string code in codes) // 유효한 카드 코드를 입력했는지 검증
        {
            Logger.AssertFormat(Convert.ToInt32(code) > 0 && Convert.ToInt32(code) < 1000,
                "{0}의 아이템 코드 {1}가 잘못됨", name, code);
            Type cardType = Type.GetType($"Card{code},Assembly-CSharp");
            Logger.AssertFormat(cardType != null, gameObject, "Card{0} 은 존재하지 않는 카드 타입입니다", code);
        }
#endif

        return codes;
    }
}
