using DMT;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

public class DisableTraderProtection_Patch
{
	public class DisableTraderProtection_Init : IModApi
    {		
		public void InitMod(Mod _modInstance)
		{
			Debug.Log(" Loading Patch: " + this.GetType().ToString());

			// Reduce extra logging stuff
			Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
			Application.SetStackTraceLogType(UnityEngine.LogType.Warning, StackTraceLogType.None);

			var harmony = new HarmonyLib.Harmony(GetType().ToString());
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
 
		//TraderArea has 5 vector3i options so we need 5 new types. A new type is needed since its a constructor.
        [HarmonyPatch(typeof(TraderArea), MethodType.Constructor)]
        [HarmonyPatch(new Type[]
        {
            typeof(Vector3i),
            typeof(Vector3i),
            typeof(Vector3i),
            typeof(Vector3i),
            typeof(Vector3i)
        })]
        public class TraderArea_Patch
        {
            public static void Postfix(TraderArea __instance)
            {
                Vector3i vector3I = new Vector3i(0, 0, 0);
                __instance.ProtectSize = vector3I;
            }
        }
		/*
		[HarmonyPatch(typeof(EntityTrader), "OnUpdateLive")]
		public class Khaine_EntityTraderOnUpdateLive_Patch
		{				
			public static void Postfix(EntityTrader __instance)
			{
				if (GameManager.Instance.World.IsDaytime() == false)
				{
					__instance.emodel.avatarController.SetBool("IsBusy", true);
					//__instance.emodel.SetVisible(false, false);					
					__instance.emodel.avatarController.SetVisible(false);
					
					Debug.Log("Is Night");
				}
				
				else if (GameManager.Instance.World.IsDaytime() == true)
				{
					__instance.emodel.avatarController.SetBool("IsBusy", false);
					//__instance.emodel.SetVisible(true, true);
					__instance.emodel.avatarController.SetVisible(true);
					Debug.Log("Is Day");
				}
			}
		}
		*/
	}
}