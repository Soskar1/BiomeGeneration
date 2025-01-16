using UnityEngine;

namespace Core.BiomeGeneration
{
    [RequireComponent(typeof(MeshFilter))]
    public class ChunkRenderer : MonoBehaviour
    {
        private MeshFilter m_meshFilter;
        private MeshRenderer m_renderer;

        private void Awake()
        {
            m_meshFilter = GetComponent<MeshFilter>();
            m_renderer = GetComponent<MeshRenderer>();
        }

        public void Render(in ChunkData chunkData)
        {
            Mesh mesh = new Mesh();
            MeshData meshData = chunkData.MeshData;
            mesh.vertices = meshData.Vertices.ToArray();
            mesh.triangles = meshData.Indices.ToArray();
            mesh.RecalculateNormals();

            Texture2D texture = new Texture2D(chunkData.ChunkSize, chunkData.ChunkSize);
            texture.SetPixels(meshData.Colors.ToArray());
            texture.Apply();

            m_renderer.material.mainTexture = texture;
            m_meshFilter.mesh = mesh;
        }
    }
}