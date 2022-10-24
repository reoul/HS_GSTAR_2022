using UnityEngine;

public sealed class EnemyCard021 : CardBase33
{
    protected override string Name => "무리생활";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int shield)
    {
        shield = 4;
        return $"아군 하나 당 {shield}방어도 획득";
    }

    private string Description456_(out int shield)
    {
        shield = 6;
        return $"아군 하나 당 {shield}방어도 획득";
    }

    protected override string Use123()
    {
        int enemyCount = BattleManager.Instance.EnemyBattleables.Count;

        string description = Description123_(out int shield);

        for(int i = 0; i < enemyCount; i++)
        {
            GetOwnerBattleable().ToShield(shield);
        }
        return description;
    }

    protected override string Use456()
    {
        int enemyCount = BattleManager.Instance.EnemyBattleables.Count;

        string description = Description456_(out int shield);

        for (int i = 0; i < enemyCount; i++)
        {
            GetOwnerBattleable().ToShield(shield);
        }
        return description;
    }
}
