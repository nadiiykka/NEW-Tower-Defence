using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    float attackRadius;
    Projectile projectile;
    Enemy targetEnemy = null;
    float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in Manager.Instance.EnemyList)
        {
            if (UnityEngine.Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }
    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemiesInRange())
        {
            if (UnityEngine.Vector2.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = UnityEngine.Vector2.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
