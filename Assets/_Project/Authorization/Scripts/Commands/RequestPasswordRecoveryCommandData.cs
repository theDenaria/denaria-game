using System;
using CBS.Models;

namespace _Project.Authorization.Scripts.Commands
{
    public class RequestPasswordRecoveryCommandData
    {
        public string Mail { get; set; }
        public Action<CBSBaseResult> OnRecoverySent { get; set; }

        public RequestPasswordRecoveryCommandData(string mail, Action<CBSBaseResult> onRecoverySent)
        {
            Mail = mail;
            OnRecoverySent = OnRecoverySent;
        }
    }
}