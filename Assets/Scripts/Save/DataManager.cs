using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    /* public Serialization Dictionary;

    //public string file = "Serialization.txt";

       //게임 세이브 및 로드
        public void GameSave()
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(file, json);
        }
        
        public void GameLoad()
        {
            data= new Serialization();
            string json = ReadFromFile(file);
            JsonUtility.FromJsonOverwrite(json, data);
        }






         //파일 저장 및 로드
        private void WriteToFile(string fileName, string json) //파일쓰기
        {
            string path = GetFilePath(fileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        private string ReadFromFIle(string fileName) //파일읽기
        {
            string path = GetFilePath(fileName);
            if(File.Exists(path))
            {
                using(StreamReader reader = new StreamReader (path))
                {
                    string json = reader.ReadToEnd();
                    return json;
                }
            }
            else
            {
                Debug.LogWarning("파일을 찾을 수 없습니다.");

                return "";
            }
        }

        private string GetFilePath(string fileName) //파일 가져오기
        {
            return Application.persistentDataPath + "/" + fileName ;
        }*/
    }



            



