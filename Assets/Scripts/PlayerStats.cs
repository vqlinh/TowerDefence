using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Rounds;
    public static int Money;
    public int startMoney;
    public static int lives;
    public int startLives = 20;


    void Start()
    {
        Rounds = 0;
        Money = startMoney;
        lives = startLives;
    }
}
