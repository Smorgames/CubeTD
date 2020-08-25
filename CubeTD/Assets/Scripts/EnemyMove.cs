using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    private Transform targer;
    private int wayPointIndex = 0;

    private Enemy enemy;

    public int endPathSubtract = 1;

    private void Start()
    {
        targer = WayPoints.points[0];
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 direction = targer.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targer.position) <= 0.3f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWayPoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wayPointIndex++;
        targer = WayPoints.points[wayPointIndex];
    }

    private void EndPath()
    {
        PlayerStats.amountOfLives -= endPathSubtract;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
