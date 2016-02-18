using UnityEngine;


public static class ExtendMethod
{
    /// <summary>
    /// Find and get component in transform. 找到子物體並且取得該子物體的組件.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T FindAndGetEX<T>(this Transform t, string name) where T : Component
    {
        Transform transform = t.Find(name);
        if (transform != null)
        {
            T target = transform.GetComponent<T>();
            if (target != null) return target;
        }

        Debug.Log("Fail to FindAndGet<T> component. Component:" + typeof(T).ToString());
        return null;
    }

    /// <summary>
    /// Get or add component in gameObject. 加入指定的組件，如果該組件不存在，那就加入新的組件.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponentEX<T>(this GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();

        if (t == null) t = go.AddComponent<T>();

        return t;
    }
}

