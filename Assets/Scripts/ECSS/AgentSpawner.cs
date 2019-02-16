using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using Random = Unity.Mathematics.Random;


public class AgentSpawner : MonoBehaviour
{
    // spawn inside unity sphere. // Put inside components.
    [SerializeField] private float3 spawnOrigin;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Rigidbody[] agentsRB;
    [SerializeField] private ushort maxAgents;

    private GameObject defaultAgent;
    private half tt; // time
    private half maxt; // maxtime
    private Vector3 spawnPosOffset;


    private void Start()
    {
        agentsRB = new Rigidbody[maxAgents];

        defaultAgent = GameObject.Find("DefualtAgent");

        for (int i = 0; i < maxAgents; i++)
        {
            var agent  = GameObject.Instantiate(defaultAgent, spawnPosOffset, Quaternion.identity);
            agent.name = "zombie id " + i;

            agentsRB[i] = agent.GetComponent<Rigidbody>();
        }
    }



    private void Update()
    {
        if(tt >= maxt)
        {
            tt = 0f;

            // random position
            Vector2 rpos_ = UnityEngine.Random.insideUnitCircle * spawnRadius;
            float2 rpos = spawnOrigin.xz + new float2(rpos_.x, rpos_.y);

            // random velocity
            Vector2 rvel = Vector2.up * UnityEngine.Random.Range(0f, 1f);

            

        }

        tt += Time.deltaTime;
    }


}
