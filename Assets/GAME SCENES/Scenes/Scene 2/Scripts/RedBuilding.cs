public class RedBuilding : Building
{
    protected override void InitializeBuilding()
    {
        resourceColor = "red";
        resourceType = "Red";
        resourceCountDisplay = ResourceManager.Instance.redResources;
        produceTimeElapsed = productionResourceInterval;
        gettingTimeElapsed = gettingResourceInterval;
        isResourceInTransition = false;
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
}
