using UnityEngine;

public class StringDistributor : MonoBehaviour
{
    [SerializeField] private WhiteStringHolder _whiteHolder;
    //[SerializeField] private PlatformaSKatushkami;

    public void Distribute(StringBolt bolt)
    {
        //if(v platforme est' katushka s neobhodimim cvetom)
            //pizduem v cvetnuiu katushku
        //else if
        if(_whiteHolder.StringCount < _whiteHolder.MaxCapacity)
            _whiteHolder.Add(bolt.GetString());
    }
}
