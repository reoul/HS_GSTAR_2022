using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int Shield { get; private set; }
    private List<string> _cardDeck;

    [SerializeField]
    private TMP_Text _healthText, _shieldText;

    private void Awake()
    {
        MaxHp = 100;
        Hp = MaxHp;
        Shield = 0;
        _cardDeck = new List<string>()
        {
            "PlayerCard001", "PlayerCard002", "PlayerCard003", "PlayerCard004", "PlayerCard005", "PlayerCard006"
        };
    }

    private void Start()
    {
        UpdateInfo();
    }

    public void ToDamage(int damage)
    {
        if (damage >= Shield)
        {
            damage -= Shield;
            Shield = 0;
        }
        else
        {
            Shield -= damage;
            damage = 0;
        }
        
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        UpdateInfo();
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void SetShield(int shield)
    {
        Shield = shield;
        UpdateInfo();
        Logger.Log($"플레이어 실드 {Shield} 설정", gameObject);
    }

    public void ToShield(int shield)
    {
        Shield += shield;
        UpdateInfo();
        Logger.Log($"플레이어 실드 {shield} 증가. 현재 실드량 : {Shield}", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        UpdateInfo();
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }

    public void AddCard(string cardCode)
    {
        _cardDeck.Add(cardCode);
    }

    public void RemoveCard(string cardCode)
    {
        Logger.Assert(_cardDeck.Remove(cardCode));
    }

    public List<string> GetCardCodes()
    {
        return _cardDeck;
    }

    private void UpdateInfo()
    {
        _healthText.text = Hp.ToString();
        _shieldText.text = Shield.ToString();
    }
}
