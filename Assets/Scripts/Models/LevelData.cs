using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelData : MonoBehaviour
{
    public int Level { get; set; }
    public int CheckPoint { get; set; }
    public List<CoinData> Coins { get; set; }

    public LevelData(int level, int checkPoint, List<CoinData> coins)
    {
        Level = level;
        CheckPoint = checkPoint;
        Coins = coins;
    }
}
