using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 1;
    public float speed = 2f;
    public int earn = 10;

    protected List<GameObject> pathWaypoints;
    protected int currentTargetIndex = 0;
    private float rotationSpeed = 5f;
    protected float originalY;

    protected virtual void Awake()
    {

        BoxCollider boxCol = GetComponent<BoxCollider>();
        if (boxCol == null)
        {
            boxCol = gameObject.AddComponent<BoxCollider>();
        }
        boxCol.isTrigger = false;

        Vector3 baseSize = new Vector3(3f, 3f, 3f);

        // Ajustamos el tamaño en función de la escala del objeto
        Vector3 adjustedSize = new Vector3(
            baseSize.x / transform.lossyScale.x,
            baseSize.y / transform.lossyScale.y,
            baseSize.z / transform.lossyScale.z
        );

        boxCol.size = adjustedSize;
        boxCol.center = new Vector3(0, adjustedSize.y / 2, 0); // Ajustar centro dinámicamente

        // Luego agregar Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.useGravity = false;
        //Debug.Log(gameObject.name + " ahora tiene un Rigidbody.");
    }

    protected virtual void Start()
    {
        pathWaypoints = PathGenerator.Instance.pathWaypoints;
        originalY = transform.position.y;

        if (pathWaypoints.Count > 0)
        {
            transform.position = new Vector3(   
                pathWaypoints[0].transform.position.x,
                originalY,
                pathWaypoints[0].transform.position.z
            );
        }
    }

    protected virtual void Update()
    {
        MoveAlongPath();
    }

    protected void MoveAlongPath()
    {
        if (pathWaypoints.Count == 0 || currentTargetIndex >= pathWaypoints.Count)
            return;

        Vector3 targetPosition = new Vector3(
            pathWaypoints[currentTargetIndex].transform.position.x,
            originalY,
            pathWaypoints[currentTargetIndex].transform.position.z
        );

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        RotateTowards(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentTargetIndex++;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (life <= 0) return;

        life -= damage;
        Debug.Log("Inmigrant took " + damage + " damage! Remaining life: " + life);

        // When the immigrant dies
        if (life <= 0 ) //&& GameManager.manager != null)
        {
            speed = 0;
            //GameManager.manager.AddCash(earn);
            //Debug.Log(gameObject.name + " ha muerto. Se añadieron " + earn + " de dinero.");
            Destroy(gameObject);
            //GameManager.manager.ReduceEnemies();
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}