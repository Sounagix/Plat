using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tecla para mover al player hacia adelante")]
    private KeyCode movForward;

    [SerializeField]
    [Tooltip("Tecla para mover al player hacia atras")]
    private KeyCode movBack;

    [SerializeField]
    [Tooltip("Tecla para saltar")]
    private KeyCode jump;

    [SerializeField]
    [Tooltip("Tecla para agacharse")]
    private KeyCode bend;

    [SerializeField]
    [Tooltip("Tecla para correr")]
    private KeyCode run;

    [SerializeField]
    [Tooltip("Tecla para ataque frontal")]
    private KeyCode frontalAttack;

    [SerializeField]
    [Tooltip("Velocidad de movimiento del player al caminar")]
    private float walkVelocity;

    [SerializeField]
    [Tooltip("Velocidad de movimiento del player al correr")]
    private float runVelocity;

    [SerializeField]
    [Tooltip("Velocidad de movimiento del player cuandoe está agachado")]
    private float movBendVelocity;

    [SerializeField]
    [Tooltip("Velocidad del salto player")]
    private float jumpVelocity;

    [SerializeField]
    [Tooltip("Numero de saltos que puede saltar el jugar")]
    private int numJumps;

    [SerializeField]
    [Tooltip("Máxima velocidad para moverse")]
    private float maxSpeed;

    [SerializeField]
    [Tooltip("Velocidad para hacer el dash")]
    private float dashSpeed;

    [SerializeField]
    [Tooltip("Máxima velocidad para moverse agachado")]
    private float maxBendSpeed;

    [SerializeField]
    [Tooltip("Tiempo de CD para el frontal Attack")]
    private float frontalAttackColdDawn;

    [SerializeField]
    [Tooltip("Rango de ataque para frontal Attack")]
    private float frontalAttackRange;


    [SerializeField]
    private int numOfLives;

    [SerializeField]
    private LevelSceneManager levelSceneManager;

    private int currentNumJump;

    private bool onGround = true;

    private bool bendActive = false;

    private bool running = false;

    private Rigidbody rb;

    private CapsuleCollider capCollider;

    [SerializeField]
    private Animator animator;

    private Vector3 initPos;

    private bool frontalAttackRdy = true;

    // El primer método de unity que se llama
    //  -> para los gets
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        capCollider = GetComponent<CapsuleCollider>();
    }


    private void Start()
    {
       
    }

    // se llama una vez x frame -> 60fps -> 60 x segundo
    private void Update()
    {
        // cuando el jugador toque cualquier input
        if (Input.anyKey)
        {
            if (Input.GetKey(movForward) && CanMove())
            {
                Move(Vector2.right);
            }
            else if (Input.GetKey(movBack) && CanMove())
            {
                Move(Vector2.left);
            }

            if (Input.GetKeyDown(frontalAttack) && frontalAttackRdy)
            {
                Attack();
            }


            if (Input.GetKey(bend))
            {
                BendPlayer();
            }

            if (Input.GetKey(run))
            {
                running = true;
                levelSceneManager.ShowMsg("shift activo");
            }

            if (Input.GetKeyDown(jump) && CanJump())
            {
                if (bendActive)
                {
                    DashPlayer();
                }
                else
                {
                    Jump();
                }
            }

            if (Input.GetKeyUp(movForward) || Input.GetKeyUp(movBack))
            {
                rb.velocity = Vector3.zero;
            }
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            float factor = rb.velocity.normalized.x * (bendActive ? maxBendSpeed : maxSpeed);
            rb.velocity = new Vector3(factor, rb.velocity.y, rb.velocity.z);
        }

        if (Input.GetKeyUp(bend))
        {
            GetUpPlayer();
        }
        else if (Input.GetKeyUp(run))
        {
            running = false;
            levelSceneManager.ShowMsg("shift desactivado");
        }

        if (bendActive)
        {
            animator.SetFloat("movY", -1.0f);
            animator.SetFloat("movX", rb.velocity.x);
        }
        else
        {
            animator.SetFloat("movY", Mathf.Abs(rb.velocity.y));
            animator.SetFloat("movX", rb.velocity.x);
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        Vector3 initPos = transform.position;
        Vector3 dir = transform.right;
        int layer = (1 | 6);

        Debug.DrawLine(initPos, initPos + dir * frontalAttackRange, Color.red, 1.0f);

        if (Physics.Raycast(initPos, dir, out hit, frontalAttackRange)) 
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                BaseEnemy dummy = hit.collider.gameObject.GetComponent<BaseEnemy>();
                dummy.ReciveDamage(1);
            }
        }

        levelSceneManager.ShowMsg("Frontal attack!");
        frontalAttackRdy = false;
        animator.SetTrigger("FrontalAttack");
        Invoke(nameof(FrontalAttackCD),frontalAttackColdDawn);
    }

    private void FrontalAttackCD()
    {
        levelSceneManager.ShowMsg("Frontal attack rdy");
        frontalAttackRdy = true;
    }

    private bool CanMove()
    {
        return Mathf.Abs(rb.velocity.x) < (bendActive ? maxBendSpeed : maxSpeed);
    }

    private bool CanJump()
    {
        if (currentNumJump == 0)
        {
            return onGround;
        }
        else
        {
            return currentNumJump < numJumps;
        }
    }

    private void DashPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.right * dashSpeed, ForceMode.Impulse);
    }

    private void Move(Vector2 dir)
    {
        float velocity = bendActive ? movBendVelocity : running ? runVelocity : walkVelocity;
        rb.AddForce(dir * velocity, ForceMode.Impulse);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        currentNumJump++;
        float velocity = jumpVelocity; 
        if (currentNumJump <= 2)
        {
            velocity = jumpVelocity / 2;
        }
        rb.AddForce(Vector2.up * velocity, ForceMode.Force);
    }

    private void BendPlayer()
    {
        
        bendActive = true;
        capCollider.height = 1;
        capCollider.center = new Vector3(0.0f, -0.5f, 0.0f);
    }

    private void GetUpPlayer()
    {
        bendActive = false;
        capCollider.height = 2;
        capCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    }


    public void ReciveDamage()
    {
        numOfLives--;
        BackToRespawn();
        if (numOfLives < 0)
        {
            Die();
        }
    }

    private void BackToRespawn()
    {
        GetComponent<Renderer>().material.color = Color.red;
        transform.position = initPos;
        Invoke(nameof(BackColor), 1.0f);
    }

    private void BackColor()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }


    private void Die()
    {

    }

    // metodo de unity que se llama al colisionar con algo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            currentNumJump = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            onGround = false;
        }
    }
}
