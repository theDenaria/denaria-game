using System;

namespace _Project.LoadingScreen.Models
{
    [Serializable]
    public class TermsOfServiceModel : ITermsOfServiceModel
    {
        public bool IsAccepted { get; set; }
    }
}