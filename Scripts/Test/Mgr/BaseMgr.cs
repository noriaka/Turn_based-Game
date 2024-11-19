using UnityEngine;

public class BaseMgr<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    // 用于获取单例实例的公共访问属性
    public static T Instance
    {
        get
        {
            // 如果实例不存在，查找现有实例或创建新实例
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).Name);
                    _instance = singletonObj.AddComponent<T>();
                    DontDestroyOnLoad(singletonObj); // 使实例在场景间不被销毁
                }
            }
            return _instance;
        }
    }

    // 确保实例在场景加载时不被销毁
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 销毁重复的实例
        }
    }
}
