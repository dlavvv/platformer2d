using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCd;
    [SerializeField] private float distance;
    [SerializeField] private float range;
    [SerializeField] private int dmg;
    [SerializeField] private BoxCollider2D boxCollider;
    private float cdTimer = Mathf.Infinity;

    [Header("Ranged Settings")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Layer Settings")]
    [SerializeField] private LayerMask playerLayer;

    private Patrolling patroller;
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
                anim.SetTrigger("attackRanged");
            }
        }

        if (patroller != null)
        {
            // inamicul patruleaza doar cand player-ul nu e detectat
            patroller.enabled = !PlayerDetected();
        }
    }

    // animation event
    private void DealDamageRanged()
    {
        fireballs[0].transform.position = firepoint.position;
        fireballs[0].GetComponent<Fireball>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private bool PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
                                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y),
                                            0,
                                            Vector2.left,
                                            0,
                                            playerLayer);


        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y));
    }
}
