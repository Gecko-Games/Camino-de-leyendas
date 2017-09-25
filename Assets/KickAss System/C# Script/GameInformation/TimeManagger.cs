using UnityEngine;
using System.Collections;

public static class TimeManagger {

	public static void NormalizeTime(){
		Time.timeScale = 1f;
	}

	public static void PauseTime(){
		Time.timeScale = 0f;
	}

	public static void AlterateTime(float timeSpeed){
		Time.timeScale = timeSpeed;
	}
}
