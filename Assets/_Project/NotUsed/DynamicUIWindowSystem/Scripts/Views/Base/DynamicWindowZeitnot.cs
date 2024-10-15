using _Project.DynamicUIWindowSystem.Scripts.Views.Interfaces;
using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;

namespace _Project.DynamicUIWindowSystem.Scripts.Views.Base
{
    public abstract class DynamicWindowZeitnot : ViewZeitnot, IDynamicWindowZeitnot
    {
        public bool IsActive { get; protected set; }
        public bool IsVisible { get; protected set; }
    }
}
