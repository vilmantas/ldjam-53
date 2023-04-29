using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Game/Configuration", order = 1)]
public class GameConfiguration_SO : ScriptableObject
{
    public LevelConfiguration_SO[] Levels;
}
