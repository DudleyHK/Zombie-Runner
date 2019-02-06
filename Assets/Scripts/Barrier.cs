using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Floor_Generator m_floor_generator;
    [SerializeField] private string m_floor_tag = "Floor";



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(m_floor_tag))
            return;

        //m_floor_generator.DecreaseSpawnCount();
        //Destroy(other.gameObject);
    }
}
