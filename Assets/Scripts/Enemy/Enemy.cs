using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public int hp;
    [HideInInspector] public float spd;
    [HideInInspector] public int atk;
    [HideInInspector] public int gold;

    [HideInInspector] public int lastNode;
    [HideInInspector] public Transform nextNode;
    [FormerlySerializedAs("path")] [HideInInspector] public EnemyPath enemyPath;

    public SO_Enemy SO_Enemy;
    [SerializeField] SpriteRenderer renderer;

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
                nextNode = enemyPath.endNode;
            }
            nextNode = enemyPath.node[lastNode];
        }
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) {
            Destroy(gameObject);
        }
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor() {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.32f);
        renderer.color = Color.white;
    }
}
