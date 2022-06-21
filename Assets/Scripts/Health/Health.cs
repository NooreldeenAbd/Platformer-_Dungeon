using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Animator playerAnimator;
    public float currentHealth { get; private set; }
    private bool dead;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
        playerAnimator = GetComponent<Animator>();
    }

    public void takeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player hurt
            playerAnimator.SetTrigger("Hurt");

            //TOFO: I-Frames
        }
        else
        {
            // player dead
            if (!dead)
            {
                playerAnimator.SetTrigger("Die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
