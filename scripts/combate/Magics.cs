using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveD;

namespace MagicSystem
{
    [System.Serializable]
    public class Magics
    {

        public Magics(){}

        public string magicName;
        public float dmgMagic;
        public int typeMagic;
        public int distanceMagic;
        public float manaCost;


    }

    public class HandlerMagics
    {

        /***********************************************************************************************************************************************************************************************************/
        // Construtor Vazio//
        public HandlerMagics()
        {

        }

        public static string nameFile;
        public static List<Magics> magics;
        public static List<Magics> magicsHolding;



        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por pegar os dados no arquivo Magics para serem passados para a classe Magics //
        public static List<Magics> GetHandlerMagics(List<string> nameMagic) // puxa da minha classe de savesystem os dados salvos no arquivo de personagem //
        {
            nameFile = "MagicsList.txt"; // set do nome do arquivo de estatus dos meus personagens //
            magics = new List<Magics>(); // instancia //
            magicsHolding = new List<Magics>();
            magicsHolding = SaveData.ReadFromJson<Magics>(nameFile); // responsavel por puxar do arquivo salvo os dados para preecher minha classe MainStats // 

            // Este looping é reponsavel por passar em todos os elementos da minha lista de magias //
            // magicHolding é um objeto criado apenas para armazenar a lista de magias inteira do arquivo MagicList //
            // Este looping é necessario para que seja feito a seleção de quais posições no list do magicHolding serão de fato utilizado para serem passados para o objeto magics //
            for (int c = 0; c < magicsHolding.Count; c++)
            {
                // nameMagic é recebido da classe LevelController e contem as magias que cada um dos pers. tem //
                // Este looping é responsavel por passa em todos os objetos dentro do list de nameMagic //
                for (int i = 0; i < nameMagic.Count; i++)
                {
                    if (magicsHolding[c].magicName.Equals(nameMagic[i])) // verifica se o nome da magia dentro do objeto magicsHolding[c] é igual ao nameMagic[i]
                    {
                        magics.Add(magicsHolding[c]); // Adiciona os objetos de magicsHolding[c] dentro de magics desta forma magics contem apenas as magias que serao utilizadas na batalha // 
                    }
                }

            }
            magicsHolding = null; // Define magicsHolding como nulo ja que nao ha necessidade de utiliza-lo novamente //
            return magics; // retorno //
        }

    }
}