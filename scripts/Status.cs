using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SaveD;


[Serializable]
public class Status
{

    public int position;
    public int hp, mp, str, res;
    public double affect, xp;
    


    public void SaveDataJson() // Metodo que acessa a classe savedata pedindo para salvar os dados desta classe //
    {
      /*  List<Status> stats = new List<Status>();
        PopulateSaveData(stats);
        SaveData.SaveJson<Status>(stats);*/

    }
    public void PopulateSaveData(List<Status> stats) // metodo feito para adicionar dados a minha coleção de Status //
    {
        stats.Add(new Status()
        {
        position = 1,
        hp = 10,
        mp = 10,
        str = 10,
        res = 10,
        affect = 10.0,
        xp = 10.0
        });

        stats.Add(new Status()
        {
            position = 2,
            hp = 9,
            mp = 9,
            str = 9,
            res = 9,
            affect = 9.0,
            xp = 9.0
        });

        stats.Add(new Status()
        {
            position = 3,
            hp = 9,
            mp = 9,
            str = 9,
            res = 9,
            affect = 9.0,
            xp = 9.0
        });

    }
 

    public  void LoadDataJson()
    {
     /*   List<Status> stats = new List<Status>();
       stats = SaveData.ReadFromJson<Status>();
        Debug.Log(stats[1].hp);*/

    }
   
}
