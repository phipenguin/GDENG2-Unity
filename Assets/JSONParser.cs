using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class JSONParser
{
    [System.Serializable]
    public class DataStruct
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public bool rigidbody;
    }

    [System.Serializable]
    public class DataStructList
    {
        public List<DataStruct> cubeStructList;
        public List<DataStruct> planeStructList;
        public List<DataStruct> sphereStructList;
        public List<DataStruct> capsuleStructList;
    }

    [MenuItem("File/Open Level...")]
    static void readFromFile()
    {
        DataStructList dataStructList = new DataStructList();

        //Open the file
        string path = EditorUtility.OpenFilePanel("Open scene file", "", "level");
        string parser;

        StreamReader jsonReader = new StreamReader(path);
        parser = jsonReader.ReadToEnd();

        dataStructList = JsonUtility.FromJson<DataStructList>(parser);

        //Translating the cubes from our engine to Unity
		foreach (DataStruct cubeStructData in dataStructList.cubeStructList)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            cube.name = cubeStructData.name;
            cube.tag = "Cube";
            cube.transform.position = cubeStructData.position;
            cube.transform.eulerAngles = new Vector3( cubeStructData.rotation.x, cubeStructData.rotation.y, cubeStructData.rotation.z);
			cube.transform.localScale = cubeStructData.scale;
            if (cubeStructData.rigidbody == true)
			{
                cube.AddComponent<Rigidbody>();
            }
        }

        //Translating the planes from our engine to Unity
        foreach (DataStruct planeStructData in dataStructList.planeStructList)
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            plane.name = planeStructData.name;
            plane.tag = "Plane";
            plane.transform.position = planeStructData.position;
            plane.transform.eulerAngles = new Vector3(planeStructData.rotation.x, planeStructData.rotation.y, planeStructData.rotation.z);
            plane.transform.localScale = planeStructData.scale;
            if (planeStructData.rigidbody == true)
            {
                plane.AddComponent<Rigidbody>();
            }
        }

        //Translating the sphere from our engine to Unity
        foreach (DataStruct sphereStructData in dataStructList.sphereStructList)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.name = sphereStructData.name;
            sphere.tag = "Sphere";
            sphere.transform.position = sphereStructData.position;
            sphere.transform.eulerAngles = new Vector3(sphereStructData.rotation.x, sphereStructData.rotation.y, sphereStructData.rotation.z);
            sphere.transform.localScale = sphereStructData.scale;
            if (sphereStructData.rigidbody == true)
            {
                sphere.AddComponent<Rigidbody>();
            }
        }

        //Translating the capsules from our engine to Unity
        foreach (DataStruct capsuleStructData in dataStructList.capsuleStructList)
        {
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            capsule.name = capsuleStructData.name;
            capsule.tag = "Capsule";
            capsule.transform.position = capsuleStructData.position;
            capsule.transform.eulerAngles = new Vector3(capsuleStructData.rotation.x, capsuleStructData.rotation.y, capsuleStructData.rotation.z);
            capsule.transform.localScale = capsuleStructData.scale;
            if (capsuleStructData.rigidbody == true)
            {
                capsule.AddComponent<Rigidbody>();
            }
        }
        
        jsonReader.Close();
    }

    [MenuItem("File/Save Level")]
    static void writeToFile()
	{
        DataStructList myCubeDataStructList2 = new DataStructList();

        GameObject[] cubeList = GameObject.FindGameObjectsWithTag("Cube");
        GameObject[] planeList = GameObject.FindGameObjectsWithTag("Plane");
        GameObject[] sphereList = GameObject.FindGameObjectsWithTag("Sphere");
        GameObject[] capsuleList = GameObject.FindGameObjectsWithTag("Capsule");

        List<DataStruct> cubeStructDataList = new List<DataStruct>();
        List<DataStruct> planeStructDataList = new List<DataStruct>();
        List<DataStruct> sphereStructDataList = new List<DataStruct>();
        List<DataStruct> capsuleStructDataList = new List<DataStruct>();

        //Translating the cubes from Unity to our engine
		for (int i = 0; i < cubeList.Length; i++)
		{
            DataStruct dataStruct = new DataStruct();

            dataStruct.name = cubeList[i].name;
            dataStruct.position = cubeList[i].transform.position;
            dataStruct.rotation = cubeList[i].transform.eulerAngles;
            dataStruct.scale = cubeList[i].transform.localScale;
            dataStruct.rigidbody = true;

            cubeStructDataList.Add(dataStruct);
		}

        //Translating the planes from Unity to our engine
        for (int i = 0; i < planeList.Length; i++)
        {
            DataStruct dataStruct = new DataStruct();

            dataStruct.name = planeList[i].name;
            dataStruct.position = planeList[i].transform.position;
            dataStruct.rotation = planeList[i].transform.eulerAngles;
            dataStruct.scale = planeList[i].transform.localScale;

            planeStructDataList.Add(dataStruct);
        }

        //Translating the spheres from Unity to our engine
        for (int i = 0; i < sphereList.Length; i++)
        {
            DataStruct dataStruct = new DataStruct();

            dataStruct.name = sphereList[i].name;
            dataStruct.position = sphereList[i].transform.position;
            dataStruct.scale = sphereList[i].transform.localScale;
            dataStruct.rotation = sphereList[i].transform.eulerAngles;

            sphereStructDataList.Add(dataStruct);
        }

        //Translating the capsules from Unity to our engine
        for (int i = 0; i < capsuleList.Length; i++)
        {
            DataStruct dataStruct = new DataStruct();

            dataStruct.name = capsuleList[i].name;
            dataStruct.position = capsuleList[i].transform.position;
            dataStruct.rotation = capsuleList[i].transform.eulerAngles;
            dataStruct.scale = capsuleList[i].transform.localScale;

            capsuleStructDataList.Add(dataStruct);
        }

        myCubeDataStructList2.cubeStructList = cubeStructDataList;
        myCubeDataStructList2.planeStructList = planeStructDataList;
        myCubeDataStructList2.sphereStructList = sphereStructDataList;
        myCubeDataStructList2.capsuleStructList = capsuleStructDataList;

        string jsonParser = JsonUtility.ToJson(myCubeDataStructList2);
		string path = "Assets/Scenes/Sample.level";
		FileStream fileStream = new FileStream(path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter(fileStream))
		{
			writer.Write(jsonParser);
		}
	}
}