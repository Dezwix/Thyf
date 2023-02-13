using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class CoinData
    {
        public int ID { get; set; }
        public bool Collected { get; set; }
    }
}