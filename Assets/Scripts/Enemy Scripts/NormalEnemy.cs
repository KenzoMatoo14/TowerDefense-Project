using UnityEngine;
public class NormalEnemy : Enemy
{
    void Awake()
    {
        base.Awake();

        life = 4;
        speed = 6f;
        earn = 10;
    }
}
