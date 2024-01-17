using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBuilding : Building
{
    

    
    private void Start()
    {
        timeElapsed = 0f;
        productionInterval = 2f;
    }
   

    protected override void ProduceResource()
    {
        if(redBuilding != null)
        {
            if(redBuilding.GetResource() >= 1f)
            {
                currentResourceCount ++;
            }
            else Debug.Log("GreenBuilding: Red resource is empty");

        }
    }

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
