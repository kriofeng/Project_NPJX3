using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManger : Singleton<SaveManger>
{
    public string currentArchiveName;

    public bool ApprentMonsterDataChanged = false;
    public ISaveable ApprentMonsterModel;

    public void PlayerIntervalSave()
    {
        if (ApprentMonsterDataChanged && ApprentMonsterModel!= null)
        {
            ApprentMonsterModel.Save();
            ApprentMonsterDataChanged = false;
        }
    }
}

public interface ISaveable
{
    void Save();
    void Load();
}

public interface ISaveableWithID : ISaveable
{
    void SetId(int id);
}
