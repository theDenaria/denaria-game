This folder is responsible for loading and unloading additive scenes as meaningful groups. 

To use this method of changing scenes:

1. Add related scenes in build settings
2. Go into "Assets/_Project/SceneManagementUtilities/Resources/SceneGroupListData.asset" 
3. Click plus icon in the inspector.
4. Select "Scene Group Data" in the inspector to add an item into Scene Group List.
5. Add related scenes into Scenes List of Scene Group Data.
6. Do additional tweaking and configure Scene Group Data if needed. (Right now it does not effect anything.)
7. Throw ChangeSceneGroupSignal with a parameter of type "SceneGroupTypes" inside.

    Example:
    [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }
    ...
    ChangeSceneGroupSignal.Dispatch(SceneGroupTypes.Teaser);

This folder uses Nested Scriptable Objects which is an Odin Inspector feature. 
To use that we had to include some extension scripts to our project. Those are:
Assets/_Project/Utilities/NestedScriptableObjectRoot.cs
Assets/_Project/Utilities/NestedScriptableObjectFieldAttribute.cs
Assets/_Project/Utilities/NestedScriptableObjectListAttribute.cs
Assets/_Project/Utilities/NestedScriptableObjectFieldAttributeDrawer.cs
Assets/_Project/Utilities/NestedScriptableObjectListAttributeDrawer.cs

Keep in mind that SceneGroupData.cs and SceneGroupListData.cs files had to be put into "Assets/Scripts" folder 
instead of "Assets/_Project/SceneManagementUtilities/Resources" 
because of the hardcoded path in NestedScriptableObjectListAttribute.GetAllObjectsOfType().