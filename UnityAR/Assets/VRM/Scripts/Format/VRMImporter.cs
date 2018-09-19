﻿using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UniGLTF;
using System.Collections.Generic;
using System.Collections;
using UniTask;
#if (NET_4_6 && UNITY_2018_1_OR_NEWER)
using System.Threading.Tasks;
#endif


namespace VRM
{
    public static class VRMImporter
    {
        const string HUMANOID_KEY = "humanoid";
        const string MATERIAL_KEY = "materialProperties";

        public static GameObject LoadFromPath(string path)
        {
            var context = new VRMImporterContext(path);
            context.ParseVrm(File.ReadAllBytes(path));
            LoadFromBytes(context);
            return context.Root;
        }

        public static GameObject LoadFromBytes(Byte[] bytes)
        {
            var context = new VRMImporterContext();
            context.ParseVrm(bytes);
            LoadFromBytes(context);
            return context.Root;
        }

        public static void LoadFromPath(VRMImporterContext context)
        {
            context.ParseVrm(File.ReadAllBytes(context.Path));
            LoadFromBytes(context);
        }

        public static void LoadFromBytes(VRMImporterContext context)
        {
            context.CreateMaterial = VRMImporter.GetMaterialFunc(glTF_VRM_Material.Parse(context.Json));

            gltfImporter.Import<glTF_VRM>(context);
            if (string.IsNullOrEmpty(context.Path))
            {
                if (string.IsNullOrEmpty(context.VRM.extensions.VRM.meta.title))
                {
                    context.Root.name = "VRM_LOADED";
                }
                else
                {
                    context.Root.name = context.VRM.extensions.VRM.meta.title;
                }
            }
            else
            {
                context.Root.name = Path.GetFileNameWithoutExtension(context.Path);
            }

            OnLoadModel(context);

            context.ShowMeshes();
        }

        static string[] VRM_SHADER_NAMES =
        {
            "Standard",
            "VRM/UnlitTexture",
            "VRM/UnlitCutout",
            "VRM/UnlitTransparent",
            "VRM/UnlitTransparentZWrite",
            "VRM/MToon",
        };

        public static CreateMaterialFunc GetMaterialFunc(List<glTF_VRM_Material> materials)
        {
            var CreateDefault = gltfImporter.CreateMaterialFuncFromShader(new ShaderStore("VRM/UnlitTexture"));
            var CreateZWrite = gltfImporter.CreateMaterialFuncFromShader(new ShaderStore("VRM/UnlitTransparentZWrite"));
            CreateMaterialFunc fallback = (ctx, i) =>
            {
                var vrm = ctx.GLTF as glTF_VRM;
                if (vrm != null && vrm.materials[i].name.ToLower().Contains("zwrite"))
                {
                    // 一応。不要かも
                    Debug.Log("fallback to VRM/UnlitTransparentZWrite");
                    return CreateZWrite(ctx, i);
                }
                else
                {
                    Debug.Log("fallback to VRM/UnlitTexture");
                    return CreateDefault(ctx, i);
                }
            };
            if (materials == null && materials.Count == 0)
            {
                return fallback;
            }

            return (ctx, i) =>
            {
                var item = materials[i];
                var shaderName = item.shader;
                var shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    if (VRM_SHADER_NAMES.Contains(shaderName))
                    {
                        Debug.LogErrorFormat("shader {0} not found. set Assets/VRM/Shaders/VRMShaders to Edit - project setting - Graphics - preloaded shaders", shaderName);
                        return fallback(ctx, i);
                    }
                    else
                    {
                        Debug.LogWarningFormat("unknown shader {0}.", shaderName);
                        return fallback(ctx, i);
                    }
                }
                else
                {
                    var material = new Material(shader);
                    material.name = item.name;
                    material.renderQueue = item.renderQueue;

                    foreach (var kv in item.floatProperties)
                    {
                        material.SetFloat(kv.Key, kv.Value);
                    }
                    foreach (var kv in item.vectorProperties)
                    {
                        if (item.textureProperties.ContainsKey(kv.Key))
                        {
                            // texture offset & scale
                            material.SetTextureOffset(kv.Key, new Vector2(kv.Value[0], kv.Value[1]));
                            material.SetTextureScale(kv.Key, new Vector2(kv.Value[2], kv.Value[3]));
                        }
                        else
                        {
                            // vector4
                            var v = new Vector4(kv.Value[0], kv.Value[1], kv.Value[2], kv.Value[3]);
                            material.SetVector(kv.Key, v);
                        }
                    }
                    foreach (var kv in item.textureProperties)
                    {
                        material.SetTexture(kv.Key, ctx.Textures[kv.Value].Texture);
                    }
                    foreach (var kv in item.keywordMap)
                    {
                        if (kv.Value)
                        {
                            material.EnableKeyword(kv.Key);
                        }
                        else
                        {
                            material.DisableKeyword(kv.Key);
                        }
                    }
                    foreach (var kv in item.tagMap)
                    {
                        material.SetOverrideTag(kv.Key, kv.Value);
                    }
                    return material;
                }
            };
        }

        #region OnLoad
        public static Unit OnLoadModel(VRMImporterContext context)
        {
            LoadMeta(context);

            try
            {
                LoadHumanoidObsolete(context);
                Debug.LogWarning("LoadHumanoidObsolete");
            }
            catch (Exception)
            {
                LoadHumanoid(context);
            }

            LoadBlendShapeMaster(context);
            VRMSpringUtility.LoadSecondary(context.Root.transform, context.Nodes,
                context.VRM.extensions.VRM.secondaryAnimation);
            LoadFirstPerson(context);

            return Unit.Default;
        }

        static void LoadMeta(VRMImporterContext context)
        {
            var meta = context.ReadMeta();
            if (meta.Thumbnail == null)
            {
                /*
                // 作る
                var lookAt = context.Root.GetComponent<VRMLookAtHead>();
                var thumbnail = lookAt.CreateThumbnail();
                thumbnail.name = "thumbnail";
                meta.Thumbnail = thumbnail;
                context.Textures.Add(new TextureItem(thumbnail));
                */
            }
            var _meta = context.Root.AddComponent<VRMMeta>();
            _meta.Meta = meta;
            context.Meta = meta;
        }

        static void LoadFirstPerson(VRMImporterContext context)
        {
            var firstPerson = context.Root.AddComponent<VRMFirstPerson>();

            var gltfFirstPerson = context.VRM.extensions.VRM.firstPerson;
            if (gltfFirstPerson.firstPersonBone != -1)
            {
                firstPerson.FirstPersonBone = context.Nodes[gltfFirstPerson.firstPersonBone];
                firstPerson.FirstPersonOffset = gltfFirstPerson.firstPersonBoneOffset;
            }
            else
            {
                // fallback
                firstPerson.SetDefault();
            }
            firstPerson.TraverseRenderers(context);

            // LookAt
            var lookAtHead = context.Root.AddComponent<VRMLookAtHead>();
            lookAtHead.OnImported(context);
        }

        static void LoadBlendShapeMaster(VRMImporterContext context)
        {
            context.BlendShapeAvatar = ScriptableObject.CreateInstance<BlendShapeAvatar>();
            context.BlendShapeAvatar.name = "BlendShape";

            var blendShapeList = context.VRM.extensions.VRM.blendShapeMaster.blendShapeGroups;
            if (blendShapeList != null && blendShapeList.Count > 0)
            {
                foreach (var x in blendShapeList)
                {
                    context.BlendShapeAvatar.Clips.Add(LoadBlendShapeBind(context, x));
                }
            }

            var proxy = context.Root.AddComponent<VRMBlendShapeProxy>();
            context.BlendShapeAvatar.CreateDefaultPreset();
            proxy.BlendShapeAvatar = context.BlendShapeAvatar;
        }

        private static BlendShapeClip LoadBlendShapeBind(VRMImporterContext context,
            glTF_VRM_BlendShapeGroup group)
        {
            var asset = ScriptableObject.CreateInstance<BlendShapeClip>();
            var groupName = group.name;
            var prefix = "BlendShape.";
            while (groupName.StartsWith(prefix))
            {
                groupName = groupName.Substring(prefix.Length);
            }
            asset.name = "BlendShape." + groupName;

            if (group != null)
            {
                asset.BlendShapeName = groupName;
                asset.Preset = EnumUtil.TryParseOrDefault<BlendShapePreset>(group.presetName);
                if (asset.Preset == BlendShapePreset.Unknown)
                {
                    // fallback
                    asset.Preset = EnumUtil.TryParseOrDefault<BlendShapePreset>(group.name);
                }
                asset.Values = group.binds.Select(x =>
                {
                    var mesh = context.Meshes[x.mesh].Mesh;
                    var node = context.Root.transform.Traverse().First(y => y.GetSharedMesh() == mesh);
                    var relativePath = UniGLTF.UnityExtensions.RelativePathFrom(node, context.Root.transform);
                    return new BlendShapeBinding
                    {
                        RelativePath = relativePath,
                        Index = x.index,
                        Weight = x.weight,
                    };
                })
                .ToArray();
                asset.MaterialValues = group.materialValues.Select(x =>
                {
                    var value = new Vector4();
                    for (int i = 0; i < x.targetValue.Length; ++i)
                    {
                        switch (i)
                        {
                            case 0: value.x = x.targetValue[0]; break;
                            case 1: value.y = x.targetValue[1]; break;
                            case 2: value.z = x.targetValue[2]; break;
                            case 3: value.w = x.targetValue[3]; break;
                        }
                    }
                    return new MaterialValueBinding
                    {
                        MaterialName = x.materialName,
                        ValueName = x.propertyName,
                        TargetValue = value,
                        BaseValue = context.Materials.First(y => y.name == x.materialName).GetColor(x.propertyName),
                    };
                })
                .ToArray();
            }

            return asset;
        }

        static String ToHumanBoneName(HumanBodyBones b)
        {
            foreach (var x in HumanTrait.BoneName)
            {
                if (x.Replace(" ", "") == b.ToString())
                {
                    return x;
                }
            }

            throw new KeyNotFoundException();
        }

        static SkeletonBone ToSkeletonBone(Transform t)
        {
            var sb = new SkeletonBone();
            sb.name = t.name;
            sb.position = t.localPosition;
            sb.rotation = t.localRotation;
            sb.scale = t.localScale;
            return sb;
        }

        [Obsolete]
        private static void LoadHumanoidObsolete(VRMImporterContext context)
        {
            var parsed = context.Json.ParseAsJson()["extensions"]["VRM"];
            var skeleton = context.Root.transform.Traverse().Select(x => ToSkeletonBone(x)).ToArray();

            var description = new HumanDescription
            {
                human = parsed[HUMANOID_KEY]["bones"]
                .ObjectItems
                .Select(x => new { x.Key, Index = x.Value.GetInt32() })
                .Where(x => x.Index != -1)
                .Select(x =>
                {
                    var humanBone = EnumUtil.TryParseOrDefault<HumanBodyBones>(x.Key);
                    var hb = new HumanBone
                    {
                        boneName = context.Nodes[x.Index].name,
                        humanName = ToHumanBoneName(humanBone)
                    };
                    hb.limit.useDefaultValues = true;
                    return hb;
                }).ToArray(),
                skeleton = skeleton,
                lowerArmTwist = 0.5f,
                upperArmTwist = 0.5f,
                upperLegTwist = 0.5f,
                lowerLegTwist = 0.5f,
                armStretch = 0.05f,
                legStretch = 0.05f,
                feetSpacing = 0.0f,
            };

            context.HumanoidAvatar = AvatarBuilder.BuildHumanAvatar(context.Root, description);
            context.HumanoidAvatar.name = Path.GetFileNameWithoutExtension(context.Path);

            context.AvatarDescription = UniHumanoid.AvatarDescription.CreateFrom(description);
            context.AvatarDescription.name = "AvatarDescription";
            var humanoid = context.Root.AddComponent<VRMHumanoidDescription>();
            humanoid.Avatar = context.HumanoidAvatar;
            humanoid.Description = context.AvatarDescription;

            var animator = context.Root.GetComponent<Animator>();
            if (animator == null)
            {
                animator = context.Root.AddComponent<Animator>();
            }
            animator.avatar = context.HumanoidAvatar;
        }

        private static void LoadHumanoid(VRMImporterContext context)
        {
            context.AvatarDescription = context.VRM.extensions.VRM.humanoid.ToDescription(context.Nodes);
            context.AvatarDescription.name = "AvatarDescription";
            context.HumanoidAvatar = context.AvatarDescription.CreateAvatar(context.Root.transform);
            context.HumanoidAvatar.name = Path.GetFileNameWithoutExtension(context.Path);

            var humanoid = context.Root.AddComponent<VRMHumanoidDescription>();
            humanoid.Avatar = context.HumanoidAvatar;
            humanoid.Description = context.AvatarDescription;

            var animator = context.Root.GetComponent<Animator>();
            if (animator == null)
            {
                animator = context.Root.AddComponent<Animator>();
            }
            animator.avatar = context.HumanoidAvatar;
        }
        #endregion

        #region LoadVrmAsync
        static IEnumerator LoadTextures(VRMImporterContext context, IStorage storage)
        {
            for (int i = 0; i < context.GLTF.textures.Count; ++i)
            {
                var x = new TextureItem(context.GLTF, i);
                x.Process(context.GLTF, storage);
                context.Textures.Add(x);
                yield return null;
            }
        }

        static IEnumerator LoadMaterials(VRMImporterContext context)
        {
            if (context.GLTF.materials == null || !context.GLTF.materials.Any())
            {
                context.Materials.Add(context.CreateMaterial(context, 0));
            }
            else
            {
                for (int i = 0; i < context.GLTF.materials.Count; ++i)
                {
                    context.Materials.Add(context.CreateMaterial(context, i));
                    yield return null;
                }
            }
        }

        static IEnumerator LoadMeshes(VRMImporterContext context)
        {
            for (int i = 0; i < context.GLTF.meshes.Count; ++i)
            {
                var meshWithMaterials = gltfImporter.ImportMesh(context, i);
                var mesh = meshWithMaterials.Mesh;
                if (string.IsNullOrEmpty(mesh.name))
                {
                    mesh.name = string.Format("UniGLTF import#{0}", i);
                }
                context.Meshes.Add(meshWithMaterials);

                yield return null;
            }
        }

        static IEnumerator LoadNodes(VRMImporterContext context)
        {
            foreach (var x in context.GLTF.nodes)
            {
                context.Nodes.Add(gltfImporter.ImportNode(x).transform);
            }

            yield return null;
        }

        static IEnumerator BuildHierarchy(VRMImporterContext context)
        {
            var nodes = new List<gltfImporter.TransformWithSkin>();
            for (int i = 0; i < context.Nodes.Count; ++i)
            {
                nodes.Add(gltfImporter.BuildHierarchy(context, i));
            }

            gltfImporter.FixCoordinate(context, nodes);

            // skinning
            for (int i = 0; i < nodes.Count; ++i)
            {
                gltfImporter.SetupSkinning(context, nodes, i);
            }

            // connect root
            context.Root = new GameObject("_root_");
            foreach (var x in context.GLTF.rootnodes)
            {
                var t = nodes[x].Transform;
                t.SetParent(context.Root.transform, false);
            }

            yield return null;
        }

#if (NET_4_6 && UNITY_2018_1_OR_NEWER)

        public static Task<GameObject> LoadVrmAsync(string path)
        {
            var context = new VRMImporterContext(path);
            context.ParseVrm(File.ReadAllBytes(path));
            return LoadVrmAsyncInternal(context).ToTask();
        }


        public static Task<GameObject> LoadVrmAsync(Byte[] bytes)
        {
            var context = new VRMImporterContext();
            context.ParseVrm(bytes);
            return LoadVrmAsync(context);
        }


        public static Task<GameObject> LoadVrmAsync(VRMImporterContext ctx)
        {
            return LoadVrmAsyncInternal(ctx).ToTask();
        }
#endif

        public static void LoadVrmAsync(string path, Action<GameObject> onLoaded, Action<Exception> onError = null)
        {
            var context = new VRMImporterContext(path);
            context.ParseVrm(File.ReadAllBytes(path));
            LoadVrmAsync(context, onLoaded, onError);
        }

        public static void LoadVrmAsync(Byte[] bytes, Action<GameObject> onLoaded, Action<Exception> onError = null)
        {
            var context = new VRMImporterContext();
            context.ParseVrm(bytes);
            LoadVrmAsync(context, onLoaded, onError);
        }

        public static void LoadVrmAsync(VRMImporterContext ctx, Action<GameObject> onLoaded, Action<Exception> onError = null)
        {
            if (onError == null)
            {
                onError = Debug.LogError;
            }
            LoadVrmAsyncInternal(ctx)
                .Subscribe(Scheduler.MainThread, onLoaded, onError);
        }

        private static Schedulable<GameObject> LoadVrmAsyncInternal(VRMImporterContext ctx)
        {
            var schedulable = Schedulable.Create();

            return schedulable
                .AddTask(Scheduler.ThreadPool, () =>
                {
                    ctx.GLTF.baseDir = Path.GetDirectoryName(ctx.Path);
                    return Unit.Default;
                })
                .ContinueWith(Scheduler.ThreadPool, _ =>
                {
                    return glTF_VRM_Material.Parse(ctx.Json);
                })
                .ContinueWith(Scheduler.MainThread, x =>
                {
                    // material function
                    ctx.CreateMaterial = VRMImporter.GetMaterialFunc(x);
                })
                .OnExecute(Scheduler.ThreadPool, parent =>
                {
                    // textures
                    for (int i = 0; i < ctx.GLTF.textures.Count; ++i)
                    {
                        var index = i;
                        parent.AddTask(Scheduler.MainThread,
                                () =>
                                {
                                    var texture = new TextureItem(ctx.GLTF, index);
                                    texture.Process(ctx.GLTF, ctx.Storage);
                                    return texture;
                                })
                            .ContinueWith(Scheduler.ThreadPool, x => ctx.Textures.Add(x));
                    }
                })
                .ContinueWithCoroutine(Scheduler.MainThread, () => LoadMaterials(ctx))
                .OnExecute(Scheduler.ThreadPool, parent =>
                {
                    // meshes
                    for (int i = 0; i < ctx.GLTF.meshes.Count; ++i)
                    {
                        var index = i;
                        parent.AddTask(Scheduler.ThreadPool,
                                () => gltfImporter.ReadMesh(ctx, index))
                        .ContinueWith(Scheduler.MainThread, x => gltfImporter.BuildMesh(ctx, x))
                        .ContinueWith(Scheduler.ThreadPool, x => ctx.Meshes.Add(x))
                        ;
                    }
                })
                .ContinueWithCoroutine(Scheduler.MainThread, () => LoadNodes(ctx))
                .ContinueWithCoroutine(Scheduler.MainThread, () => BuildHierarchy(ctx))
                .ContinueWith(Scheduler.MainThread, _ => VRMImporter.OnLoadModel(ctx))
                .ContinueWith(Scheduler.MainThread,
                    _ =>
                    {
                        /*
                        Debug.LogFormat("task end: {0}/{1}/{2}/{3}",
                            ctx.Textures.Count,
                            ctx.Materials.Count,
                            ctx.Meshes.Count,
                            ctx.Nodes.Count
                            );
                            */
                        ctx.Root.name = Path.GetFileNameWithoutExtension(ctx.Path);

                        // 非表示のメッシュを表示する
                        ctx.ShowMeshes();

                        return ctx.Root;
                    });
        }
        #endregion
    }
}
