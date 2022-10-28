using UnityEngine;

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int OffensivePower { get; private set; }
    public int DefensivePower { get; private set; }
    public int PiercingDamage { get; private set; }
    public InfoWindow InfoWindow { get; set; }

    private void Awake()
    {
        MaxHp = 100;
        Hp = MaxHp;
        OffensivePower = 2;
        DefensivePower = 0;
        PiercingDamage = 2;
    }

    private void Start()
    {
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        InfoWindow.UpdateOffensivePowerText(OffensivePower);
        InfoWindow.UpdateDefensivePowerText(DefensivePower);
        InfoWindow.UpdatePiercingDamageText(PiercingDamage);
    }

    public void ToDamage(int damage)
    {
        damage = damage >= DefensivePower ? damage - DefensivePower : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 관통 데미지 {piercingDamage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        DefensivePower = defensivePower;
        InfoWindow.UpdateDefensivePowerText(DefensivePower);
        Logger.Log($"플레이어 방어력 {defensivePower}로 설정됨", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }
}
