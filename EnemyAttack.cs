using UnityEngine;
using cowsins;

public class EnemyAttack : MonoBehaviour
{
    // PlayerManager target;
    PlayerStats target;
    [SerializeField] float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerStats>();
    }

    public void AttackHitEvent() {
        if (target==null) {return;}
        target.Damage(damage);
        Debug.Log("Damaging Player");
    }

}
