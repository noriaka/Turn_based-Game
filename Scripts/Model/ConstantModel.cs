using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using static Spine.Unity.Editor.SkeletonBaker.BoneWeightContainer;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ConstantModel : AbstractModel
{
    private int maxLevel;

    private string petConfigPath;
    private string skillConfigPath;
    private string playerConfigPath;

    private string petAniPath;
    private string petBattleAniPath;
    private string elementImgPath;

    private string hpPanelPath;
    private string skillPanelPath;

    private Vector2 posFieldOffset;
    private Vector2 hpOffset;

    private Dictionary<ElementType, List<string>> elementNameDic;
    private Dictionary<int, Vector2> battlePosOffsetDic;

    public int MaxLevel { get => maxLevel; private set => maxLevel = value; }
    public string PetConfigPath { get => petConfigPath; private set => petConfigPath = value; }
    public string PetAniPath { get => petAniPath; private set => petAniPath = value; }
    public string PetBattleAniPath { get => petBattleAniPath; private set => petBattleAniPath = value; }
    public string PlayerConfigPath { get => playerConfigPath; set => playerConfigPath = value; }
    public string HpPanelPath { get => hpPanelPath; set => hpPanelPath = value; }
    public Vector2 PosFieldOffset { get => posFieldOffset; set => posFieldOffset = value; }
    public Vector2 HpOffset { get => hpOffset; set => hpOffset = value; }
    public string ElementImgPath { get => elementImgPath; set => elementImgPath = value; }
    public Dictionary<ElementType, List<string>> ElementNameDic { get => elementNameDic; set => elementNameDic = value; }
    public string SkillPanelPath { get => skillPanelPath; set => skillPanelPath = value; }
    public string SkillConfigPath { get => skillConfigPath; set => skillConfigPath = value; }
    public Dictionary<int, Vector2> BattlePosOffsetDic { get => battlePosOffsetDic; set => battlePosOffsetDic = value; }

    public enum ElementType
    {
        aqua = 0,           // 水
        ignis = 1,          // 火
        verdant = 2,        // 草
        lumen = 3,          // 光
        umbra = 4,          // 暗
        aether = 5,         // 空
        origin = 6,         // 创
        nexus = 7           // 源
    }

    public enum PlayerType
    {
        player = 0,
        enemy = 1
    }

    protected override void OnInit()
    {
        MaxLevel = 100;
        PetConfigPath = "Configs/PetConfig.json";
        PetAniPath = "Prefabs/petPrefabs/petAni/";
        PetBattleAniPath = "Prefabs/petPrefabs/petBattleAni/";
        SkillConfigPath = "Configs/SkillConfig.json";
        PlayerConfigPath = "Configs/PlayerConfig.json";

        HpPanelPath = "Prefabs/UIPanels/HpPanel";
        SkillPanelPath = "Prefabs/UIPanels/SkillPanel";

        ElementImgPath = "Texture/";

        posFieldOffset = new(-320, 500);
        hpOffset = new(300, -220);

        ElementNameDic = new Dictionary<ElementType, List<string>>
        {
            { ElementType.aqua, new List<string> { "aqua", "水" } },
            { ElementType.ignis, new List<string> { "ignis", "火" } },
            { ElementType.verdant, new List<string> { "verdant", "草" } },
            { ElementType.lumen, new List<string> { "lumen", "光" } },
            { ElementType.umbra, new List<string> { "umbra", "暗" } },
            { ElementType.aether, new List<string> { "aether", "空" } },
            { ElementType.origin, new List<string> { "origin", "创" } },
            { ElementType.nexus, new List<string> { "nexus", "源" } }
        };
        BattlePosOffsetDic = new Dictionary<int, Vector2>
        {
            { 0, new Vector2(-80, 500  - 150 ) },
            { 1, new Vector2(-80, 840  - 150 ) },
            { 2, new Vector2(-80, 1180 - 150 ) },
            { 3, new Vector2(270, 500  - 150 ) },
            { 4, new Vector2(270, 840  - 150 ) },
            { 5, new Vector2(270, 1180 - 150 ) },
            { 6, new Vector2(620, 500  - 150 ) },
            { 7, new Vector2(620, 840  - 150 ) },
            { 8, new Vector2(620, 1180 - 150 ) }
        };
    }
}
