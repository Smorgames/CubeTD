using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use laser")]

    public bool useLaser = false;

    public int damageOverTime = 30;

    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public Light lightImpact;

    public float slowPercentage = 0.5f;

    [Header("Unity Setup Fields")]

    public Transform partToRotate;
    public float rotationSpeed = 10;

    public Transform firePoint;

    private SinglePlayAudio singlePlayAudio;
    private float explosionRadius;

    public AudioSource audioSource;
    public AudioClip laserSound;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        singlePlayAudio = SinglePlayAudio.instance;
        explosionRadius = bulletPrefab.GetComponent<Bullet>().explosionRadius;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistanse = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanseToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanseToEnemy < shortestDistanse)
            {
                shortestDistanse = distanseToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistanse <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    lightImpact.enabled = false;
                    if (audioSource.isPlaying)
                        audioSource.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(laserSound);
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                if (explosionRadius == 0)
                    singlePlayAudio.PlayShootClip(); // add audio manager
                else
                    singlePlayAudio.PlayLauncherShootClip(); // add audio manager
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    private void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercentage);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            lightImpact.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserEffect.transform.position = target.position + dir.normalized * 1.7f;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}
}
