using LethalCompanyFlickeringFlashlight;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlashlightFlickeringSystem : MonoBehaviour
{
    public ulong flashlightId;
    public bool critical;
    public bool isFlickering;
    public Coroutine flickerRoutine;
    private FlashlightItem flashlight;

    private float nextFlick = 0f;

    public void Init(FlashlightItem _flashlight)
    {
        flashlightId = _flashlight.GetComponent<NetworkObject>().NetworkObjectId;
        isFlickering = false;
        critical = false;
        flashlight = _flashlight;
    }

    private void Update()
    {
        if (FlickeringFlashlight.infinityCriticalFlashlight.Value && flashlight.insertedBattery.charge < .05f)
            flashlight.insertedBattery.charge = .05f;

        if (isFlickering || !flashlight.isBeingUsed)
            return;

        if (flashlight.insertedBattery.charge <= FlickeringFlashlight.criticalEnergy.Value)
        {
            critical = true;
            StartFlickering();
        }
        else if ((flashlight.insertedBattery.charge <= FlickeringFlashlight.lowEnergy.Value
             && Random.Range(0, 100) < FlickeringFlashlight.lowEnergyFlickerChance.Value))
        {
            critical = false;
            StartFlickering();
        }
        else if (flashlight?.playerHeldBy && (
             FlickeringFlashlight.fearLevel.Value <= flashlight?.playerHeldBy?.playersManager?.fearLevel
             || FlickeringFlashlight.insanityLevel.Value <= (flashlight?.playerHeldBy?.insanityLevel / flashlight?.playerHeldBy?.maxInsanityLevel)))
            {
                critical = false;
                StartFlickering();
            }
    }

    private void StartFlickering()
    {
        if (nextFlick > Time.time)
            return;

        float min = critical ? (FlickeringFlashlight.minFlickerDelay.Value / FlickeringFlashlight.criticalEnergyFlickerMultiplier.Value) : FlickeringFlashlight.minFlickerDelay.Value;
        float max = critical ? (FlickeringFlashlight.maxFlickerDelay.Value / FlickeringFlashlight.criticalEnergyFlickerMultiplier.Value) : FlickeringFlashlight.maxFlickerDelay.Value;

        if (flickerRoutine != null) CoroutineRunner.Instance.Stop(flickerRoutine);
        flickerRoutine = CoroutineRunner.Instance.Run(Flickering(min, max));
    }

    private IEnumerator Flickering(float min, float max)
    {
        if(flashlight.IsOwner)
            flashlight.SyncBatteryServerRpc((int)(flashlight.insertedBattery.charge * 100f));
        if (FlickeringFlashlight.enableDebugLog.Value)
            if (flashlight?.playerHeldBy)
                FlickeringFlashlight.GetLogger().LogInfo($"{flashlight?.playerHeldBy?.playerUsername}'s flashlight is flickering ({(critical ? "CRITICAL" : "LOW")} battery).");
            else
                FlickeringFlashlight.GetLogger().LogInfo($"Flashlight ({flashlight?.name}) is flickering ({(critical ? "CRITICAL" : "LOW")} battery).");

        nextFlick = Time.time + (critical ? FlickeringFlashlight.cooldownCritical.Value : FlickeringFlashlight.cooldownLow.Value);
        isFlickering = true;
        float elapsed = 0f;
        while (elapsed < FlickeringFlashlight.flickerDuration.Value / (critical ? 1f : 2f) && flashlight?.insertedBattery?.charge > 0f && isFlickering)
        {
            flashlight.SwitchFlashlight(!flashlight.isBeingUsed);
            float delay = Random.Range(min, max);
            yield return new WaitForSeconds(delay);
            elapsed += delay;
        }
        flashlight.SwitchFlashlight(flashlight.insertedBattery.charge > 0f && isFlickering);
        isFlickering = false;
    }
}
