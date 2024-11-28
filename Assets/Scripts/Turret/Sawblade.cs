using System;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    [SerializeField] private int Atk;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float animTurnSpeed;
    [SerializeField] private GameObject blade;

    private void FixedUpdate() {
        transform.Rotate(0, 0, turnSpeed);
        blade.transform.Rotate(0, 0, -animTurnSpeed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().takeDamage(Atk);
        }
    }
}
