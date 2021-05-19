using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamHats
{
    [HarmonyPatch]
    public static class VersionPingPatch
{
        public static string fullCredentials = $"<color=#FCCE03FF>VPHats</color> v{StreamHatsPlugin.Version.ToString()}\n<size=70%>par <color=#FCCE03FF>Lyzane</color></size>\n<size=60%>Adapté de <color=#FCCE03FF>StreamHats</color> par <color=#FCCE03FF>SideSxope</color></size>";

        [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
        private static class VersionShowerPatch {
            static void Postfix(VersionShower __instance) {
                string spacer = new String('\n', 1);

                if (__instance.text.text.Contains(spacer))
                    __instance.text.text = __instance.text.text + "\n" + fullCredentials;
                else
                    __instance.text.text = __instance.text.text + spacer + fullCredentials;
					__instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
					__instance.text.margin = new Vector4(0, 0, 0.5f, 0);
					__instance.text.transform.localPosition = new Vector3(0, 0, 0);
					Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
					__instance.text.transform.position = new Vector3(topRight.x - 0.1f, topRight.y - 1.1f);
            }
        }

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {   
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            AspectPosition position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(1.9f, 0.28f, 0);
            position.AdjustPosition();
            
            __instance.text.text = "<color=#FCCE03FF><size=120%>StreamHats</color></size>\n<color=#FCCE03FF><size=80%>by SideSxope</color>\n</size>";
                if (AmongUsClient.Instance.Ping < 100) {
                    __instance.text.text += $"<size=60%>Ping: <color=#00FF00>{AmongUsClient.Instance.Ping}ms</color></size>\n";
                    __instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
                }
                else if (AmongUsClient.Instance.Ping < 200) {
                    __instance.text.text += $"<size=60%>Ping: <color=#CCCC00>{AmongUsClient.Instance.Ping}ms</color></size>\n";
                    __instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
                }
                else if (AmongUsClient.Instance.Ping > 200) {
                    __instance.text.text += $"<size=60%>Ping: <color=#FF0000>{AmongUsClient.Instance.Ping}ms</color></size>\n";
                    __instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
                }

                if (MeetingHud.Instance) {
                    __instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
				    __instance.text.margin = new Vector4(0, 0, 0.5f, 0);
				    __instance.text.fontSize = 3.0f;
				    __instance.text.transform.localPosition = new Vector3(0, 0, 0);
				    Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
				    __instance.text.transform.position = new Vector3(topRight.x - 0.1f, topRight.y - 1.8f);
                }


            
        }
    }
    }
}