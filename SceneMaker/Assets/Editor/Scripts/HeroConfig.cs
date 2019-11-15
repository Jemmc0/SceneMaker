
using UnityEngine;

[CreateAssetMenu(fileName = "Hero",menuName = "Hero/Myhero")] //Para crear en el editor
public class HeroConfig : ScriptableObject
{
    public int hp;
    public int speed;
    public string characterName;
    public bool godMode;
    public string[] inventory;
}
