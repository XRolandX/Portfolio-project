using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBuilding : Building
{
    
    public override float GetResource()
    {

        if (currentResourceCount >= 1f)
        {
            float resource = 1f;
            currentResourceCount--;
            return resource;
        }
        else return 0f;

    }

}
