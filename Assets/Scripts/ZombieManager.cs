using System.Collections.Generic;
using UnityEngine;


public struct Agent
{
    public bool active;
    public Vector3 pos;
    public Vector3 vel;
}

[System.Serializable]
public struct Entity
{
    public Transform transform;
    public SkinnedMeshRenderer renderer;
}

public class ZombieManager : MonoBehaviour
{
    public GameObject prefab;

    public Entity[] entities;

    public ushort maxEntities = 1000;


    // Spawn 1000 agents but only have 50 turned on. 


    private void Awake()
    {
        entities = new Entity[maxEntities];
    }

    private void Start()
    {
        int kk = 0;
        for (int i = 0; i < maxEntities; i++)
        {
            var zombie = Instantiate(prefab, new Vector3(0f, 0f, kk), Quaternion.identity, transform);
            entities[i].renderer = zombie.GetComponentInChildren<SkinnedMeshRenderer>();
            entities[i].transform = zombie.transform;
            zombie.name = "zombie index: " + i;

            kk += -2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        for (int i = 0; i < entities.Length; i++)
        {
            Entity entity = entities[i];
            Vector3 screenPos = cam.WorldToViewportPoint(entity.transform.position);

            if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1)
            {
                entities[i].renderer.enabled = true;
            }
            else
            {
                entities[i].renderer.enabled = false;
            }
        }
    }
}