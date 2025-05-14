using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private float waitTime; // cate secunde asteapta pana schimba directia
    private float timer;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool facingLeft;
    [SerializeField] private Animator anim;
    

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if(facingLeft)
        {
            if(enemy.position.x >= left.position.x)
            {
                MovementDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
            
        }
        else
        {
            if (enemy.position.x <= right.position.x)
            {
                MovementDirection(1);
            }
            else
            {
                ChangeDirection();
            }
                
        }
        
    }

    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        timer += Time.deltaTime;

        if(timer > waitTime)
        {
            facingLeft = !facingLeft;
        }
    }

    private void MovementDirection(int _direction)
    {
        timer = 0;
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, 
                                        initScale.y, 
                                        initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, 
                                    enemy.position.y, 
                                    enemy.position.z);
    }
}
