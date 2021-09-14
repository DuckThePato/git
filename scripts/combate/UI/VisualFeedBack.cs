using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VisualFeedBackSystem
{

    public class VisualFeedBack
    {
        public GameObject  parentObj, canvas;
        public GameObject textObj, textDmg;
        public Text atkText, dmgText;
        public Vector2 vector2;
        public RectTransform rect;
        public string text;
        public Light lightSelect;
        public Color color;
        string s, c;


        public GameObject selector, selectorChar;
    
        public Image image_selector;
        public Sprite sprite_selector;
        public RectTransform rectTransformSelector, rectTransformGO;
        public Vector2 vector2Selector, vector2GO;

        public GameObject CanvasCoolDown, coolDownBar;
        public RectTransform rectTransformCanvas, rectTransformMainCanvas, rectTransformCoolDownBar;
        public float timeElapsed, widthCoolDownBar;

        public bool emptyBar;



        /***********************************************************************************************************************************************************************************************************/
        // Construtor chamado na classe de LevelController //
        public VisualFeedBack(GameObject mob, GameObject main)
        {
            s = ""; // inicia a variavel que salva qual o nome do mob que esta selecionado para uso no metodo InstantiateSelector //,
            c = "";
            emptyBar = false;
            InstantiateSelector(mob); // Responsavel por chamar o metodo que cria a seta de qual mob esta selecionado //
            InstantiateSelectorChar(main);
            InstantiateText(); // inicia a criação do texto //
            InstantiateCoolDownBar();
        }








        /***********************************************************************************************************************************************************************************************************/
        // responsavel por iniciar o processo de controle do texto (recebe qual o nome do ataque execultado, o gameobject para manipulação na criação do texto, monobehavior para o uso do metodo startcoroutine) // 
        public void TextAppear(string text, float dmg, GameObject gameObject, MonoBehaviour mono, GameObject mainCanvas) 
        {
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf"); // define a fonte do texto puxando dos arquivos e passando para o objeto de Font//
        
            atkText.font = ArialFont; // define a fonte do texto //
            atkText.material = ArialFont.material;
            atkText.text = text; // define o texto escrito //

            dmgText.font = ArialFont; // define a fonte do texto //
            dmgText.material = ArialFont.material;
            dmgText.text = dmg.ToString(); // define o texto escrito //

            canvas.transform.SetParent(gameObject.transform); // define o parente pegando o GameObject recebido //
            rect = (RectTransform)gameObject.transform; // pega o transform e passa para um objeto do tipo RectTransform //
            vector2 = rect.sizeDelta; // passa a informação de tamanho para o Objeto de Vector2 //
            canvas.transform.localPosition = new Vector3(0, vector2.y / 2 + 50, 0); // define a posição do canvas (x = 0 ; y = metade do tamanho do meu Gameobject que representa um mob fazendo o texto fica no topo deste objeto +50 que serve como offset, z=0) //
            canvas.transform.localScale = new Vector3(1, 1, 1); // define escala //
            canvas.SetActive(true); // destroi o objeto canvas criado no metodo InstantiateText //
            mono.StartCoroutine(TextDesapear(2.0f)); // Rotina em paralelo que tem como função fazer o   texto desaparecer após 2 segundos //
        }






        /***********************************************************************************************************************************************************************************************************/
        // responsvel por criar o texto //
        public void InstantiateText()
        {

            canvas = new GameObject("CanvasText");

            canvas.AddComponent<Canvas>(); // adiciona o componente canvas //
            textObj = new GameObject("AtkText"); // cria um GameObect de texto //
            textObj.transform.SetParent(canvas.transform); // define o parente sendo o canvas //
            textObj.AddComponent<Text>(); // adiciona o componente texto //
            atkText = textObj.GetComponent<Text>(); // pego o componente texto e passa para o objeto texto para manipulação posterior //
            canvas.transform.localScale = new Vector3(1, 1, 1); // define escala //
            textObj.transform.localPosition = new Vector3(0, 0, 0); // define a posição do texto ficando centralizado na posição do canvas //

            textDmg = new GameObject("DmgText"); // cria um GameObect de texto //
            textDmg.transform.SetParent(canvas.transform); // define o parente sendo o canvas //
            textDmg.AddComponent<Text>(); // adiciona o componente texto //
            dmgText = textDmg.GetComponent<Text>(); // pego o componente texto e passa para o objeto texto para manipulação posterior //
            textDmg.transform.localPosition = new Vector3(0, 25, 0); // define a posição do texto ficando centralizado na posição do canvas //



        }




        /***********************************************************************************************************************************************************************************************************/
        // Coroutine uma interface que executa tarefas em paralelo as outras //
        IEnumerator TextDesapear(float delay) 
        {
            yield return new WaitForSeconds(delay); // esta linha controla o delay (tempo de espera) em que esta interface ficara até poder proceguir com suas tarefas //         
            canvas.SetActive(false); // destroi o objeto canvas criado no metodo InstantiateText //
        }




        /***********************************************************************************************************************************************************************************************************/
        // Metodo  responsavel por instanciar as setas de seleção de mob a ser atacado //
        // o metodo recebe um gameobject que representa um mob que está selecionado //
        public void InstantiateSelector(GameObject mob)
        {

            if (c != mob.gameObject.name) // Repsonsavel por Verificar se o mob que esta com a seta de seleção é o mesmo que esta sendo selecionado neste instante //
            {
                if (selector != null)
                {
                    //Debug.Log("nao nulo");
                    Object.Destroy(selector);
                }
                selector = new GameObject("Selector"); // Responsavel por instancia meu gameobject e nomea-lo //
                selector.transform.SetParent(mob.transform); // responsavel por setar um parent //
                selector.AddComponent<Image>(); // Responsavel por criar o componente image //
                rectTransformSelector = (RectTransform)selector.transform; // Responsavel por passar as dimensões do meu objeto selector para um Objeto do tipo RectTransform //
                rectTransformGO = (RectTransform)mob.transform;  // Responsavel por passar as dimensões do meu objeto gameobject para um Objeto do tipo RectTransform //
                vector2GO = rectTransformGO.sizeDelta; // Responsavel por passa os dados de dimensões dentro do RectTransform do GameObject para um Vector2 para manipulação posteror // 
                rectTransformSelector.sizeDelta = new Vector2(vector2GO.x / 3, vector2GO.y / 1.3f); // Responsavel por definir o tamanho da seta calculando no Vector2(Tamanho do objeto que representa o mob no eixo x / 3 fazendo com que fique 3 vezes menor que meu mob, Tamanho do objeto que representa o mob no eixo y / 1.3)
                vector2Selector = rectTransformSelector.sizeDelta; // Responsavel por passa para o Vector2 as dimensões do objeto que representa as setas //
                selector.transform.localPosition = new Vector3(vector2GO.x / 2 + vector2Selector.x / 2 + 40, 0, 0); // calculo responsavel por definir a posição do objeto (new vector3 (pega o tamanho do meu canvas no x /2 para descobrir o centro e subtrai o mesmo do objeto de canvaHP para posiciona-lo nas bordas, mesma coisa do eixo x, z = 0)) //
                selector.transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                image_selector = selector.GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                image_selector.sprite = sprite_selector; // Responsavel por passar um sprite para meu image.sprite //    
                c = mob.gameObject.name; // Responsavel por passar qual o gameobject que representa um mob esta selecionado e passa o nome para uma string dessa forma podera haver a comparação se o mob selecionado atualmente é o mesmo salvo na string //
            }
        }






        /***********************************************************************************************************************************************************************************************************/
        // Metodo  responsavel por instanciar as setas de seleção de mob a ser atacado //
        // o metodo recebe um gameobject que representa um mob que está selecionado //
        public void InstantiateSelectorChar(GameObject main)
        {

            if (s != main.gameObject.name) // Repsonsavel por Verificar se o mob que esta com a seta de seleção é o mesmo que esta sendo selecionado neste instante //
            {
                if (selectorChar != null)
                {
                    //Debug.Log("nao nulo");
                    Object.Destroy(selectorChar);
                }
                selectorChar = new GameObject("Selector"); // Responsavel por instancia meu gameobject e nomea-lo //
                selectorChar.transform.SetParent(main.transform); // responsavel por setar um parent //
                selectorChar.AddComponent<Image>(); // Responsavel por criar o componente image //
                rectTransformSelector = (RectTransform)selectorChar.transform; // Responsavel por passar as dimensões do meu objeto selector para um Objeto do tipo RectTransform //
                rectTransformGO = (RectTransform)main.transform;  // Responsavel por passar as dimensões do meu objeto gameobject para um Objeto do tipo RectTransform //
                vector2GO = rectTransformGO.sizeDelta; // Responsavel por passa os dados de dimensões dentro do RectTransform do GameObject para um Vector2 para manipulação posteror // 
                rectTransformSelector.sizeDelta = new Vector2(vector2GO.x / 3, vector2GO.y / 3f); // Responsavel por definir o tamanho da seta calculando no Vector2(Tamanho do objeto que representa o mob no eixo x / 3 fazendo com que fique 3 vezes menor que meu mob, Tamanho do objeto que representa o mob no eixo y / 1.3)
                vector2Selector = rectTransformSelector.sizeDelta; // Responsavel por passa para o Vector2 as dimensões do objeto que representa as setas //
                selectorChar.transform.localPosition = new Vector3(0, vector2GO.y/2 + vector2Selector.y/2 + 50, 0); // calculo responsavel por definir a posição do objeto (new vector3 (pega o tamanho do meu canvas no x /2 para descobrir o centro e subtrai o mesmo do objeto de canvaHP para posiciona-lo nas bordas, mesma coisa do eixo x, z = 0)) //
                selectorChar.transform.localScale = new Vector3(1, 1, 1); // responsavel por setar a escala(tamanho) do meu gameobject //
                image_selector = selectorChar.GetComponent<Image>(); // Responsavel por pegar o componente image do gameobject para um objecto do tipo image //
                image_selector.sprite = sprite_selector; // Responsavel por passar um sprite para meu image.sprite //
                s = main.gameObject.name; // Responsavel por passar qual o gameobject que representa um mob esta selecionado e passa o nome para uma string dessa forma podera haver a comparação se o mob selecionado atualmente é o mesmo salvo na string //
            }
        }


            /***********************************************************************************************************************************************************************************************************/
            // Metodo que ativa a luz de seleção do mob que o mouse se encontra em cima //
            // chamado na classe LevelController quando o mouse sai de cima de um mob // 
            public void ActiveLight(GameObject light, GameObject mob) 
        {
            if (!light.active) // Verifica se o objeto de luz não esta ativado para que seja ativado //
            {

                lightSelect = light.GetComponent<Light>(); // Responsavel por pegar o componente Light e passar para uma objeto do tipo Light //
                color.g = 0.5f; // Responsavel por definir a cor verde //
                color.r = 0.5f; // Responsavel por definir a cor vermelho //
                color.b = 0.5f; // Responsavel por definir a cor azul //
                color.a = 1f; // Responsavel por definir a transparencia //
                lightSelect.color = color; // Responsavel por definir a cor do lightSelect para 
                lightSelect.range = 8f; // Responsavel por definir o tamanho do raio da luz de seleção de mob //
                light.transform.SetParent(mob.transform); // Responsavel por definir um Parent recebendo o Mob que o mouse esta em cima como Parent //
                light.transform.localPosition = new Vector3(0, 0, 0); // Responsavel por definir a posição local do objeto de luz para o centro do Mob definido como parent // 
                light.SetActive(true); // Responsavel por definir o Objeto de luz como ativo //

            }

        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por desativar o Objeto que representa a luz //
        // chamado na classe LevelController quando o mouse sai de cima de um mob // 
        public void DesactiveLight(GameObject light)
        {
            if(light.active) // Reponsavel por verificar se o objeto luz esta ativado (necessario pois ficara passando aqui sempre quando o mouse não estiver em cima do mob, então para não ficar desativando o objeto que ja esta desativado) //
            {
                light.SetActive(false); // Responsavel por definir o Objeto de luz como desativado //
            }

        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por diminuir o raio da luz quando o mob é clicado //
        // Para que há um feedback de click no momento de selecionar um mob para atacar //
        public void OnClickMobSelect(int i)
        {
            if (i == 0) // 0 Representa o botão do mouse pressionado //
            {
                lightSelect.range = 6f;
            }
            if (i == 1) // 1 Representa que o botão do mouse foi solto //
            {
                lightSelect.range = 8f;
            }
        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por criar a barra de cooldown e o canvas//
        public void InstantiateCoolDownBar()
        {

            /**********************************************************/
            // Criação da barra do Canvas //
            /**********************************************************/
            CanvasCoolDown = new GameObject("CanvasCoolDown"); // Responsavel por instancia meu gameobject e nomea-lo //
            CanvasCoolDown.transform.SetParent(GameObject.Find("Canvas").gameObject.transform); // responsavel por setar um parent //
            CanvasCoolDown.AddComponent<Canvas>(); // Responsavel por criar o componente image //
            rectTransformCanvas = (RectTransform)CanvasCoolDown.transform; // Responsavel por passar as dimensões do meu objeto selector para um Objeto do tipo RectTransform //
            rectTransformMainCanvas = (RectTransform)GameObject.Find("Canvas").gameObject.transform;
            rectTransformCanvas.sizeDelta = new Vector2(rectTransformMainCanvas.sizeDelta.x /2.5f , rectTransformMainCanvas.sizeDelta.y / 20);
            CanvasCoolDown.transform.localPosition = new Vector3(rectTransformMainCanvas.sizeDelta.x / 2 - rectTransformCanvas.sizeDelta.x / 2 - 15, - rectTransformMainCanvas.sizeDelta.y / 2 + rectTransformCanvas.sizeDelta.y / 2 + 5);
            CanvasCoolDown.transform.localScale = new Vector3(1, 1, 1);

            coolDownBar = new GameObject("CoolDownBar");
            coolDownBar.transform.SetParent(CanvasCoolDown.transform); // responsavel por setar um parent //
            coolDownBar.AddComponent<Image>(); // Responsavel por criar o componente image //
            rectTransformCoolDownBar = (RectTransform)coolDownBar.transform;
            rectTransformCoolDownBar.sizeDelta = new Vector2(rectTransformCanvas.sizeDelta.x, rectTransformCanvas.sizeDelta.y);
            coolDownBar.transform.localPosition = new Vector3(0,0,0);
            coolDownBar.transform.localScale = new Vector3(1, 1, 1);
            widthCoolDownBar = rectTransformCoolDownBar.sizeDelta.x; // variavel widthCoolDownBar necessario para manipulãção do lerp no metodo cooldownbarcontrol devido a necessidade de haver o valor inicial do lerp igual ao tamanho original da barra de cooldown//

        }






        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por diminuir a barra de cooldown para zero//
        // Este metodo é chamado na Classe de LevelController //
        public void CoolDownBarLerp(float coolDownTime, bool wait)
        {
            if (timeElapsed < coolDownTime)
            {
                timeElapsed += Time.deltaTime; // Responsavel por definir timeElapsed para uma soma de Time.deltaTime assim almentando o passa a ser dado no lerp // 
                
                // Debug.Log("local position " + (rectTransformCoolDownBar.sizeDelta.x / 2 - rectTransformCanvas.sizeDelta.x / 2));
                //Debug.Log("size delta " + rectTransformCoolDownBar.sizeDelta);

                /* Responsavel por redefinir o tamanho da barra de cooldown para zero calculando um lerp (primeiro parametro contem o tamanho original da barra (necessario um valor fixo
                 * pois o calculo deve ser feito utilizando o valor inicial de tamanho e não o valor que esta sendo definido em tempo de execução)  no segundo parametro está o valor
                 * de onde minha barra deve chegar, no terceiro parametro esta a velocidade do lerp  */
                rectTransformCoolDownBar.sizeDelta = new Vector2(Mathf.Lerp(widthCoolDownBar, 0, timeElapsed / 0.3f), rectTransformCoolDownBar.sizeDelta.y);


                /* Responsavel por redefinir a posição da barra de cooldown para a esquerda do canvas mantendo sempre ele fixo em uma posição calculando um lerp (primeiro 
                 * parametro contem a posição da barra, no segundo parametro esta definido o calculo que pega o tamanho da barra atual (/2) que esta sendo diminuida sempre (-)
                 * o tamanho do canvas (/2) o resultado desta conta sempre fara que minha barra esteja posicionada na esquerda junto com a ponta esquerda do canvas,
                 * no terceiro parametro esta a velocidade do lerp  que é 1 pois deve ser feito a mudança de posição instantaneamente (1 = 100%)*/
                coolDownBar.transform.localPosition = new Vector3(Mathf.Lerp(coolDownBar.transform.localPosition.x, rectTransformCoolDownBar.sizeDelta.x / 2 - rectTransformCanvas.sizeDelta.x / 2, 1), 0, 0); // 
            }
            else if (timeElapsed >= coolDownTime && !wait)
                {
                timeElapsed += Time.deltaTime;
                rectTransformCoolDownBar.sizeDelta = new Vector2(Mathf.Lerp(0, widthCoolDownBar, timeElapsed / 5f), rectTransformCoolDownBar.sizeDelta.y);


                /* Responsavel por redefinir a posição da barra de cooldown para a esquerda do canvas mantendo sempre ele fixo em uma posição calculando um lerp (primeiro 
                 * parametro contem a posição da barra, no segundo parametro esta definido o calculo que pega o tamanho da barra atual (/2) que esta sendo diminuida sempre (-)
                 * o tamanho do canvas (/2) o resultado desta conta sempre fara que minha barra esteja posicionada na esquerda junto com a ponta esquerda do canvas,
                 * no terceiro parametro esta a velocidade do lerp  que é 1 pois deve ser feito a mudança de posição instantaneamente (1 = 100%) */
                coolDownBar.transform.localPosition = new Vector3(Mathf.Lerp(coolDownBar.transform.localPosition.x, rectTransformCoolDownBar.sizeDelta.x / 2 - rectTransformCanvas.sizeDelta.x / 2, 1), 0, 0);
            }
            if (rectTransformCoolDownBar.sizeDelta.x >= widthCoolDownBar)
            {
                rectTransformCoolDownBar.sizeDelta = new Vector2(widthCoolDownBar, rectTransformCoolDownBar.sizeDelta.y);
            }

        }




    }

}

