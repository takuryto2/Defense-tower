using UnityEngine;
using UnityEngine.Serialization;

public class Bulldozer : MonoBehaviour
{
    [SerializeField] private float spd;
    [SerializeField] private int atk;
    [FormerlySerializedAs("path")] [HideInInspector] public EnemyPath enemyPath;
    private int lastNode;
    private Transform nextNode;
    private void Update()
    {
        float step = spd * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextNode.position.x, nextNode.position.y, transform.position.z), step);

        if(Vector3.Distance(nextNode.position,transform.position) < 0.000000001f)
        {
            lastNode++;
            if(lastNode == 7)
            {
                nextNode = enemyPath.endNode;
            }
            nextNode = enemyPath.node[lastNode];
        }
    }
}
