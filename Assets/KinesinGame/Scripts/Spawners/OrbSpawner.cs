using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private OrbData[] orbTypes;

    private void Start()
    {
        SpawnOrb();
    }

    private OrbData GetRandomOrb()
    {
        float totalSpawnChance = 0f;

        foreach (OrbData orb in orbTypes)
            totalSpawnChance += orb.spawnChance; // Calculate the total spawn chance by summing up the spawn chances of all orb types.

        float randomPoint = Random.value * totalSpawnChance;

        foreach (OrbData orb in orbTypes)
        {
            if (randomPoint < orb.spawnChance)
                return orb; 

            randomPoint -= orb.spawnChance;
        }

        return orbTypes[0];
    }

    public void SpawnOrb()
    {
        OrbData orbData = GetRandomOrb();
        Collider tileCollider = GetComponentInParent<GroundTile>().GetComponent<Collider>();

        int lane = Random.Range(0, 3);
        int orbsToSpawn = Random.Range(1, orbData.maxAmountToSpawn + 1);

        float laneDistance = 3f;
        float x = tileCollider.bounds.center.x + (lane - 1) * laneDistance;
        float startZ = tileCollider.bounds.min.z + 1f;
        float spacing = 2f;

        for (int i = 0; i < orbsToSpawn; i++)
        {
            GameObject orb = Instantiate(orbData.orbPrefab, transform);

            float z = startZ + i * spacing;
            float y = 1f;

            orb.transform.position = new Vector3(x, y, z);

            CollectibleOrb orbComponent = orb.GetComponent<CollectibleOrb>();
            orbComponent.Initialize(orbData.happinessValue);
        }
    }

    //Vector3 GetRandomPointCollder (Collider collider)
    //{
    //    Vector3 point = new(
    //        Random.Range(collider.bounds.min.x, collider.bounds.max.x),
    //        Random.Range(collider.bounds.min.y, collider.bounds.max.y),
    //        Random.Range(collider.bounds.min.z, collider.bounds.max.z)
    //    );
    //    if (point != collider.ClosestPoint(point))
    //    {
    //        return GetRandomPointCollder(collider);
    //    }
    //    point.y = 1;
    //    return point;
    //}
}