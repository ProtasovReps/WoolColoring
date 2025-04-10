using YG;

public class FillerSaver : BuffSaver
{
    public FillerSaver(BuffBag bag) : base(bag) { }

    public override void Save(IBuff buff)
    {
        if (buff is not Filler)
            return;

        YG2.saves.Fillers = BuffBag.GetCount(buff);
        YG2.SaveProgress();
    }
}