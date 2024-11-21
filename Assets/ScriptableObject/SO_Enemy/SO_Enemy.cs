using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy" , order = 1)]
public class SO_Enemy : ScriptableObject
{
    public int hp;
    public float spd;
    public int atk;
    public int gold;
}
