using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySummonManager : MonoBehaviour
{
    public List<SummonHolder> SummonHolders;
    
    public void SetCurrent(SummonsController summon)
    {
        print(summon);
        SummonHolders[2].SetPrice(summon.GetBloodCost().ToString());
        SummonHolders[2].SetImage(summon.GetComponentInChildren<SpriteRenderer>().sprite);
    }
    
    public void SetPrevious(SummonsController summon)
    {
        SummonHolders[0].SetImage(summon.GetComponentInChildren<SpriteRenderer>().sprite);
        SummonHolders[0].SetPrice(summon.GetBloodCost().ToString());
    }
    
    public void SetNext(SummonsController summon)
    {
        SummonHolders[1].SetImage(summon.GetComponentInChildren<SpriteRenderer>().sprite);
        SummonHolders[1].SetPrice(summon.GetBloodCost().ToString());
    }
    
    
}
