using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Transform[] buildings;
    
    public BuildingManager Load(int level)
    {
        var building = Instantiate(buildings[level % buildings.Length]);
        return building.GetComponent<BuildingManager>();
    }
}
