public class RedBuilding : Building
{
    protected string redColor = "red";
    protected string redResourceType = "Red";

    private void Start()
    {

        resourceColor = redColor;
        resourceType = redResourceType;
        resourceCountDisplay = ResourceManager.Instance.redResources;
    }

    public override void ProduceResource()
    {
        if (ResourceManager.Instance.redResources.Count < maxResourceCount)
        {
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.redResourcePrefab,
                resSpawnPoint.transform, ResourceManager.Instance.redResources);
        }
        
    }
    public override void GetResource()
    {
        // null
    }
}
