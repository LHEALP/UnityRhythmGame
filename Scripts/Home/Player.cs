using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public string ClassName { set; get; }

    public int PlayerClass { set; get; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



    /*
    public void SetClass()
    {
        bool isAlreadyJoin = false;

        // 등록여부 체크 및 등록
        switch (PlayerClass)
        {
            case 0:
                isAlreadyJoin = IsAlreadyJoin("SmartIT");
                SetPlayerprefs(isAlreadyJoin, "SmartIT", 0);
                ClassName = PlayerPrefs.GetString("SmartIT");
                break;
            case 1:
                isAlreadyJoin = IsAlreadyJoin("Drone");
                SetPlayerprefs(isAlreadyJoin, "Drone", 1);
                ClassName = PlayerPrefs.GetString("Drone");
                break;
            case 2:
                isAlreadyJoin = IsAlreadyJoin("Security");
                SetPlayerprefs(isAlreadyJoin, "Security", 2);
                ClassName = PlayerPrefs.GetString("Security");
                break;
            case 3:
                isAlreadyJoin = IsAlreadyJoin("CarSoft");
                SetPlayerprefs(isAlreadyJoin, "CarSoft", 3);
                ClassName = PlayerPrefs.GetString("CarSoft");
                break;
            case 4:
                isAlreadyJoin = IsAlreadyJoin("Display");
                SetPlayerprefs(isAlreadyJoin, "Display", 4);
                ClassName = PlayerPrefs.GetString("Display");
                break;
            case 5:
                isAlreadyJoin = IsAlreadyJoin("Music");
                SetPlayerprefs(isAlreadyJoin, "Music", 5);
                ClassName = PlayerPrefs.GetString("Music");
                break;
            case 6:
                isAlreadyJoin = IsAlreadyJoin("Beauty");
                SetPlayerprefs(isAlreadyJoin, "Beauty", 6);
                ClassName = PlayerPrefs.GetString("Beauty");
                break;
            case 7:
                isAlreadyJoin = IsAlreadyJoin("Media");
                SetPlayerprefs(isAlreadyJoin, "Media", 7);
                ClassName = PlayerPrefs.GetString("Media");
                break;
            case 8:
                isAlreadyJoin = IsAlreadyJoin("HotelAir");
                SetPlayerprefs(isAlreadyJoin, "HotelAir", 8);
                ClassName = PlayerPrefs.GetString("HotelAir");
                break;
            default:
                break;
        }
    }

    void SetPlayerprefs(bool isAlreadyJoin, string playerClass, int playerClassValue)
    {
        if (isAlreadyJoin.Equals(false))
            PlayerPrefs.SetInt(playerClass, playerClassValue);
    }

    public bool IsAlreadyJoin(string playerClass)
    {
        return PlayerPrefs.HasKey(playerClass);
    }
    */
}
