using UnityEngine;
using TMPro;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float resourceProductionRate;
    [SerializeField] protected float produceTimeElapsed;
    [SerializeField] protected float gettingTimeElapsed;
    [SerializeField] protected float productionInterval;

    [SerializeField] protected float currentResourceCount;
    [SerializeField] protected float currentStorageCount;
    [SerializeField] protected float currentStorageCount2;

    [SerializeField] protected float maxResourceCount;
    [SerializeField] protected float maxResourceStorage;

    [SerializeField] protected RedBuilding redBuilding;
    [SerializeField] protected GreenBuilding greenBuilding;
    [SerializeField] protected BlueBuilding blueBuilding;
    [SerializeField] protected TextMeshPro resourceDisplay;

    protected string resourceColor;
    protected string resourceType;

    [SerializeField] protected Transform resSpawnPoint;
    [SerializeField] protected Transform resStorePoint;
    [SerializeField] protected Transform resStorePoint2;

    protected List<GameObject> displayResource;

    
    protected virtual void Update()
    {
        ProduceTimeElapse();
        ResourceDisplay();
        GetResourceTimeElapse();
    }
    


    private void Awake()
    {
        redBuilding = FindObjectOfType<RedBuilding>();
        greenBuilding = FindObjectOfType<GreenBuilding>();
        blueBuilding = FindObjectOfType<BlueBuilding>();
        resourceDisplay = FindTMPInScene();
    }

    

    private void ProduceTimeElapse()
    {
        produceTimeElapsed += Time.deltaTime;

        if (produceTimeElapsed >= productionInterval)
        {
            ProduceResource();
            produceTimeElapsed = 0f;
        }
    }
    private void GetResourceTimeElapse()
    {
        gettingTimeElapsed += Time.deltaTime;

        if (gettingTimeElapsed >= productionInterval)
        {
            GetResource();
            gettingTimeElapsed = 0f;
        }
    }


    public abstract void ProduceResource();


    private void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color=" + resourceColor + ">" + resourceType + "</color> resource: "
                + displayResource.Count.ToString("F2")
                + "\nTime elapsed: " + gettingTimeElapsed.ToString("F2");
        }
    }
    public abstract void GetResource();
    public abstract TextMeshPro FindTMPInScene();
}