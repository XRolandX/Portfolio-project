using UnityEngine;

public class RedBuilding : Building
{
    protected override void InitializeBuilding()
    {
        resourceColor = "red";
        resourceType = "Red";
        resourceCountDisplay = ResourceManager.Instance.redResources;
        produceTimeElapsed = productionResourceInterval;
        gettingTimeElapsed = gettingResourceInterval;
    }

    protected override void ProduceResource()
    {
        if (ResourceManager.Instance.redResources.Count < maxResourceCount)
        {
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.redResourcePrefab,
                resSpawnPoint.transform, ResourceManager.Instance.redResources);
        }
    }

    protected override void GetResource()
    {
        // null
    }

    protected override void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color=" + resourceColor + ">" + resourceType + "</color> resource: "
                + resourceCountDisplay.Count.ToString("F0")
                + "\nProduction time: " + produceTimeElapsed.ToString("F3");
        }
        else
        {
            Debug.LogError("Resource Index TMP is not set in Inspector on some Building");
        }
    }
}
