using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    Judgement judgement;

    // Start is called before the first frame update
    void Start()
    {
        judgement = FindObjectOfType<Judgement>();
    }

    public void Down()
    {
        judgement.judgeTimeFromUserSetting += 100;
    }

    public void Up()
    {
        judgement.judgeTimeFromUserSetting -= 100;
    }
}
