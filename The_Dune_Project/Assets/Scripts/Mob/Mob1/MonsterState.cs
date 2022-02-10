using UnityEngine;

public class MonsterState : MonoBehaviour
{
    //attributes
    [SerializeField] private float enemySpeed = 2f;
    public static float currentSpeed => Instance.enemySpeed;
    [SerializeField] private float aggroRad = 4f;
    public static float AggroRadius => Instance.aggroRad;
    [SerializeField] private float attackRange = 3f;
    public static float AttackRange => Instance.attackRange;
    public static MonsterState Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

    }
    
    //setter for speed
    public void setSpeed(float speed)
    {
        enemySpeed = speed;
    }

}
