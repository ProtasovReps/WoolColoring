using Reflex.Attributes;

public class RemoverBuyButton : BuyBuffButton
{
    [Inject]
    private void Inject(Remover remover)
    {
        base.Initialize(remover);
    }
}