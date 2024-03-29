using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
// using System;

[BurstCompile]
public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle input)
    {
        var output = Entities.ForEach((ref Translation translation, ref Direction direction, in DistanceToOrigin distance, in Velocity velocity) =>
        {
            // Upper bound of movement
            if (translation.Value.y >= 2.5)
            {
                direction.Value = -direction.Value;
                translation.Value.y = 2.5f;
            }
            // Lower bound of movement
            else if (translation.Value.y <= -1.5)
            {
                direction.Value = -direction.Value;
                translation.Value.y = -1.5f;
            }
            // 0.01666... = 1/60
            // Multiple to achieve a reasonable velocity
            // Also 0.016666666666666666666666666f is actually 0.01666666753590106964111328125f
            translation.Value.y += velocity.Value * direction.Value * 0.01666666753590106964111328125f;
        }).Schedule(input);

        return output;
    }
}
