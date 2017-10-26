using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour 
{

	public class PlayerDieEvent : UnityEvent{}
	public class CheckPoint : UnityEvent<string>{}
	public class PlayerPassEvent : UnityEvent{}
	public class EnemyTakeDamageEvent : UnityEvent<GameObject,float>{}
	public class MagicPerformEvent : UnityEvent<string>{}
	public class AfterAdEvent : UnityEvent{}


	public static PlayerDieEvent PlayerDie = new PlayerDieEvent ();
	public static PlayerPassEvent PlayerPass = new PlayerPassEvent();
	public static EnemyTakeDamageEvent EnemyTakeDamage = new EnemyTakeDamageEvent();
	public static MagicPerformEvent MagicCast = new MagicPerformEvent();
	public static AfterAdEvent AfterRewardAd = new AfterAdEvent ();
	public static AfterAdEvent AfterRelifeAd = new AfterAdEvent();

}
