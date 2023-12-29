using UnityEngine;

public class Turret : MonoBehaviour
{
    public Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    [Header("Use bullet default")]
    public GameObject bulletPrefabs;
    public float fireRate = 1f;
    public float fireCountdown = 0;

    [Header("Use laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public float slowAmount = .5f;

    private Transform tartget;
    [Header("UNITY SETUP")]
    public Transform firePoint;
    public float turnSpeed = 10f;
    public Transform partToRotate;
    public string enemyTag = "Enemy";

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            tartget = nearestEnemy.transform;
            targetEnemy=nearestEnemy.GetComponent<Enemy>(); 
        } 
        else tartget = null;
    }

    private void Update()
    {
        if (tartget == null)
        {
            if (useLaser)
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled=false;
                }
            return;
        } 
        TurretRotation();
        if (useLaser) Laser();
        else SetUpShoot();

    }

    void SetUpShoot()
    {
        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = GameObject.Instantiate(bulletPrefabs, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null) bullet.Seek(tartget);
    }

    void TurretRotation()
    {
        Vector3 dir = tartget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime*Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,tartget.position);
        Vector3 dir = firePoint.position-tartget.position;
        impactEffect.transform.position = tartget.position + dir.normalized ;
        impactEffect.transform.rotation=Quaternion.LookRotation(dir);
        impactEffect.transform.position=tartget.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
