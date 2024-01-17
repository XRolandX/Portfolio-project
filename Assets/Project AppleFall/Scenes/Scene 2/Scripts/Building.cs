using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float resourceProductionRate;
    [SerializeField] protected float currentResourceCount;
    [SerializeField] protected float timeElapsed;
    [SerializeField] protected float productionInterval;
    [SerializeField] protected RedBuilding redBuilding;
    [SerializeField] protected GreenBuilding greenBuilding;

    protected virtual void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= productionInterval)
        {
            ProduceResource();
            timeElapsed = 0f;
        }
    }

    private void Awake()
    {
        redBuilding = FindObjectOfType<RedBuilding>();
        greenBuilding = FindObjectOfType<GreenBuilding>();
        resourceProductionRate = 1f;
    }

    protected virtual void ProduceResource()
    {
        currentResourceCount += resourceProductionRate * Time.deltaTime;
    }

    public abstract float GetResource();
}