using UnityEngine;

public class BaseMgr<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    // ���ڻ�ȡ����ʵ���Ĺ�����������
    public static T Instance
    {
        get
        {
            // ���ʵ�������ڣ���������ʵ���򴴽���ʵ��
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).Name);
                    _instance = singletonObj.AddComponent<T>();
                    DontDestroyOnLoad(singletonObj); // ʹʵ���ڳ����䲻������
                }
            }
            return _instance;
        }
    }

    // ȷ��ʵ���ڳ�������ʱ��������
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // �����ظ���ʵ��
        }
    }
}
