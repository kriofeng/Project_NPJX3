
using System;
using System.Collections.Generic;

public class SingletonSavable<T> where T :class,ISaveable,new()
{
    protected static T _instance;
    public static T Instance {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                _instance.Load();
            }
            return _instance;
        }
    }
    public void Clear()
    {
        _instance = null;
    }
}

//字典类单例
public class SingletonMapSavable<T> where T : class, ISaveableWithID, new()
{
    protected static Dictionary<int, T> _dictionary;
    protected static HashSet<int> idInUse;

    public static T GetInstance(int id)
    {
        if (idInUse == null || _dictionary == null)
        {
            _dictionary = new Dictionary<int, T>();
            idInUse = new HashSet<int>();
        }
        if (!idInUse.Contains(id))
        {
            T data = new T();
            data.SetId(id);
            data.Load();
            _dictionary[id] = data;
        }
        return _dictionary[id];
    }

    public static void AddId(int id)
    {
        if (idInUse == null)
        {
            idInUse = new HashSet<int>();
        }

        idInUse.Add(id);
    }

    public static int GetUniqueID(int id)
    {
        if (idInUse == null)
        {
            idInUse = new HashSet<int>();
        }
        while (idInUse.Contains(id))
        {
            id++;
        }

        idInUse.Add(id);
        return id;
    }
}