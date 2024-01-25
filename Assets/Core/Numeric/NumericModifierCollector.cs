using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class NumericModifier
{
    public float value;

    public NumericModifier(int value)
    {
        this.value = value;
    }

    public NumericModifier(float value)
    {
        this.value = value;
    }
}

public class NumericAddModifierColler
{
    public float TotalValue { get; private set; }
    private List<NumericModifier> Modifiers { get; } = new List<NumericModifier>();

    public float AddModifier(NumericModifier modifier)
    {
        Modifiers.Add(modifier);
        Update();
        return TotalValue;
    }

    public float RemovaModifier(NumericModifier modifier)
    {
        Modifiers.Remove(modifier);
        Update();
        return TotalValue;
    }

    public float Update()
    {
        TotalValue = 0;
        for (int i = 0; i < Modifiers.Count; i++)
        {
            TotalValue += Modifiers[i].value;
        }

        return TotalValue;
    }
}
public class NumericSubModifierColler
{
    public float TotalValue { get; private set; }
    private List<NumericModifier> Modifiers { get; } = new List<NumericModifier>();

    public float AddModifier(NumericModifier modifier)
    {
        Modifiers.Add(modifier);
        Update();
        return TotalValue;
    }

    public float RemovaModifier(NumericModifier modifier)
    {
        Modifiers.Remove(modifier);
        Update();
        return TotalValue;
    }

    public float Update()
    {
        TotalValue = 1;
        for (int i = 0; i < Modifiers.Count; i++)
        {
            TotalValue *= (1 + Modifiers[i].value);
        }
        return TotalValue;
    }
}

public class Numeric
{
    public float Value { get; private set; }
    public float baseValue { get; private set; }
    public float pctModify { get; private set; }
    public float fixModify { get; private set; }
    public float gloModify { get; private set; }

    private NumericSubModifierColler PctModiCollector { get; } = new NumericSubModifierColler();
    private NumericAddModifierColler FixModiCollector { get; } = new NumericAddModifierColler();
    private NumericSubModifierColler GloModiCollector { get; } = new NumericSubModifierColler();

    public void Initialize()
    {
        Value = baseValue =  fixModify = gloModify = 0;
        pctModify = gloModify = 1;
    }

    public Numeric(float value)
    {
        Initialize();
        baseValue = value;
        Update();
    }

    public Numeric(int value)
    {
        Initialize();
        baseValue = value;
        Update();
    }

    public void ChangeByMode(NumericModifier modifier, int NumMdoeID, bool Addflag)
    {
        ChangeByMode(modifier, (NumericMode)NumMdoeID, Addflag);
        
    }
    public void ChangeByMode(NumericModifier modifier, NumericMode NumMdoe, bool Addflag)
    {
        switch (NumMdoe)
        {
            case NumericMode.Fixed:
            {
                if(Addflag)
                    AddFixModiCollector(modifier);
                else
                    RemoveFixModiCollector(modifier);
                break;
            }
            case NumericMode.Percent:
            {
                if(Addflag)
                    AddPctModiCollector(modifier);
                else
                    RemovePctModiCollector(modifier);
                break;
            }
            case NumericMode.Global:
            {
                if(Addflag)
                    AddGloModiCollector(modifier);
                else
                    RemoveGloModiCollector(modifier);
                break;
            }
        }
    }

    public void AddPctModiCollector(NumericModifier modifier)
    {
        pctModify = PctModiCollector.AddModifier(modifier);
        Update();
    }
    public void RemovePctModiCollector(NumericModifier modifier)
    {
        pctModify = PctModiCollector.RemovaModifier(modifier);
        Update();
    }
    public void AddFixModiCollector(NumericModifier modifier)
    {
        fixModify = FixModiCollector.AddModifier(modifier);
        Update();
    }
    public void RemoveFixModiCollector(NumericModifier modifier)
    {
        fixModify = FixModiCollector.RemovaModifier(modifier);
        Update();
    }
    public void AddGloModiCollector(NumericModifier modifier)
    {
        gloModify = GloModiCollector.AddModifier(modifier);
        Update();
    }
    public void RemoveGloModiCollector(NumericModifier modifier)
    {
        gloModify = GloModiCollector.RemovaModifier(modifier);
        Update();
    }

    public void Update()
    {
        Value = ((baseValue * pctModify) + fixModify) * gloModify;
    }

    public void ModifierChange(int mode)
    {
        ModifierChange((NumericMode)mode);
    }
    public void ModifierChange(NumericMode mode)
    {
        switch (mode)
        {
            case NumericMode.Fixed:
                fixModify = FixModiCollector.Update();
                break;
            case NumericMode.Global:
                gloModify = GloModiCollector.Update();
                break;
            case NumericMode.Percent:
                pctModify = PctModiCollector.Update();
                break;
        }
        Update();
    }
    
    public enum NumericMode
    {
        Percent = 1,
        Fixed = 2,
        Global = 3
    }
}
