using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using StatsMobSystem;
using MagicSystem;
using MainStatsSystem;
using MpHpControlSystem;
using ItemsSystem;

namespace MobControlSystem
{
    public class MobControl
    {



        public System.Random random;
        public List<StatsMob> stats;
        public List<Magics> magics;
        public List<MainStats> mainStats;
        public MpHpControl mpHpControl;

        public float dmg;
        public string atk;
        public int position;
        public int mobSelected;
        public float individualPercent;
        public bool isAtackMob;




        // Start is called before the first frame update
        public MobControl(List<StatsMob> statsMobs, List<Magics> magics, List<MainStats> mainStats, MpHpControl mpHpControl)
        {
            random = new System.Random(); // define minha instancia de numeros randomicos //
            stats = statsMobs; // Metodo da classe estatica HandlerStatsMob que puxa da minha classe savesystem os dados salvos dos mobs //
            this.magics = magics;
            this.mainStats = mainStats;
            this.mpHpControl = mpHpControl;
        }






        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por definir e controlar os ataques dos mobs //
        // no random number inicial o 0 significa ataque fisico e o 1 ataque magico //
        public void Atacks(int qtdCharacter) // metodo responsavel por controlar quais ataque serão executados no combate //
        {

            mobSelected = random.Next(stats.Count);
          //  Debug.Log(stats[mobSelected].paralyse);
            if(stats[mobSelected].paralyse)
            {
                Debug.Log("paralisado");
            }

            if (!stats[mobSelected].paralyse)
            {
                int atkPos;  // variavel que define qual ataque na lista ira ser execultado //

            float lifeCharacterTotal = 0; // Variavel criada para armazenar o total de vida de todos os personagens somados //

            List<float> percentAtk = new List<float>();


            for (int c = 0; c < qtdCharacter; c++)
            {
                lifeCharacterTotal += mainStats[c].life; // Somatoria dos pontos de vida de cada um dos personagens //
            }

            for (int c = 0; c < qtdCharacter; c++)
            {
                if (mainStats[c].life > 0)
                {
                    percentAtk.Add(100 * mainStats[c].life / lifeCharacterTotal);
                }
                else
                {
                    percentAtk.Add(0);
                }
            }

            float percent = (float)random.Next(1, 101); // define na variavel percent um numero randomico de 1 a 100 //

            for (int c = 0; c < qtdCharacter; c++)
            {

                if (c == 0)
                {

                    if (percentAtk[c] >= percent && !mainStats[c].isDead)
                    {

                        position = c; // randomicamente escolhe um personagem a ser atacado pegando o valor passado do LevelController que define quantos personagens há em campo //

                    }
                }
                else
                {
                    if (percentAtk[c - 1] < percent && percentAtk[c] + percentAtk[c - 1] >= percent && !mainStats[c].isDead)
                    {
                        position = c; // randomicamente escolhe um personagem a ser atacado pegando o valor passado do LevelController que define quantos personagens há em campo //
                    }
                }

            }
        //    Debug.Log(position);


            atkPos = random.Next(stats[mobSelected].attack.Count); // define qual ataque sera execultado pegando uma posição especifica no list de status e verificando quantos ataques existem no list //
            atk = stats[mobSelected].magics[atkPos];  // pega o nome do ataque //
          

                foreach (Magics m in magics)
                {
                    if (m.magicName.Equals(atk)) // se o nome da magia dentro da lista de magias for igual ao ataque escolhido aleatoriamente //
                    {
                        dmg = m.dmgMagic; // define a variavel dmg com o valor de dano da magia escolhida que esta xontida dentro da classe Magics //
                        mainStats[position].HpControl(dmg, m.typeMagic); // chama o metodo de HpControl dento da classe de MainStats e passa o dano e o tipo de dano para que seja feita os calculos para se alterar a vida do personagem sendo atacado //
                        mpHpControl.SetHp(mainStats[position].life, position, mainStats[position].maxLife, 0); // 
                    }
                    else
                    {
                        // necessario fazer na lista de magias ataques fisicos //
                        //  Debug.Log("nao encontrado");
                    }

                }
                isAtackMob = true;
            }
        }


    }
}
