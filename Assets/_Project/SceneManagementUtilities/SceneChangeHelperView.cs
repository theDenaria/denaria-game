using System.Linq;
using _Project.SceneManagementUtilities.Services;
using _Project.StrangeIOCUtility;

namespace _Project.SceneManagementUtilities
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