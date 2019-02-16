using Unity.Entities;

[System.Serializable]
public struct FloorTag : IComponentData { }

[UnityEngine.DisallowMultipleComponent]
public class FloorTagComponent : ComponentDataWrapper<FloorTag> { }



