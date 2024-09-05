using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NestedScriptableObjectRoot")]
public class NestedScriptableObjectRoot : ScriptableObject
{
    [NestedScriptableObjectField]
    public NestedScriptableObject field;
    [NestedScriptableObjectList]
    public List<NestedScriptableObject> list = new List<NestedScriptableObject>();
}

public abstract class NestedScriptableObject : ScriptableObject {}

//////