using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkData
    {
        public MeshData MeshData { get; private set; }
        public Vector3Int WorldPosition { get; private set;}

        public ChunkData(MeshData meshData, Vector3Int worldPosition)
        {
            MeshData = meshData;
            WorldPosition = worldPosition;
        }
    }
}