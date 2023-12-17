Sassy Coilhead v0.1.0 (development)
==============

Client-sided mod that makes the Coilhead do a funny dance if you stay near it for too long.<br>
It knows it's gonna kill you.

Bugs
----
* Who knows.

To-do
-----
* Hope it works
* Server-side sync?

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