using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;
using SaveD;

namespace StatsMobSystem 
{

    [System.Serializable]
    public class StatsMob : ICloneable
    {
        public StatsMob()
        {

        }

        public string mob;
        public float life;
        public float mp;
        public int lv;
        public List<string> attack;
        public List<string> magics;
        public List<int> res;
        public bool isDead;
        public float maxLife;
        public bool paralyse;
        public bool bleeding;
        public bool poison;



        /***********************************************************************************************************************************************************************************************************/
        // Metodo que controla o Hp //
        // recebe o dano e o tipo de dano //
        public void SetHp(float dmg, int type)
        {
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


        /***********************************************************************************************************************************************************************************************************/
        // Define qual a vida maxima dos personagens ja que a variavel life ira ser modificada com a vida atual //
        public void SetMaxLife()
        {
            maxLife = life;
        }

        public void SetIsDead(bool isDead)
        {
            this.isDead = isDead;
        }

        public void SetDeBuff()
        {
            paralyse = false;
            bleeding = false;
            poison = false;
        }

        public void BleedDamage(float dmg)
        {
            life -= dmg;
        }


        public void PosionDamage(float dmg)
        {
            life -= dmg;
        }


        // metodo vindo da interface de Icloneable necessario para que a classe quando fazer referencia a um mesmo objeto poder ser alocada em outro endereco de memoria //
        public object Clone()
        {
            StatsMob novo = new StatsMob();
 

            novo.mob = mob;
            novo.life = life;
            novo.mp = mp;
            novo.lv = lv;
            novo.attack = attack;
            novo.magics = magics;
            novo.res = res;
            novo.isDead = isDead;
            novo.maxLife = maxLife;
            novo.paralyse = paralyse;
            novo.bleeding = bleeding;
            novo.poison = poison;
            return novo;
        }
    }

  


public static class HandlerStatsMob
    {
        public static List<StatsMob> stats;
        public static List<StatsMob> statsHolding;
        public static string nameFile;

        public static List<StatsMob> GetHandlerStats() // puxa da minha classe de savesystem os dados salvos no arquivo de personagem //
        {
            nameFile = "ListaMobEst.txt"; // set do nome do arquivo de estatus dos meus personagens //
            stats = new List<StatsMob>(); // instancia //
            statsHolding = SaveData.ReadFromJson<StatsMob>(nameFile); // responsavel por puxar do arquivo salvo os dados para preecher minha classe MainStats //

            for (int c = 0; c < 2; c++)
            {
                stats.Add((StatsMob)statsHolding[0].Clone());
            }


        for (int c = 0; c < stats.Count; c++)
            {
                stats[c].isDead = false;
                stats[c].SetMaxLife();
                stats[c].SetDeBuff();
            }
            statsHolding = null;
            return stats; // retorno //
        }
    }


}

