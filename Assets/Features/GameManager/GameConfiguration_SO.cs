using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Game/Configuration", order = 1)]
public class GameConfiguration_SO : ScriptableObject
{
    public LevelList_SO Levels;
}
