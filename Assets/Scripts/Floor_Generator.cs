using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Generator : MonoBehaviour
{
    [SerializeField] private Vector3 m_spawn_position = Vector3.zero;
    [SerializeField] private Vector3 m_position_change = Vector3.zero;
    [SerializeField] private Vector3 m_camera_checkpoint = Vector3.zero;

    [SerializeField] private List<GameObject> m_floor_objects;
    [SerializeField] private List<GameObject> m_floor;

    [SerializeField] private GameObject m_parent;

    [SerializeField] private int m_last_floor_index = 0;
    [SerializeField] private int m_floor_index = 0;
    [SerializeField] private int m_floor_max_index = 0;

    [SerializeField] private int m_max_spawn_count = 100;

    [SerializeField] private bool m_reverse = false;



    
    // Use this for initialization
    void Start ()
    {
        if (FloorObjectExists())
            PositionChangeInit();

        m_camera_checkpoint = Camera.main.transform.position;
        
        for (int i = 0; i < m_max_spawn_count; i++)
        {
            m_floor.Add(SpawnFloor());
        }
	}



    void PositionChangeInit()
    {
        MeshRenderer mesh_renderer = m_floor_objects[m_floor_index].GetComponent<MeshRenderer>();

        m_position_change = Vector3.Scale(m_floor_objects[m_floor_index].transform.localScale, mesh_renderer.bounds.size);

        if (!m_reverse)
            m_position_change = new Vector3(0.0f, 0.0f, m_position_change.z);
        else
            m_position_change = new Vector3(0.0f, 0.0f, -m_position_change.z);
    }



    // Update is called once per frame
    void Update ()
    { 
        UpdateFloor();
	}



    bool FloorObjectExists()
    {
        m_floor_max_index = m_floor_objects.Count;
        
        if (m_floor_max_index <= 0)
            return false;

        return true;
    }



    GameObject SpawnFloor()
    {
        m_spawn_position += m_position_change;

        GameObject value = Instantiate(m_floor_objects[m_floor_index], m_spawn_position, Quaternion.identity, m_parent.transform);

        m_floor_index++;
        if (m_floor_index >= m_floor_max_index)
            m_floor_index = 0;

        return value;
    }



    void UpdateFloor()
    {
        if (!CanFloorMove())
            return;

        m_spawn_position += m_position_change;
        m_floor[m_last_floor_index].transform.position = m_spawn_position;


        m_last_floor_index++;
        if (m_last_floor_index >= m_max_spawn_count)
            m_last_floor_index = 0;

        m_camera_checkpoint = Camera.main.transform.position;

    }


    
    bool CanFloorMove()
    {
        Vector3 difference = Camera.main.transform.position - m_camera_checkpoint;

        if (difference.z > 0.0f)
            if (m_position_change.z <= difference.z)
                return true;
        if (difference.z < 0.0f)
            if (m_position_change.z >= difference.z)
                return true;

        return false;
    }
}
