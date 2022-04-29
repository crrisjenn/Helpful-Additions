## What it is

Other mods can communicate with Helpful Additions in order for their content to appear within its various features, such as in the round info screen.

## Functions available

* AddCustomBloon\
This function allows other modders to add custom bloon graphics, such that they show up within Helpful Additions.

## How to set it up

In order to interface with Helpful Additions without adding it as a dependency add the following file to your project:\
[HelpfulAdditions.cs](https://github.com/Baydock/Helpful-Additions/blob/main/ModInterop/HelpfulAdditions.cs)

Make sure that the functions from here are called from `MelonMod.OnApplicationLateStart` or after to make sure that Helpful Additions has been loaded.
