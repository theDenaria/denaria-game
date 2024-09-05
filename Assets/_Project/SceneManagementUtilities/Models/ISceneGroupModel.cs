using System.Collections.Generic;
using UnityEngine;

namespace _Project.SceneManagementUtilities.Models
{
    public interface ISceneGroupModel
    {
        //[field:SerializeField] public List<SceneGroupData> SceneGroupDataList { get; }
        [field:SerializeField] public List<SceneGroupData> SceneGroupModels{ get; set;  }
    }
}