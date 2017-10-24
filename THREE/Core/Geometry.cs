﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisLib
{
    /// <summary>
    /// Base class for all geometries. 
    /// Analogous to https://threejs.org/docs/index.html#api/core/Geometry
    /// Design based on need for Three.js Loaders.
    /// </summary>
    public class Geometry : Object3D, IGeometry
    {
        /// <summary>
        /// Geometry data.
        /// </summary>
        [JsonProperty("data")]
        GeometryData Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<float> Vertices
        {
            get { return Data.Vertices; }
            set {  Data.Vertices = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<int> Colors
        {
            get { return Data.Colors; }
            set { Data.Colors = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<int> Faces
        {
            get { return Data.Faces; }
            set { Data.Faces = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<object> Normals
        {
            get { return Data.Normals; }
            set { Data.Normals = value; }
        }

        /// <summary>
        /// The list of UVs associated with this geometry.
        /// </summary>
        [JsonIgnore]
        public List<List<float>> Uvs
        {
            get { return Data.Uvs; }
            set { Data.Uvs = value; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Geometry()
        {
            Type = "Geometry";
            Data = new GeometryData();
        }

        /// <summary>
        /// Constructor with default values = null.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="faces"></param>
        /// <param name="normals"></param>
        /// <param name="colors"></param>
        /// <param name="uvs"></param>
        public Geometry(List<float> vertices = null, List<int> faces = null,  List<object> normals = null, List<int> colors = null, List<List<float>> uvs = null ) :this()
        {

            if (vertices == null) return;
            Vertices = vertices;

            if(normals != null && normals.Count > 0)
                Normals = normals;

            if (colors != null && colors.Count > 0)
                Colors = colors;

            if (uvs != null && uvs.Count > 0)
                Uvs = uvs;

            if (faces != null)
                Faces = faces;
        }

        /// <summary>
        /// Utility method for processing faces.
        /// TODO: Extend for all types of faces and switches.
        /// </summary>
        /// <param name="faces"></param>
        /// <param name="vertexColors"></param>
        /// <param name="uvs"></param>
        /// <returns>A list of int.</returns>
        public static List<int> ProcessFaceArray(List<int[]> faces, bool vertexColors, bool uvs)
        {

            var face = new GeometryFace
            {
                Topology = false,
                VertexColors = vertexColors,
                FaceColor = false,
                FaceMaterial = false,
                FaceNormals = false,
                FaceUVs = false,
                FaceVertexUVs = uvs,
                VertexNormals = true
            };

            List<int> facesIndex = new List<int>();

            if (faces != null)
                foreach (var meshFace in faces)
                {
                    if (meshFace.Length == 3)//has count 3
                    {

                        face.Topology = false;

                        facesIndex.Add(face.GetFaceType());

                        facesIndex.Add(meshFace[0]); //A
                        facesIndex.Add(meshFace[1]); //B
                        facesIndex.Add(meshFace[2]); //C

                        if (face.VertexNormals)
                        {
                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                        }

                        if (face.VertexColors)
                        {
                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                        }

                        if (face.FaceVertexUVs)
                        {
                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                        }
                    }
                    else
                    {

                        face.Topology = true;

                        facesIndex.Add(face.GetFaceType());

                        facesIndex.Add(meshFace[0]); //A
                        facesIndex.Add(meshFace[1]); //B
                        facesIndex.Add(meshFace[2]); //C
                        facesIndex.Add(meshFace[3]); //D

                        if (face.VertexNormals)
                        {

                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                            facesIndex.Add(meshFace[3]); //D
                        }

                        if (face.VertexColors)
                        {
                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                            facesIndex.Add(meshFace[3]); //D
                        }

                        if (face.FaceVertexUVs)
                        {
                            facesIndex.Add(meshFace[0]); //A
                            facesIndex.Add(meshFace[1]); //B
                            facesIndex.Add(meshFace[2]); //C
                            facesIndex.Add(meshFace[3]); //D
                        }
                    }

                }

            return facesIndex;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static List<float> ProcessVertexArray(List<float[]> vertices)
        {
            var Vertices = new List<float>();

            foreach (var vert in vertices)
            {
                Vertices.Add(vert[0]);
                Vertices.Add(vert[1]);
                Vertices.Add(vert[2]);
            }

            return Vertices;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    internal class GeometryData
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("vertices")]
        internal List<float> Vertices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("colors")]
        internal List<int> Colors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("faces")]
        internal List<int> Faces { get; set; }

        /// <summary>
        /// The list of UVs associated with this geometry.
        /// </summary>
        [JsonProperty("uvs")]
        public List<List<float>> Uvs { get; set; }

        /// <summary>
        /// The list of normals associated with this geometry.
        /// </summary>
        [JsonProperty("normals")]
        internal List<object> Normals { get; set; }

        internal GeometryData()
        {
            Vertices = new List<float>();
            Colors = new List<int>();
            Faces = new List<int>();
            Normals = new List<object>();
            Uvs = new List<List<float>>();
        }
    }

    /// <summary>
    /// Class for storing geometry face data.
    /// </summary>
    internal class GeometryFace
    {
        /// <summary>
        /// False for triangle, true for quad.
        /// </summary>
        internal bool Topology { get; set; } //false for triangle, true for quad

        /// <summary>
        /// 
        /// </summary>
        internal bool FaceMaterial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool FaceUVs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool FaceVertexUVs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool FaceNormals { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool VertexNormals { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool FaceColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal bool VertexColors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal byte GetFaceType()
        {
            bool[] faceBits = new bool[] { Topology, FaceMaterial, FaceUVs, FaceVertexUVs,
                                           FaceNormals, VertexNormals, FaceColor, VertexColors };
            System.Collections.BitArray bits = new System.Collections.BitArray(faceBits);

            byte b = 0;
            if (bits.Get(0)) b++;
            if (bits.Get(1)) b += 2;
            if (bits.Get(2)) b += 4;
            if (bits.Get(3)) b += 8;
            if (bits.Get(4)) b += 16;
            if (bits.Get(5)) b += 32;
            if (bits.Get(6)) b += 64;
            if (bits.Get(7)) b += 128;
            return b;
        }
    }
}