using UnityEngine;
using System.Collections;
public class Turret : Tower
{
    public Transform target;
    public string enemyTag = "Enemy";
    public Transform partToRotate;

    public Turret()
    {
        damage = 2;
        range = 20.0f;
        cost = 30;
        firerate = 1.5f;
        distractable = true;
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion LookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, LookRotation, Time.deltaTime * 10f).eulerAngles;

            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else return;
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = range;
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
            target = nearestEnemy.transform;
        }
    }

}