using UnityEngine;

[CreateAssetMenu(fileName = "SO_Turret", menuName = "Scriptable Objects/SO_Turret", order = 2)]
public class SO_Turret : ScriptableObject
{
    public int atk;
    public float atkspd;
    public float minRange;
    public float maxRange;
    public bool isMono;
}
