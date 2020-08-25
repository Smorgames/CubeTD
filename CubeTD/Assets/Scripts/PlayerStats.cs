using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int amountOfLives;
    public int startLives = 20;

    public static int rounds;

    private void Start()
    {
        Money = startMoney;
        amountOfLives = startLives;
        rounds = 0;
    }

    private void Update()
    {
        amountOfLives = Mathf.Clamp(amountOfLives, 0, 10000);
    }
}
