Sassy Coilhead v1.0.2 (stable)
==============

Client-sided mod that makes the Coilhead do a funny dance if you stay near it for too long.<br>

> It knows it's gonna kill you.

Features
--------
* It works.
* Should be compatible with anything.
* Configurable in config/ghoulmage.funny.sassycoilhead.cfg

Bugs/Incompatibilities
--------
* External Issue: As of version 1.0.2 this should not be an issue, but heads up: Some autoinstallers (like mod managers) might change how the mod folders are structured. Make sure that the folders GhoulMage/funny are inside any folder inside the plugins folder and that the file 'sassycoilhead' exists inside the 'funny' folder. ie "plugins/blabla/moreblabla/GhoulMage/funny/sassycoilhead"
* Probable Issue (Untested): I have a hunch that it's easier for the Coilhead to kill you if you stand very very close to it while it is dancing because the player camera might clip into the collider. I would consider it a glitch instead of a bug, and I won't fix it to avoid conflicts with other Coilhead mods.
<br>

I'll update this mod as needed. Report any bugs, incompatibilities or push request the appropiate fixes and I'll look into them.

To-do
--------
* Sync across clients?

For Players
-----------
Ignore everything here. Look at Releases or into the [Thunderstore page](https://thunderstore.io/c/lethal-company/p/GhoulMage/SassyCoilhead/) to download a working build with the Asset Bundle in the correct folder.

For Devs
--------
1. **Useful API**<br>
* **static event CoilheadDanceCheck.OnCoilheadDance(SpringManAI);** raises whenever a coilhead succesfully begins their dance.<br>
* **CoilheadDanceCheck.StopDance();** Stops any ongoing dance.<br>
* **CoilheadDanceCheck.IsDancing;** True if currently dancing.<br>

> Besides all of that, know that the script CoilheadDanceCheck will attach itself to anything that spawns with SpringManAI, so you can find it with GetComponent()

<br>

2. **Dependencies**
* Assembly-CSharp.dll (From Lethal Company_Data/Managed)
* UnityEngine.dll
* Unity.Netcode.Runtime.dll
<br>

3. **Build**
* Build normaly using dotnet or whatever .NET build method you want.
* Exclude the DEBUG symbol for the intended behaviour. Otherwise, a test code that replaces all enemies with Coilhead will execute on all levels.
* Requires sassycoilhead Asset Bundle in GhoulMage/funny, inside any folder in the BepinEx plugins.
<br>

4. **Copyright**
* MIT License. For details see [LICENSE.md](https://github.com/GhoulMage/SassyCoilhead/blob/main/LICENSE.md)
<br>

5. **"How did you create an animation for it?"**
Basically, get the SpringMan GameObject from the game's assets (in shared assets1) and the body mesh (It's called Body, was hard to find).<br>
Then, clean up all missing references from SpringMan and set the Body mesh to it's missing mesh reference.<br>
Add an Animation component wherever the Animator is and create an animation clip. Create an AnimatorController with only that clip inside.<br>
Export the AnimationClip and AnimatorController in an AssetBundle, load it into the mod and replace the original coilhead RuntimeAnimatorController wtih mine whenever I want to make it dance.<br>
