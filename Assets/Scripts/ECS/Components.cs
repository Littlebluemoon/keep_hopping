using Unity.Entities;

public struct DistanceToOrigin : IComponentData { public float Value; }
public struct Direction : IComponentData { public float Value; }
// If only I could get random velocity within a job...
public struct Velocity : IComponentData { public float Value; }