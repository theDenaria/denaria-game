using System.Linq;
using _Project.SceneManagementUtilities.Scripts.Services;
using _Project.StrangeIOCUtility.Scripts.Views;

namespace _Project.SceneManagementUtilities.Scripts.Views
{
    public class SceneChangeHelperView : ViewZeitnot
    {
        public SceneGroupListData SceneGroupListData;
        
        [Inject] public ISceneChangeService SceneChangeService { get; set; }

        public void init()
        {
            SceneChangeService.SceneGroupDataList = SceneGroupListData.sceneGroupList.Cast<SceneGroupData>().ToList();
        }
        
    }
}