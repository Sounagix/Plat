using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;

    private int numStars;

    private int currentNumStarPicked;

    [SerializeField]
    private TextMeshProUGUI starText;

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
        starText.text = currentNumStarPicked.ToString() + "/" + numStars.ToString();
        if (currentNumStarPicked >= numStars)
        {
            if (GameManager.Instance.CanActiveMoreLevels())
            {
                print("Todas las estrellas colecionadas");
                GameManager.Instance.ActiveNextLevel();
            }
        }
    }

    public void AddStar()
    {
        numStars++;
        starText.text = currentNumStarPicked.ToString() + "/" + numStars.ToString();
    }
}
