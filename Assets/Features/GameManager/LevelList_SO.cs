using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Game/Level List", order = 1)]
public class LevelList_SO : ScriptableObject
{
    public LevelConfiguration_SO[] Levels;
}
