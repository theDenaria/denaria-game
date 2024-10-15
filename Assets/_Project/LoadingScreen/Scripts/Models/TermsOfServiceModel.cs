using System;

namespace _Project.LoadingScreen.Scripts.Models
{
    [Serializable]
    public class TermsOfServiceModel : ITermsOfServiceModel
    {
        public bool IsAccepted { get; set; }
    }
}