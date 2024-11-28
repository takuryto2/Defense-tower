using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float spd;
    public int atk;
    public int gold;

    public int lastNode;

    public Transform nextNode;
    public Path path;

    public SO_Enemy SO_Enemy;

    private void Awake()
    {
        hp = SO_Enemy.hp;
        spd = SO_Enemy.spd;
        atk = SO_Enemy.atk;
        gold = SO_Enemy.gold;
    }


    private void Update()
    {
        float step = spd * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextNode.position.x, nextNode.position.y, transform.position.z), step);

        if( Vector3.Distance(nextNode.position,transform.position) < 0.000000001f)
        {
            lastNode++;
            if(lastNode == 7)
            {
                nextNode = path.endNode;
            }
            nextNode = path.node[lastNode];
        }
    }
}
