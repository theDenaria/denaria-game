using _Project.ForceUpdate.Scripts.TitleData;
using System.Threading.Tasks;

namespace _Project.ForceUpdate.Scripts.Services
{
    public interface IMinimumClientVersionNeededService
    {
        public Task GetTitleData(string titleDataKey,
            System.Action<MinimumClientVersionNeededTitleData> callbackOnComplete);
    }
}