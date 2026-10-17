using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    public List<Enemy> enemiesInTrigger = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void AddEnemy(Enemy enemy) {
        enemiesInTrigger.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy) {
        enemiesInTrigger.Remove(enemy);
    }
}
