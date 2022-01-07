using DMT;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

public class Khaine_ForceBloodmoonMoveSpeed
{ 
    public class Khaine_ForceBloodmoonMoveSpeed_Logger
    {
        public static bool blDisplayLog = true;

        public static void Log(String strMessage)
        {
            if (blDisplayLog)
                UnityEngine.Debug.Log(strMessage);
        }
    }
	
	public class Khaine_ForceBloodmoonMoveSpeed_Init : IModApi
    {
		public void InitMod(Mod _modInstance)
		{
			Log.Out(" Loading Patch: " + this.GetType().ToString());

			// Reduce extra logging stuff
			Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
			Application.SetStackTraceLogType(UnityEngine.LogType.Warning, StackTraceLogType.None);

			var harmony = new HarmonyLib.Harmony(GetType().ToString());
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
    }
	
	[HarmonyPatch(typeof(EntityPlayer), "Start")]
	public class Khaine_EntityPlayer_Patch
    {				
		public static void Postfix(EntityPlayer __instance)
		{
			int nukeAll = 0;
			
			GamePrefs.Set(EnumGamePrefs.BloodMoonFrequency, nukeAll);
			GamePrefs.Set(EnumGamePrefs.BloodMoonRange, nukeAll);
			GamePrefs.Set(EnumGamePrefs.BloodMoonWarning, nukeAll);
			GamePrefs.Set(EnumGamePrefs.BloodMoonEnemyCount, nukeAll);
			
			GamePrefs.Set(EnumGamePrefs.ZombieMove, nukeAll);
			GamePrefs.Set(EnumGamePrefs.ZombieMoveNight, nukeAll);
			GamePrefs.Set(EnumGamePrefs.ZombieFeralMove, nukeAll);
			GamePrefs.Set(EnumGamePrefs.ZombieBMMove, nukeAll);
		}
	}
	
}