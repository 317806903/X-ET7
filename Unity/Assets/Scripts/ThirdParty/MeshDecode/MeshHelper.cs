using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace ET
{

    /// <summary>
    /// Helper class to deal with draco bytes, textured mesh and nav mesh with native codes.
    /// </summary>
    public static class MeshHelper
    {
        private class ExternalApi
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            private const string LIB_NAME = "libmesh_utils";
#else
            // Linux editor and others.
            private const string LIB_NAME = "libmesh_utils";
#endif

            [DllImport(LIB_NAME)]
            public static extern bool BytesToMesh(
                IntPtr objData, int objDataSize,
                int meshType,
                ref IntPtr verticePositiondata,
                ref IntPtr verticeNormalData,
                ref IntPtr verticeUVData,
                ref IntPtr verticeColorData,
                ref int verticeNum,
                ref IntPtr faceIndexData, ref int faceIndexNum
            );

            [DllImport(LIB_NAME)]
            public static extern void Free(IntPtr ptr);
        }

        public struct MeshData
        {
            public float3[] vertices;
            public float3[] verticesOrg;
            public float3[] normals;
            public float2[] uv;
            public int[] triangles;
            public int[] trianglesOrg;
            public byte[] texturePng;

            public bool parsed;
        }

        /// <summary>
        /// Mesh <c>GetMeshFromBytes</c> Generate a Unity Mesh from bytes array.
        /// The Mesh may consists of some attributes such as Position, Normal, Texture, Color.
        /// </summary>
        /// <param name="meshBytes">byte array with either Obj format or Draco format</param>
        /// <returns>return a MeshData struct holds all parsed data for mesh generation later.</returns>
        public static MeshData GetMeshDataFromBytes(byte[] meshBytes)
        {
            MeshData meshData = new MeshData();

            if (meshBytes == null || meshBytes.Length == 0)
            {
                meshData.parsed = false;
                return meshData;
            }

            IntPtr meshDataPtr = Marshal.AllocHGlobal(meshBytes.Length);
            int meshDataLength = meshBytes.Length;
            Marshal.Copy(meshBytes, 0, meshDataPtr, meshDataLength);

            IntPtr verticePositionPtr = IntPtr.Zero;
            IntPtr verticeNormalPtr = IntPtr.Zero;
            IntPtr verticeUVPtr = IntPtr.Zero;
            IntPtr verticeColorPtr = IntPtr.Zero;
            IntPtr faceIndexPtr = IntPtr.Zero;
            int verticeNum = 0;
            int faceNum = 0;

            bool status = ExternalApi.BytesToMesh(
                meshDataPtr, meshDataLength, 1,
                ref verticePositionPtr,
                ref verticeNormalPtr,
                ref verticeUVPtr,
                ref verticeColorPtr,
                ref verticeNum,
                ref faceIndexPtr, ref faceNum
            );

            if (!status)
            {
                meshData.parsed = false;
                return meshData;
            }
            // There should be always true if status is true
            if (verticePositionPtr != IntPtr.Zero)
            {
                byte[] vertexPositionBytes = new byte[verticeNum * 3 * 4];
                Marshal.Copy(verticePositionPtr, vertexPositionBytes, 0, verticeNum * 3 * 4);
                ExternalApi.Free(verticePositionPtr);
                float3[] tempArr = new float3[verticeNum];
                float3[] tempArrOrg = new float3[verticeNum];
                for (int i = 0; i < verticeNum; i++)
                {
                    float x = BitConverter.ToSingle(vertexPositionBytes, 3 * 4 * i + 0);
                    float y = BitConverter.ToSingle(vertexPositionBytes, 3 * 4 * i + 4);
                    float z = BitConverter.ToSingle(vertexPositionBytes, 3 * 4 * i + 8);
                    // reverse Z axis to make right-handed to left handed for Unity.
                    tempArr[i] = new float3(x, y, -z);
                    tempArrOrg[i] = new float3(-x, y, -z);
                }
                meshData.vertices = tempArr;
                meshData.verticesOrg = tempArrOrg;
            }
            if (verticeNormalPtr != IntPtr.Zero)
            {
                byte[] verticeNormalBytes = new byte[verticeNum * 3 * 4];
                Marshal.Copy(verticeNormalPtr, verticeNormalBytes, 0, verticeNum * 3 * 4);
                ExternalApi.Free(verticeNormalPtr);

                float3[] tempArr = new float3[verticeNum];
                for (int i = 0; i < verticeNum; i++)
                {
                    float x = BitConverter.ToSingle(verticeNormalBytes, 3 * 4 * i + 0);
                    float y = BitConverter.ToSingle(verticeNormalBytes, 3 * 4 * i + 4);
                    float z = BitConverter.ToSingle(verticeNormalBytes, 3 * 4 * i + 8);
                    tempArr[i] = new float3(x, y, z);
                }
                meshData.normals = tempArr;
            }
            if (verticeUVPtr != IntPtr.Zero)
            {
                byte[] verticeUVBytes = new byte[verticeNum * 2 * 4];
                Marshal.Copy(verticeUVPtr, verticeUVBytes, 0, verticeNum * 2 * 4);
                ExternalApi.Free(verticeUVPtr);

                float2[] tempArr = new float2[verticeNum];
                for (int i = 0; i < verticeNum; i++)
                {
                    float u = BitConverter.ToSingle(verticeUVBytes, 2 * 4 * i + 0);
                    float v = BitConverter.ToSingle(verticeUVBytes, 2 * 4 * i + 4);
                    tempArr[i] = new float2(u, v);
                }
                meshData.uv = tempArr;
            }
            if (verticeColorPtr != IntPtr.Zero)
            {
                byte[] verticeColorBytes = new byte[verticeNum * 3 * 4];
                Marshal.Copy(verticeColorPtr, verticeColorBytes, 0, verticeNum * 3 * 4);
                ExternalApi.Free(verticeColorPtr);

            }

            byte[] faceIndexBytes = new byte[faceNum * 3 * 4];
            Marshal.Copy(faceIndexPtr, faceIndexBytes, 0, faceNum * 3 * 4);
            ExternalApi.Free(faceIndexPtr);

            int[] tempFace = new int[faceNum * 3];
            int[] tempFaceOrg = new int[faceNum * 3];
            for (int i = 0; i < faceNum; i++)
            {
                int index1 = BitConverter.ToInt32(faceIndexBytes, 12 * i + 0);
                int index2 = BitConverter.ToInt32(faceIndexBytes, 12 * i + 4);
                int index3 = BitConverter.ToInt32(faceIndexBytes, 12 * i + 8);
                // Reverse the order so triangle is facing out.
                tempFace[3 * i + 0] = index1;
                tempFace[3 * i + 1] = index3;
                tempFace[3 * i + 2] = index2;
                tempFaceOrg[3 * i + 0] = index1;
                tempFaceOrg[3 * i + 1] = index2;
                tempFaceOrg[3 * i + 2] = index3;
            }
            meshData.triangles = tempFace;
            meshData.trianglesOrg = tempFaceOrg;
            meshData.parsed = true;
            return meshData;
        }

        public static void ParseObj(byte[] objBytes, ref MeshData meshData)
        {
            string[] lines = System.Text.Encoding.UTF8.GetString(objBytes).Split('\n');

            List<float3> v = new List<float3>();
            List<float3> vn = new List<float3>();
            List<float2> vt = new List<float2>();
            List<int[]> f = new List<int[]>();

            foreach (string line in lines)
            {
                if (line == "" || line.StartsWith("#"))
                    continue;

                string[] token = line.Split(' ');
                switch (token[0])
                {
                    case ("v"):
                        v.Add(new float3(
                            float.Parse(token[1]),
                            float.Parse(token[2]),
                            float.Parse(token[3])));
                        break;
                    case ("vn"):
                        vn.Add(new float3(
                            float.Parse(token[1]),
                            float.Parse(token[2]),
                            float.Parse(token[3])));
                        break;
                    case ("vt"):
                        vt.Add(new float2(
                            float.Parse(token[1]),
                            float.Parse(token[2])));
                        break;
                    case ("f"):
                        for (int i = 1; i < 4; i += 1)
                        {
                            int[] triplet = Array.ConvertAll(token[i].Split('/'), value => {
                                if (String.IsNullOrEmpty(value))
                                    return 0;
                                return int.Parse(value) - 1;
                            });
                            f.Add(triplet);
                        }
                        break;
                }
            }

            float3[] vertices = new float3[f.Count];
            float3[] normals = new float3[f.Count];
            float2[] uvs = new float2[f.Count];
            int[] triangles = new int[f.Count];
            for (int i = 0; i < f.Count; i += 1)
            {
                vertices[i] = v[f[i][0]];
                vertices[i].z = -vertices[i].z;  // flip Z to left-handed.
                normals[i] = vn[f[i][2]];
                uvs[i] = vt[f[i][1]];
            }

            int faceNum = f.Count / 3;
            for (int j = 0; j < faceNum; j++)
            {
                // Reverse triangles to let normal face out after Z is flipped.
                triangles[j * 3 + 0] = j * 3 + 0;
                triangles[j * 3 + 1] = j * 3 + 2;
                triangles[j * 3 + 2] = j * 3 + 1;
            }

            meshData.vertices = vertices;
            meshData.uv = uvs;
            meshData.triangles = triangles;
            meshData.parsed = true;
        }
    }
}
