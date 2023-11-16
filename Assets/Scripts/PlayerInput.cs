using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tecla para mover al player hacia adelante")]
    private KeyCode movForward;

    [SerializeField]
    [Tooltip("Tecla para mover al player hacia atras")]
    private KeyCode movBack;

    [SerializeField]
    [Tooltip("Tecla para mover al player hacia la derecha")]
    private KeyCode movRight;

    [SerializeField]
    [Tooltip("Tecla para mover al player hacia la izquierda")]
    private KeyCode movLeft;

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
    [Tooltip("Tecla para el disparo")]
    private KeyCode shot;

    [SerializeField]
    [Tooltip("Tecla para el disparo")]
    private KeyCode defensiveMode;

    private Player player;


    private void Awake()
    {
        player = GetComponent<Player>();    
    }

    private void Update()
    {
        // cuando el jugador toque cualquier input
        if (Input.anyKey)
        {
            if (Input.GetKey(movForward) && player.CanMove())
            {
                player.Move(Vector3.right);
            }
            else if (Input.GetKey(movBack) && player.CanMove())
            {
                player.Move(Vector3.left);
            }

            if (Input.GetKey(movRight) && player.CanMove())
            {
                player.Move(Vector3.back);
            }
            else if (Input.GetKey(movLeft) && player.CanMove())
            {
                player.Move(Vector3.forward);
            }

            if (Input.GetKeyDown(frontalAttack) && player.FrontalAttackRdy())
            {
                player.Attack();
            }

            if (Input.GetKeyDown(shot) && player.CanShot())
            {
                player.InitShot();
            }


            if (Input.GetKey(bend))
            {
                player.BendPlayer();
            }

            if (Input.GetKey(run))
            {
                player.Run();

            }

            if (Input.GetKeyDown(jump) && player.CanJump())
            {
                if (player.IsBendActive())
                {
                    player.DashPlayer();
                }
                else
                {
                    player.Jump();
                }
            }

            if (Input.GetKeyUp(movForward) || Input.GetKeyUp(movBack))
            {
                player.StopMove();
                
            }

            if (Input.GetKeyDown(defensiveMode))
            {
                player.ActiveDefensiveMode();
            }

        }

        if (Input.GetKeyUp(bend))
        {
            player.GetUpPlayer();
        }
        else if (Input.GetKeyUp(run))
        {
            player.StopRun();
        }
        else if (Input.GetKeyUp(defensiveMode))
        {
            player.DeactiveDefensiveMode();
        }
    }
}
