using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using SaveD;

namespace MainStatsSystem
{

    [Serializable]
    public class MainStats
    {

        /***********************************************************************************************************************************************************************************************************/
        // construtor vazio //
        public MainStats()
        {

        }

        public string name;
        public float life;
        public float mp;
        public int xp;
        public float str;
        public List<byte> res;
        public List<string> magics;
        public List<string> golpes;
        public List<string> items;
        public bool select;
        public bool isDead;
        public float maxLife;
        public float maxMp;
        public bool paralyse;
        public bool bleeding;
        public bool poison;
        public bool isRes;


        /***********************************************************************************************************************************************************************************************************/
        // Metodo que controla o Hp //
        // recebe o dano e o tipo de dano //
        public void HpControl(float dmg, int type)
        {
            if (type == 0) // 0 significa healing //
            {
                life += dmg;
                if (life > maxLife)
                {
                    life = maxLife;
                }
            }

            // 2 =dano eletrico || 3 = dano de fogo || 4 = dano de gelo || 6 = sangramento || 7 = veneno || 9 = dano fisico // 

            switch (type)
            {
                case 0:
                    life += dmg;
                    if (life > maxLife)
                    {
                        life = maxLife;
                    }
                    break;

                case 2:
                    life -= dmg;
                    break;

                case 3:
                    life -= dmg;
                    break;

                case 4:
                    life -= dmg;
                    break;

                case 5:
                    paralyse = true;
                    break;

                case 6:
                    bleeding = true;
                    break;

                case 7:
                    poison = true;
                    break;

                case 8:
                    
                    break;

                case 9:
                    life -= dmg;
                    break;

            }

            if (life < 0)
            {
                life = 0;
                isDead = true;
            }
        }


        public void MpControl(float mpCost)
        {
            mp -= mpCost;
        }




        /***********************************************************************************************************************************************************************************************************/
        // Metodo que controla o efeito dos itens //
        // Recebe a porcentagem dos efeitos e o tipo de efeito do item //
        public void ItemControl(float per, int type)
        {

            if (type == 0) // 0 significa healing //
            {
                life += per * maxLife / 100; // Regra de tres feita para calcular a porcentagem de quanto sera curado (a porcentagem é calculada em relação a vida maxima)

                if (life > maxLife) // Condição que verifica se a vida não é maior que a vida maxima //
                {
                    life = maxLife;
                }
            }
            if (type == 1) // 1 significa mana Restore //
            {
                mp += per * maxMp / 100; // Regra de tres feita para calcular a porcentagem de quanto sera curado (a porcentagem é calculada em relação a vida maxima)

                if (mp > maxMp) // Condição que verifica se o mp não é maior que a vida maxima //
                {
                    mp = maxMp;
                }
            }
            if (type == 2) // 2 significa resistencia contra sangramento //
            {
                for (int c = 0; c < res.Count; c++)
                {
                    if (res[c] == 6)
                    {
                        isRes = true;
                    }
                }
                if (!isRes)
                {
                    res.Add(6);
                    isRes = false;
                }
            }

        }




        /***********************************************************************************************************************************************************************************************************/
        // Define qual a vida maxima dos personagens ja que a variavel life ira ser modificada com a vida atual //
        public void SetMax()
        {
            maxLife = life;
            maxMp = mp;
        }

        public void SetDeBuff()
        {
            paralyse = false;
            bleeding = false;
            poison = false;
        }

    }




    public static class HandlerStats
    {

        public static string nameFile;
        public static List<MainStats> mainStats;
        public static List<MainStats> mainStatsHandler;


        /***********************************************************************************************************************************************************************************************************/
        // Puxa da minha classe de savesystem os dados salvos no arquivo de personagem //
        public static List<MainStats> GetHandlerStats()
        {
            nameFile = "ListaMainEst.txt"; // set do nome do arquivo de estatus dos meus personagens //
            mainStats = new List<MainStats>(); // instancia //
            mainStatsHandler = new List<MainStats>(); // instancia //
            mainStatsHandler = SaveData.ReadFromJson<MainStats>(nameFile); // responsavel por puxar do arquivo salvo os dados para preecher minha classe MainStats // 
            foreach (MainStats m in mainStatsHandler)
            {
                if (m.select)
                {
                    mainStats.Add(m);
                }
            }
            foreach (MainStats m in mainStats)
            {
                m.isDead = false;
                m.SetMax();
                m.SetDeBuff();
                //Debug.Log(m.name);
            }


            return mainStats; // retorno //
        }

    }
}