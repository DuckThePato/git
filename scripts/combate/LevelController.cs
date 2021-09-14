using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SpawnSystem;
using MpHpControlSystem;
using MobControlSystem;
using VisualFeedBackSystem;
using ButtonSystem;
using CombatControlSystem;
using CanvasControlSystem;
using MainStatsSystem;
using StatsMobSystem;
using ItemsSystem;
using MagicSystem;
using AnimationSystem;

namespace LevelControl
{
    public class LevelController : MonoBehaviour
    {

        public SpawnControl spawnControl;
        public MpHpControl mpHpControl;
        public MobControl mobControl;
        public CombatControl combatControl;
        public VisualFeedBack visualFeedBack;
        public CanvasControl canvasControl;
        public List<MainStats> mainStats;
        public List<StatsMob> statsMobs;
        public List<Items> items;
        public List<Magics> magics;
        public AnimationControl animationControl;

        public MonoBehaviour mono;
        public Botao botao;
        public RaycastHit2D hit;

        public GameObject gameObjectCanvas;
        public GameObject gameObjectCamera;
        public GameObject lightObject;
        public Light light;

        public bool coroutine;
        public bool coroutineBleeding;

        public List<string> itemsHolding;
        public List<string> magicsHolding;

        public float coolDownMob;
        public float timePass;
        public float timeParalyse;
        public float timeParalyseMob;
        public float timeBleedingMob;
        public float timeBleeding;
        public float timePoisonMob;


        /***********************************************************************************************************************************************************************************************************/
        // Start is called before the first frame update
        void Start()
        {

            mainStats = HandlerStats.GetHandlerStats();
            spawnControl = new SpawnControl(); // A classe SpawnControl � responsavel pelo controle de instancias dos objetos que representam os personagens controlaveis e os mobs //
            spawnControl.InstanceObjects(mainStats.Count, 1); // Metodo da classe SpawnControl responsavel por instanciar os objetos //
            animationControl = new AnimationControl(spawnControl.character, spawnControl.mob);
            items = HandlerItems.GetHandlerItems();
            MagicsInstantiate(); // Inicia a Classe Magics responsavel por conter as Magias dos personagens //
            statsMobs = HandlerStatsMob.GetHandlerStats();
            mono = GameObject.FindObjectOfType<MonoBehaviour>(); // passa para meu objeto de MonoBehavior o tipo Monobehavior para uso na Classe VisualFeedback para executar um coroutine //
            mpHpControl = new MpHpControl(spawnControl.character, spawnControl.mob); // A classe MpHpControl � responsavel por instaciar e controlar os objetos que representam as barras de vidas e manas (Esta sendo enviado um gameobject contendo as informa��es do objeto de personagens)//
            mobControl = new MobControl(statsMobs, magics, mainStats, mpHpControl); // instancia da Classe MobControl que contem o sistema de controle de IA dos mob //
            combatControl = new CombatControl(mpHpControl, mainStats, items, magics, statsMobs);
            botao = new Botao(combatControl);
            visualFeedBack = new VisualFeedBack(spawnControl.mob[0], spawnControl.character[0]); // Instancia da Classe que contem o controle dos textos que aparecem em tela mostrando os danos e ataques execultados //    
            lightObject = GameObject.Find("Point Light"); // Responsavel encontrar o Objeto de Luz e passar para um objeto do tipo Light para manipula��o na classe VisualFeedback //
            lightObject.SetActive(false); // Responsavel por definir o Objeto de Luz como desativado ao iniciar a cena //
            light = lightObject.GetComponent<Light>();
            coroutine = false;
            gameObjectCanvas = GameObject.Find("Canvas");  // Responsavel por achar o gameobject canvas e passa-lo para um Objeto do tipo Gameobject para manipula��o na classe de CanvasControl //
            gameObjectCamera = GameObject.Find("Main Camera"); // Responsavel por achar o gameobject Main Camera e passa-lo para um Objeto do tipo Gameobject para manipula��o na classe de CanvasControl //
            canvasControl = new CanvasControl(gameObjectCamera, gameObjectCanvas);  // Inicia o canvasControl chamando o contrutor que Recebe os gameobjects que representam camera e canvas respectivamente //

            coolDownMob = 3f;
            timePass = 0;
        }




        /***********************************************************************************************************************************************************************************************************/
        // Update is called once per frame
        void Update()
        {


            if (!coroutine)
            {
                StartCoroutine(attackWait(coolDownMob));
            }

            // Condicao que verifica se meu personagens esta atacando //
            if (combatControl.isAtackChar)
            {
                animationControl.LerpCharacterObject(combatControl.charSelect, combatControl.mobSelect);
                combatControl.isAtackChar = animationControl.animationStart;
            }

            // Condicao que verifica se meu mob esta atacando //
            // neste setor é feito o lerp do mob ate o persongem //
            if (mobControl.isAtackMob)
            {
                animationControl.LerpMobObject(mobControl.position, mobControl.mobSelected, coolDownMob);
                mobControl.isAtackMob = animationControl.animationStart;
                combatControl.SetIsAtack(animationControl.animationStart);
            }

            if (combatControl.coolDown) // Responsavel por verificar se a acao do jogador entrou em cooldown para que seja executado o metodo de controle do tempo de duracao do cooldown //
            {
                CoolDownControl(5f);
            }

            for (int c = 0; c < mainStats.Count; c++)
            {
                if (mainStats[c].paralyse) // Responsavel por verificar se a acao do jogador entrou em cooldown para que seja executado o metodo de controle do tempo de duracao do cooldown //
                {
                    ParalyseControl(3f, c);
                }
            }

            for (int c = 0; c < statsMobs.Count; c++)
            {
                if (statsMobs[c].paralyse) // Responsavel por verificar se a acao do jogador entrou em cooldown para que seja executado o metodo de controle do tempo de duracao do cooldown //
                {
                    ParalyseControlMob(3f, c);
                }
                if (statsMobs[c].bleeding)
                {
                    StartCoroutine(BleedDamage(3f, c));
                }
                if (statsMobs[c].poison)
                {
                    StartCoroutine(PoisonDamage(3f, c));
                }
            }


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Responsavel por ficar passando a possi��o do mouse referente a coordenada da tela (esquerda inferior 0,0 e o lado superior direito � a resolu��o) //
            hit = Physics2D.Raycast(mousePos, Vector2.zero);


            if (hit) // Responsavel por verificar se o RayCast (que segue a posicao do mouse) esta em cima de algum objeto //
            {

                // esta secao controla a selecao de mob em batalha //
                for (int c = 0; c < spawnControl.mob.Count; c++) // Responsavel por fazer o looping para que a lista de mob seja toda verificada //
                {
                    if (spawnControl.mob[c] == hit.collider.gameObject) // Responsavel  por verificar se o Mob no index c e o mesmo em que o meu mouse esta em cima //
                    {
                        visualFeedBack.ActiveLight(lightObject, hit.collider.gameObject); // Responsavel por chamar o metodo da Classe VisualFeedBack que instancia as setas de selecao dos mobs que recebe o mob que esta selecionado //
                        if (Input.GetMouseButtonDown(0)) // Responsavel por verificar se o bot�o esquerdo do mouse foi pressionado //
                        {
                            if (!animationControl.animationStart)
                            {
                                visualFeedBack.OnClickMobSelect(0); // Responsavel por chamar o metodo que muda o range da luz mostrando para o jogador o mob sendo selecionado //
                                visualFeedBack.InstantiateSelector(hit.collider.gameObject); // Responsavel por chamar o Meto da classe VisualFeedback responsavel por instanciar as setas de sele��o de mob a ser atacado //
                                combatControl.SetMobSelect(spawnControl.mob.IndexOf(hit.collider.gameObject)); // Responsavel por chamar o metodo da classe CombatControl que define a variavel que contem o index do mob que esta sendo atacado por isso a passado qual o mob que foi selecionado pelo jogador //
                            }

                        }
                    }

                }

                // esta secao controla a selecao de personagens em batalha //
                for (int c = 0; c < spawnControl.character.Count; c++) // Responsavel por fazer o looping para que a lista de mob seja toda verificada //
                {
                    if (spawnControl.character[c] == hit.collider.gameObject) // Responsavel  por verificar se o Mob no index c e o mesmo em que o meu mouse esta em cima //
                    {
                        if (Input.GetMouseButtonDown(0)) // Responsavel por verificar se o bot�o esquerdo do mouse foi pressionado //
                        {
                            if (!animationControl.animationStart)
                            {
                                //Debug.Log(spawnControl.mob.IndexOf(hit.collider.gameObject));
                                visualFeedBack.InstantiateSelectorChar(hit.collider.gameObject); // Responsavel por chamar o Meto da classe VisualFeedback responsavel por instanciar as setas de selecao de mob a ser atacado //
                                combatControl.SetCharSelect(spawnControl.character.IndexOf(hit.collider.gameObject)); // Responsavel por chamar o metodo da classe CombatControl que define a variavel que contem o index do mob que esta sendo atacado por isso a passado qual o mob que foi selecionado pelo jogador //
                                botao.SetCharSelect(combatControl.charSelect); // Responsavel por passar qual personagem está selecionado para a classe Button (necessario para que seja feito os botões em relação as listas de golpes dos pers);
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0)) // Responsavel por verificar se o Botao do mouse foi solto //
                {
                    //  visualFeedBack.OnClickMobSelect(1);
                }
            }
            else // Responsavel por verificar se o mouse nao esta em cima de nenhum objeto com hitbox //
            {

                visualFeedBack.DesactiveLight(lightObject);
            }


        }







        /***********************************************************************************************************************************************************************************************************/
        // Coroutine uma interface que executa tarefas em paralelo as outras para controle de ataques dos mobs//
        IEnumerator attackWait(float delay)
        {
            if (!mobControl.isAtackMob && !combatControl.isAtackChar)
            {
                timePass += Time.deltaTime;
                if (timePass > delay)
                {
                    mobControl.Atacks(mainStats.Count); // chama o metodo Atacks que e passado a quantidade de personagem jogavel em tela // 
                    visualFeedBack.TextAppear(mobControl.atk, mobControl.dmg, spawnControl.mob[mobControl.mobSelected], mono, gameObjectCanvas); // pega o metodo da classe VisualFeedback que controla o texto em tela e passa o nome do ataque, qual o mob atacando definindo o parente como este mesmo mob, passa o monobehavor para o uso do corotine que faz o texto desaparecer // 
                    coroutine = false;
                    timePass = 0;
                }
            }
            yield return null;
        }







        /***********************************************************************************************************************************************************************************************************/
        // Metodo responsavel por definir o intervalo entre uma acao e outra dos personagens principais assim que o tempo de delay terminar o jogador pode fazer outra acao//
        public void CoolDownControl(float delay)
        {
            if (!mobControl.isAtackMob)
            {
                visualFeedBack.CoolDownBarLerp(0.3f, animationControl.animationStart);
                if (visualFeedBack.timeElapsed >= delay) // Responsavel por verificar se c e menor que o tempo de delay ja que c contem o tempo passado //
                {
                    combatControl.coolDown = false;
                    visualFeedBack.timeElapsed = 0;
                    return; // Responsavel por finalizar o metodo e retornar a execucao do update //     
                }
            }
            return; // Responsavel por controlar o delay(tempo de espera) em que esta interface ficara at� poder prosseguir com suas tarefas //         
        }






        /***********************************************************************************************************************************************************************************************************/
        // //
        public void MagicsInstantiate()
        {

            magicsHolding = new List<string>(); // itemsHolding e apenas um Handler para pegar os nomes dos itens que os meus personagens tem para passa no metodo GetHandlerItems() da classe HandlerItems dentro do arquivo Items //
            magics = new List<Magics>();
            for (int c = 0; c < mainStats.Count; c++)
            {
                for (int i = 0; i < mainStats[c].magics.Count; i++)
                {
                    magicsHolding.Add(mainStats[c].magics[i]); // Responsavel por armazenar os nomes da magias contidas dentro das listas //
                }
            }
            foreach (StatsMob s in statsMobs)
            {
                foreach (string st in s.magics)
                {
                    magicsHolding.Add(st); // Responsavel por armazenar os nomes da magias contidas dentro das listas //
                }
            }

            magics = HandlerMagics.GetHandlerMagics(magicsHolding);
        }






        public void ParalyseControl(float delay, int pos)
        {
            timeParalyse += Time.deltaTime;
            if (timeParalyse >= delay)
            {
                mainStats[pos].paralyse = false;
                timeParalyse = 0;
            }
            return;
        }





        public void ParalyseControlMob(float delay, int pos)
        {
            timeParalyseMob += Time.deltaTime;
            // Debug.Log(timeParalyseMob);
            if (timeParalyseMob >= delay)
            {
                Debug.Log("ParalyseControlMob");
                statsMobs[pos].paralyse = false;
                timeParalyseMob = 0;
            }
            return;
        }


        IEnumerator BleedDamage(float delay, int pos)
        {
            timeBleedingMob += Time.deltaTime;
            if (timeBleedingMob >= 1)
            {
                statsMobs[pos].BleedDamage(10f);
                mpHpControl.SetHp(statsMobs[pos].life, pos, statsMobs[pos].maxLife, 1);
                timeBleedingMob = 0;
            }

            yield return new WaitForSeconds(delay);
            statsMobs[pos].bleeding = false;
        }

        IEnumerator PoisonDamage(float delay, int pos)
        {
            timePoisonMob += Time.deltaTime;
            if (timePoisonMob >= 1)
            {
                statsMobs[pos].PosionDamage(10f);
                mpHpControl.SetHp(statsMobs[pos].life, pos, statsMobs[pos].maxLife, 1);
                timePoisonMob = 0;
            }

            yield return new WaitForSeconds(delay);
            statsMobs[pos].bleeding = false;
        }
    }
}




