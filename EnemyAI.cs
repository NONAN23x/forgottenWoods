using UnityEngine;
using UnityEngine.AI;
using cowsins;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distaceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealthLegacy health;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerStats>().transform;
        health = GetComponent<EnemyHealthLegacy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead()) {
            enabled = false;
            navMeshAgent.enabled = false;
        }

        distaceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked) {
            EngageTarget();
        } 
        else if (distaceToTarget <= chaseRange) {
            isProvoked = true;
        }


    }

    public void OnDamageTaken() {
        isProvoked = true;
    }

    public void EngageTarget() {
            
        FaceTarget();
        if (distaceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }

        if (distaceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        }
    }

    private void ChaseTarget() {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget() {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
