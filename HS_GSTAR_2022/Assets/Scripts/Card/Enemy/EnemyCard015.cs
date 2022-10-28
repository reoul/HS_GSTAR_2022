using UnityEngine;

public sealed class EnemyCard015 : CardBase222
{
    protected override string Name => "회복의 불";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);


    private string Description12_(out int heal)
    {
        heal = 3;
        return $"{heal}회복";
    }

    private string Description34_(out int heal)
    {
        heal = 6;
        return $"{heal}회복";
    }

    private string Description56_(out int heal)
    {
        heal = 9;
        return $"{heal}회복";
    }            

    protected override string Use12()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description12_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use34()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description34_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use56()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description56_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }
}
