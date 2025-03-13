using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private float distance;
    [SerializeField] private float range;
    [SerializeField] private int dmg;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;

    private Health playerHealth;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;

        if (PlayerDetected())
        {
            if (cdTimer >= attackCd)
            {
                cdTimer = 0;
                anim.SetTrigger("attackMelee");
            }
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

    private void DealDamage()
    {
        if (PlayerDetected())
        {
            playerHealth.TakeDamage(dmg);
        }
    }
}
