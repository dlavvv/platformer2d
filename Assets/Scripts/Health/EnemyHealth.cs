using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private bool dead = false;

    [Header("Health")]
    [SerializeField]private float startingHealth;
    private float currentHealth;
    
    [Header("iFrames")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberOfFlashes;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        sprite =  GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void EnemyTakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            // red flashes
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("die");
                SoundManager.instance.PlaySound(deathSound);
                gameObject.SetActive(false);
            }
        }

    }
}
