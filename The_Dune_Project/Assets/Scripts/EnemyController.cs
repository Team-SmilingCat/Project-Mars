using Fight;
using UnityEngine;
using UnityEngine.AI;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] protected float lookRadius = 25f;
    [SerializeField] protected float timeToDeagrro = 5f;
    private float aggroTimer = 0.0f;
    [ReadOnlyAttribute, SerializeField] protected bool isAggroed = false;

    [SerializeField] protected float hitCooldown = 3.0f;
    private float hitCooldownTimer;
    
    protected Vector3 homeLocation;
    protected NavMeshAgent myAgent;
    protected Fighter myFighter;
    
    [ReadOnlyAttribute, SerializeField] protected Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerFighter>().transform;
        myAgent = GetComponent<NavMeshAgent>();
        myFighter = GetComponent<Fighter>();

        homeLocation = transform.position; // when player runs away, enemy goes back to their initial location
    }

    private void Update()
    {
        hitCooldownTimer = Mathf.Max( hitCooldownTimer - Time.deltaTime, 0.0f);
        aggroTimer = Mathf.Max( aggroTimer - Time.deltaTime, 0.0f);

        if (myFighter.curHealth <= 0)
        {
            Destroy(this);
        }
    }

    protected virtual void GoToTarget()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            myAgent.SetDestination(target.position);
            MeleeAttackTarget(); // TODO: dont have to do
            
            aggroTimer = timeToDeagrro;
            isAggroed = true;
        }

        if (isAggroed && aggroTimer <= 0)
        {
            isAggroed = false;
            myAgent.SetDestination(homeLocation);
        }
    }

    protected virtual void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z);
        if (direction.sqrMagnitude > 0.25f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    protected virtual void MeleeAttackTarget()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= myAgent.stoppingDistance)
        {
            FaceTarget();
            
            Fighter targetFighter = target.GetComponent<Fighter>();
            if(targetFighter != null && hitCooldownTimer <= 0.0f)
            {
                targetFighter.TakeDamage(10); // TODO

                hitCooldownTimer = hitCooldown;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}