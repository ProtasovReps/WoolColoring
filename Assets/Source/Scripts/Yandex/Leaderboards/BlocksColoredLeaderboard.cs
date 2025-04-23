using System;
using YG;

public class BlocksColoredLeaderboard : IDisposable
{
    private const string LeaderboardName = "MostColoredBlocks";

    private readonly Picture _picture;
    private int _blockColorizedCount;

    public BlocksColoredLeaderboard(Picture picture)
    {
        _picture = picture;
        _picture.BlockCountChanged += OnBlocksCountChanged;
        _picture.Finished += OnColorized;
        _blockColorizedCount = YG2.saves.BlocksColorized;
    }

    public void Dispose()
    {
        _picture.BlockCountChanged -= OnBlocksCountChanged;
        _picture.Finished -= OnColorized;
        YG2.saves.BlocksColorized = _blockColorizedCount;
    }

    private void OnBlocksCountChanged()
    {
        _blockColorizedCount++;
    }

    private void OnColorized()
    {
        YG2.SetLeaderboard(LeaderboardName, _blockColorizedCount);
    }
}