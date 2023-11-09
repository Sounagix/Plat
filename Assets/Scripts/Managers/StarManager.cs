using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;

    private int numStars;

    private int currentNumStarPicked;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeStar()
    {
        currentNumStarPicked++;
        if (currentNumStarPicked >= numStars)
        {
            print("Todas las estrellas colecionadas");
        }
    }

    public void AddStar()
    {
        numStars++;
    }
}
