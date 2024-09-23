using System;

namespace _Project.ServerTimeStamp
{
    public class SetTimeStampDifferenceCommandData
    {
        public Action<bool> IsSucceed { get; set; }
        public bool LoginChain { get; set; }

        public SetTimeStampDifferenceCommandData(Action<bool> isSucceed, bool loginChain)
        {
            IsSucceed = isSucceed;
            LoginChain = loginChain;
        }
    }
}