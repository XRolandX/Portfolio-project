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
        displayResource = ResourceManager.Instance.redResources;
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

    public override void GetResource()
    {
        // doesn't get any resources
    }
}
