using YG;

public class UnlockerSaver : BuffSaver
{
    public UnlockerSaver(BuffBag bag) : base(bag) { }

    public override void Save(IBuff buff)
    {
        if (buff is not Unlocker)
            return;

        YG2.saves.Unlockers = BuffBag.GetCount(buff);
        YG2.SaveProgress();
    }
}