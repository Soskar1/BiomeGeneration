using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkData
    {
        public MeshData MeshData { get; private set; }

        public ChunkData(in int chunkSize)
        {
            MeshData = new MeshData();
        }
    }
}