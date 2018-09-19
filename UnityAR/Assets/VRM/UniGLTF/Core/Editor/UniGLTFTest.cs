﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;


public class UniGLTFTest
{
    static GameObject CreateSimpelScene()
    {
        var root = new GameObject("gltfRoot").transform;

        var scene = new GameObject("scene0").transform;
        scene.SetParent(root, false);
        scene.localPosition = new Vector3(1, 2, 3);

        return root.gameObject;
    }

    void AssertAreEqual(Transform go, Transform other)
    {
        var lt = go.Traverse().GetEnumerator();
        var rt = go.Traverse().GetEnumerator();

        while (lt.MoveNext())
        {
            if (!rt.MoveNext())
            {
                throw new Exception("rt shorter");
            }

            MonoBehaviourComparator.AssertAreEquals(lt.Current.gameObject, rt.Current.gameObject);
        }

        if (rt.MoveNext())
        {
            throw new Exception("rt longer");
        }
    }

    [Test]
    public void UniGLTFSimpleSceneTest()
    {
        var go = CreateSimpelScene();
        var context = new ImporterContext();

        try
        {
            // export
            var gltf = new glTF();
            using (var exporter = new gltfExporter(gltf))
            {
                exporter.Prepare(go);
                exporter.Export();

                // import
                context.ParseJson<glTF>(gltf.ToJson(), new SimpleStorage(new ArraySegment<byte>()));
                Debug.LogFormat("{0}", context.Json);
                gltfImporter.Import<glTF>(context);

                AssertAreEqual(go.transform, context.Root.transform);
            }
        }
        finally
        {
            //Debug.LogFormat("Destory, {0}", go.name);
            GameObject.DestroyImmediate(go);
            context.Destroy(true);
        }
    }

    void BufferTest(int init, params int[] size)
    {
        var initBytes = init == 0 ? null : new byte[init];
        var storage = new ArrayByteBuffer(initBytes);
        var buffer = new glTFBuffer(storage);

        var values = new List<byte>();
        int offset = 0;
        foreach(var x in size)
        {
            var nums = Enumerable.Range(offset, x).Select(y => (Byte)y).ToArray();
            values.AddRange(nums);
            var bytes = new ArraySegment<Byte>(nums);
            offset += x;
            buffer.Append(bytes, glBufferTarget.NONE);
        }

        Assert.AreEqual(values.Count, buffer.byteLength);
        Assert.True(Enumerable.SequenceEqual(values, buffer.GetBytes().ToArray()));
    }

    [Test]
    public void BufferTest()
    {
        BufferTest(0, 0, 100, 200);
        BufferTest(0, 128);
        BufferTest(0, 256);

        BufferTest(1024, 0);
        BufferTest(1024, 128);
        BufferTest(1024, 2048);
        BufferTest(1024, 900, 900);
    }
}
