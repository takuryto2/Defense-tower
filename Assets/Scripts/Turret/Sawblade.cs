using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sawblade : MonoBehaviour
{
    [SerializeField] private int Atk;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float animTurnSpeed;
    [SerializeField] private GameObject blade;
    [SerializeField] private GameObject pochita;
    [SerializeField] private bool isPochita;

    private void FixedUpdate() {
        transform.Rotate(0, 0, turnSpeed);
        if (!isPochita)
        {
            blade.SetActive(true);
            pochita.SetActive(false);
            blade.transform.Rotate(0, 0, -animTurnSpeed, Space.Self);
        }
        else
        {
            blade.SetActive(false);
            pochita.SetActive(true);
            pochita.transform.Rotate(0, 0, -animTurnSpeed, Space.Self);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().takeDamage(Atk);
        }
    }

    public void changePochita(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPochita = true;
        }

        if (context.canceled)
        {
            isPochita = false;
        }
    }
}
