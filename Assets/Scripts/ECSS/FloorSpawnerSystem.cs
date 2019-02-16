using Unity.Mathematics;
using UnityEngine;


public class FloorSpawnerSystem : MonoBehaviour
{
    public static float3 offsetSpawnPosition;

    private void Awake()
    {
        GameObject defaultFloor = GameObject.FindGameObjectWithTag("Floor");
        // GameObject defaultFloor = Object.FindObjectOfType(typeof(FloorTag)) as GameObject;

        float offsetZ = defaultFloor.transform.position.z + defaultFloor.GetComponent<MeshRenderer>().bounds.size.z;
        Vector3 position = new Vector3(defaultFloor.transform.position.x, defaultFloor.transform.position.y, offsetZ);

        var fwd = GameObject.Instantiate(defaultFloor, position, Quaternion.identity);
        var back = GameObject.Instantiate(defaultFloor, -position, Quaternion.identity);

        offsetSpawnPosition = new float3(fwd.transform.position.x, fwd.transform.position.y, fwd.transform.position.z);
    }


}