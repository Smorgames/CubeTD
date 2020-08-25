using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public GameObject bulletImpactEffect;

    public float explosionRadius = 0;

    public int damage = 50;

    private SinglePlayAudio singlePlayAudio;

    private void Start()
    {
        singlePlayAudio = SinglePlayAudio.instance;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            if (explosionRadius > 0)
            {
                Explode();
                GameObject bulletImpactEffectVariable = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
                Destroy(bulletImpactEffectVariable, 5f);
            }
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject bulletImpactEffectVariable = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(bulletImpactEffectVariable, 5f);

        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObjects)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

        singlePlayAudio.PlayExplosionClip();
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
            e.TakeDamage(damage);
    }
}
