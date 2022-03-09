using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> where T : class, new()
{
    // Check to see if we're about to be destroyed.
    private static readonly object m_Lock = new object();
    private static T m_instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                }

                return m_instance;
            }
        }
    }
}