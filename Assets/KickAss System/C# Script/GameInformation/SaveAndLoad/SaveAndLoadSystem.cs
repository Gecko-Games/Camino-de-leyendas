using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoadSystem {


	public static void Save <T>(T classToSave, string fileName) where T : class, new(){
		
		BinaryFormatter binFor = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + ".bin");
		binFor.Serialize(file, classToSave);
		file.Close();

	}


	public static T Load <T>( string fileName) where T : class, new(){
		
		BinaryFormatter binFor = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.Open);
		T loadClass = binFor.Deserialize(file) as T;
		file.Close();
		return loadClass;


	}


	public static bool FileExist(string fileName){

		Debug.Log(Application.persistentDataPath + "/" + fileName + ".bin");
		return File.Exists(Application.persistentDataPath + "/" + fileName + ".bin");

	}
}
