using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBuilding : Building
{
    
    public override float GetResource()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        timeElapsed = 0f;
        productionInterval = 5f;
        
    }


    protected override void ProduceResource()
    {
        if (redBuilding != null & greenBuilding != null)
        {
            if(redBuilding.GetResource() >= 1f && greenBuilding.GetResource() >= 1f)
            {
                currentResourceCount++;
            }
            else
            {
                Debug.Log("BlueBuilding: Required recource is empty");
            }
        }
    }
}
