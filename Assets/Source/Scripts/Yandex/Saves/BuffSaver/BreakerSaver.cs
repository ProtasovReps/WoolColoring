using YG;

public class BreakerSaver : BuffSaver
{
    public BreakerSaver(BuffBag bag) : base(bag) { }

    public override void Save(IBuff buff)
    {
        if (buff is not Breaker)
            return;

        YG2.saves.Breakers = BuffBag.GetCount(buff);
        YG2.SaveProgress();
    }
}