using System;
using System.Collections.Generic;

public class ActionQueue
{
    private Queue<Action> _actions;

    public bool IsAnimated { get; private set; }

    public ActionQueue()
    {
        _actions = new Queue<Action>();
    }

    public void AddAction(Action action)
        => _actions.Enqueue(action);

    public void ValidateAction()
    {
        if (IsAnimated == false)
        {
            IsAnimated = true;
            ProcessQueuedAction();
        }
    }

    public void ProcessQueuedAction()
    {
        if (_actions.Count == 0)
        {
            IsAnimated = false;
            return;
        }

        _actions.Dequeue()?.Invoke();
    }
}
