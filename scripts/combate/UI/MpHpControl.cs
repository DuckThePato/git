using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainStatsSystem;
using StatsMobSystem;


namespace MpHpControlSystem
{
    public class MpHpControl 
    {

        public float hp;
        public float maxHp;
  
        public float actualLifeMob;
        public float fullLifeMob;

        public int offsetLifeBar;

        public List<bool> isDead;
        public int qtdCharacterLive;

        public Texture lifeBar, frame;
        public Image imageHpBar;
        public Sprite spriteHpBar;


        public List<GameObject> hpBar, border, canvasGameObject, characters, mobs;
        public List<GameObject> mpBar, borderMp, canvasGameObjectMp;
        public GameObject gameObjectParent;
        


        private RectTransform rectTransformCharacter , rectTransformHpBar, rectTransformCanvas;

        public Vector2 vectorBar;
        public Vector2 vectorMpBar;
        public List<Vector2> vectorCanvas, vectorCharacter;
        public List<Vector2> vectorCanvasMp;
        private Canvas canvas;



        /***********************************************************************************************************************************************************************************************************/

        public MpHpControl(List<GameObject> characters, List<GameObject> mobs)
        {
            this.characters = characters; // passa para o objeto de List<GameObject> um objeto de mesmo tipo //
            this.mobs = mobs;  // passa para o objeto de List<GameObject> um objeto de mesmo tipo //
            isDead = new List<bool>();
            for (int c = 0; c < characters.Count; c++)
            {
                isDead.Add(false);
            }
            offsetLifeBar = 25; // offset de distansiamento das bordas do canvas principal //
            InstantiateHpBar(); // cria as barras de hp //
            InstantiateMpBar();

            qtdCharacterLive = characters.Count;
        }





        /***********************************************************************************************************************************************************************************************************/

        public void SetHp(float hp, int pos, float maxHp,int who)
        {
            this.maxHp = maxHp;
            this.hp = hp;
            actualLifeMob = hp;
            fullLifeMob = maxHp;
            HpControl(pos,who);
        }






        /****************************************************************************************/
        /****************************************************************************************/
        // Metodo que puxa do meu handlerstats os dados do personagem e pega o MP para inserir na variavel fullMana //
        /****************************************************************************************/
        /****************************************************************************************/
        /***********************************************************************************************************************************************************************************************************/

        public void SetMp(float mp, int pos, float maxMp)
        {
            MpControlBar(pos, mp, maxMp);
        }






        /***********************************************************************************************************************************************************************************************************/
        
        public void InstantiateHpBar()
        {
            int i = 0; // variavel necessaria para definir posição dos objetos enemy ja que é necessario começar com 0 sendo assim não podendo se utilizar da variavel c dentro do looping//


            hpBar = new List<GameObject>();
            border = new List<GameObject>();
            
          
            canvasGameObject = new List<GameObject>(); // instancia do canvas de Hp //
            vectorCanvas = new List<Vector2>(); 
            vectorCharacter = new List<Vector2>(); 



            /**********************************************************/
            // Criação da barra do Canvas //
            /**********************************************************/
            for (int c = 0; c < characters.Count + mobs.Count; c++)
            {
                if (c < characters.Count) // verifica se a quantidade de personagem é menor que c //
                {
                    gameObjectParent = GameObject.Find("Character" + c.ToString()); // define o parente como sendo um objeto de personagem na posição de c //
                }
                else // caso c seja igual ou maior que a quantidade de personagem significa que não existe mais personagens para definir parentes e sim apenas inimigos //
                {
                    gameObjectParent = GameObject.Find("enemy" + i.ToString()); // define o parente como sendo um objeto de enemy na posição de i //
                    i++; // passo de 1 para a varivel i //
                }

                rectTransformCharacter = (RectTransform)gameObjectParent.transform; // define o objeto de RectTransform fazendo um parse com o transform do objeto de GameObject que contem meu Parent definido acima //
                vectorCharacter.Add(rectTransformCharacter.sizeDelta); // adiciona os valores de SizeDelta e passa para um Vector2 para que seja possivel pegar as informações de tamanhos nos eixos X e Y do objeto de personagem para manipulação de posição e tamanho do canvas // 
                canvasGameObject.Add(new GameObject("CavasHP"+ c.ToString())); // adiciona as intancias de GameObject no list //
                canvasGameObject[c].transform.SetParent(gameObjectParent.transform); // define o parente do canvas de Hp //
                canvasGameObject[c].AddComponent<Canvas>(); // Responsavel por criar o componente canvas //
                rectTransformCanvas = (RectTransform)canvasGameObject[c].transform;  // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                rectTransformCanvas.sizeDelta = new Vector2(vectorCharacter[c].x / 1.4f, vectorCharacter[c].y / 25); // define as dimensões do meu canvas para o hp (new vector2(pega o tamanho do canvas e divide pela proporção definida pelo dev, pega o tamanho do canvas e divide pela proporção definida pelo dev))
                vectorCanvas.Add(rectTransformCanvas.sizeDelta); // pega as dimensões que forão definidas e passa para um objeto de Vector2 para manipulação //
                canvasGameObject[c].transform.localScale = new Vector3(1, 1, 1); // define a escala do o canvas //
                canvasGameObject[c].transform.localPosition = new Vector3(-vectorCharacter[c].x / 2 + vectorCanvas[c].x / 2, vectorCharacter[c].y/2 + 25,0); // calculo responsavel por definir a posição do objeto (new vector3 (define 0 para a centralizção do objeto, divide em 2 o tamanho total do objeto para posiciona-lo no topo e soma com o offset, 0)) //
                canvas = canvasGameObject[c].GetComponent<Canvas>(); // pega o componente canvas para manipulação //
                canvas.renderMode = RenderMode.ScreenSpaceOverlay; // define o rendemode //
            }

            i = 0;

            /**********************************************************/
            // Criação da barra de Hp do Canvas //
            /**********************************************************/
            for (int c = 0; c < characters.Count + mobs.Count; c++)
            {
                if (c < characters.Count)
                {
                    gameObjectParent = GameObject.Find("CavasHP" + c.ToString()); // define o valor do meu objeto com o parente desejado //
                    hpBar.Add(new GameObject("HpBar_character_" + c.ToString())); // Responsavel por instancia meu gameobject e nomea-lo //
                }
                else
                {
                    gameObjectParent = GameObject.Find("CavasHP" + c.ToString()); // define o valor do meu objeto com o parente desejado //
                    hpBar.Add(new GameObject("HpBar_mob_" + i.ToString())); // Responsavel por instancia meu gameobject e nomea-lo //
                    i++;
                }
                
                hpBar[c].transform.SetParent(gameObjectParent.transform); // responsavel por setar um parent //
                hpBar[c].AddComponent<Image>(); // Responsavel por criar o componente image //
                rectTransformHpBar = (RectTransform)hpBar[c].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                hpBar[c].transform.localPosition = new Vector3(0, 0, 0); 
                hpBar[c].transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                rectTransformHpBar.sizeDelta = rectTransformCanvas.sizeDelta; // define o tamanho do objeto de Hp para o mesmo tamanho do canvas de HP //
                imageHpBar = hpBar[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                imageHpBar.sprite = spriteHpBar; // Responsavel por passar um sprite para meu image.sprite //
                
            }





            /**********************************************************/
            // Criação da borda do Hp //
            /**********************************************************/
          /*  for (int c = 0; c < characters.Count + mobs.Count; c++)
            {
                gameObjectParent = GameObject.Find("CavasHP" + c.ToString()); // define o valor do meu objeto com o parente desejado //
                border.Add(new GameObject("border")); // Responsavel por instancia meu gameobject e nomea-lo //
                border[c].transform.SetParent(gameObjectParent.transform); // responsavel por setar um parent //
                border[c].AddComponent<Image>(); // Responsavel por criar o componente image //
                rectTransformHpBar = (RectTransform)border[c].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                rectTransformHpBar.sizeDelta = rectTransformCanvas.sizeDelta; // define o tamanho do objeto de border para o mesmo tamanho do canvas de HP //
                border[c].transform.localPosition = new Vector3(0, 0, 0); // calculo responsavel por definir a posição do objeto (new vector3 (pega o tamanho do meu canvas no x /2 para descobrir o centro e subtrai o mesmo do objeto de hp para posiciona-lo nas bordas, mesma coisa do eixo x, z = 0)) //
                border[c].transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                imageHpBar = border[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                imageHpBar.sprite = spriteHpBar; // Responsavel por passar um sprite para meu image.sprite //
            }*/



        }



    

        /****************************************************************************************/
        /****************************************************************************************/
        // Metodo para criação das barras de Mp //
        /****************************************************************************************/
        /****************************************************************************************/
        public void InstantiateMpBar()
        {
         


            mpBar = new List<GameObject>();
            borderMp = new List<GameObject>();


            canvasGameObjectMp = new List<GameObject>(); // instancia do canvas de Hp //
            vectorCanvasMp = new List<Vector2>();
            vectorCharacter = new List<Vector2>();
           


            /**********************************************************/
            // Criação da barra do Canvas //
            /**********************************************************/
            for (int c = 0; c < characters.Count ; c++)
            {
                gameObjectParent = GameObject.Find("Character" + c.ToString()); // define o parente como sendo um objeto de personagem na posição de c //
                rectTransformCharacter = (RectTransform)gameObjectParent.transform; // define o objeto de RectTransform fazendo um parse com o transform do objeto de GameObject que contem meu Parent definido acima //
                vectorCharacter.Add(rectTransformCharacter.sizeDelta); // adiciona os valores de SizeDelta e passa para um Vector2 para que seja possivel pegar as informações de tamanhos nos eixos X e Y do objeto de personagem para manipulação de posição e tamanho do canvas // 
                canvasGameObjectMp.Add(new GameObject("CanvasMP" + c.ToString())); // adiciona as intancias de GameObject no list //
                canvasGameObjectMp[c].transform.SetParent(gameObjectParent.transform); // define o parente do canvas de Hp //
                canvasGameObjectMp[c].AddComponent<Canvas>(); // Responsavel por criar o componente canvas //
                rectTransformCanvas = (RectTransform)canvasGameObjectMp[c].transform;  // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                rectTransformCanvas.sizeDelta = new Vector2(vectorCharacter[c].x / 2f, vectorCharacter[c].y / 25); // define as dimensões do meu canvas para o hp (new vector2(pega o tamanho do canvas e divide pela proporção definida pelo dev, pega o tamanho do canvas e divide pela proporção definida pelo dev))
                vectorCanvasMp.Add(rectTransformCanvas.sizeDelta); // pega as dimensões que forão definidas e passa para um objeto de Vector2 para manipulação //
                canvasGameObjectMp[c].transform.localScale = new Vector3(1, 1, 1); // define a escala do o canvas //
                canvasGameObjectMp[c].transform.localPosition = new Vector3(-vectorCharacter[c].x/2 + vectorCanvasMp[c].x/2, vectorCharacter[c].y / 2 + 15, 0); // calculo responsavel por definir a posição do objeto (new vector3 (define 0 para a centralizção do objeto, divide em 2 o tamanho total do objeto para posiciona-lo no topo e soma com o offset, 0)) //
                canvas = canvasGameObjectMp[c].GetComponent<Canvas>(); // pega o componente canvas para manipulação //
                canvas.renderMode = RenderMode.ScreenSpaceOverlay; // define o rendemode //
            }


            /**********************************************************/
            // Criação da barra de Mp do Canvas //
            /**********************************************************/


           


            for (int c = 0; c < characters.Count; c++)
            {
    
                gameObjectParent = GameObject.Find("CanvasMP" + c.ToString()); // define o valor do meu objeto com o parente desejado //
                mpBar.Add(new GameObject("MpBar" + c.ToString())); // Responsavel por instancia meu gameobject e nomea-lo //


                mpBar[c].transform.SetParent(gameObjectParent.transform); // responsavel por setar um parent //
                mpBar[c].AddComponent<Image>(); // Responsavel por criar o componente image //
                rectTransformHpBar = (RectTransform)mpBar[c].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                mpBar[c].transform.localPosition = new Vector3(0, 0, 0);
                mpBar[c].transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                rectTransformHpBar.sizeDelta = rectTransformCanvas.sizeDelta; // define o tamanho do objeto de Hp para o mesmo tamanho do canvas de HP //
                imageHpBar = mpBar[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                imageHpBar.sprite = spriteHpBar; // Responsavel por passar um sprite para meu image.sprite //

            }





            /**********************************************************/
            // Criação da borda do Hp //
            /**********************************************************/
            /*  for (int c = 0; c < characters.Count + mobs.Count; c++)
              {
                  gameObjectParent = GameObject.Find("CavasHP" + c.ToString()); // define o valor do meu objeto com o parente desejado //
                  border.Add(new GameObject("border")); // Responsavel por instancia meu gameobject e nomea-lo //
                  border[c].transform.SetParent(gameObjectParent.transform); // responsavel por setar um parent //
                  border[c].AddComponent<Image>(); // Responsavel por criar o componente image //
                  rectTransformHpBar = (RectTransform)border[c].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                  rectTransformHpBar.sizeDelta = rectTransformCanvas.sizeDelta; // define o tamanho do objeto de border para o mesmo tamanho do canvas de HP //
                  border[c].transform.localPosition = new Vector3(0, 0, 0); // calculo responsavel por definir a posição do objeto (new vector3 (pega o tamanho do meu canvas no x /2 para descobrir o centro e subtrai o mesmo do objeto de hp para posiciona-lo nas bordas, mesma coisa do eixo x, z = 0)) //
                  border[c].transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                  imageHpBar = border[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                  imageHpBar.sprite = spriteHpBar; // Responsavel por passar um sprite para meu image.sprite //
              }*/



        }






        /***********************************************************************************************************************************************************************************************************/

        public void HpControl(int c, int who) // controla o tamanho da barra de Hp //
        {
            
            if (who == 0) // 0 significa que o dano esta sendo desferido no personagem controlavel //
            {
           
                int i = hpBar.FindIndex(gameObject => string.Equals("HpBar_character_"+ c.ToString(), gameObject.name)); // acha o index dentro do list de hpbar de personagem para manipulação posterior //
                rectTransformHpBar = (RectTransform)hpBar[i].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                rectTransformHpBar.sizeDelta = new Vector2(vectorCanvas[i].x * hp / maxHp , vectorCanvas[i].y); // define o tamanho do meu objeto de vivo (Vector2(calculo feito para definir o tamanho da barra (faz uma regra de 3 ), define o y para o mesmo tamanho)) //
                vectorBar = rectTransformHpBar.sizeDelta; // pega as dimensões da barra de hp e passa para um Vector2 para manipulação posterior //
                hpBar[i].transform.localPosition = new Vector3(-vectorCanvas[i].x / 2 + vectorBar.x / 2, 0, 0); // necessario definir a posição devido ao centro sempre mudar por causa do redimensionamento da barra de Hp (vector3 (calcula a metade do tamnho do canvas de Hp e soma com a nova metade da barra de hp ) desse jeito a barra de hp diminui mas se mantem alinhado a esquerda) //
            }

            if (who == 1) // 1 significa que o dano esta sendo desferido nos Mob //
            {

                int i = hpBar.FindIndex(gameObject => string.Equals("HpBar_mob_" + c.ToString(), gameObject.name)); // acha o index dentro do list de hpbar de mob para manipulação posterior //
              
                rectTransformHpBar = (RectTransform)hpBar[i].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                
                rectTransformHpBar.sizeDelta = new Vector2(vectorCanvas[i].x * actualLifeMob / fullLifeMob, vectorCanvas[i].y); // define o tamanho do meu objeto de vivo (Vector2(calculo feito para definir o tamanho da barra (faz uma regra de 3 ), define o y para o mesmo tamanho)) //
                vectorBar = rectTransformHpBar.sizeDelta; // pega as dimensões da barra de hp e passa para um Vector2 para manipulação posterior //
                hpBar[i].transform.localPosition = new Vector3(-vectorCanvas[i].x / 2 + vectorBar.x / 2, 0, 0); // necessario definir a posição devido ao centro sempre mudar por causa do redimensionamento da barra de Hp (vector3 (calcula a metade do tamnho do canvas de Hp e soma com a nova metade da barra de hp ) desse jeito a barra de hp diminui mas se mantem alinhado a esquerda) //

            }

        }



        /***********************************************************************************************************************************************************************************************************/

        public void MpControlBar(int c, float actualMp, float fullMp) // controla o tamanho da barra de Mp //
        {
                rectTransformHpBar = (RectTransform)mpBar[c].transform; // pega o componente RectTransform que contem a informação de tamanho e passa para um  objeto transform necessario para manipulação posteriores //
                rectTransformHpBar.sizeDelta = new Vector2(vectorCanvasMp[c].x * actualMp / fullMp, vectorCanvas[c].y); // define o tamanho do meu objeto de vivo (Vector2(calculo feito para definir o tamanho da barra (faz uma regra de 3 ), define o y para o mesmo tamanho)) //
                vectorBar = rectTransformHpBar.sizeDelta; // pega as dimensões da barra de hp e passa para um Vector2 para manipulação posterior //
                mpBar[c].transform.localPosition = new Vector3(-vectorCanvasMp[c].x / 2 + vectorBar.x / 2, 0, 0); // necessario definir a posição devido ao centro sempre mudar por causa do redimensionamento da barra de Hp (vector3 (calcula a metade do tamnho do canvas de Hp e soma com a nova metade da barra de hp ) desse jeito a barra de hp diminui mas se mantem alinhado a esquerda) //

        }


    }
}
