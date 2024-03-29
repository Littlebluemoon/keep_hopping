using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class EntitySpawner : Spawner
{
    public override void Initialize(int width, int height, Mesh entityMesh, Material entityMaterial)
    {
        float startX = width / 2 * -1;
        float startZ = height / 2 * -1;

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archetype = entityManager.CreateArchetype(typeof(Translation), 
                                                                  typeof(RenderMesh), 
                                                                  typeof(LocalToWorld),
                                                                  typeof(RenderBounds), 
                                                                  typeof(WorldRenderBounds),
                                                                  typeof(DistanceToOrigin),
                                                                  typeof(Direction),
                                                                  typeof(Velocity));

        NativeArray<Entity> array = new NativeArray<Entity>(width * height, Allocator.Temp, NativeArrayOptions.ClearMemory);

        entityManager.CreateEntity(archetype, array);

        int indexer = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xPosition = startX + (i + 1) * 1;
                float zPosition = startZ + (j + 1) * 1;

                float magnitude = Mathf.Sqrt(xPosition * xPosition + zPosition * zPosition);

                Entity entity = array[indexer];
                entityManager.SetComponentData(entity, new Translation { Value = new Unity.Mathematics.float3(xPosition, 0, zPosition) });
                entityManager.SetComponentData(entity, new DistanceToOrigin { Value = magnitude });
                entityManager.SetComponentData(entity, new Direction { Value = 1f });
                entityManager.SetComponentData(entity, new Velocity { Value = Random.Range(0f, 1f) });
                entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = entityMesh, material = entityMaterial });

                indexer++;
            }
        }

        array.Dispose();
    }
}
