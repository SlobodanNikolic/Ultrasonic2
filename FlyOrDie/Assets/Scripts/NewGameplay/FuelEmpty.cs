using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelEmpty : MonoBehaviour {
    public GameObject QRScanner;
    public GameObject Map;
    
    public void OpenScanner()
    {
        QRScanner.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OpenMap()
    {

    }
}
