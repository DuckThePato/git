using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;
using SaveD;

namespace Act

{
    [Serializable]
    public static class HandlerAct
    {

        // Start is called before the first frame update
        public static void Start()
        {

        }



        //é necessario ter uma metodo que transforma meu Json em Dados para meu objeto devido a não ter necessidade da classe magias ser uma coleção// 
        public static string FromJsonFile(string fileName) // Le o arquivo //
        {
            string content = SaveData.ReadFile(fileName);
            return content;
         
        }
    }
    [Serializable]
    public class Magias
    {
        public int fisico;
        public int thunder;
        public int fireball;
        public bool area_escorregadia;
        public int drenar_forca_vital;
        public int resistencia;
        public int martelo_do_caos;

        public Magias GetMagias()
        {
            Magias magic = new Magias();
            magic = JsonUtility.FromJson<Magias>(HandlerAct.FromJsonFile("ListaAtaques.txt"));
            return magic;
        }
    }
}
