using Extensions;
using LevelInterface.Buttons;
using UnityEngine;
using YG;

namespace YandexGamesSDK.Metrics
{
    public class MetricsSendButton : ButtonView
    {
        [SerializeField] private MetricParams _metricParams;

        protected override void OnButtonClick()
        {
            if (_metricParams == MetricParams.None)
                return;

            YG2.MetricaSend(_metricParams.ToString());
        }
    }
}