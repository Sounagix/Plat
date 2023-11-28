using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !StarManager.instance.AllStarsTaken())
        {
            player = other.GetComponent<Player>();
            Invoke(nameof(CallReset), 2.0f);
        }
    }

    private void CallReset()
    {
        player.ResetPlayer();
    }
}
