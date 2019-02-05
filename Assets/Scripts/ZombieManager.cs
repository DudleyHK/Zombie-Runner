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
    public GameObject floor;
    public Entity[] entities;

    public float frameStep = 0.1f;
    public float entitySpacing = 2f;
    public float popout = 1.5f;
    public ushort entitiesPerline = 3;
    public ushort maxEntities = 1000;
    public Vector3 worldCentre;

    
    private float timer = 0f;


    // Spawn 1000 agents but only have 50 turned on. 


    private void Awake()
    {
        entities = new Entity[maxEntities];
    }

    private void Start()
    {
        float rz = -5f;
        float rx = -1.5f;
        for (int i = 0; i < maxEntities; i++)
        {
            var zombie = Instantiate(prefab, new Vector3(rx, 0f, rz), Quaternion.identity, transform);
            entities[i].renderer = zombie.GetComponentInChildren<SkinnedMeshRenderer>();
            entities[i].transform = zombie.transform;
            zombie.name = "zombie index: " + i;

            if(i % entitiesPerline == 0)
                rz -= entitySpacing;

            rx -= 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Toggle();
        
        
        // Wrap in timeer. 
        if (timer <= 0f)
        {
            timer = frameStep;
            
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            
            
            foreach (var hit in Physics.RaycastAll(ray))
            {
                if (hit.transform.name == "Floor")
                    worldCentre = hit.point;
                else
                {
                    Debug.Log("name - " + hit.transform.name);
                }
                Debug.DrawRay(ray.origin, ray.direction * 50f, Color.cyan);
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    private float GetSpawnX()
    {
        return  Random.Range(worldCentre.x - 1f, worldCentre.x + 1f);
    }
    
    
    
    private void Toggle()
    {
        Camera cam = Camera.main;
        for (int i = 0; i < entities.Length; i++)
        {
            Entity entity = entities[i];
            Vector3 screenPos = cam.WorldToViewportPoint(entity.transform.position);

            
            // check if its outside camera view
            if (screenPos.z < 0f || screenPos.x < popout || screenPos.x > 1f || screenPos.y < 0f || screenPos.y > 1f)
            {
                entities[i].renderer.enabled = false;
                                               
                Vector3 pos = entities[i].transform.position;
                pos.x = GetSpawnX();

                entities[i].transform.position = pos;
            }
            else
            {
                entities[i].renderer.enabled = true;

            }
        }
    }

    private void OnDrawGizmos()
    {
        float offsetx = floor.transform.position.x - 1.5f;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(offsetx, 0f, 0f), .75f);
        
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(worldCentre, .75f);
    }
}