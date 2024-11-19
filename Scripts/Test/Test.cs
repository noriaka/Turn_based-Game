using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Test : MonoBehaviour, ICanGetModel
{
    #region AnimationTest
    public static AnimationClip[] GetAniClips(string path)
    {
        // 尝试加载 Animator Controller
        AnimatorController aniController = Resources.Load<AnimatorController>(path);

        // 如果加载成功，返回动画剪辑
        if (aniController != null)
        {
            return aniController.animationClips;
        }
        else
        {
            Debug.LogError($"Failed to load AnimatorController at path: {path}");
            return new AnimationClip[0]; // 返回一个空数组
        }
    }
    #endregion

    #region NewTest
    public Vector2 posOffset = new (-320, 500);
    public List<Transform> trans = new(9);

    private void Start()
    {
        GameObject petBattleAni5427 = Resources.Load<GameObject>
            (this.GetModel<ConstantModel>().PetBattleAniPath + "petBattleAni5427");
        Instantiate(petBattleAni5427, trans[0]);
        //Vector3 pos = new Vector3(trans[0].position.x + posOffset.x, trans[0].position.y + posOffset.y, trans[0].position.z);
        petBattleAni5427.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        petBattleAni5427.gameObject.GetComponent<RectTransform>().localPosition = posOffset;
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    #endregion
}
