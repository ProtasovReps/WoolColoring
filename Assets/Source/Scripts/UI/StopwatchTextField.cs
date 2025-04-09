using Reflex.Attributes;
using TMPro;
using UnityEngine;

public class StopwatchTextField : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Stopwatch _stopwatch;

    [Inject]
    private void Inject(Stopwatch stopwatch)
    {
        _stopwatch = stopwatch;
    }

    private void OnEnable()
    {
        int elapsedSeconds = (int)_stopwatch.ElapsedTime;
        _text.text = elapsedSeconds.ToString();
    }
}