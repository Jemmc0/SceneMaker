using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Warrior Data", menuName = "Character Data/Warrior")]
public class WarriorData : CharactersData
{
    public WarriorClassType classType;
    public WarriorWpnType wpnType;
}
