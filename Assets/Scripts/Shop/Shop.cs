using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> turrets;
    [SerializeField] private GameObject mainTurret;
    [SerializeField] private int moneyToUpgrade;
    [SerializeField] private int upgradeAtkBy;
    [SerializeField] private int upgradeSpdBy;
    private List<int> upgradesPurchased = new List<int>();
    
    private void Start()
    {
        for(int i = 0; i < turrets.Count; i++)
        {
            upgradesPurchased.Add(0);
            upgradesPurchased.Add(0);
        }
    }

    public void TryUpgradeTurret(int index)
    {
        if (mainTurret.GetComponent<MainTower>().money >= moneyToUpgrade)
        {
            mainTurret.GetComponent<MainTower>().money -= moneyToUpgrade;
            //pourrais ètre optimisé via de l'éritage sur les tourelles avec un peut plus de temps
            switch (index)
            {
                case 0:
                    turrets[index].GetComponent<Balista>().Atk += upgradeAtkBy;
                    break;
                case 1:
                    turrets[index].GetComponent<Balista>().Atkspd += upgradeSpdBy;
                    break;
                case 2:
                    turrets[index].GetComponent<Mortar>().Atk += upgradeAtkBy;
                    break;
                case 3:
                    turrets[index].GetComponent<Mortar>().Atkspd += upgradeSpdBy;
                    break;
                case 4:
                    turrets[index].GetComponent<Sawblade>().Atk += upgradeAtkBy;
                    break;
                case 5:
                    turrets[index].GetComponent<Sawblade>().TurnSpeed += upgradeSpdBy;
                    break; 
                default:
                    Debug.Log("Invalid index");
                    break;
            }
            upgradesPurchased[index]++;
        }
        else
        {
            print("Not enough money");
        }
    }
}
