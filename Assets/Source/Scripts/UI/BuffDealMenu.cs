using Reflex.Attributes;
using System;

public class BuffDealMenu : ActivatableUI
{
    private BuffBag _buffBag;
    private IBuff _buff;

    public void SetNeedBuff(IBuff buff) //Получить необходимый бафф и при просмотре рекламы добавить его в bag
    {
        if(buff == null)
            throw new ArgumentNullException(nameof(buff));

        _buff = buff;
    }

    [Inject]
    private void Inject(BuffBag buffBag)
    {
        _buffBag = buffBag;
    }
}