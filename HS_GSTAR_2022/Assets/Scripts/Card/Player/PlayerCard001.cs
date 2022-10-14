using UnityEngine.Assertions;

public sealed class PlayerCard001 : CardBase222
{
    protected override string Name => "화염구";
    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string Description12_(out int value)
    {
        value = 2;
        return $"HP가 가장 적은 적에게 {value}데미지";
    }
    private string Description34_(out int value)
    {
        value = 3;
        return $"HP가 가장 적은 적에게 {value}데미지";
    }
    private string Description56_(out int value)
    {
        value = 4;
        return $"HP가 가장 적은 적에게 {value}데미지";
    }

    protected override void Use12()
    {
        string description = Description12_(out int damage);

        Logger.Log($"{Name} :{description}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemyBattle = battleManager.GetMinHpEnemy();
        enemyBattle.ToDamage(damage);
        if (enemyBattle.Hp == 0)
        {
            battleManager.RemoveEnemy(enemyBattle);
        }
    }

    protected override void Use34()
    {
        string description = Description34_(out int damage);

        Logger.Log($"{Name} :{description}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemyBattle = battleManager.GetMinHpEnemy();
        enemyBattle.ToDamage(damage);
        if (enemyBattle.Hp == 0)
        {
            battleManager.RemoveEnemy(enemyBattle);
        }
    }

    protected override void Use56()
    {
        string description = Description56_(out int damage);

        Logger.Log($"{Name} :{description}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemyBattle = battleManager.GetMinHpEnemy();
        enemyBattle.ToDamage(damage);
        if (enemyBattle.Hp == 0)
        {
            battleManager.RemoveEnemy(enemyBattle);
        }
    }
}
