using YG;

public class RemoverSaver : BuffSaver
{
    public RemoverSaver(BuffBag bag) : base(bag) { }

    public override void Save(IBuff buff)
    {
        if (buff is not Remover)
            return;

        YG2.saves.Removers = BuffBag.GetCount(buff);
        YG2.SaveProgress();
    }
}