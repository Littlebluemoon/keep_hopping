using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationSystem : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    private void Awake()
    {
        System.Type spawnerType = typeof(EntitySpawner);
        GameObject spawnerObject = new GameObject("Spawner", spawnerType);
        spawnerObject.GetComponent<Spawner>().Initialize(100, 100, _mesh, _material);
    }
}
