using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy" , order = 1)]
public class SO_Enemy : ScriptableObject
{
    public int hp;
    public int atk;
    public float spd;
    public int gold;
    public GameObject MainTower;
}
