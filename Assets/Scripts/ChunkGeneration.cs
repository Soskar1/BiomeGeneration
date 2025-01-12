using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkGeneration
    {
        public MeshData Generate(in Vector3 position, in int chunkSize)
        {
            MeshData meshData = new MeshData();

            for (int x = 0; x < chunkSize; ++x)
            {
                for (int y = 0; y < chunkSize; ++y)
                {
                    Vector3 localPosition = new Vector3(x, 0, y);
                    meshData.AddQuad(localPosition);
                }
            }

            return meshData;
        }
    }
}