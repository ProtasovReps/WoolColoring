using UnityEngine;
using YG;

namespace YandexGamesSDK.Inaps
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
