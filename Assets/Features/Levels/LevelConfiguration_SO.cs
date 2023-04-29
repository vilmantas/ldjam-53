using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels/Level Configuration", order = 1)]
public class LevelConfiguration_SO : ScriptableObject
{
    public string LevelName;

    public SceneList_SO AdditionalScenes;
}
