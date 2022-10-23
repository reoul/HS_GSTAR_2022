using System.Collections.Generic;
using UnityEngine.Assertions;

public sealed class EventCard001 : CardBase222
{
    protected override string Name => "생명력 전환";
    protected override string Description12 => Description12_(out _,out _);
    protected override string Description34 => Description34_(out _, out _);
    protected override string Description56 => Description56_(out _, out _);

    private string _description = "최대체력을 {0} 잃고 {1}회복";
    private string Description12_(out int maxHP, out int heal)
    {
        maxHP = 10;
        heal = 20;
        return string.Format(_description, maxHP, heal);
    }
    private string Description34_(out int maxHP, out int heal)
    {
        maxHP = 10;
        heal = 25;
        return string.Format(_description, maxHP, heal);
    }
    private string Description56_(out int maxHP, out int heal)
    {
        maxHP = 10;
        heal = 30;
        return string.Format(_description, maxHP, heal);
    }

    protected override string Use12()
    {
        string description = Description12_(out int maxHP, out int heal);
        return description;
    }

    protected override string Use34()
    {
        string description = Description34_(out int maxHP, out int heal);
        return description;
    }

    protected override string Use56()
    {
        string description = Description56_(out int maxHP, out int heal);
        return description;
    }
}
