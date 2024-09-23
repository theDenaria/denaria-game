using System;

namespace _Project.Utilities
{
    public class UniqueIDUtility
    {
        public static string GenerateProcessID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
