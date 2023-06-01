using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public int dmg = 2;
    public float speed = 70f;
    public GameObject hitParticle;

    public void Seek(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distEachFrame = speed * Time.deltaTime;

        if(dir.magnitude<=distEachFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized*distEachFrame, Space.World);

    }

    void HitTarget()
    {
        EnemyHealth enemy = target.GetComponent<EnemyHealth>();
        enemy.TakeDamage(dmg);
        Destroy(gameObject);
        GameObject hitEffect = Instantiate(hitParticle, target.transform.position, transform.rotation);
        Destroy(hitEffect, 1f);
    }

}
