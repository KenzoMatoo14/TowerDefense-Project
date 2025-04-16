using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 1;
    public float range = 1;
    public int cost = 1;
    public float firerate = 1;
    public bool distractable = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
