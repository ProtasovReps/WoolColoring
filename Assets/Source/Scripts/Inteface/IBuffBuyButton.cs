namespace Interface
{
    public interface IBuffBuyButton<out T>
        where T : IBuff
    {
        T CurrentBuff { get; }

        void SetActive(bool isActive);
    }
}
