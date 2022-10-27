using UnityEngine;

public sealed class EnemyCard009 : CardBase222
{
    protected override string Name => "전신갑주";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);


    private string Description12_(out int shield)
    {
        shield = 5;
        return $"{shield}의 방어도";
    }

    private string Description34_(out int shield)
    {
        shield = 10;
        return $"{shield}의 방어도";
    }

    private string Description56_(out int shield)
    {
        shield = 15;
        return $"{shield}의 방어도";
    }            

    protected override string Use12()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description12_(out int shield);
        //GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use34()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description34_(out int shield);
        //GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use56()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description56_(out int shield);
        //GetOwnerBattleable().ToShield(shield);
        return description;
    }
}
