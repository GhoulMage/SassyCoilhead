Disaster Box v0.1.0 (dev)
==============

Client-sided mod that changes the Jester's windup music and adds a cool looping metal theme over it.<br>
[Audio Original by LENK64 - YouTube](https://www.youtube.com/watch?v=RjtfGj1dNFI)

Features
--------
* Should be compatible with anything that doesn't change the Jester's windup music or modifies it's popup timer.
* Configurable in config/ghoulmage.funny.disasterbox.cfg

Bugs
--------
* Shouldn't have.
<br>

I'll update this mod as needed. Report any bugs, incompatibilities or push request the appropiate fixes and I'll look into them.

Download Instructions
-----------
Ignore everything here. Look at Releases or into the Thunderstore page to download a working build with the Asset Bundle in the correct folder.

For Devs
--------
1. **Dependencies**
* Assembly-CSharp.dll (From Lethal Company_Data/Managed)
* UnityEngine.dll
* Unity.Netcode.Runtime.dll
* [LC_API](https://thunderstore.io/c/lethal-company/p/2018/LC_API/)
* [GhoulMage.LethalCompany.dll](https://github.com/GhoulMage/GhoulMage.LethalCompany)
<br>

2. **Build**
* Build normaly using dotnet or whatever.
* Requires disasterbox Asset Bundle in BepinEx/plugins/GhoulMage/funny
<br>

3. **Copyright**
* MIT License. For details see LICENSE
<br>

4. **When I build this by myself, all I can see is Jester. Jester everywhere...**
With a DEBUG symbol it will add a patch to set the rarities of anything but Jesters to 0. Remove the code or build without DEBUG.<br>