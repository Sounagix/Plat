using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public void Shot()
    {
        player.Shot();
    }
}
