using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;


namespace SaveD
{

    [System.Serializable]
    public static class SaveData
    {


        public static void SaveJson<T>(List<T> save, string nameFile) // metodo para converter minha classe para json //
        {
            string content = JsonHelper.ToJson<T>(save.ToArray());
            WriteFile(content, nameFile);
        }



        public static void WriteFile(string content, string nameFile) // metodo que grava meu arquivo //
        {
            FileStream fileStream = new FileStream(Application.dataPath + "/"+ nameFile, FileMode.Create);

            using (StreamWriter stream = new StreamWriter(fileStream))
            {
                stream.Write(content);
            }
        }





        /**********************************************************************************************************************************/
        // o Inicio da Leitura do arquivo é aqui onde sera chamado o metodo ReadFile e depois os dados serão inserido em um list generico//
        /*********************************************************************************************************************************/

        public static List<T> ReadFromJson<T>(string nameFile) // Le o arquivo Json //
        {
            string content = ReadFile(nameFile); //chamada do metodo ReadFile para a leitura do arquivo // 

            if (string.IsNullOrEmpty(content) || content == "{}") // Verifica se o conteudo do arquivo não é nulo ou vazio //
            {
                return new List<T>(); //se for vazio ele retorna um list vazio // 
            }
         
                List<T> res = JsonHelper.FromJson<T>(content).ToList(); // Inseri os dados do arquivo no List generico //
                return res;
        }






        /*******************************************************************************************************************/
        // Neste metodo sera recebido o nome do arquivo e ele ira verificara a existencia do mesmo para ser feito a leitura//
        /******************************************************************************************************************/

        public static string ReadFile(string nameFile) // Le o arquivo na pasta salva //
        {
            if (File.Exists(Application.dataPath + "/" + nameFile))
            {
                using (StreamReader reader = new StreamReader(Application.dataPath + "/" + nameFile))
                {
                    string content = reader.ReadToEnd();
                    return content;  
                }
   
            }
            return "";

        }

        public static class JsonHelper
        {
            public static T[] FromJson<T>(string json) // converte um arquivo Json para objeto //
            {
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items;
            }

            public static string ToJson<T>(T[] array) // converte os dados passado do metodo SaveJson para Json //
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper);
            }

            public static string ToJson<T>(T[] array, bool prettyPrint)
            {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper, prettyPrint);
            }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] Items;
            }
        }



    }
}

