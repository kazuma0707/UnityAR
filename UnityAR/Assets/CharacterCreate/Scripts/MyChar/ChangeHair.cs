using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHair : MonoBehaviour
{

    private GameObject RootBone;                                               // 基礎となる骨構造
    private GameObject resourceObject;                                         // 差し替えるモデル                                                                               
    private List<Transform> BoneTransformList = new List<Transform>();         // 髪型のBoneの構造リスト
    private Dictionary<string, int> RootBoneIndexList =
                                                new Dictionary<string, int>(); // 髪型のIndexList

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | obj：髪型, bone：素体のBone
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairs(GameObject obj, GameObject bone)
    {
        // 髪型オブジェクトと素体のHeadBoneを取得
        resourceObject = obj;
        RootBone = bone;

        // いらないBoneを削除
        //DeleteOldBones();
        // いらないモデル削除
        DeleteOldModels();


        // リストを再登録
        BoneTransformList.Clear();
        BoneTransformList.AddRange(RootBone.GetComponentsInChildren<Transform>());
        RootBoneIndexList.Clear();

        // 素体のBoneを基にリストを登録
        for (int boneIndex = 0; boneIndex < BoneTransformList.Count; ++boneIndex)
        {
            Transform baseTrans = BoneTransformList[boneIndex].transform;
            RootBoneIndexList.Add(baseTrans.name, boneIndex);
        }
        
        // モデルを変える
        ChangeHairModels();
    }

    // モデルを変える
    private void ChangeHairModels()
    {
        // 差し替え用のモデルにあるすべてのRendererコンポーネントの取得
        SkinnedMeshRenderer[] smRenderersParts = resourceObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int smrIndex = 0; smrIndex < smRenderersParts.Length; smrIndex++)
        {
            SkinnedMeshRenderer smr = smRenderersParts[smrIndex];

            // Boneの構造を変える
            ChangeBones(smr);
        }
    }

    // Boneの構造を変える
    private void ChangeBones(SkinnedMeshRenderer smr)
    {
        // Boneの取得
        Transform[] meshBones = smr.bones;

        // SkinnedMeshRenderer毎のBoneIndexに対応したTransformの配列
        Transform[] localTransforms = new Transform[meshBones.Length];

        // Boneの参照を差し替える用のデータ用意
        for (int boneIndex = 0; boneIndex < meshBones.Length; boneIndex++)
        {
            // 部位と素体の同名のBoneを検索し、部位と素体のindexを整理/保存
            if (RootBoneIndexList.ContainsKey(meshBones[boneIndex].name))
            {
                // 部位側のBoneIndexリストと同じサイズのリストに、同じindexで素体側のtransformを設定
                Transform t = BoneTransformList[RootBoneIndexList[meshBones[boneIndex].name]];
                t.localPosition = meshBones[boneIndex].localPosition;
                t.localEulerAngles = meshBones[boneIndex].localEulerAngles;
                localTransforms[boneIndex] = t;
            }
            else
            {
                // 無ければボーンを新規で作成
                CreateBones(meshBones, boneIndex, ref localTransforms);
            }
        }

        // MeshObjectを作成する
        CreateMeshObject(smr, localTransforms);
    }


    // ボーンを新規で作成
    private void CreateBones(Transform[] meshBones, int boneIndex, ref Transform[] localTransforms)
    {
        // 同名のBoneが無ければ作成
        GameObject obj = Instantiate(meshBones[boneIndex].gameObject);
        obj.name = meshBones[boneIndex].name;

        obj.transform.SetParent(BoneTransformList[RootBoneIndexList[meshBones[boneIndex].parent.name]]);
        obj.transform.localPosition = meshBones[boneIndex].gameObject.transform.localPosition;
        obj.transform.localEulerAngles = meshBones[boneIndex].gameObject.transform.localEulerAngles;
        // bone index登録
        RootBoneIndexList.Add(obj.name, RootBoneIndexList.Count);
        BoneTransformList.Add(obj.transform);
        // 部位側のBoneIndexリストと同じサイズのリストに、同じindexで素体側のtransformを設定
        int num = RootBoneIndexList[meshBones[boneIndex].name];

        localTransforms[boneIndex] = BoneTransformList[num];

        // 子オブジェクトを取得
        GameObject[] child = obj.GetChildren();

        // 子オブジェクト分のbone index登録
        for (int i = 0; i < child.Length; i++)
        {
            // BoneIndexリストに子オブジェクトが登録されていなければ
            if (!RootBoneIndexList.ContainsKey(child[i].name))
            {
                // bone index登録
                RootBoneIndexList.Add(child[i].name, RootBoneIndexList.Count);
                BoneTransformList.Add(child[i].transform);
            }
        }
    }

    // MeshObjectを作成する
    private void CreateMeshObject(SkinnedMeshRenderer smr, Transform[] localTransforms)
    {
        //  キャラクターモデル用のMeshObjectを作成
        GameObject newMeshObject = new GameObject();
        newMeshObject.transform.SetParent(RootBone.transform.parent.transform);
        newMeshObject.name = smr.gameObject.name;
        newMeshObject.transform.localEulerAngles = Vector3.zero;
        newMeshObject.transform.localPosition = Vector3.zero;
        newMeshObject.tag = smr.gameObject.tag;

        // SkinnedMeshRenderer生成
        SkinnedMeshRenderer r = newMeshObject.AddComponent<SkinnedMeshRenderer>();
        // 部位モデルのMesh情報を複製して適用
        r.sharedMesh = Instantiate(smr.sharedMesh);
        // マテリアルの適用
        r.materials = smr.sharedMaterials;
        // Bonesの情報を整合性をあわせてリストに更新
        r.bones = localTransforms;
        // rootBoneを設定を設定
        foreach (Transform t in BoneTransformList)
        {
            if (t.name == smr.rootBone.name)
            {
                r.rootBone = t;
            }
        }

        // 各種細かい設定
        r.receiveShadows = false;
        r.quality = SkinQuality.Auto;

        // FreeSelectColor生成
        //SetHairsFreeColor hairsFreeColor = newMeshObject.AddComponent<SetHairsFreeColor>();

        //hairsFreeColor.Color = MyCharDataManager.Instance.Data.hairColor2;
    }

    // 古いモデルを削除する
    private void DeleteOldModels()
    {
        // 素体のBoneから親オブジェクトを取得する
        GameObject model = RootBone.transform.parent.gameObject;
        // 親オブジェクトからSkinnedMeshRendererコンポーネントを持つ子オブジェクトを取得
        SkinnedMeshRenderer[] renderers = model.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            if (!renderers[i]) continue;
            // 髪型オブジェクトを削除
            if (renderers[i].gameObject.tag == "HairObj")
                Destroy(renderers[i].gameObject);
        }
    }

    // 古いBoneを削除する
    private void DeleteOldBones()
    {        
        // HeadBoneの子オブジェクトを取得        
        Transform[] headBoneTransforms = RootBone.GetComponentsInChildren<Transform>();
        
        for (int i = 0; i < headBoneTransforms.Length; i++)
        {
            if (!headBoneTransforms[i]) continue;
            // いらないBoneを削除
            if (headBoneTransforms[i].tag == "HairBone")
                Destroy(headBoneTransforms[i].gameObject);
        }
        
    }
}
