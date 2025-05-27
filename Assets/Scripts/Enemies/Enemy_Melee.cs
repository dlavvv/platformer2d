using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCd;
    [SerializeField] private float distance;
    [SerializeField] private float range;
    [SerializeField] private int dmg;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private AudioClip attackSound;
    private float cdTimer = Mathf.Infinity;

    [Header("Layer Settings")]
    [SerializeField] private LayerMask playerLayer;

    private Patrolling patroller;
    private Health playerHealth;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patroller = GetComponentInParent<Patrolling>();
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;

        // daca player-ul e in raza de detectie
        if (PlayerDetected())
        {
            if (cdTimer >= attackCd)
            {
                cdTimer = 0;
                SoundManager.instance.PlaySound(attackSound);
                anim.SetTrigger("attackMelee");
            }
        }

        if(patroller != null)
        {
            // inamicul patruleaza doar cand player-ul nu e detectat
            patroller.enabled = !PlayerDetected();
        }
    }

    private bool PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance, 
                                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y), 
                                            0, 
                                            Vector2.left, 
                                            0, 
                                            playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance, 
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y));
    }

    // animation event
    private void DealDamage()
    {
        if (PlayerDetected())
        {
            playerHealth.TakeDamage(dmg);
        }
    }
}
