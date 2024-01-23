
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedBuilding : Building
{
    private void Start()
    {
        resourceProductionRate = 1f;
        productionInterval = 1.33f;
        currentResourceCount = 0f;
        maxResourceCount = 5f;
        resourceColor = "red";
        resourceType = "Red";
    }


    public override void ProduceResource()
    {
        if (ResourceManager.Instance.redResources.Count < maxResourceCount)
        {
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.redResourcePrefab,
                resSpawnPoint.transform, ResourceManager.Instance.redResources);
            currentResourceCount = ResourceManager.Instance.redResources.Count;
        }
        
    }


    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Red TMP").GetComponent<TextMeshPro>();
    }
    public override void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color=" + resourceColor + ">" + resourceType + "</color> resource: "
                + ResourceManager.Instance.redResources.Count.ToString("F2")
                + "\nTime elapsed: " + gettingTimeElapsed.ToString("F2");
        }
    }


    public override void GetResource()
    {
        // doesn't get any resources
    }

}
