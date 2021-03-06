﻿using System;
using System.Collections.Generic;
using THREE;
using THREE.Core;
using THREE.Geometries;
using THREE.Lights;
using THREE.Materials;
using THREE.Math;
using THREE.Objects;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var scene = new Scene
            {
                Background = new  Color(255,0,255).ToInt(),
                Name = "My Scene"
            };

            var verts = new List<float[]>
            {
                new float[] { 0, 0, 0 },
                new float[] { 0, 0, 10.1234f },
                new float[] { 10, 0, 10 },
                new float[] { 10, 0, 0 }
            };

            var norms = new List<float[]>
            {
                new float[] { 0, 1, 0 },
                new float[] { 0, 1, 0 },
                new float[] { 0, 1, 0 },
                new float[] { 0, 1, 0 }
            };

            var vertices = Geometry.ProcessVertexArray(verts);

            var normals = Geometry.ProcessNormalArray(norms);

            var face = new int[] { 0, 1, 2, 3 };

            var faces = Geometry.ProcessFaceArray(new List<int[]> { { face } }, false, false);

            var geometry = new Geometry(vertices, faces, normals);
            var geometry2 = new Geometry(vertices, faces, normals);
            var material = MeshStandardMaterial.Default();

            var mesh = new Mesh
            {
                Geometry = geometry,
                Material = material,
                Name = "My Mesh"
            };

            scene.Add(mesh);

            var material2 = MeshStandardMaterial.Default();
            material2.Roughness = 0.25;

            var mesh2 = new Mesh
            {
                Geometry = geometry,
                Material = material2,
                Position = new Vector3(20,20,20),
                Name = "My Mesh2"
            };

            scene.Add(mesh2);

            var material3 = MeshStandardMaterial.Default();

            var mesh3 = new Mesh
            {
                Geometry = geometry2,
                Material = material3,
                Position = new Vector3(30, 30, 30),
                Name = "My Mesh3"
            };

            scene.Add(mesh3);

            var line = new Line
            {
                Geometry = new Geometry(vertices),
                Material = new LineBasicMaterial { Color = new Color(255,0,0).ToInt(), LineWidth = 20 },
                Name = "My Curves"
            };

            scene.Add(line);

            var points = new Points
            {
                Geometry = new Geometry(vertices),
                Material = new PointsMaterial { Color = new Color(255, 255, 255).ToInt() },
                Name = "My Points"
            };

            scene.Add(points);

            var group = new Group();

            group.Add(mesh3);
            group.Add(mesh2);
            group.Add(mesh);

            scene.Add(group);

            var group2 = new Group();

            group2.Add(mesh3);
            group2.Add(mesh2);
            group2.Add(mesh);

            scene.Add(group2);

            var sphereGeometry = new SphereGeometry
            {
                Radius = 10,
                WidthSegments = 10,
                HeightSegments = 5,
                PhiStart = 0,
                PhiLength = (float)Math.PI*2,
                ThetaStart = 0,
                ThetaLength = (float)Math.PI * 2
            };

            var sphereMesh = new Mesh
            {
                Geometry = sphereGeometry,
                Material = material,
                Position = new Vector3(-45,10,45),
                Name = "My Sphere"
            };

            scene.Add(sphereMesh);

            #region Lights

            var pointLight = new PointLight
            {
                Color = new Color(100, 100, 100).ToInt(),
                Decay = 1,
                Intensity = 3,
                Name = "My PointLight",
                Position = new Vector3(10, 10, 10)
            };

            scene.Add(pointLight);

            var ambientLight = new AmbientLight
            {
                Color = new Color(255, 0, 255).ToInt(),
                Intensity = 5,
                Name = "My AmbientLight"
            };

            scene.Add(ambientLight);

            var directionalLight = new DirectionalLight
            {
                Target = new Object3D { Position = new Vector3(3, 0, 0) },
                Position = new Vector3(-10,10,5),
                Name = "My DirectionalLight"
            };

            scene.Add(directionalLight);

            var spotLight = new SpotLight
            {
                Target = new Object3D { Position = new Vector3(3, 0, 3) },
                Position = new Vector3(20,20,0),
                Name = "My SpotLight"
            };

            scene.Add(spotLight);

            var hemiLight = new HemisphereLight
            {
                SkyColor = new Color(0,30,255).ToInt(),
                GroundColor = new Color(30,30,30).ToInt(),
                Name = "My HemisphereLight"
            };

            scene.Add(hemiLight);

            #endregion

            //Console.WriteLine(geometry.ToJSON(true));

            Console.WriteLine(scene.ToJSON(false));

            Console.ReadLine();

        }
    }
}
