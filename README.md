# MarkovJunior-unity-lib
[MarkovJunior](https://github.com/mxgmn/MarkovJunior) is a probabilistic programming language where programs are combinations of rewrite rules and inference is performed via constraint propagation, developed by Maxim Gumin.

This fork is based on fork [MarkovJunior-docs](https://github.com/kaya3/MarkovJunior-docs) by Andrew Kay which adds documentation and a few explanatory comments, which may be useful to others reading the code.

This fork uses PriorityQueue implementation from [.NET Platform](https://github.com/dotnet).

## What is it
This is slightly modified version of [MarkovJunior](https://github.com/mxgmn/MarkovJunior) and [MarkovJunior-docs](https://github.com/kaya3/MarkovJunior-docs) repositories to provide MarkovJunior algorithm as API library for Unity. It can be built as DLL and put to Unity project, or as alternative folder `MarkovJuniorUnity` could be just dropped somewhere in Unity project. Library can also be used with Godot by building `MarkovJuniorLibGodot` and referencing it.

It does not have any GUI or behaviors to interact with world and it will not place objects automatically.

For input it accepts textures, .vox files, xmls. It returns either `Texture2D` either `byte[]` containing .vox file contents.

Also it has nodes to use together with [Map Graph](https://assetstore.unity.com/packages/tools/utilities/map-graph-177023) asset by [Insane Scatterbrain](https://www.insanescatterbrain.com/).

## Limitations
There is ability to pass "tileset" and "folder" tags as well as ".vox" files, but it is not tested yet - could be buggy.

Nodes for Map Graph does not support tilesets, 3D models and some config parameters. And copy-pasting dowsn't copy XML.

Probably some other bugs

## Folder structure
- `/MarkovJuniorLib` - base MarkovJunior library based on .Net Standard 2.0. It could be used as is without Unity or Godot
- `/MarkovJuniorLibGodot` - MarkovJunior library adapted to be build for Godot
- `/MarkovJuniorLibUnity` - MarkovJunior library adapted to be build for Unity
- `/MarkovJuniorUnity` - MarkovJunior library adapted to be placed into `/Assets` folder of Unity project
- `/Stuff` - MarkovJunior models taken from initial [MarkovJunior](https://github.com/mxgmn/MarkovJunior) repository that are supported.

# How to:
## Install to Unity
### Option 1 - install .unitypackage

You can find latest package in [releases](https://github.com/giollord/MarkovJunior-unity-lib/releases/latest).

If you do not use Map Graph, just do not import folder `MapGraphExt` or remove it after import.

### Option 2 - copy-paste MarkovJuniorUnity folder

Copy `MarkovJuniorUnity` folder to your project's `Assets` folder.

If you do not use Map Graph, just remove folder `MapGraphExt`

### Option 3 - DLL
Might be needed if you use older Unity version and having compilation errors.

Build solution separately (will need to update dependency to UnityEngine.dll) and put DLL to `Assets` folder of your Unity project.
<br>
More info can be found [on official Unity docs](https://docs.unity3d.com/Manual/UsingDLL.html).

If you need Map Graph nodes as well, copy folder `Unity/MapGraphExt` to your Unity project

## MarkovJunior models documentation

Check documentation at [MarkovJunior](https://github.com/mxgmn/MarkovJunior), developed by Maxim Gumin, as well as unofficial [technical notes](https://gist.github.com/dogles/a926ab890552cc7e45400a930398449d) by Dan Ogles and [code documentation](https://github.com/kaya3/MarkovJunior-docs) by Andrew Kay.

Since this repository forked from https://github.com/kaya3/MarkovJunior-docs, you can check it as well.

Also, clone locally original [MarkovJunior](https://github.com/mxgmn/MarkovJunior) repository, build it and run and then look at results and compare with model XMLs.

## Use it with Map Graph

You need to own asset [Map Graph](https://assetstore.unity.com/packages/tools/utilities/map-graph-177023) asset by [Insane Scatterbrain](https://www.insanescatterbrain.com/) to use MarkovJunior-unity-lib with it.

1. Install MarkovJunior-unity-lib (see [Install to Unity](#install-to-unity) section) to your project that already has Map Graph installed
2. Create new Map Graph or open existing one (but better to create new one just in case)
3. Add "Markov Junior" node. It will have pre-defined [cave XML](https://github.com/mxgmn/MarkovJunior/blob/main/models/Cave.xml)
4. Attach input port "Size". It could be square like 50x50 or rectangle like 10x20 or 300x123
5. Run graph. Result will look like in [Example 1](#example-1)

## Using asset textures
In order to use MarkovJunior-unity-lib with asset textures (like in [Example 3](#example-3)), ensure that these settings match:
1. Non-Power of 2 is set to None
2. Read/Write is checked
3. Generate mipmaps is unchecked
4. Compression is set to None
![Texture requirements](/ReadmeImages/texture-requirements.png)

## Samples
These samples require [Map Graph](https://assetstore.unity.com/packages/tools/utilities/map-graph-177023) asset. Samples are included with unitypackage in [releases](https://github.com/giollord/MarkovJunior-unity-lib/releases/latest).

### Sample 1
Basic example

![Cave sample](/ReadmeImages/cave.PNG)

### Sample 2
Input image is generated using default colors with Map Graph and then passed to MarkovJunior runner.
Also black color 'B' is overridden with dark magenta.

In this example used modified [voronoi.xml](https://github.com/mxgmn/MarkovJunior/blob/main/models/Voronoi.xml)

```xml
<sequence values="BYE">
  <all steps="2">
    <rule in="EB" out="*E"/>
    <rule in="YB" out="*Y"/>
  </all>
</sequence>
```

![Initial texture](/ReadmeImages/initial-texture.PNG)

### Sample 3
Demonstrates how to use texture sample named "Flowers" in WFC node. XML is taken from [WaveFlowers.xml](https://github.com/mxgmn/MarkovJunior/blob/main/models/WaveFlowers.xml), image taken from [Flowers.png](https://github.com/mxgmn/MarkovJunior/blob/main/resources/samples/Flowers.png).

Used texture import settings should follow the rules in section [Using asset textures](#Using-asset-textures)

![Flowers](/ReadmeImages/flowers.PNG)

### Sample 4
Demonstrates one of the coolest examples of MarkovJunior use - dungeon generation. XML is taken from [DungeonGrowth.xml](https://github.com/mxgmn/MarkovJunior/blob/main/models/DungeonGrowth.xml), images are taken from [DungeonGrowth](https://github.com/mxgmn/MarkovJunior/tree/main/resources/rules/DungeonGrowth).

Used texture import settings should follow the rules in section [Using asset textures](#Using-asset-textures)

![Dungeon growth](/ReadmeImages/dungeon-growth.png)

### Sample 5
Demonstrates how to use random seed from graph runner. XML is taken from [BlueNoise.xml](https://github.com/mxgmn/MarkovJunior/blob/main/models/BlueNoise.xml).

![Dungeon growth](/ReadmeImages/blue-noise.png)

