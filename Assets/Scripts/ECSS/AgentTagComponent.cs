using Unity.Entities;

[System.Serializable]
public struct AgentTag : IComponentData { }

[UnityEngine.DisallowMultipleComponent]
public class AgentTagComponent : ComponentDataWrapper<AgentTag> { }



