using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public int damage = 50;
    public float speed = 70f;
    public GameObject effect;
    public float explosionRadius = 0f;

    private void Start()
    {
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = GameObject.Instantiate(effect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        if (explosionRadius > 0)
        {
            Explode();
            GameManager.instance.AudioMissile();
        }
        else
        {
            Damage(target);
            GameManager.instance.AudioStandard();

        }
        Destroy(gameObject);
    }

    void Explode()
    {
        // Tìm tất cả các Collider trong một quả cầu có tâm tại vị trí của đối tượng hiện tại (transform.position)
        // và bán kính là explosionRadius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        // Duyệt qua mảng các Collider thu được
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            } 
        }
    }

    void Damage(Transform enemy)
    {
 
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null) e.TakeDamage(damage);
    }

 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
