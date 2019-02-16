using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;
using Debug = System.Diagnostics.Debug;


public class MoveFloorSystem : ComponentSystem
{


    [BurstCompile]
    struct ScrollFloorJob : IJobProcessComponentData<Position, FloorTag>
    {
        public float3 offsetPosition;
        public float dt;


        public void Execute(ref Position pos, [ReadOnly] ref FloorTag tag)
        {

        }
    }
    

    //private TransformAccessArray floorTiles = new TransformAccessArray(3);
    private EntityManager entityManager;
    private ComponentGroup floorGroup;

    private float3 offsetSpawnPosition;


    protected override void OnCreateManager()
    {



        entityManager = World.Active.GetOrCreateManager<EntityManager>();
    }



    protected override void OnUpdate()
    {
        offsetSpawnPosition = FloorSpawnerSystem.offsetSpawnPosition;
        UnityEngine.Debug.Log(offsetSpawnPosition);

        ScrollFloorJob scrollFloorJob = new ScrollFloorJob
        {
            offsetPosition = offsetSpawnPosition,
            dt = Time.deltaTime
        };
        
        JobHandle scrollFloorJobHandle = scrollFloorJob.Schedule(this);
        scrollFloorJobHandle.Complete();
        
        return;
    }
}