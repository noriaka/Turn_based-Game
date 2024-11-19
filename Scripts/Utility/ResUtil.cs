using Newtonsoft.Json;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResUtil : IUtility, ICanGetUtility, ICanGetModel
{
    [Serializable]
    public class PetConfig
    {
        public int pid;
        public string name;
        public int hp;
        public int phyAtk;
        public int phyDef;
        public int magAtk;
        public int magDef;
        public int speed;
        public string skills;
        public string element;
    }

    [Serializable]
    public class SkillConfig
    {
        public int sid;
        public string name;
        public string tag;
        public int power;
        public string element;
        public string target;
        public int targetNum;
        public int cost;
        public string desc;
    }

    [Serializable]
    public class PlayerConfig
    {
        public int uid;
        public string name;
        public string ownedPets;
        public string curBattlePets;
    }

    readonly ConstantModel model = GameArchitecture.Interface.GetModel<ConstantModel>();
    Dictionary<int, PetConfig> petConfigs;
    Dictionary<int, SkillConfig> skillConfigs;

    #region PetConfig
    public void LoadPetConfig(int pid, PetModel petModel)
    {
        if (petConfigs == null)
        {
            TextAsset jsonText = Resources.Load<TextAsset>(model.PetConfigPath);
            if (jsonText != null)
            {
                petConfigs = JsonConvert.DeserializeObject<Dictionary<int, PetConfig>>(jsonText.text);
            }
        }
        if (petConfigs != null)
        {
            petModel.Name = petConfigs[pid].name;
            petModel.Hp = petConfigs[pid].hp;
            petModel.PhyAtk = petConfigs[pid].phyAtk;
            petModel.PhyDef = petConfigs[pid].phyDef;
            petModel.MagAtk = petConfigs[pid].magAtk;
            petModel.MagDef = petConfigs[pid].magDef;
            petModel.Speed = petConfigs[pid].speed;
            petModel.Skills = petConfigs[pid].skills
                               .Split("#")
                               .Select(int.Parse)
                               .ToList();
            var dic = model.ElementNameDic;
            foreach (var kvp in dic)
            {
                if (kvp.Value[0] == petConfigs[pid].element)
                {
                    petModel.Element = kvp.Key;
                    break;
                }
            }
            petModel.PetAniPath = this.GetUtility<CommonUtil>().GetPetAniPath(pid);
            petModel.PetBattleAniPath = this.GetUtility<CommonUtil>().GetPetBattleAniPath(pid);
        }
    }
    
    public Dictionary<int, PetModel> LoadPetModels()
    {
        Dictionary<int, PetModel> petModels = new();
        if (petConfigs == null)
        {
            TextAsset jsonText = Resources.Load<TextAsset>(model.PetConfigPath);
            if (jsonText != null)
            {
                petConfigs = JsonConvert.DeserializeObject<Dictionary<int, PetConfig>>(jsonText.text);
            }
        }
        if (petConfigs != null )
        {
            foreach (var pet in petConfigs)
            {
                if (!petModels.ContainsKey(pet.Key))
                {
                    PetModel petModel = new(pet.Key);
                    petModels.Add(pet.Key, petModel);
                }
            }
        }
        return petModels;
    }
    #endregion

    #region SkillConfig
    public void LoadSkillConfig(int sid, SkillModel skillModel)
    {
        if (skillConfigs == null)
        {
            TextAsset jsonText = Resources.Load<TextAsset>(model.SkillConfigPath);
            if (jsonText != null)
            {
                skillConfigs = JsonConvert.DeserializeObject<Dictionary<int, SkillConfig>>(jsonText.text);
            }
        }
        if (skillConfigs != null)
        {
            skillModel.Name = skillConfigs[sid].name;
            skillModel.Tag = skillConfigs[sid].tag;
            skillModel.Power = skillConfigs[sid].power;
            var dic = model.ElementNameDic;
            foreach (var kvp in dic)
            {
                if (kvp.Value[0] == skillConfigs[sid].element)
                {
                    skillModel.Element = kvp.Key;
                    break;
                }
            }
            skillModel.Target = skillConfigs[sid].target;
            skillModel.TargetNum = skillConfigs[sid].targetNum;
            skillModel.Cost = skillConfigs[sid].cost;
            skillModel.Desc = skillConfigs[sid].desc;
        }
    }

    public Dictionary<int, SkillModel> LoadSkillModels()
    {
        Dictionary<int, SkillModel> skillModels = new();
        if (skillConfigs == null)
        {
            TextAsset jsonText = Resources.Load<TextAsset>(model.SkillConfigPath);
            if (jsonText != null)
            {
                skillConfigs = JsonConvert.DeserializeObject<Dictionary<int, SkillConfig>>(jsonText.text);
            }
        }
        if (skillConfigs != null)
        {
            foreach (var skill in skillConfigs)
            {
                if (!skillModels.ContainsKey(skill.Key))
                {
                    SkillModel skillModel = new(skill.Key);
                    skillModels.Add(skill.Key, skillModel);
                }
            }
        }
        return skillModels;
    }
    #endregion

    #region PlayerConfig
    public void LoadPlayerConfig(PlayerModel playerModel)
    {
        PlayerConfig playerConfig = null;
        TextAsset jsonText = Resources.Load<TextAsset>(model.PlayerConfigPath);
        if (jsonText != null)
        {
            playerConfig = JsonConvert.DeserializeObject<PlayerConfig>(jsonText.text);
            int uid = playerConfig.uid;
            string name = playerConfig.name;
            List<int> ownedPets = playerConfig.ownedPets
                                  .Split("#")
                                  .Select(int.Parse)
                                  .ToList();
            List<string> PosAndPets = playerConfig.curBattlePets
                                      .Split("#")
                                      .ToList();
            Dictionary<int, int> curBattlePets = new();
            foreach (var pp in PosAndPets)
            {
                int pos = int.Parse(pp.Split("-")[0]);
                int pid = int.Parse(pp.Split("-")[1]);
                if (!curBattlePets.ContainsKey(pos))
                {
                    curBattlePets.Add(pos, pid);
                }
            }
            playerModel.Name = name;
            playerModel.OwnedPets = ownedPets;
            playerModel.CurBattlePets = curBattlePets;
        }
    }
    #endregion

    public GameObject InitPetBattleAniObj(int pid)
    {
        GameObject petBattleAniObj = new();
        var pModels = this.GetModel<PetModels>().petModels;
        if (pModels == null) return petBattleAniObj;
        else
        {
            if (!pModels.ContainsKey(pid))
            {
                Debug.Log(" ‰»Îpid∑«∑®");
                return petBattleAniObj;
            }
            PetModel pModel = pModels[pid];
            petBattleAniObj = Resources.Load<GameObject>(pModel.PetBattleAniPath);
            return petBattleAniObj;
        }
    } 

    public GameObject InitPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public Sprite GetElementSprite(ConstantModel.ElementType elementType)
    {
        string path = this.GetUtility<CommonUtil>().GetElementImgPath(elementType);
        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }

    //public List<int> getIntList(string str)
    //{
    //    List<int> intList = new List<int>();
    //    if (str != null)
    //    {
    //        intList = str.Split("#").Select(int.Parse).ToList();
    //    }
    //    return intList;
    //}

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
