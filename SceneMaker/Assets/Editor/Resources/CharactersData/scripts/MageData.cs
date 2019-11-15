using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName ="New Mage Data", menuName ="Character Data/Mage")]
public class MageData : CharactersData
{
    public MageDmgType dmgType;
    public MageWpnType wpnType;
}
