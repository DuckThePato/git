using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CanvasControlSystem
{
    public class CanvasControl
    {




        public Camera cam;
        public RectTransform rectTransforCamera;
       
        public Canvas canvas;
        public RectTransform rectTransforCanvas;


        /**************************************************************************************/
        /**************************************************************************************/
        // construtor responsavel por Definir o rendermode e posição do canvas recebendo camera e canvas do levelcontroller para manipulação //
        /**************************************************************************************/
        /**************************************************************************************/
        public CanvasControl(GameObject gameObjectCamera, GameObject gameObjectCanvas)
        {

            canvas = gameObjectCanvas.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            gameObjectCanvas.transform.localPosition = gameObjectCamera.transform.localPosition;

        }

    }
}
