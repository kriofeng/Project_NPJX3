using System;
using System.Collections.Generic;

public class EventCenter : Singleton<EventCenter>
{
    private static Dictionary<string, IEventData> eventDictionary = new Dictionary<string, IEventData>();

    public static void AddSingleEventListener<T>(string eventKey, Action<T> listener)
    {
        eventDictionary[eventKey] = new EventData<T>(listener);
    }
    
    public static void AddSingleEventListener(string eventKey, Action listener)
    {
        eventDictionary[eventKey] = new EventData(listener);
    }
    // 添加事件监听器
    public static void AddEventListener<T>(string eventKey, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventKey,out var preciousAction))
        {
            if (preciousAction is EventData<T> eventData)
            {
                eventData.Listeners += listener;
            }
        }
        else
        {
            eventDictionary.Add(eventKey,new EventData<T>(listener));
        }
    }

    public static void AddEventListener<T>(string eventKey, Action listener)
    {
        if (eventDictionary.TryGetValue(eventKey,out var preciousAction))
        {
            if (preciousAction is EventData eventData)
            {
                eventData.Listeners += listener;
            }
        }
        else
        {
            eventDictionary.Add(eventKey,new EventData(listener));
        }
    }

    // 移除事件监听器
    public static void RemoveEventListener<T>(string eventKey, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventKey, out var previousAction))
        {
            if (previousAction is EventData<T> eventData)
            {
                eventData.Listeners -= listener;
            }
        }
    }
    public static void RemoveEventListener(string eventKey, Action listener)
    {
        if (eventDictionary.TryGetValue(eventKey, out var previousAction))
        {
            if (previousAction is EventData eventData)
            {
                eventData.Listeners -= listener;
            }
        }
    }

    // 触发事件
    public static void TriggerEvent<T>(string eventKey, T eventData)
    {
        if (eventDictionary.TryGetValue(eventKey, out var previousAction))
        {
            (previousAction as EventData<T>)?.Listeners?.Invoke(eventData);
        }
    }
    public static void TriggerEvent(string eventKey)
    {
        if (eventDictionary.TryGetValue(eventKey, out var previousAction))
        {
            (previousAction as EventData)?.Listeners?.Invoke();
        }
    }
}

public interface IEventData{}

public class EventData<T> : IEventData
{
    public Action<T> Listeners;

    public EventData(Action<T> action)
    {
        Listeners += action;
    }
}

public class EventData : IEventData
{
    public Action Listeners;

    public EventData(Action action)
    {
        Listeners += action;
    }
}

public class EventKey
{
    public static readonly string SATURATION_CHANGE = "SATURATION_CHANGE";//饥饿值改变
    public static readonly string BATTLE_FINISHED = "BATTLE_FINISHED";//战斗结束
    //UI相关
    public static readonly string UI_OPEN_TREASURE = "UI_OPEN_TREASURE";//打开宝箱UI
    public static readonly string UI_OPEN_PLAYERBAG = "UI_OPEN_PLAYERBAG";//打开背包UI
    public static readonly string UI_OPEN_DROPS = "UI_OPEN_DROPS";//打开掉落物UI
    public static readonly string UI_TEAMDIALOGREFRESH = "UI_TEAMDIALOGREFRESH";//刷新队伍状态UI
    //创建UI
    public static readonly string UI_CREATE_CONFIRM = "UI_CREATE_CONFIRM";//创建地城进入UI
    //交互相关
    public static readonly string ENTER_INTERRACT_RANGE = "ENTER_INTERRACT_RANGE";//进入可交互物范围
    public static readonly string EXIT_INTERRACT_RANGE = "EXIT_INTERRACT_RANGE";//离开可交互物范围
    //测试相关
    public static readonly string TEST_DUNGEON_AMBUSH = "TEST_DUNGEON_AMBUSH";//测试——地城——遭遇埋伏
    public static readonly string TEST_DUNGEON_FINDHERB = "TEST_DUNGEON_FINDHERB";//测试——地城——发现草药
    public static readonly string TEST_DUNGEON_HIDDENARROW = "TEST_DUNGEON_HIDDENARROW";//测试——地城——暗箭袭击
}