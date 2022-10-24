using System.Collections.Generic;
using UnityEngine.Assertions;

public sealed class EventCard003 : CardBase222
{
    protected override string Name => "샘물";
    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string Description12_(out int heal)
    {
        heal = 2;
        return $"체력을 {heal} 회복합니다.";
    }
    private string Description34_(out int heal)
    {
        heal = 3;
        return $"체력을 {heal} 회복합니다.";
    }
    private string Description56_(out int heal)
    {
        heal = 4;
        return $"체력을 {heal} 회복합니다.";
    }

    protected override string Use12()
    {
        string description = Description12_(out int heal);
        BattleManager.Instance.PlayerBattleable.ToHeal(heal);
        return description;
    }

    protected override string Use34()
    {
        string description = Description34_(out int heal);
        BattleManager.Instance.PlayerBattleable.ToHeal(heal);
        return description;
    }

    protected override string Use56()
    {
        string description = Description56_(out int heal);
        BattleManager.Instance.PlayerBattleable.ToHeal(heal);
        return description;
    }
}
