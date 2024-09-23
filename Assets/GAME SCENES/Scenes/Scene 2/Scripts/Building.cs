using UnityEngine;
using TMPro;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float productionInterval;
    [SerializeField] protected float maxResourceCount = 5.0f;

    [SerializeField] protected float produceTimeElapsed;
    [SerializeField] private float gettingTimeElapsed;

    [SerializeField] private RedBuilding redBuilding;
    [SerializeField] private GreenBuilding greenBuilding;
    [SerializeField] private BlueBuilding blueBuilding;
    [SerializeField] protected TextMeshPro resourceDisplay;

    protected string resourceColor;
    protected string resourceType;

    [SerializeField] protected Transform resSpawnPoint;
    [SerializeField] protected Transform redResStorePoint;
    [SerializeField] protected Transform greenResStorePoint;

    protected List<GameObject> resourceCountDisplay;

    
    protected virtual void Update()
    {
        ProduceTimeElapse();
        ResourceDisplay();
        GetResourceTimeElapse();
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

    private void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color=" + resourceColor + ">" + resourceType + "</color> resource: "
                + resourceCountDisplay.Count.ToString("F0")
                + "\nTime elapsed: " + gettingTimeElapsed.ToString("F2");
        }
        else
        {
            Debug.LogError("Resource Index TMP is not set in Inspector on some Building");
        }
    }

    public abstract void ProduceResource();
    public abstract void GetResource();
}
