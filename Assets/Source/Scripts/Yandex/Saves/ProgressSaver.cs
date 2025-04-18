using System;
using YG;

public class ProgressSaver : ISaver, IDisposable
{
    private readonly Picture _picture;
    private readonly ISaver[] _savers;

    public ProgressSaver(Picture picture, ISaver[] savers)
    {
        _picture = picture;
        _savers = savers;
        _picture.Finished += Save;
    }

    public void Dispose()
    {
        _picture.Finished -= Save;
    }

    public void Save()
    {
        for (int i = 0; i < _savers.Length; i++)
            _savers[i].Save();

        YG2.SaveProgress();
    }
}