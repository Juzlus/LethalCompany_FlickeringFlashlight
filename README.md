# 🤔 About
<img src="https://github.com/Juzlus/LethalCompany_FlickeringFlashlight/blob/main/Juzlus-Flickering%20Flashlight/icon.png?raw=true" align="right" width="100px"/>

This is a modification to [Lethal Company](https://store.steampowered.com/app/1966720/Lethal_Company/) that adds random flashlight flickering at low battery levels, enhancing tension and realism during gameplay. Possible configuration.

The script is based on the following [YouTube video](https://www.youtube.com/watch?v=jfgUotZXiBM&ab_channel=GameDevBox).

 [Page on Thunderstore](https://thunderstore.io/c/lethal-company/p/Juzlus/FlickeringFlashlight/). 

# 🔨 How it works

**The flickering flashlight will start when:**
- The **battery** goes below specific level
- The player's **fear level** jumps to the required level
- The **insanity level** goes above a specific threshold


## 📁 Configuration

- **Energy Threshold**
   - `Low Energy Threshold` - The battery level (%) below which the flashlight starts flickering. <0,1> [default 0.3]
   - `Critical Energy Threshold` - The battery level (%) at which the flashlight enters critical mode (heavy flickering). <0,1> [default 0.1]
- **Duration**
   - `Flicker Duration` - Duration (in seconds) of flashlight flickering. <0.2,15> [default 1.5]
   - `Min Flicker Delay` - Minimum delay (in seconds) between flickers. <0.01,2> [default 0.05]
   - `Max Flicker Delay` - Maximum delay (in seconds) between flickers. <2,8> [default 0.2]
   - `Next Low Flick` - Minimum delay (in seconds) between flashlight flickers when battery is low. <0,60> [default 10]
   - `Next Critical Flick` - Delay (in seconds) between flashlight flickers when battery is critically low. <0,30> [default 3]
- **Chance**
   - `Low Energy Flicker Chance` - Chance (%) that the flashlight will flicker when battery is low. <0.01,10> [default 0.01]
   - `Minimal Fear Level` - Flashlight start flickering when player fear goes above this percentage. <0,1> [default 0.4]
   - `Minimal Insanity Level` - Flashlight starts flickering when player insanity goes above this percentage (You gain insanity by being alone). <0,1> [default 0.9]
- **Frequency**
   - `Critical Energy Flicker Multiplier` - Multiplier applied to flicker frequency when battery is at critical level. <1,10> [default 2]
- **Other**
   - `Infinity Critical Energy` - Prevents the flashlight battery from dropping below 5% (infinity critical energy). [default false]
- **Debug**
   - `Enable Logs` - Show debug messages in the console about flashlight flickering. [default false]


## ✨ Usage
- [YouTube video](https://www.youtube.com/watch?v=jfgUotZXiBM&ab_channel=GameDevBox)
- [BepInEx](https://github.com/BepInEx/BepInEx)


## 🔥 Preview

![flickering_flashlight_1](https://github.com/Juzlus/LethalCompany_FlickeringFlashlight/blob/main/Juzlus-Flickering%20Flashlight/Preview/flickering_flashlight_1.gif?raw=true)
![flickering_flashlight_2](https://github.com/Juzlus/LethalCompany_FlickeringFlashlight/blob/main/Juzlus-Flickering%20Flashlight/Preview/flickering_flashlight_2.gif?raw=true)
![flickering_flashlight_3](https://github.com/Juzlus/LethalCompany_FlickeringFlashlight/blob/main/Juzlus-Flickering%20Flashlight/Preview/flickering_flashlight_3.gif?raw=true)
![flickering_flashlight_4](https://github.com/Juzlus/LethalCompany_FlickeringFlashlight/blob/main/Juzlus-Flickering%20Flashlight/Preview/flickering_flashlight_4.gif?raw=true)

Mods were used at the preview: [ShyHUD](https://thunderstore.io/c/lethal-company/p/letmusicring/ShyHUD/), [EladsHUD](https://thunderstore.io/c/lethal-company/p/EladNLG/EladsHUD/), [Full Darkness](https://thunderstore.io/c/lethal-company/p/IntegrityChaos/Full_Darkness/)


## 📝 Feedback

If you have any Feedback or questions, please contact me at juzlus.biznes@gmail.com or [Discord](https://discordapp.com/users/284780352042434570).


## 💝 Donate
<span>
  <a href="https://www.buymeacoffee.com/juzlus" target="_blank" alt="buymeacoffee" style="width: 40%; text-decoration: none; margin-right: 20px;">
    <img src="https://www.codehim.com/wp-content/uploads/2022/09/bmc-button-640x180.png" style="height: 60px;">
  </a>
  <a>⠀</a>
  <a href="https://buycoffee.to/juzlus" target="_blank" alt="buycoffee" style="text-decoration: none; width: 40%; background-color: rgb(0, 169, 98);border-radius: 10px;">
    <img src="https://buycoffee.to/btn/buycoffeeto-btn-primary.svg" style="height: 60px">
  </a>
</span>
