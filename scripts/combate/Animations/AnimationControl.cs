using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationSystem
{
    public  class AnimationControl
    {


        public AnimationControl(List<GameObject> characterObject,List<GameObject> mobObject)
        {
            this.characterObject = characterObject;
            this.mobObject = mobObject;
            characterVector = new List<Vector3>();
            mobVector = new List<Vector3>();
            animationTime = 1.9f;
            foreach (GameObject gm in characterObject)
            {
                characterVector.Add(gm.transform.localPosition); // Este campo é necessario para que as posicoes iniciais do personagens sejam salvos para que seja possivel  retornarem ao ponto inicial //
            }

            foreach (GameObject gm in mobObject)
            {
                mobVector.Add(gm.transform.localPosition); // Este campo é necessario para que as posicoes iniciais do personagens sejam salvos para que seja possivel  retornarem ao ponto inicial //
            }
            animationStart = false;
        }

        public  List<GameObject> characterObject;
        public  List<GameObject> mobObject;
        public  List<Vector3> characterVector;
        public  List<Vector3> mobVector;
        public  float timeElapsed;
        
        public RectTransform rect, rectMob;
        public bool animationStart;
        public bool animationStartChar;
        public float animationTime;


        /*
         * Metodo responsavel por controlar a aproximacao dos personagens ate os mobs 
         * O indice do metodo recebe qual personagem esta selecionado juntamente do mob selecionado 
         * Este metodo e chamado na classe LevelController 
         */
        public void LerpCharacterObject(int posChar, int posMob)
        {
            if (timeElapsed < 1.5f) // Condicao que verifica o se o tempo passado e menor que 1.5 //
            {
                animationStart = true; // Define que a animacao de lerp comecou //
                rect = (RectTransform)characterObject[posChar].transform; // Pega as informacoes de tamanho do personagem selecionado //
                rectMob = (RectTransform)mobObject[posMob].transform; // Pega as informacoes de tamanho do mob selecionado //
                timeElapsed += Time.deltaTime; // Somatoria do tempo passado a cada frame //
                /*
                 * Lerp que define a posicao inicial sendo a posicao do personagem e a posicao final sendo a posicao do mob selecionado calculando o set de posicao para a borda esquerda do mob mais um offset 
                */
                characterObject[posChar].transform.localPosition = new Vector2(Mathf.Lerp(characterObject[posChar].transform.localPosition.x, mobObject[posMob].transform.localPosition.x - rect.sizeDelta.x / 2 - rectMob.sizeDelta.x / 2 - 20, timeElapsed / 0.3f), Mathf.Lerp(characterObject[posChar].transform.localPosition.y, mobObject[posMob].transform.localPosition.y, timeElapsed / 0.3f));
            }
            else if (timeElapsed >= 1.5f && timeElapsed < 1.9f)
            {
                timeElapsed += Time.deltaTime;// Somatoria do tempo passado a cada frame //

                /*
               * Lerp que define a posicao inicial sendo a posicao do personagem e a posicao final sendo a posicao do persongem inicial 
              */
                characterObject[posChar].transform.localPosition = new Vector2(Mathf.Lerp(characterObject[posChar].transform.localPosition.x, characterVector[posChar].x, timeElapsed / 1.9f), Mathf.Lerp(characterObject[posChar].transform.localPosition.y, characterVector[posChar].y, timeElapsed / 1.9f));
            }
            else if (timeElapsed >= 1.9f)
            {
                timeElapsed = 0;
                animationStart = false;
            }

        }





        /*
      * Metodo responsavel por controlar a aproximacao dos personagens ate os mobs 
      * O indice do metodo recebe qual personagem esta selecionado juntamente do mob selecionado 
      * Este metodo e chamado na classe LevelController 
      */
        public void LerpMobObject(int posChar, int posMob, float coolDownMob)
        {
            if (timeElapsed < 1.5f) // Condicao que verifica o se o tempo passado e menor que 1.5 //
            {
                animationStart = true; // Define que a animacao de lerp comecou //
                rect = (RectTransform)characterObject[posChar].transform; // Pega as informacoes de tamanho do personagem selecionado //
                rectMob = (RectTransform)mobObject[posMob].transform; // Pega as informacoes de tamanho do mob selecionado //
                timeElapsed += Time.deltaTime; // Somatoria do tempo passado a cada frame //

                /*
                 * Lerp que define a posicao inicial sendo a posicao do personagem e a posicao final sendo a posicao do mob selecionado calculando o set de posicao para a borda esquerda do mob mais um offset 
                */
                mobObject[posMob].transform.localPosition = new Vector2(Mathf.Lerp(mobObject[posMob].transform.localPosition.x, characterObject[posChar].transform.localPosition.x + rect.sizeDelta.x / 2 + rectMob.sizeDelta.x / 2 + 20, timeElapsed / 0.3f), Mathf.Lerp(mobObject[posMob].transform.localPosition.y, characterObject[posChar].transform.localPosition.y, timeElapsed / 0.3f));
            }
            else if (timeElapsed >= 1.5f && timeElapsed < 1.9f)
            {
                timeElapsed += Time.deltaTime;// Somatoria do tempo passado a cada frame //

                /*
               * Lerp que define a posicao inicial sendo a posicao do personagem e a posicao final sendo a posicao do persongem inicial 
              */
                mobObject[posMob].transform.localPosition = new Vector2(Mathf.Lerp(mobObject[posMob].transform.localPosition.x, mobVector[posMob].x, timeElapsed / 1.9f), Mathf.Lerp(mobObject[posMob].transform.localPosition.y, mobVector[posMob].y, timeElapsed / 1.9f));
            }
            else if (timeElapsed >= 1.9f)
            {
                animationStart = false;
                timeElapsed = 0;
           
            }

        }


    }
}