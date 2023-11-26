using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    [SerializeField]
    private LevelSceneManager levelSceneManager;

    [SerializeField]
    private MovingPlatform movingPlatform;

    [SerializeField]
    private Transform switchPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.ActivePlatform(switchPosition.position);
            levelSceneManager.ShowMsg("Presiona tecla x para traer la plataforma");
        }
    }
}
