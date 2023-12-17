Sassy Coilhead v1.0.0 (stable)
==============

Client-sided mod that makes the Coilhead do a funny dance if you stay near it for too long.<br>

> It knows it's gonna kill you.

Features
--------
* It works.
* Should be compatible with anything.
* Configurable in config/ghoulmage.funny.sassycoilhead.cfg

Bugs
--------
* Shouldn't have.
* But... I have a hunch that it's easier for the Coilhead to kill you if you stand very very close to it (similar to standing near it and looking at its head instead of at its body, but without looking at its head part).
<br>

I'll update this mod as needed. Report any bugs, incompatibilities or push request the appropiate fixes and I'll look into them.

To-do
--------
* Sync across clients?

For Players
-----------
Ignore everything here. Look at Releases or into the Thunderstore page to download a working build with the Asset Bundle in the correct folder.

For Devs
--------
1. **Useful API**<br>
* **static event CoilheadDanceCheck.OnCoilheadDance(SpringManAI);** raises whenever a coilhead succesfully begins their dance.<br>
* **CoilheadDanceCheck.StopDance();** Stops any ongoing dance.<br>
* **CoilheadDanceCheck.IsDancing;** True if currently dancing.<br>

> Besides all of that, know that the script CoilheadDanceCheck will attach itself to anything that spawns with SpringManAI

<br>

2. **Dependencies**
* Assembly-CSharp.dll (From Lethal Company_Data/Managed)
* UnityEngine.dll
* Unity.Netcode.Runtime.dll
<br>

3. **Build**
* Build normaly using dotnet or whatever.
* Requires sassycoilhead Asset Bundle in BepinEx/plugins/GhoulMage/funny
<br>

4. **Copyright**
* MIT License. For details see LICENSE
<br>

5. **"How did you create an animation for it?"**
Basically, get the SpringMan GameObject from the game's assets (in shared assets1) and the body mesh (It's called Body, was hard to find).<br>
Then, clean up all missing references from SpringMan and set the Body mesh to it's missing mesh reference.<br>
Add an Animation component wherever the Animator is and create an animation clip. Create an AnimatorController with only that clip inside.<br>
Export the AnimationClip and AnimatorController in an AssetBundle, load it into the mod and replace the original coilhead RuntimeAnimatorController wtih mine whenever I want to make it dance.<br>

6. **When I build this by myself, all I can see is Coilhead. Coilhead everywhere...**
With a DEBUG symbol it will add a patch to set the rarities of anything but Coilheads to 0. Remove the code or build without DEBUG.<br>