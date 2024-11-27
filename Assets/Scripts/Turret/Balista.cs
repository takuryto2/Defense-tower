using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balista : MonoBehaviour
{
    [SerializeField] private int Atk;
    [SerializeField] private float Atkspd;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    private bool isMono;
    
    [Header("Colider")]
    [SerializeField] private CircleCollider2D balistaCollider;
    private List<Collider2D> targets = new List<Collider2D>();
    private float closestTarget ;
    private bool coroutineRunning;

    private void Start()
    {
        closestTarget = maxRange;
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
            if (targets.Count > 1) {
                CompareDistance();
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        targets.Remove(other); if (targets.Count <= 0){
            balistaCollider.radius = maxRange;
            closestTarget = maxRange;
        }
    }
    
    private void CompareDistance()
    {
        foreach (Collider2D t in targets)
        {
            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance < closestTarget){
                closestTarget = distance;
                balistaCollider.radius = closestTarget;
            }
        }
    }
    private IEnumerator Attack()
    {
        coroutineRunning = true;
        targets[0].GetComponent<Enemy>().hp -= Atk;
        LookAt();
        yield return new WaitForSeconds(Atkspd);
        coroutineRunning = false;
    }
    private void LookAt()
    {
        Vector3 look = transform.InverseTransformPoint(targets[0].transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }
}
