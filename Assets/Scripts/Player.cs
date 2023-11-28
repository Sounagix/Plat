using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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
    [Tooltip("Componente que se encarga de disparar")]
    private GunBehaviour gun;

    [SerializeField]
    [Tooltip("Vidas del jugador")]
    private int numOfLives;

    [SerializeField]
    [Tooltip("Tiempo de CD para el frontal Attack")]
    private int maxHp;

    [SerializeField]
    [Tooltip("Referencia al controlador del nivel")]
    private LevelSceneManager levelSceneManager;

    [SerializeField]
    [Tooltip("Referencia al animator del player")]
    private Animator animator;

    [SerializeField]
    private Scrollbar hp;

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

    private PlayerSounds playerSounds;

    private int currentHp;

    // Cantidad de saltos que lleva
    private int currentNumJump;

    // Determina si el jugador está tocando el suelo
    private bool onGround = true;

    // Booleano que determina si el player está agachado o no
    private bool bendActive = false;

    // Determina si el jugador está corriendo
    private bool running = false;

    // Determina si el jugador está con la defensa activada
    private bool defensiveModeActive = false;

    // Referencia al rigidBody
    private Rigidbody rb;

    // Referencia al collider del player
    private CapsuleCollider capCollider;

    // Posición inicial del player (para respawn)
    private Vector3 initPos;

    // Determina si el player está ejecutando el frontal attack
    private bool frontalAttackRdy = true;

    // Gets
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
        // Guardar posición inicial del player
        initPos = transform.position;
        playerSounds = GetComponent<PlayerSounds>();    
    }

    private void Start()
    {
        currentHp = maxHp;
        hp.numberOfSteps = maxHp;
    }

    // correción de velocidad (velocidad física y velocidad para animación)
    private void Update()
    {
        // capar la velocidad si se pasa, poner el player a la velocidad máxima
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            float maximaVelocidadPermitidaX = rb.velocity.normalized.x * (bendActive ? maxBendSpeed : maxSpeed);
            rb.velocity = new Vector3(maximaVelocidadPermitidaX, rb.velocity.y, rb.velocity.z);
        }

        if (Mathf.Abs(rb.velocity.z) > maxSpeed)
        {
            float maximaVelocidadPermitidaZ = rb.velocity.normalized.z * (bendActive ? maxBendSpeed : maxSpeed);
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maximaVelocidadPermitidaZ);
        }
        
        
        // Pasar a las variables de animación las velocides para el blend tree
        if (bendActive)// variables de agachado
        {
            animator.SetFloat("movXA", rb.velocity.z);
            animator.SetFloat("movZA", rb.velocity.x);
        }
        else if (!onGround) // para modificar las variables de salto
        {
            animator.SetFloat("movY", rb.velocity.y);
        }
        else // variables de movimiento
        {
            animator.SetFloat("movZ", rb.velocity.x);
            animator.SetFloat("movX",rb.velocity.z);
            animator.SetFloat("movY", rb.velocity.y);
        }

        //if(onGround && rb.velocity.magnitude <= 0.0f && playerSounds.GetStatus(SOUNDS.MOV))
        //{
        //    playerSounds.StopSound();
        //}

    }

    // Devuelve true si el player está ejecutando frontal attack
    public bool FrontalAttackRdy()
    {
        return frontalAttackRdy;
    }

    // Devuelve true si el player puede disparar
    public bool CanShot()
    {
        return gun.CanShot();
    }

    // crea una ataque
    public void Attack()
    {
        // donde guardo el objeto al que golpié con el rayo
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

    // si el jugador va a menos de la velocidad máxima devuelve true
    public bool CanMove()
    {
        return Mathf.Abs(rb.velocity.x) < (bendActive ? maxBendSpeed : maxSpeed);
    }

    // Devuelve true si el jugador puede saltar
    public bool CanJump()
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

    // Devuelve true si el jugador está agachado
    public bool IsBendActive()
    {
        return bendActive;
    }

    // Aumenta la velocidad del jugar hacia adeltante
    public void DashPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.right * dashSpeed, ForceMode.Impulse);
    }

    // Mueve al jugador en una dirección
    public void Move(Vector3 dir)
    {
        //playerSounds.PlaySound(SOUNDS.MOV);
        float velocity = bendActive ? movBendVelocity : running ? runVelocity : walkVelocity;
        velocity = defensiveModeActive ? velocity / 2 : velocity;
        rb.AddForce(dir * velocity, ForceMode.Impulse);
    }

    // HAce que el jugador salte
    public void Jump()
    {
        animator.SetTrigger("Jump");
        currentNumJump++;
        float velocity = jumpVelocity; 
        if (currentNumJump <= 2)
        {
            velocity = jumpVelocity / 2;
        }
        playerSounds.PlaySound(SOUNDS.JUMP);
        rb.AddForce(Vector2.up * velocity, ForceMode.Force);
    }

    // Método para agacharse
    public void BendPlayer()
    {
        animator.SetBool("Crouch", true);
        bendActive = true;
        capCollider.height = 1;
        capCollider.center = new Vector3(0.0f, -0.5f, 0.0f);
    }

    public void GetUpPlayer()
    {
        animator.SetBool("Crouch", false);
        bendActive = false;
        capCollider.height = 2;
        capCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    }


    public void ReciveDamage(float damage)
    {
        meshRenderer.material.color = Color.red;
        playerSounds.PlaySound(SOUNDS.DAMAGE);
        currentHp -= (int)damage;
        float v = (float)(currentHp - 0) / (maxHp - 0);
        hp.size = v;
        if (currentHp <= 0)
        {
            //Die();
            BackToRespawn();
        }
        //if (numOfLives < 0)
        //{
        //    Die();
        //}
        Invoke(nameof(BackFromTint), 1.0f);
    }

    private void BackFromTint()
    {
        meshRenderer.material.color = Color.white;
    }

    private void BackToRespawn()
    {
        playerSounds.PlaySound(SOUNDS.DIE);
        //GetComponent<Renderer>().material.color = Color.red;
        currentHp = maxHp;
        hp.size = 1;
        transform.position = initPos;
        //Invoke(nameof(BackColor), 1.0f);
    }

    public void ResetPlayer()
    {
        playerSounds.PlaySound(SOUNDS.DIE);
        levelSceneManager.ShowMsg("Faltan estrellas por tocar!");
        rb.velocity = Vector3.zero;
        transform.position = initPos;
    }


    private void Die()
    {
        currentHp = maxHp;
        hp.size = 1.0f;
        rb.velocity = Vector3.zero;
        transform.position = initPos;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Die();
        }
    }

    public void InitShot()
    {
        animator.SetTrigger("Shot");
    }

    public void Run()
    {
        running = true;
        levelSceneManager.ShowMsg("shift activo");
    }

    public void StopRun()
    {
        running = false;
        levelSceneManager.ShowMsg("shift desactivado");
    }

    public void StopMove()
    {
        rb.velocity = Vector3.zero;
    }

    public void ActiveDefensiveMode()
    {
        playerSounds.PlaySound(SOUNDS.DEFENSIVE);
        animator.SetBool("Defensive", true);
        defensiveModeActive = true;
        levelSceneManager.ShowMsg("defensive mode activo");
    }

    public void DeactiveDefensiveMode()
    {
        playerSounds.StopSound();
        animator.SetBool("Defensive", false);
        defensiveModeActive = false;
        levelSceneManager.ShowMsg("defensive mode desactivado");
    }

    public void Shot()
    {
        gun.Shoot();
    }

}
