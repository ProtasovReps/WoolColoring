using System;
using YG;

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
        YG2.onRewardAdv -= reward => SendMetricsRewarded();
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