using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketTabs : MonoBehaviour {
    public GameObject Tab1;
    public GameObject Tab2;
    public GameObject CarShop;
    public GameObject CarRepair;

    public void SelectTab1()
    {
        Tab1.SetActive(true);
        Tab2.SetActive(false);
        CarShop.SetActive(true);
        CarRepair.SetActive(false);
    }
    public void SelectTab2()
    {
        Tab1.SetActive(false);
        Tab2.SetActive(true);
        CarShop.SetActive(false);
        CarRepair.SetActive(true);
    }
    public void Close()
    {

    }
}
