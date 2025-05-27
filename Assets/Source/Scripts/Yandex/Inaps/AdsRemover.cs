using UnityEngine;
using YG;

namespace Yandex.Inaps
{
    public class AdsRemover : MonoBehaviour
    {
        public void RemoveAds()
        {
            YG2.saves.IfAdsRemoved = true;
            YG2.StickyAdActivity(false);
        }
    }
}
