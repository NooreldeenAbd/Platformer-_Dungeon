using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private float colldownTimer = Mathf.Infinity;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && colldownTimer > attackCoolDown && playerMovement.canAttack())
            attack();

        colldownTimer += Time.deltaTime;
    }

    private void attack()
    {
        playerAnimator.SetTrigger("Attack");
        colldownTimer = 0;

        int fireballIndex = findFireball();
        fireballs[fireballIndex].transform.position = firePoint.position;
        fireballs[fireballIndex].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    // Returns first inactive fireball index
    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
