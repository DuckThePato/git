using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Act;
using CombatControlSystem;
using TMPro;
using LevelControl;


namespace ButtonSystem
{
    public class Botao
    {

        public List<Button> botao;
        public List<Button> botao_frame;
        public List<GameObject> botao_object;

        public GameObject gameObjectFrame;
        public GameObject gameObject_parent;
        public List<GameObject> gameObject_text;
        public Image background;
        public Transform parent;
        private Graphic graphic;
        public Color color;
        public CombatControl combatControl;
        public TextMeshProUGUI txt;
        public Font font;
        public RectTransform rt; // RectTransform serve para mexer no transform //

        public bool existFrame;
        public bool buttonPress;

        public int charSelect;
        public int actualFrame;

        // Start is called before the first frame update
        public Botao(CombatControl combatControl)
        {
            this.combatControl = combatControl;
            botao = new List<Button>(); // instancia //
            charSelect = 0;
            //buttonPress = false;
            existFrame = false; // frame inicia não existente //
            botao_object = new List<GameObject>(); // instancia //
            botao_frame = new List<Button>(); // instancia //
            setFrame(0); // para a criação do frame de botão inicial //
            ButtonCreate(0); // criação dos botões iniciais //
        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo reponsavel por definir um valor para charSelect(variavel responsavel por definir qual personagem esta selecionado) //
        // Este metodo é chamado na classe LevelController //
        public void SetCharSelect(int charSelect)
        {
            this.charSelect = charSelect;
            if (actualFrame == 2)
            {
                ButtonCreate(actualFrame);
            }
        }



        /***********************************************************************************************************************************************************************************************************/
        // Listener dos botões de seleção iniciais da interface de batalha //
        public void onClick()
        {
            if (EventSystem.current.currentSelectedGameObject.name == "botao_0") // botão de golpes //
            {
                actualFrame = 1;
                setFrame(1);
                ButtonCreate(1);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "botao_1") // botão de magias //
            {
                actualFrame = 2;
                setFrame(2);
                ButtonCreate(2);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "botao_2") // botão de itens //
            {
                actualFrame = 3;
                setFrame(3);
                ButtonCreate(3);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "botao_3") // botão de fugir //
            {
                actualFrame = 4;
                setFrame(4);
                ButtonCreate(4);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "botao_back") // botão de fugir //
            {
                actualFrame = 0;
                setFrame(0);
                ButtonCreate(0);
            }


        }






        /***********************************************************************************************************************************************************************************************************/
        // Metodo para criação e manipulação do frame dos botões //
        public void setFrame(int i)
        {


            gameObject_parent = GameObject.Find("Canvas");
            parent = gameObject_parent.transform;

            if (!existFrame) // se meu objeto não existir ele cria um //
            {
                gameObjectFrame = new GameObject("frame"); // instancia //
                gameObjectFrame.transform.SetParent(parent); // set de parent do meu GameObject //
                gameObjectFrame.AddComponent<Image>(); // adiciona o componente de imagem ao meu gameobject //
                graphic = gameObjectFrame.GetComponent<Graphic>(); // pega meu componente graphic //
                color.r = 0.45f; // set de cor do vermelho //
                color.g = 0.47f;  // set de cor do verde //
                color.b = 1f;  // set de cor do azul //
                color.a = 0.37f;  // set de transparencia //
                graphic.color = color; // set de cor //
                gameObjectFrame.transform.localPosition = new Vector3(-330, -125, 0); // set de posicao do meu gameobject //
                gameObjectFrame.transform.localScale = new Vector3(1, 1, 1); // set de escala do meu gameobject //
                existFrame = true; // set booleano para true (Significa que o frame existe)//
            }
            else // se meu frame ja existe //
            {
                rt = (RectTransform)gameObjectFrame.transform; // pega o RectTransform do meu GameObject e transfere para o objeto rt //
                if (i == 0) // frame 0 = frame inicial //
                {
                    rt.sizeDelta = new Vector2(100f, 150f); // redefine as dimensões do frame de botão //
                }
                if (i == 1)
                {
                    rt.sizeDelta = new Vector2(100f, 175f); // redefine as dimensões do frame de botão //
                }
                if (i == 2) // frame 2 = frame de magias //
                {
                    rt.sizeDelta = new Vector2(100f, 175f); // redefine as dimensões do frame de botão //
                }
                if (i == 3) // frame 2 = frame de magias //
                {
                    rt.sizeDelta = new Vector2(100f, 175f); // redefine as dimensões do frame de botão //
                }
            }

        }




        /***********************************************************************************************************************************************************************************************************/
        // metodo responsavel por criar os botões //
        public void ButtonCreate(int activeFrame)
        {

            if (botao_object.Count != 0) // se meu List conter objetos //
            {
                for (int c = 0; c < botao_object.Count; c++) // for para destruir meus gameobjects //
                {
                    Object.Destroy(botao_object[c]); // destroi imediatamente meu Botao na posição de c //
                }
                botao_object.Clear(); // limpa minha lista de objeto //
                botao_frame.Clear(); // limpa minha lista de componente botão //
                gameObject_text.Clear();//limpa minha lista de Texto //
            }
            if (botao_object.Count == 0) // se não houver objetos no list //
            {
                gameObject_parent = GameObject.Find("frame"); // encontra o GameObject frame e passa para meu objeto //
                parent = gameObject_parent.transform; // passa o transform para meu objeto parent do tipo Transform //

            }


            //**************************************************************************************//
            // cria os botões iniciais // 
            //**************************************************************************************//

            if (botao_object.Count == 0 && activeFrame == 0) // se minha lista estiver vazia e o frame ativo for 0 //
            {
                for (int c = 0; c < 4; c++)
                {
                    botao_object.Add(new GameObject("botao_" + c.ToString()));
                    botao_object[c].transform.SetParent(parent); // define o parente que meu objeto esta ancorado //
                    botao_object[c].AddComponent<Button>();
                    botao_object[c].AddComponent<Image>(); // adiciona o componente8t de imagem ao meu gameobject //
                    graphic = botao_object[c].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 1f; // set de cor do vermelho //
                    color.g = 0.47f;  // set de cor do verde //
                    color.b = 1f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    botao_object[c].transform.localPosition = new Vector3(0, (-50 * 0.2f + 50) - 25 * c, 0); // set de posicao do meu gameobject //
                    botao_object[c].transform.localScale = new Vector3(0.4f, 0.2f, 1); // set de escala do meu gameobject //
                    botao_frame.Add(botao_object[c].GetComponent<Button>()); // adiciona o componente botão dentro do meu gameobject inserido no list e passa para botão frame //
                    rt = (RectTransform)botao_object[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 100f); // set de dimensão do meu objeto //

                    botao_frame[c].onClick.AddListener(() => onClick()); // adiciona o listener no botão chamando o metodo onClick //
                }



                gameObject_text = new List<GameObject>(); // intancia meu list de texto//

                for (int i = 0; i < 4; i++) // looping para criação dos texto dos botões //
                {
                    gameObject_text.Add(new GameObject("text")); // adiciona uma nova instancia ao meu array //
                    gameObject_parent = GameObject.Find("botao_" + i.ToString()); // pega meu GameObject e passa para meu objeto //
                    parent = gameObject_parent.transform;  // pega o transform do meu GameObject (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].transform.SetParent(parent); // set de parent do meu GameObject de texto (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].AddComponent<TextMeshProUGUI>(); // adiciona o componente TextMeshProUGUI //
                    graphic = gameObject_text[i].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 0f; // set de cor do vermelho //
                    color.g = 0f;  // set de cor do verde //
                    color.b = 0f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    gameObject_text[i].transform.localPosition = new Vector3(0, 0, 0); // define a posição local do meu gameobject //
                    gameObject_text[i].transform.localScale = new Vector3(1, 1, 1); // define a escala local do meu gameobject //
                    rt = (RectTransform)gameObject_text[i].transform;  // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 50f); // set de dimensão do meu objeto //


                    txt = gameObject_text[i].GetComponent<TextMeshProUGUI>(); // pega meu componente TextMeshProUGUI e passa para o objeto txt //
                                                                              //txt.font = font;
                    txt.alignment = TextAlignmentOptions.Center; // define o alinhamento do texto //
                    txt.enableAutoSizing = true; // define o autosize como verdadeiro //
                    txt.enableWordWrapping = true;

                    if (i == 0)
                    {
                        txt.text = "golpes"; // define o texto //
                    }
                    if (i == 1)
                    {
                        txt.text = "magias"; // define o texto //
                    }
                    if (i == 2)
                    {
                        txt.text = "itens"; // define o texto //
                    }
                    if (i == 3)
                    {
                        txt.text = "fugir"; // define o texto //
                    }
                }
            }




            //**************************************************************************************//
            // cria os botões para seleção de golpes // 
            //**************************************************************************************//

            if (botao_object.Count == 0 && activeFrame == 1)// se minha lista estiver vazia e o frame ativo for 2 //
            {


                for (int c = 0; c < combatControl.GetMainStats()[charSelect].golpes.Count; c++) // looping responsavel por controlar quantos botões são criados vendo quantas magias o personagem tem //
                {
                    botao_object.Add(new GameObject("botao_golpes_" + c.ToString())); // adição de instancia dentro do meu list //
                    botao_object[c].transform.parent = parent; // define o parente que meu objeto esta ancorado //
                    botao_object[c].AddComponent<Button>(); // adição de componente botão //
                    botao_object[c].AddComponent<Image>(); // adiciona o componente de imagem ao meu gameobject //
                    graphic = botao_object[c].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 1f; // set de cor do vermelho //
                    color.g = 0.47f;  // set de cor do verde //
                    color.b = 1f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    botao_object[c].transform.localPosition = new Vector3(0, (-50 * 0.2f + 50) - 25 * c, 0); // set de posicao do meu gameobject //
                    botao_object[c].transform.localScale = new Vector3(0.4f, 0.2f, 1); // set de escala do meu gameobject //
                    botao_frame.Add(botao_object[c].GetComponent<Button>()); // adiciona o componente botão dentro do meu gameobject inserido no list e passa para botão frame //
                    rt = (RectTransform)botao_object[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 100f); // set de dimensão do meu objeto //

                    // botao_frame[c].onClick.AddListener(() => onClickMagias(c, botao_object[c])); // Adiciona o Listener Chamando o metodo onClickMagias
                }




                gameObject_text = new List<GameObject>(); // intancia meu list //

                for (int i = 0; i < combatControl.GetMainStats()[charSelect].golpes.Count; i++) // loop para criação de texto //
                {
                    gameObject_text.Add(new GameObject("text")); // adiciona uma nova instancia ao meu array //
                    gameObject_parent = GameObject.Find("botao_golpes_" + i.ToString()); // pega meu GameObject e passa para meu objeto //
                    parent = gameObject_parent.transform;  // pega o transform do meu GameObject (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].transform.SetParent(parent); // set de parent (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].AddComponent<TextMeshProUGUI>(); // adiciona o componente TextMeshProUGUI //
                    graphic = gameObject_text[i].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 0f; // set de cor do vermelho //
                    color.g = 0f;  // set de cor do verde //
                    color.b = 0f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    gameObject_text[i].transform.localPosition = new Vector3(0, 0, 0); // define a posição local do meu gameobject //
                    gameObject_text[i].transform.localScale = new Vector3(1, 1, 1); // define a escala local do meu gameobject //
                    rt = (RectTransform)gameObject_text[i].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 50f); // set de dimensão do meu objeto //


                    txt = gameObject_text[i].GetComponent<TextMeshProUGUI>(); // pega meu componente TextMeshProUGUI e passa para o objeto txt //
                    txt.alignment = TextAlignmentOptions.Center; // define o alinhamento do texto //
                    txt.enableAutoSizing = true; // define o autosize como verdadeiro //
                    txt.enableWordWrapping = true;
                    txt.text = combatControl.mainStats[charSelect].golpes[i]; // define o texto //
                }
                buttonBack(botao_object.Count);
            }


            //**************************************************************************************//
            // cria os botões para seleção de magias // 
            //**************************************************************************************//

            if (botao_object.Count == 0 && activeFrame == 2)// se minha lista estiver vazia e o frame ativo for 2 //
            {


                for (int c = 0; c < combatControl.GetMainStats()[charSelect].magics.Count; c++) // looping responsavel por controlar quantos botões são criados vendo quantas magias o personagem tem //
                {
                    botao_object.Add(new GameObject("botao_magia_" + c.ToString())); // adição de instancia dentro do meu list //
                    botao_object[c].transform.parent = parent; // define o parente que meu objeto esta ancorado //
                    botao_object[c].AddComponent<Button>(); // adição de componente botão //
                    botao_object[c].AddComponent<Image>(); // adiciona o componente de imagem ao meu gameobject //
                    graphic = botao_object[c].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 1f; // set de cor do vermelho //
                    color.g = 0.47f;  // set de cor do verde //
                    color.b = 1f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    botao_object[c].transform.localPosition = new Vector3(0, (-50 * 0.2f + 50) - 25 * c, 0); // set de posicao do meu gameobject //
                    botao_object[c].transform.localScale = new Vector3(0.4f, 0.2f, 1); // set de escala do meu gameobject //
                    botao_frame.Add(botao_object[c].GetComponent<Button>()); // adiciona o componente botão dentro do meu gameobject inserido no list e passa para botão frame //
                    rt = (RectTransform)botao_object[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 100f); // set de dimensão do meu objeto //
                    botao_frame[c].onClick.AddListener(() => onClickMagias()); // Adiciona o Listener Chamando o metodo onClickMagias
                }


                gameObject_text = new List<GameObject>(); // intancia meu list //



                for (int i = 0; i < combatControl.GetMainStats()[charSelect].magics.Count; i++) // loop para criação de texto //
                {

                    gameObject_text.Add(new GameObject("text")); // adiciona uma nova instancia ao meu array //
                    gameObject_text[i].transform.SetParent(botao_object[i].transform); // set de parent (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].AddComponent<TextMeshProUGUI>(); // adiciona o componente TextMeshProUGUI //
                    graphic = gameObject_text[i].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 0f; // set de cor do vermelho //
                    color.g = 0f;  // set de cor do verde //
                    color.b = 0f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    gameObject_text[i].transform.localPosition = new Vector3(0, 0, 0); // define a posição local do meu gameobject //
                    gameObject_text[i].transform.localScale = new Vector3(1, 1, 1); // define a escala local do meu gameobject //
                    rt = (RectTransform)gameObject_text[i].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 50f); // set de dimensão do meu objeto //


                    txt = gameObject_text[i].GetComponent<TextMeshProUGUI>(); // pega meu componente TextMeshProUGUI e passa para o objeto txt //
                                                                              //txt.font = font;
                    txt.alignment = TextAlignmentOptions.Center; // define o alinhamento do texto //
                    txt.enableAutoSizing = true; // define o autosize como verdadeiro //
                    txt.enableWordWrapping = true;
                    txt.text = combatControl.mainStats[charSelect].magics[i]; // define o texto //
                }

                buttonBack(botao_object.Count);

            }





            //**************************************************************************************//
            // cria os botões para seleção de itens // 
            //**************************************************************************************//

            if (botao_object.Count == 0 && activeFrame == 3)// se minha lista estiver vazia e o frame ativo for 2 //
            {


                for (int c = 0; c < combatControl.items.Count; c++) // looping responsavel por controlar quantos botões são criados vendo quantas magias o personagem tem //
                {
                    botao_object.Add(new GameObject("botao_itens_" + c.ToString())); // adição de instancia dentro do meu list //
                    botao_object[c].transform.parent = parent; // define o parente que meu objeto esta ancorado //
                    botao_object[c].AddComponent<Button>(); // adição de componente botão //
                    botao_object[c].AddComponent<Image>(); // adiciona o componente de imagem ao meu gameobject //
                    graphic = botao_object[c].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 1f; // set de cor do vermelho //
                    color.g = 0.47f;  // set de cor do verde //
                    color.b = 1f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    botao_object[c].transform.localPosition = new Vector3(0, (-50 * 0.2f + 50) - 25 * c, 0); // set de posicao do meu gameobject //
                    botao_object[c].transform.localScale = new Vector3(0.4f, 0.2f, 1); // set de escala do meu gameobject //
                    botao_frame.Add(botao_object[c].GetComponent<Button>()); // adiciona o componente botão dentro do meu gameobject inserido no list e passa para botão frame //
                    rt = (RectTransform)botao_object[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 100f); // set de dimensão do meu objeto //

                    botao_frame[c].onClick.AddListener(() => onClickItems()); // Adiciona o Listener Chamando o metodo onClickMagias
                }




                gameObject_text = new List<GameObject>(); // intancia meu list //

                for (int i = 0; i < combatControl.items.Count; i++) // loop para criação de texto //
                {
                    gameObject_text.Add(new GameObject("text")); // adiciona uma nova instancia ao meu array //
                    gameObject_parent = GameObject.Find("botao_itens_" + i.ToString()); // pega meu GameObject e passa para meu objeto //
                    parent = gameObject_parent.transform;  // pega o transform do meu GameObject (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].transform.SetParent(parent); // set de parent (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                    gameObject_text[i].AddComponent<TextMeshProUGUI>(); // adiciona o componente TextMeshProUGUI //
                    graphic = gameObject_text[i].GetComponent<Graphic>(); // pega meu componente graphic //
                    color.r = 0f; // set de cor do vermelho //
                    color.g = 0f;  // set de cor do verde //
                    color.b = 0f;  // set de cor do azul //
                    color.a = 1f;  // set de transparencia //
                    graphic.color = color; // set de cor //
                    gameObject_text[i].transform.localPosition = new Vector3(0, 0, 0); // define a posição local do meu gameobject //
                    gameObject_text[i].transform.localScale = new Vector3(1, 1, 1); // define a escala local do meu gameobject //
                    rt = (RectTransform)gameObject_text[i].transform; // pega o as dimensões do transform e insere no objeto rt //
                    rt.sizeDelta = new Vector2(200f, 50f); // set de dimensão do meu objeto //


                    txt = gameObject_text[i].GetComponent<TextMeshProUGUI>(); // pega meu componente TextMeshProUGUI e passa para o objeto txt //
                                                                              //txt.font = font;
                    txt.alignment = TextAlignmentOptions.Center; // define o alinhamento do texto //
                    txt.enableAutoSizing = true; // define o autosize como verdadeiro //
                    txt.enableWordWrapping = true;
                    txt.text = combatControl.items[i].nameItem; // define o texto //
                }
                buttonBack(botao_object.Count);
            }





            //******************************************************************//
            // metodo interno metodo ButtonCreate para criação do botão voltar //
            //****************************************************************//

            void buttonBack(int c) // metodo interno do metodo ButtonCreate //
            {
                gameObject_parent = GameObject.Find("frame");
                botao_object.Add(new GameObject("botao_back")); // adição de instancia dentro do meu list //
                botao_object[c].transform.parent = gameObject_parent.transform; // define o parente que meu objeto esta ancorado //
                botao_object[c].AddComponent<Button>(); // adição de componente botão //
                botao_object[c].AddComponent<Image>(); // adiciona o componente de imagem ao meu gameobject //
                graphic = botao_object[c].GetComponent<Graphic>(); // pega meu componente graphic //
                color.r = 1f; // set de cor do vermelho //
                color.g = 0.47f;  // set de cor do verde //
                color.b = 1f;  // set de cor do azul //
                color.a = 1f;  // set de transparencia //
                graphic.color = color; // set de cor //
                botao_object[c].transform.localPosition = new Vector3(0, -67, 0); // set de posicao do meu gameobject //
                botao_object[c].transform.localScale = new Vector3(0.4f, 0.2f, 1); // set de escala do meu gameobject //
                botao_frame.Add(botao_object[c].GetComponent<Button>()); // adiciona o componente botão dentro do meu gameobject inserido no list e passa para botão frame //
                rt = (RectTransform)botao_object[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                rt.sizeDelta = new Vector2(200f, 100f); // set de dimensão do meu objeto //

                botao_frame[c].onClick.AddListener(() => onClick()); // Adiciona o Listener Chamando o metodo onClickMagias





                gameObject_text.Add(new GameObject("text")); // adiciona uma nova instancia ao meu array //
                gameObject_text[c].transform.SetParent(botao_object[c].transform); // set de parent (necessario dentro do looping devido ao parent ter que mudar por haver varios botões) //
                gameObject_text[c].AddComponent<TextMeshProUGUI>(); // adiciona o componente TextMeshProUGUI //
                graphic = gameObject_text[c].GetComponent<Graphic>(); // pega meu componente graphic //
                color.r = 0f; // set de cor do vermelho //
                color.g = 0f;  // set de cor do verde //
                color.b = 0f;  // set de cor do azul //
                color.a = 1f;  // set de transparencia //
                graphic.color = color; // set de cor //
                gameObject_text[c].transform.localPosition = new Vector3(0, 0, 0); // define a posição local do meu gameobject //
                gameObject_text[c].transform.localScale = new Vector3(1, 1, 1); // define a escala local do meu gameobject //
                rt = (RectTransform)gameObject_text[c].transform; // pega o as dimensões do transform e insere no objeto rt //
                rt.sizeDelta = new Vector2(200f, 50f); // set de dimensão do meu objeto //


                txt = gameObject_text[c].GetComponent<TextMeshProUGUI>(); // pega meu componente TextMeshProUGUI e passa para o objeto txt //
                                                                          //txt.font = font;
                txt.alignment = TextAlignmentOptions.Center; // define o alinhamento do texto //
                txt.enableAutoSizing = true; // define o autosize como verdadeiro //
                txt.enableWordWrapping = true;
                txt.text = "voltar"; // define o texto //
            }


        }





        /***********************************************************************************************************************************************************************************************************/
        // Metodo para verificar o botão que foi precionado no menu de magias //
        public void onClickMagias()
        {
            for (int c = 0; c < botao_object.Count - 1; c++) // looping ("botao_object.Count" esta -1 devido haver o botao de voltar no list) //
            {
                if (EventSystem.current.currentSelectedGameObject.name == botao_object[c].name) // if para verificar se meu botão clicado é o mesmo do atual no looping //
                {
                    combatControl.AtackMagic(combatControl.mainStats[charSelect].magics[c]); // envia qual o botão clicado para meu metodo "AtackMagic" no script de "CombatControl" //
                }
            }


        }



        /***********************************************************************************************************************************************************************************************************/
        // Metodo para verificar o botão que foi precionado no menu de magias //
        public void onClickItems()
        {

            for (int c = 0; c < botao_object.Count - 1; c++) // looping ("botao_object.Count" esta -1 devido haver o botao de voltar no list) //
            {
                if (EventSystem.current.currentSelectedGameObject.name == botao_object[c].name) // if para verificar se meu botão clicado é o mesmo do atual no looping //
                {
                    combatControl.UseItems(combatControl.GetMainStats()[charSelect].items[c]); // envia qual o botão clicado para meu metodo "AtackMagic" no script de "CombatControl" //
                }
            }


        }

    }
}