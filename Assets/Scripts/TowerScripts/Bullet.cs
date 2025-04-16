using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float damage = 0f;
    public GameObject impactEffect;

    public void Seek(Transform newTarget)
    {
        if (newTarget == null) return;

        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;  
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else Destroy(gameObject); return; 
    }

    void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.TakeDamage(damage); 
        //Debug.Log("Impacto en " + enemy.gameObject.name + " con " + damage + " de daño.");
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
