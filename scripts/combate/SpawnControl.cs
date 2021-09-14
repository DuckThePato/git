using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace SpawnSystem
{

    public class SpawnControl
    {

        public List<GameObject> character;
        private Image image_cha;
        public Sprite sprite_char;
        private int offsetX, offsetY;
        public BoxCollider2D boxCollider2D;

        public List<GameObject> mob;
        public GameObject objectParent;
        public Image image_enemy;
        public Sprite sprite_enemy;

        

        public Transform parent;
        public RectTransform rectTransformCharacter;
        public RectTransform rectTransformCanvas;
        public Vector2 vectorCanvas, vectorCharacter;


        public RectTransform rectTransformMob;
        public Vector2  vectorMob;


        // Start is called before the first frame update
        public void InstanceObjects(int qtdCharacter, int qtdMob)
        {
            offsetX = 150;
            offsetY = 300;

            character = new List<GameObject>();
            objectParent = GameObject.Find("Canvas");
            rectTransformCanvas = (RectTransform)objectParent.transform;
            vectorCanvas = rectTransformCanvas.sizeDelta;

            parent = objectParent.transform;

            for (int c = 0; c < qtdCharacter; c++)
            {
                character.Add(new GameObject("Character"+c.ToString())); // Responsavel por instancia meu gameobject e nomea-lo //
                character[c].transform.SetParent(parent); // responsavel por setar um parent //
                character[c].AddComponent<Image>(); // Responsavel por criar o componente image //
                character[c].AddComponent<BoxCollider2D>(); // Responsavel por criar o componente image dentro deo meu objeto//
                boxCollider2D = character[c].GetComponent<BoxCollider2D>(); // Responsavel por criar um BoxCollider para uso com o Raycast //
                rectTransformCharacter = (RectTransform)character[c].transform;
                vectorCharacter = rectTransformCharacter.sizeDelta;
                character[c].transform.localPosition = new Vector3(-(vectorCanvas.x / 2) + (vectorCharacter.x / 2) + offsetX, -(vectorCanvas.y / 2) + (vectorCharacter.y / 2) + (offsetY * c), 0); // calculo responsavel por definir a posição do objeto (new vector3 (pega o tamanho do meu canvas no x /2 para descobrir o centro e subtrai o mesmo do objeto de canvaHP para posiciona-lo nas bordas, mesma coisa do eixo x, z = 0)) //
                character[c].transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                boxCollider2D.size = new Vector2(vectorCharacter.x, vectorCharacter.y); // Responsavel por definir o tamanho do boxCollider para o mesmo do Mob //
                image_cha = character[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                image_cha.sprite = sprite_char; // Responsavel por passar um sprite para meu image.sprite //
            }







            /*********************************************************************************/
            // Criação dos mobs//
            /*********************************************************************************/


            mob = new List<GameObject>();

            for (int c = 0; c < 2; c++)
            {
                mob.Add(new GameObject("enemy" + c.ToString())); // Responsavel por instanciar meu gameobject, nomea-lo e adiciona-lo no list//
                mob[c].transform.SetParent(parent); // responsavel por setar um parent //
                mob[c].AddComponent<Image>(); // Responsavel por criar o componente image dentro deo meu objeto//
                mob[c].AddComponent<BoxCollider2D>(); // Responsavel por criar o componente image dentro deo meu objeto//
                boxCollider2D = mob[c].GetComponent<BoxCollider2D>(); // Responsavel por criar um BoxCollider para uso com o Raycast //
                mob[c].transform.localPosition = new Vector3(300, (c * -100 - 15 * 1), 0); // Responsavel por setar a posição do meu gameobject //
                mob[c].transform.localScale = new Vector3(1f, 1f, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                rectTransformMob = (RectTransform)mob[c].transform; // Responsavel por pegar as dimensões do objeto que representa os mob e passa para um vector2 para manipulação posterior //
                rectTransformMob.sizeDelta = new Vector2(50, 50);
                vectorMob = rectTransformMob.sizeDelta; // Responsavel por passar as dimensões dos mobs para um Vector2 //
                boxCollider2D.size = new Vector2(vectorMob.x, vectorMob.y); // Responsavel por definir o tamanho do boxCollider para o mesmo do Mob //
                image_enemy = mob[c].GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                image_enemy.sprite = sprite_enemy; // Responsavel por passar um sprite para meu image.sprite (este set é feito no inspetor do unity)//           
            }
        }



    }
}
