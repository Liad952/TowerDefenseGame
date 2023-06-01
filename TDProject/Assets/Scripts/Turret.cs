using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("General")]
    public float range = 15;
    public float speed = 10;
    public Transform firePoint;
    public Transform top;
    public static bool Upgraded = false;
    public bool upgraded = false;

    [Header("Use Bullets")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public ParticleSystem laserGlow;
    public float slowAmount = 0.5f;
    public int damageOverTime = 10;

    [Header("Use AoE")]
    public bool useAoe = false;
    public GameObject AoEEffectPos;
    public GameObject AoEEffectPrefab;
    public float cooldown;
    private float atkTimer;
    public int dmg;
    public GameObject AoeHit;


    private Transform target;
    private EnemyHealth currTarget;
    private Navmesh targetNav;

    public TurretBlueprint myBlueprint;
    public TurretBlueprint standardTurret;
    public TurretBlueprint LaserTurret;
    public TurretBlueprint aoeTurret;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        if(useAoe)
            myBlueprint = aoeTurret;
        else if(useLaser)
            myBlueprint = LaserTurret;
        else
            myBlueprint = standardTurret;
    }

    // Update is called once per frame
    void Update()
    {
        Upgraded = upgraded;
       if(target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    laserGlow.Stop();
                }
            }
            return;
        }
        else
        {
            if (useAoe)
            {
                AoE();
            }
            else
            {
                RotateTurret();
                if (useLaser)
                {
                    Laser();
                }
                else
                {
                    if (fireCountdown <= 0)
                    {
                        Shoot();
                        fireCountdown = 1f / fireRate;
                    }
                    fireCountdown -= Time.deltaTime;
                }
            }
            
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy!= null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            currTarget = nearestEnemy.GetComponent<EnemyHealth>();
            targetNav = nearestEnemy.GetComponent<Navmesh>();
        }
    }

    void RotateTurret()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(top.rotation,lookRotation,Time.deltaTime*speed).eulerAngles;
        top.rotation = Quaternion.Euler(0f, rotation.y,0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (Upgraded)
            bullet.dmg *= 2;
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
    private void Laser()
    {
        currTarget.TakeDamage(damageOverTime * Time.deltaTime);
        targetNav.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled= true;
            laserEffect.Play();
            laserGlow.Play();
        }

        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.rotation = Quaternion.LookRotation(dir);

        laserEffect.transform.position = target.position + dir.normalized * 0.5f;
    }

    private void AoE()
    {
        if (target!=null && atkTimer <= 0)
        {
            Instantiate(AoEEffectPrefab, AoEEffectPos.transform.position, Quaternion.identity);
            atkTimer = cooldown;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range);
            List<Collider> enemiesInRange = new List<Collider>();
            foreach (Collider enemy in enemies)
            {
                if (enemy.tag == "Enemy")
                {
                    enemiesInRange.Add(enemy);
                    EnemyHealth temp = enemy.GetComponent<EnemyHealth>();
                    temp.TakeDamage(dmg);
                    Instantiate(AoeHit,temp.transform.position,Quaternion.identity);
                }
            }
            if (enemiesInRange.Count == 0)
            {
                target = null;
            }
        }
        atkTimer -= Time.deltaTime;
    }

}
