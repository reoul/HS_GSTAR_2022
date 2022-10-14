using System.Collections.Generic;

public sealed class PlayerCard004_JGS : CardBase222_JGS
{
    protected override string Name => "베리어";

    protected override Info Information12 => CreateInfo_(new List<int> {2});
    protected override Info Information34 => CreateInfo_(new List<int> {3});
    protected override Info Information56 => CreateInfo_(new List<int> {4});

    private Info CreateInfo_(List<int> value)
    {
        
        Info information = new Info {description = $"플레이어에게 {value[0]}방어도", values = value};
    
        return information;
    }

    protected override void Use12()
    {
        Logger.Log($"{Name} : 12 : {Information12.description}");
        GetOwnerBattleable().ToShield(Information12.values[0]);
    }

    protected override void Use34()
    {
        Logger.Log($"{Name} : 34 : {Information34.description}");
        GetOwnerBattleable().ToShield(Information34.values[0]);
    }

    protected override void Use56()
    {
        Logger.Log($"{Name} : 56 : {Information56.description}");
        GetOwnerBattleable().ToShield(Information56.values[0]);
    }

}
