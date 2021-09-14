using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveD;
using MainStatsSystem;
using ButtonSystem;
using MpHpControlSystem;
using ItemsSystem;
using MagicSystem;
using MobControlSystem;
using StatsMobSystem;
using AnimationSystem;

namespace CombatControlSystem
{
    public class CombatControl
    {

        public List<MainStats> mainStats;
        public List<StatsMob> statsMob;
        public List<Items> items;
        public List<Magics> magics;
        public MpHpControl hp;



        public int mobSelect;
        public int charSelect;
        public bool isAtack;
        public bool isAtackChar;
        public bool coolDown;

        /***********************************************************************************************************************************************************************************************************/
        // Construtor que recebe MpHpControl
        public CombatControl(MpHpControl hp, List<MainStats> main, List<Items> items, List<Magics> magics, List<StatsMob> stats)
        {
            mainStats = main; // instancia //
            statsMob = stats;
            mobSelect = 0; // Responsavel por iniciar a variavel como 0 inicialmente para que há um mob selecionado sem o jogador selecionar algum mob //
            charSelect = 0;
            this.magics = magics;
            this.hp = hp;
            this.items = items;
            isAtackChar = false;
            isAtack = false;
            coolDown = false;
            this.items = items;
        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo que retorna o mainstats recebido do levelController  //
        // Este metodo é chamado na classe de button no metodo ButtonCreate //
        public List<MainStats> GetMainStats()
        {
            return mainStats;
        }


        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por definir os danos e magias que serão executados //
        // Tambem é responsavel por passar para a classe  MpHpControl os danos e mob selecionado para ser aplicado o dano //
        public void AtackMagic(string magicName)
        {
            if (!isAtack && !coolDown && !mainStats[charSelect].paralyse)
            {
                foreach (Magics i in magics)
                {

                    if (i.magicName.Equals(magicName))
                    {
                        if (i.manaCost <= mainStats[charSelect].mp)
                        {
                            if (i.typeMagic == 0) // Efeito 0 significa healing de Vida //
                            {
                                mainStats[charSelect].HpControl(i.dmgMagic, i.typeMagic); // Repsponsavel por chamar o metodo da classe MainStats que controla o hp (neste caso uma magia de cura estaria sendo utilizada) é passado para o metodo o quanto é curado e o tipo de magia (neste caso 0) //
                                mainStats[charSelect].MpControl(i.manaCost); // Repsponsavel por chamar o metodo da classe MainStats que controla o hp (neste caso uma magia de cura estaria sendo utilizada) é passado para o metodo o quanto é curado e o tipo de magia (neste caso 0) //
                                hp.SetHp(mainStats[charSelect].life, charSelect, mainStats[charSelect].maxLife, 0); // Responsavel por Chamar o metodo de SetHp dentro da classe MpHpControl que define as variaveis de vidas dentro da classe e depois chama o metodo que controla a UI barra de vida que ira redimensionar o tamanho da barra //
                                hp.SetMp(mainStats[charSelect].mp, charSelect, mainStats[charSelect].maxMp); // Responsavel por Chamar o metodo de SetHp dentro da classe MpHpControl que define as variaveis de vidas dentro da classe e depois chama o metodo que controla a UI barra de vida que ira redimensionar o tamanho da barra //
                            }
                            if (i.typeMagic == 1) // Efeito 1 significa healing de Mana //
                            {

                            }
                            Debug.Log("atack");
                            statsMob[mobSelect].SetHp(i.dmgMagic, i.typeMagic); // Responsavel por Chamar o metodo de SetHp dentro da classe MpHpControl que define as variaveis de vidas dentro da classe e depois chama o metodo que controla a UI barra de vida que ira redimensionar o tamanho da barra //
                            mainStats[charSelect].MpControl(i.manaCost); // Repsponsavel por chamar o metodo da classe MainStats que controla o hp (neste caso uma magia de cura estaria sendo utilizada) é passado para o metodo o quanto é curado e o tipo de magia (neste caso 0) //
                            hp.SetHp(statsMob[mobSelect].life, mobSelect, statsMob[mobSelect].maxLife, 1); // Responsavel por Chamar o metodo de SetHp dentro da classe MpHpControl que define as variaveis de vidas dentro da classe e depois chama o metodo que controla a UI barra de vida que ira redimensionar o tamanho da barra //
                            hp.SetMp(mainStats[charSelect].mp, charSelect, mainStats[charSelect].maxMp); // Responsavel por Chamar o metodo de SetHp dentro da classe MpHpControl que define as variaveis de vidas dentro da classe e depois chama o metodo que controla a UI barra de vida que ira redimensionar o tamanho da barra //
                            isAtackChar = true;
                            coolDown = true;
                        }
                        else { Debug.Log("sem mana"); }
                    }
                }
            }

        }


        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por definir o mob selecionado para ataque //
        // Este metodo é chamado na classe de LevelController e recebe o index de mob que foi selecionado //
        public void SetMobSelect(int mobSelect)
        {
            this.mobSelect = mobSelect;
        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por definir o mob selecionado para ataque //
        // Este metodo é chamado na classe de LevelController e recebe o index de personagem que foi selecionado //
        public void SetCharSelect(int charSelect)
        {
            this.charSelect = charSelect;
        }


        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por usar itens //
        // Este metodo é chamado na classe de Button no metodo onClickItems() //
        public void UseItems(string itemName)
        {
            coolDown = true;
            foreach (Items i in items)
            {
                if (i.nameItem.Equals(itemName))
                {
                    if (i.wichEffectItem == 0) // Efeito 0 significa healing de Vida //
                    {
                        mainStats[charSelect].ItemControl(i.percentEffectItem, i.wichEffectItem);
                        hp.SetHp(mainStats[charSelect].life, charSelect, mainStats[charSelect].maxLife, 0);
                    }
                    if (i.wichEffectItem == 1) // Efeito 1 significa healing de Mana //
                    {
                        mainStats[charSelect].ItemControl(i.percentEffectItem, i.wichEffectItem);
                        hp.SetMp(mainStats[charSelect].mp, charSelect, mainStats[charSelect].maxMp);
                    }
                }
            }
        }

        public void SetIsAtack(bool isAtack)
        {
            this.isAtack = isAtack;

        }




    }



}
