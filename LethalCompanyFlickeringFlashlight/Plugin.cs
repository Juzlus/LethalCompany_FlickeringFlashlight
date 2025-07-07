using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LethalCompanyFlickeringFlashlight.Patches;

namespace LethalCompanyFlickeringFlashlight
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class FlickeringFlashlight : BaseUnityPlugin
    {
        private const string modGUID = "Juzlus.FlickeringFlashlight";
        private const string modName = "Flickering Flashlight";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static FlickeringFlashlight instance;
        internal ManualLogSource mls;

        public static ConfigEntry<float> lowEnergy;
        public static ConfigEntry<float> criticalEnergy;
        public static ConfigEntry<float> flickerDuration;
        public static ConfigEntry<float> minFlickerDelay;
        public static ConfigEntry<float> maxFlickerDelay;
        public static ConfigEntry<float> lowEnergyFlickerChance;
        public static ConfigEntry<float> criticalEnergyFlickerMultiplier;
        public static ConfigEntry<bool> infinityCriticalFlashlight;
        public static ConfigEntry<bool> enableDebugLog;
        public static ConfigEntry<float> fearLevel;
        public static ConfigEntry<float> insanityLevel;
        public static ConfigEntry<float> cooldownLow;
        public static ConfigEntry<float> cooldownCritical;

        public static ManualLogSource GetLogger() => instance.mls;

        void Awake()
        {
            if (!instance)
                instance = this;

            this.InitializeLogger();
            CreateConfig();
            this.SetHarmony();
        }

        private void InitializeLogger()
        {
            this.mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            this.mls.LogInfo("Plugin \"Flickering Flashlight\" is loaded!");
        }

        private static void CreateConfig()
        {
            lowEnergy = instance.Config.Bind<float>("Energy Threshold", "Low Energy Threshold", .3f,
                new ConfigDescription("The battery level (%) below which the flashlight starts flickering.", new AcceptableValueRange<float>(0f, 1f)));
            criticalEnergy = instance.Config.Bind<float>("Energy Threshold", "Critical Energy Threshold", .1f,
                new ConfigDescription("The battery level (%) at which the flashlight enters critical mode (heavy flickering).", new AcceptableValueRange<float>(0f, 1f)));

            flickerDuration = instance.Config.Bind<float>("Duration", "Flicker Duration", 1.5f,
                new ConfigDescription("Duration (in seconds) of flashlight flickering.", new AcceptableValueRange<float>(.2f, 15f)));
            minFlickerDelay = instance.Config.Bind<float>("Duration", "Min Flicker Delay", .05f,
                new ConfigDescription("Minimum delay (in seconds) between flickers.", new AcceptableValueRange<float>(.01f, 2f)));
            maxFlickerDelay = instance.Config.Bind<float>("Duration", "Max Flicker Delay", .2f,
                new ConfigDescription("Maximum delay (in seconds) between flickers.", new AcceptableValueRange<float>(2f, 8f)));

            cooldownLow = instance.Config.Bind<float>("Duration", "Next Low Flick", 10f,
                new ConfigDescription("Minimum delay (in seconds) between flashlight flickers when battery is low.", new AcceptableValueRange<float>(0f, 60f)));
            cooldownCritical = instance.Config.Bind<float>("Duration", "Next Critical Flick", 3f,
                new ConfigDescription("Delay (in seconds) between flashlight flickers when battery is critically low", new AcceptableValueRange<float>(0f, 30f)));

            lowEnergyFlickerChance = instance.Config.Bind<float>("Chance", "Low Energy Flicker Chance", .01f,
                new ConfigDescription("Chance (%) that the flashlight will flicker when battery is low.", new AcceptableValueRange<float>(.01f, 10f)));
            fearLevel = instance.Config.Bind<float>("Chance", "Minimal Fear Level", .4f,
                new ConfigDescription("Flashlight start flickering when player fear goes above this percentage.", new AcceptableValueRange<float>(0f, 1f)));
            insanityLevel = instance.Config.Bind<float>("Chance", "Minimal Insanity Level", .9f,
                new ConfigDescription("Flashlight starts flickering when player insanity goes above this percentage (You gain insanity by being alone).", new AcceptableValueRange<float>(0f, 1f)));

            criticalEnergyFlickerMultiplier = instance.Config.Bind<float>("Frequency", "Critical Energy Flicker Multiplier", 2f,
                new ConfigDescription("Multiplier applied to flicker frequency when battery is at critical level.", new AcceptableValueRange<float>(1f, 10f)));

            infinityCriticalFlashlight = instance.Config.Bind<bool>("Other", "Infinity Critical Energy", false, "Prevents the flashlight battery from dropping below 5% (infinity critical energy).");

            enableDebugLog = instance.Config.Bind<bool>("Debug", "Enable Logs", false, "Show debug messages in the console about flashlight flickering.");
        }

        private void SetHarmony()
        {
            this.harmony.PatchAll(typeof(FlashlightItemPatch));
        }
    }
}
