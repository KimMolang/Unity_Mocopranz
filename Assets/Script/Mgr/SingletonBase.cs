using System;
using System.Reflection;

// http://lab.gamecodi.com/board/zboard.php?id=GAMECODILAB_QnA_etc&page=1&sn1=&divpage=1&sn=off&ss=on&sc=on&select_arrange=headnum&desc=asc&no=2393
// http://lonpeach.com/2017/02/04/unity3d-singleton-pattern-example/
public abstract class SingletonBase<T> where T : class
{
    private static object _syncobj = new object();
    private static volatile T _instance = null;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                CreateInstance();
            }
            return _instance;
        }
    }

    private static void CreateInstance()
    {
        lock (_syncobj)
        {
            if (_instance == null)
            {
                Type t = typeof(T);

                // Ensure there are no public constructors...  
                ConstructorInfo[] ctors = t.GetConstructors();
                if (ctors.Length > 0)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "{0} has at least one accesible ctor making it impossible to enforce singleton behaviour", t.Name));
                }

                // Create an instance via the private constructor  
                // http://debuglog.tistory.com/34
                _instance = (T)Activator.CreateInstance(t, true);
            }
        }
    }
}