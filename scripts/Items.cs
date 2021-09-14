using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using SaveD;


namespace ItemsSystem
{
    [Serializable]
    public class Items
    {

        public string nameItem;
        public float percentEffectItem;
        public byte wichEffectItem;
        public byte sizeItem;

    }




    [Serializable]
    public class Inventory
    {
        public string nameItem;
    }


    public class HandlerItems
    {

        /***********************************************************************************************************************************************************************************************************/
        // Construtor Vazio//
        public HandlerItems()
        {

        }

        public static string nameFile;
        public static List<Items> items;
        public static List<Items> itemsHolding;
        public static List<Inventory> inventory;
        


        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por pegar os dados no arquivo Items para serem passados para a classe Items //
        public static List<Items> GetHandlerItems() // puxa da minha classe de savesystem os dados salvos no arquivo de personagem //
        {
            nameFile = "Items.txt"; // set do nome do arquivo de estatus dos meus personagens //
            items = new List<Items>(); // instancia //

            inventory = SaveData.ReadFromJson<Inventory>("Inventory.txt"); // responsavel por puxar do arquivo salvo os dados para preecher minha classe MainStats // 
            itemsHolding = SaveData.ReadFromJson<Items>(nameFile); // responsavel por puxar do arquivo salvo os dados para preecher minha classe MainStats // 


            for (int c = 0; c < itemsHolding.Count; c++)
            {
                Debug.Log(c);

                for (int i = 0; i < inventory.Count; i++)
                {
                    if (itemsHolding[c].nameItem.Equals(inventory[i].nameItem))
                    {
                        items.Add(itemsHolding[c]);
                      
                    }
                }
            
            }
            itemsHolding = null;
            return items; // retorno //
        }

    }
}