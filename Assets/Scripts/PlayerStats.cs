using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int greatLeaves = 0, goodLeaves = 0, badLeaves = 0;

    public void AddLeafCount(int leafType)
    {
        if (leafType == 0)
        {
            badLeaves ++;
        }
        else if (leafType == 1)
        {
            goodLeaves++;
        }
        else if (leafType == 2)
        {
            greatLeaves++;
        }
        else
        {
            Debug.LogWarning("Leaf type not found");
        }
    }

    public int GetLeafCount(int leafType)
    {
        if (leafType == 0)
        {
            return badLeaves;
        }
        else if (leafType == 1)
        {
            return goodLeaves;
        }
        else if (leafType == 2)
        {
            return greatLeaves;
        }
        else
        {
            Debug.LogWarning("Leaf type not found");
            return 0;
        }
    }
}
