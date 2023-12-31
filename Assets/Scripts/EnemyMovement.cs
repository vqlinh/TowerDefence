using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        enemy=GetComponent<Enemy>();
        target = Waypoints.points[0];
        if (enemy.IsDeadrun)
        {
            return;
        }
        GameManager.instance.AudioEnemyRun();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * enemy.speed, Space.World);
        if (Vector3.Distance(transform.position, target.position) < 0.4f) GetNextWayPoint();
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWayPoint()
    {
       
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.enemiesALives--;
        Destroy(gameObject);
    }
}
