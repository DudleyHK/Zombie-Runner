using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor;

using Random = Unity.Mathematics.Random;


public class AgentSpawner : MonoBehaviour
{
    // spawn inside unity sphere. // Put inside components.
    [SerializeField] private float3 spawnOrigin;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Rigidbody[] agentsRB;
    [SerializeField] private Transform[] agents;
    [SerializeField] private ushort maxAgents;
    [SerializeField] private half maxt = .1f; // maxtime

    private GameObject defaultAgent;
    private half tt; // time
    private Vector3 spawnPosOffset;


    private void Start()
    {
        agents = new Transform[maxAgents];

        defaultAgent = GameObject.Find("DefaultAgent");
       

        for(int i = 0; i < maxAgents; i++)
        {
            var agent = Instantiate(defaultAgent, spawnPosOffset, Quaternion.identity);
            agent.name = "zombie id " + i;

            agents[i] = agent.transform;

            //agent.SetActive(false);
        } 
        Destroy(defaultAgent);
        StartCoroutine(Spawn(maxt));
    }


    private IEnumerator Spawn(float _t)
    {
        for(int i = 0; i < maxAgents; i++)
        {
            yield return new WaitForSeconds(_t);

            // random position
            Vector2 rpos_ = UnityEngine.Random.insideUnitCircle * spawnRadius;
            float2 rpos = spawnOrigin.xz + new float2(rpos_.x, rpos_.y);

            // random velocity
            Vector2 rvel = Vector2.up * UnityEngine.Random.Range(0f, 1f);

            var position = agents[i].position;
            position.x = rpos.x;
            position.z = rpos.y;

            agents[i].position = position;

            //agentsRB[i].velocity = rvel;
            //
            // agentsRB[i].gameObject.SetActive(true);
        }

        yield return true;
    }


    private void Update()
    {
        if(tt >= maxt)
        {
            tt = 0f;





        }

        tt += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnOrigin, spawnRadius);
    }

}
