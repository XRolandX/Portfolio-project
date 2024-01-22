using System.Collections;
using System;
using TMPro;
using UnityEngine;

public class GreenBuilding : Building
{
    
    private void Start()
    {

        maxResourceCount = 5f;
        currentResourceCount = 0f;
        resourceProductionRate = 1f;
        productionInterval = 3f;
        resourceColor = "green";
        resourceType = "Green";
    }
    
    protected override void ProduceResource()
    {
        GetResource();
    }

    protected override void GetResource()
    {
        if (currentResourceCount < maxResourceCount)
        {
            
            // Transition the red resource to the green building
            ResourceManager.Instance.GetLatestResource();
            currentResourceCount = ResourceManager.Instance.greenWarehouse.Count;
        }
        
    }

    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Green TMP").GetComponent<TextMeshPro>();
    }

    
}
