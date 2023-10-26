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
    [Tooltip("Velocidad de movimiento del player")]
    private float movVelocity;

    [SerializeField]
    [Tooltip("Velocidad del salto player")]
    private float jumpVelocity;

    [SerializeField]
    [Tooltip("Numero de saltos que puede saltar el jugar")]
    private int numJumps;

    [SerializeField]
    private int numOfLives;


    // para saber si el jugador está en el suelo
    [SerializeField]
    private bool onGround = true;
    private Rigidbody rb;

    private Vector3 initPos;

    // El primer método de unity que se llama
    //  -> para los gets
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
    }


    // El segundo método que se llama al crear mi juego
    //  Para que se usa? -> para instanciar
    private void Start()
    {
        
    }

    // se llama una vez x frame -> 60fps -> 60 x segundo
    private void Update()
    {
        // cuando el jugador toque cualquier input
        if (Input.anyKey)
        {
            if (Input.GetKey(movForward))
            {
                Move(Vector2.right);
            }
            else if (Input.GetKey(movBack))
            {
                Move(Vector2.left);
            }

            if (Input.GetKeyDown(jump) && onGround)
            {
                Jump();
            }
        }
    }

    private void Move(Vector2 dir)
    {
        rb.AddForce(dir * movVelocity, ForceMode.Impulse);
    }

    private void Jump()
    {
        onGround = false;
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode.Force);
    }

    // metodo de unity que se llama al colisionar con algo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            // normalize = (0/1)
            onGround = true;
            Debug.DrawLine(transform.position, collision.transform.position, Color.red, 1.0f);
            Vector3 dir = Vector3.Normalize(collision.transform.position - transform.position);
            if (dir.y >= 0.0f)
            {
                
            }
        }
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
}
