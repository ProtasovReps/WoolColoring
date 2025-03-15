using System;
using System.Collections.Generic;

public class ActionQueue
{
    private Queue<Action> _actions;

    public bool IsAnimating { get; private set; }

    public ActionQueue()
    {
        _actions = new Queue<Action>();
    }

    public void AddAction(Action action)
        => _actions.Enqueue(action);

    public void ValidateAction()
    {
        if (IsAnimating == false)
        {
            IsAnimating = true;
            ProcessQueuedAction();
        }
    }

    public void ProcessQueuedAction()
    {
        if (_actions.Count == 0)
        {
            IsAnimating = false;
            return;
        }

        _actions.Dequeue()?.Invoke();
    }
}
