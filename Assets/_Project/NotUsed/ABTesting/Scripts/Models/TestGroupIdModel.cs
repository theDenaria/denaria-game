using System;

namespace _Project.ABTesting.Scripts.Models
{
    [Serializable]
    public class TestGroupIdModel : ITestGroupIdModel
    {
        public int TestGroupId { get; set; }
    }
}