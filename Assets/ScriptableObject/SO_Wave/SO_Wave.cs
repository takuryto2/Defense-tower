using UnityEngine;

[CreateAssetMenu(fileName = "SO_Wave", menuName = "Scriptable Objects/SO_Wave")]
public class SO_Wave : ScriptableObject
{
    public int[] nbEnemy;
    public GameObject[] enemy;
}
