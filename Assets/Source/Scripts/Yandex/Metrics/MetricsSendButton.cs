using Extensions;
using UISystem.Buttons;
using UnityEngine;
using YG;

namespace Yandex.Metrics
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