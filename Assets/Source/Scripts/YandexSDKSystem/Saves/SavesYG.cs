using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Coins;
        public int Unlockers;
        public int Fillers;
        public int Removers;
        public int Breakers;
        public int BlocksColorized;
        public int LastLevelIndex;
        public List<int> PassedLevelIndexes = new List<int>();
        public int UnlockedLevelsCount;
        public bool IfFreeBuffsGiven;
        public bool IfFreeCoinsGiven;
        public bool IfGuidePassed;
        public bool IfAdsRemoved;
        public float LastElapsedTimerTime;
    }
}