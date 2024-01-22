using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueBuilding : Building
{
    
    

    private void Start()
    {
        resourceProductionRate = 2f;
        productionInterval = 5f;
        resourceColor = "blue";
        resourceType = "Blue";
    }


    protected override void ProduceResource()
    {
       
    }

    protected override void GetResource() => throw new System.NotImplementedException();
    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Blue TMP").GetComponent<TextMeshPro>();
    }

}
