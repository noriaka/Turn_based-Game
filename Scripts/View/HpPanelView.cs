using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPanelView : MonoBehaviour, ICanGetUtility
{
    public Slider hpSilder;
    public Image elementImg;

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public void Init(int curHp, int maxHP, ConstantModel.ElementType element)
    {
        UpdateInfo(curHp, maxHP);
        Sprite ele = this.GetUtility<ResUtil>().GetElementSprite(element);
        elementImg.sprite = ele;
    }

    public void UpdateInfo(int curHp, int maxHP)
    {
        hpSilder.value = (float)curHp / maxHP;
    }
}
