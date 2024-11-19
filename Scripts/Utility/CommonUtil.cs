using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommonUtil : IUtility, ICanGetModel
{
    readonly ConstantModel model = GameArchitecture.Interface.GetModel<ConstantModel>();

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public string GetPetAniPath(int pid)
    {
        return model.PetAniPath + $"petMovie{pid}";
    }

    public string GetPetBattleAniPath(int pid)
    {
        return model.PetBattleAniPath + $"petBattleAni{pid}";
    }
    
    public string GetElementImgPath(ConstantModel.ElementType elemnetType)
    {
        return model.ElementImgPath + "element_" + model.ElementNameDic[elemnetType][0];
    }

    public class BidirectionalDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> keyToValue = new Dictionary<TKey, TValue>();
        private readonly Dictionary<TValue, TKey> valueToKey = new Dictionary<TValue, TKey>();

        // 添加键值对
        public void Add(TKey key, TValue value)
        {
            if (keyToValue.ContainsKey(key))
                throw new ArgumentException("Key already exists.");
            if (valueToKey.ContainsKey(value))
                throw new ArgumentException("Value already exists.");

            keyToValue[key] = value;
            valueToKey[value] = key;
        }

        // 根据键获取值
        public TValue GetValue(TKey key)
        {
            if (keyToValue.TryGetValue(key, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException("Key not found.");
        }

        // 根据值获取键
        public TKey GetKey(TValue value)
        {
            if (valueToKey.TryGetValue(value, out var key))
            {
                return key;
            }
            throw new KeyNotFoundException("Value not found.");
        }

        // 移除键值对（通过键）
        public void RemoveByKey(TKey key)
        {
            if (keyToValue.TryGetValue(key, out var value))
            {
                keyToValue.Remove(key);
                valueToKey.Remove(value);
            }
            else
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }

        // 移除键值对（通过值）
        public void RemoveByValue(TValue value)
        {
            if (valueToKey.TryGetValue(value, out var key))
            {
                valueToKey.Remove(value);
                keyToValue.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException("Value not found.");
            }
        }

        // 检查是否包含键或值
        public bool ContainsKey(TKey key) => keyToValue.ContainsKey(key);
        public bool ContainsValue(TValue value) => valueToKey.ContainsKey(value);

        // 清空字典
        public void Clear()
        {
            keyToValue.Clear();
            valueToKey.Clear();
        }
    }
}