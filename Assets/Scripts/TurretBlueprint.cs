using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefabs;
    public int cost;

    public GameObject upgradePrefabs;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
