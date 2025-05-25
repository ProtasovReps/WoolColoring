using Extensions;
using System;
using YG;

namespace YandexGamesSDK.Metrics
{
    public class AdWatchedMetrics : IDisposable
    {
        public AdWatchedMetrics()
        {
            YG2.onOpenInterAdv += SendMetricsInter;
            YG2.onRewardAdv += reward => SendMetricsRewarded();
        }

        public void Dispose()
        {
            YG2.onOpenInterAdv -= SendMetricsInter;
        }

        private void SendMetricsInter()
        {
            YG2.MetricaSend(MetricParams.InterAdWatched.ToString());
        }

        private void SendMetricsRewarded()
        {
            YG2.MetricaSend(MetricParams.RewardedAdWatched.ToString());
        }
    }
}