using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    private Animator playerAnimator;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numFlashes;
    private SpriteRenderer sprietRen;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
        playerAnimator = GetComponent<Animator>();
        sprietRen = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player hurt
            playerAnimator.SetTrigger("Hurt");

            StartCoroutine(invunerability());
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

    private IEnumerator invunerability()
    {
        // player on layer 8 and enemy on 9
        Physics2D.IgnoreLayerCollision(8, 9, true);

        //invunerable duration
        for (int i = 0; i < numFlashes; i++)
        {
            sprietRen.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / numFlashes * 2);
            sprietRen.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / numFlashes * 2);
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
