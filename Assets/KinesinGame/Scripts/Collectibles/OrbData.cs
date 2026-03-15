using UnityEngine;

[CreateAssetMenu(fileName = "OrbData", menuName = "Game/Orb Data")]
public class OrbData : ScriptableObject
{
    public string orbName;
    public float happinessValue;
    public float spawnChance; 
    public int maxAmountToSpawn;
    public GameObject orbPrefab;

}