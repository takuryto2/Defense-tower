using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] private int Atk;
    [SerializeField] private float Atkspd;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [SerializeField] private float aoeRadius;
    [SerializeField] private GameObject projectile;
    private bool isMono = false;

    [Header("Colider")]
    [SerializeField] private CircleCollider2D mortarCollider;
    [SerializeField] private List<Collider2D> targets = new List<Collider2D>();
    [SerializeField] private List<Enemy> targetsInAoE = new List<Enemy>();
    private float closestTarget;
    private bool coroutineRunning;

    private void Start()
    {
        mortarCollider.radius = maxRange;
        closestTarget = maxRange;
        projectile.SetActive(false);
    }

    private void Update()
    {
        if (targets.Count > 0 && !coroutineRunning)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other);
            if (targets.Count > 1)
            {
                CompareDistance();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        targets.Remove(other); if (targets.Count <= 0)
        {
            mortarCollider.radius = maxRange;
            closestTarget = maxRange;
        }
    }

    private void CompareDistance()
    {
        foreach (Collider2D t in targets)
        {
            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance < closestTarget)
            {
                closestTarget = distance;
                mortarCollider.radius = closestTarget;
            }
        }
    }

    private List<Enemy> GetTargetInAoE()
    {
        List<Enemy> list = new List<Enemy>();
        RaycastHit2D[] hit = Physics2D.CircleCastAll(targets[0].gameObject.transform.position, aoeRadius, Vector2.right, 0,1<<6);

        for (int i = 0; i < hit.Length; i++)
        {
            list.Add(hit[i].collider.gameObject.GetComponent<Enemy>()); 
        }
        return list;
    }

    private IEnumerator Attack()
    {

        for (int i = 0; i < targets.Count;)
        {
            if (Vector2.Distance(gameObject.transform.position, targets[i].gameObject.transform.position) < minRange)
            {
                targets.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        if (targets.Count == 0)
        {
            mortarCollider.radius = maxRange;
            closestTarget = maxRange;
            yield break;
        }

        coroutineRunning = true;
        projectile.SetActive(true);
        LookAt();

        targetsInAoE = GetTargetInAoE();

        for(int i = 0; i < targetsInAoE.Count; i++)
        {
            targetsInAoE[i].takeDamage(Atk);
        }

        targetsInAoE.Clear();

        yield return new WaitForSeconds(Atkspd / 4);
        projectile.SetActive(false);
        yield return new WaitForSeconds((Atkspd / 4) * 3);
        coroutineRunning = false;
    }
    private void LookAt()
    {
        Vector3 look = transform.InverseTransformPoint(targets[0].transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }
}
