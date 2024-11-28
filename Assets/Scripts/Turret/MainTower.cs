using UnityEngine;

public class 
    MainTower : MonoBehaviour
{
    public int health;
    public int money;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= other.GetComponent<Enemy>().atk;
        }
    }
    public void addMoney(int amount)
    {
        money += amount;
    }
    public void withdrawMoney(int amount)
    {
        money -= amount;
    }
}
