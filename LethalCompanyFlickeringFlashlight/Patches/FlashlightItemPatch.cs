using HarmonyLib;

namespace LethalCompanyFlickeringFlashlight.Patches
{
    [HarmonyPatch(typeof(FlashlightItem))]
    internal class FlashlightItemPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.Start))]
        private static void FlashlightItem_Start(FlashlightItem __instance)
        {
            __instance.gameObject.AddComponent<FlashlightFlickeringSystem>().Init(__instance);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.ItemActivate))]
        private static void FlashlightItem_ItemActivate(FlashlightItem __instance)
        {
            if (GetState(__instance).isFlickering)
            {
                __instance.SwitchFlashlight(false);
                GetState(__instance).isFlickering = false;
            }
        }

        private static FlashlightFlickeringSystem GetState(FlashlightItem flashlight)
        {
            FlashlightFlickeringSystem state = flashlight.GetComponent<FlashlightFlickeringSystem>();
            if (!state)
                state = flashlight.gameObject.AddComponent<FlashlightFlickeringSystem>();
            return state;
        }
    }
}
