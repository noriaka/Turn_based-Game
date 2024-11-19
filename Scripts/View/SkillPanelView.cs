using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelView : MonoBehaviour, ICanGetUtility
{
    public Button btnSkill;
    public Image imgSkillElementImg;
    public Text txtSkillName;
    public Text txtSkillPowerAndCost;
    public Text txtSkillDesc;

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public void Init(ConstantModel.ElementType element, string sName,
                     int sPower, int sCost, string sDesc)
    {
        btnSkill = GetComponent<Button>();
        imgSkillElementImg.sprite = this.GetUtility<ResUtil>().GetElementSprite(element);
        txtSkillName.text = sName;
        string str = $"ÍþÁ¦: {sPower}          ";
        if (sCost < 0)
        {
            str += $"»Ø¸´: {-sCost}";
            if (ColorUtility.TryParseHtmlString("#00A600", out Color newColor))
            {
                txtSkillPowerAndCost.transform.GetComponent<Outline>().effectColor = newColor;
            }
        } else
        {
            str += $"ÏûºÄ: {sCost}";
        }
        txtSkillPowerAndCost.text = str;
        txtSkillDesc.text = sDesc;
        gameObject.SetActive(false);
    }
}
