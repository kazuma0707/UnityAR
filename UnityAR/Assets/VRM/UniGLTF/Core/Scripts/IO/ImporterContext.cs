﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UniGLTF.SimpleJSON;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace UniGLTF
{
    public class ImporterContext
    {
        #region Source
        String m_path;

        /// <summary>
        /// GLTF or GLB path
        /// </summary>
        public String Path
        {
            get { return m_path; }
            set
            {
                if (m_path == value) return;
                m_path = value;
            }
        }

        /// <summary>
        /// JSON source
        /// </summary>
        public String Json;

        /// <summary>
        /// GLTF parsed from JSON
        /// </summary>
        public glTF GLTF; // parsed

        /// <summary>
        /// URI access
        /// </summary>
        public IStorage Storage;
        #endregion

        public void ParseGlb(Byte[] bytes)
        {
            ParseGlb<glTF>(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void ParseGlb<T>(Byte[] bytes) where T : glTF
        {
            var chunks = glbImporter.ParseGlbChanks(bytes);

            if (chunks.Count != 2)
            {
                throw new Exception("unknown chunk count: " + chunks.Count);
            }

            if (chunks[0].ChunkType != GlbChunkType.JSON)
            {
                throw new Exception("chunk 0 is not JSON");
            }

            if (chunks[1].ChunkType != GlbChunkType.BIN)
            {
                throw new Exception("chunk 1 is not BIN");
            }

            var jsonBytes = chunks[0].Bytes;
            ParseJson<T>(Encoding.UTF8.GetString(jsonBytes.Array, jsonBytes.Offset, jsonBytes.Count), 
                new SimpleStorage(chunks[1].Bytes));
        }

        public void ParseJson(string json, IStorage storage)
        {
            ParseJson<glTF>(json, storage);
        }

        public void ParseJson<T>(string json, IStorage storage) where T : glTF
        {
            Json = json;
            Storage = storage;

            GLTF = JsonUtility.FromJson<T>(Json);
            if (GLTF.asset.version != "2.0")
            {
                throw new UniGLTFException("unknown gltf version {0}", GLTF.asset.version);
            }

            // Version Compatibility
            RestoreOlderVersionValues();

            // parepare byte buffer
            GLTF.baseDir = System.IO.Path.GetDirectoryName(Path);
            foreach (var buffer in GLTF.buffers)
            {
                buffer.OpenStorage(GLTF.baseDir, storage);
            }
        }

        void RestoreOlderVersionValues()
        {
            var parsed = JSON.Parse(Json);
            for (int i = 0; i < GLTF.images.Count; ++i)
            {
                if (string.IsNullOrEmpty(GLTF.images[i].name))
                {
                    try
                    {
                        var extraName = parsed["images"][i]["extra"]["name"].Value;
                        if (!string.IsNullOrEmpty(extraName))
                        {
                            //Debug.LogFormat("restore texturename: {0}", extraName);
                            GLTF.images[i].name = extraName;
                        }
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                }
            }
            for (int i = 0; i < GLTF.meshes.Count; ++i)
            {
                var mesh = GLTF.meshes[i];
                try
                {
                    for (int j = 0; j < mesh.primitives.Count; ++j)
                    {
                        var primitive = mesh.primitives[j];
                        for (int k = 0; k < primitive.targets.Count; ++k)
                        {
                            var extraName = parsed["meshes"][i]["primitives"][j]["targets"][k]["extra"]["name"].Value;
                            //Debug.LogFormat("restore morphName: {0}", extraName);
                            primitive.extras.targetNames.Add(extraName);
                        }
                    }
                }
                catch (Exception)
                {
                    // do nothing
                }
            }
            for (int i = 0; i < GLTF.nodes.Count; ++i)
            {
                var node = GLTF.nodes[i];
                try
                {
                    var extra = parsed["nodes"][i]["extra"]["skinRootBone"].AsInt;
                    //Debug.LogFormat("restore extra: {0}", extra);
                    node.extras.skinRootBone = extra;
                }
                catch (Exception)
                {
                    // do nothing
                }
            }
        }

        public CreateMaterialFunc CreateMaterial;

        public bool HasVertexColor(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GLTF.materials.Count)
            {
                return false;
            }

            var hasVertexColor = GLTF.meshes.SelectMany(x => x.primitives).Any(x => x.material == materialIndex && x.HasVertexColor);
            return hasVertexColor;
        }
        #region Imported
        public GameObject Root;
        public List<Transform> Nodes = new List<Transform>();
        public List<TextureItem> Textures = new List<TextureItem>();
        public List<Material> Materials = new List<Material>();
        public List<MeshWithMaterials> Meshes = new List<MeshWithMaterials>();
        public void ShowMeshes()
        {
            foreach (var x in Meshes)
            {
                if (x.Renderer != null)
                {
                    x.Renderer.enabled = true;
                }
            }
        }
        public AnimationClip Animation;
        #endregion

#if UNITY_EDITOR
        #region PrefabPath
        string m_prefabPath;
        string PrefabPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_prefabPath))
                {
                    m_prefabPath = GetPrefabPath();
                }
                return m_prefabPath;
            }
        }
        protected virtual string GetPrefabPath()
        {
            var dir = System.IO.Path.GetDirectoryName(Path);
            var name = System.IO.Path.GetFileNameWithoutExtension(Path);
            var prefabPath = string.Format("{0}/{1}.prefab", dir, name);
#if false
            if (!Application.isPlaying && File.Exists(prefabPath))
            {
                // already exists
                if (IsOwn(prefabPath))
                {
                    //Debug.LogFormat("already exist. own: {0}", prefabPath);
                }
                else
                {
                    // but unknown prefab
                    var unique = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
                    //Debug.LogFormat("already exist: {0} => {1}", prefabPath, unique);
                    prefabPath = unique;
                }
            }
#endif
            return prefabPath.Replace("\\", "/");
        }
        public string GetAssetFolder(string suffix)
        {
            var path = String.Format("{0}/{1}{2}",
                System.IO.Path.GetDirectoryName(PrefabPath),
                System.IO.Path.GetFileNameWithoutExtension(PrefabPath),
                suffix
                )
                ;
            return path;
        }
        #endregion

        #region Assets
        IEnumerable<UnityEngine.Object> GetSubAssets(string path)
        {
            return AssetDatabase.LoadAllAssetsAtPath(path);
        }

        protected virtual bool IsOwn(string path)
        {
            foreach (var x in GetSubAssets(path))
            {
                //if (x is Transform) continue;
                if (x is GameObject) continue;
                if (x is Component) continue;
                if (AssetDatabase.IsSubAsset(x))
                {
                    return true;
                }
            }
            return false;
        }

        protected virtual IEnumerable<UnityEngine.Object> ObjectsForSubAsset()
        {
            HashSet<Texture2D> textures = new HashSet<Texture2D>();
            foreach (var x in Textures.SelectMany(y => y.GetTexturesForSaveAssets()))
            {
                if (!textures.Contains(x))
                {
                    textures.Add(x);
                }
            }
            foreach (var x in textures) { yield return x; }
            foreach (var x in Materials) { yield return x; }
            foreach (var x in Meshes) { yield return x.Mesh; }
            if (Animation != null) yield return Animation;
        }

        void EnsureFolder(string assetPath)
        {
            var fullPath = assetPath.AssetPathToFullPath();
            if (!Directory.Exists(fullPath))
            {
                AssetDatabase.CreateFolder(
                    System.IO.Path.GetDirectoryName(assetPath),
                    System.IO.Path.GetFileName(assetPath)
                    );
            }
        }

        public bool MeshAsSubAsset = false;

        public void SaveAsAsset()
        {
            ShowMeshes();

            var prefabPath = PrefabPath;
            if (File.Exists(prefabPath))
            {
                // clear SubAssets
                foreach (var x in GetSubAssets(prefabPath).Where(x => !(x is GameObject) && !(x is Component)))
                {
                    GameObject.DestroyImmediate(x, true);
                }
            }

            // Add SubAsset
            var materialDir = GetAssetFolder(".Materials");
            EnsureFolder(materialDir);
            var textureDir = GetAssetFolder(".Textures");
            EnsureFolder(textureDir);

            var meshDir = GetAssetFolder(".Meshes");
            if (!MeshAsSubAsset)
            {
                EnsureFolder(meshDir);
            }

            var paths = new List<string>(){
                prefabPath
            };
            foreach (var o in ObjectsForSubAsset())
            {
                if (o is Material)
                {
                    var materialPath = string.Format("{0}/{1}.asset",
                        materialDir,
                        o.name.EscapeFilePath()
                        );
                    AssetDatabase.CreateAsset(o, materialPath);
                    paths.Add(materialPath);
                }
                else if (o is Texture2D)
                {
                    var texturePath = string.Format("{0}/{1}.asset",
                        textureDir,
                        o.name.EscapeFilePath()
                        );
                    AssetDatabase.CreateAsset(o, texturePath);
                    paths.Add(texturePath);
                }
                else if (o is Mesh && !MeshAsSubAsset)
                {
                    var meshPath = string.Format("{0}/{1}.asset",
                        meshDir,
                        o.name.EscapeFilePath()
                        );
                    AssetDatabase.CreateAsset(o, meshPath);
                    paths.Add(meshPath);
                }
                else
                {
                    // save as subasset
                    AssetDatabase.AddObjectToAsset(o, prefabPath);
                }
            }

            // Create or upate Main Asset
            if (File.Exists(prefabPath))
            {
                Debug.LogFormat("replace prefab: {0}", prefabPath);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                PrefabUtility.ReplacePrefab(Root, prefab, ReplacePrefabOptions.ReplaceNameBased);
            }
            else
            {
                Debug.LogFormat("create prefab: {0}", prefabPath);
                PrefabUtility.CreatePrefab(prefabPath, Root);
            }
            foreach (var x in paths)
            {
                AssetDatabase.ImportAsset(x);
            }
        }
        #endregion
#endif

        public void Destroy(bool destroySubAssets)
        {
            if (Root != null) GameObject.DestroyImmediate(Root);
            if (destroySubAssets)
            {
#if UNITY_EDITOR
                foreach (var o in ObjectsForSubAsset())
                {
                    UnityEngine.Object.DestroyImmediate(o, true);
                }
#endif
            }
        }
    }
}
