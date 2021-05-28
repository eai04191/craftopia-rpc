# Discord Rich Presence for Craftopia

![image](https://user-images.githubusercontent.com/3516343/120014047-87821a80-c01c-11eb-8205-5cd2ea943af5.png)

## Installation

0. If you haven't installed [BepInEx](https://bepinex.github.io/bepinex_docs/master/index.html) yet, follow the [instructions](https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html) to install it. BepInEx will become the standard for Craftopia modding.
1. Download binary from [releases](https://github.com/eai04191/craftopia-rpc/releases).
2. Put `CraftopiaRPC.dll` into `Craftopia\BepInEx\plugins`
3. Download `discord-rpc-win.zip` from [discord-rps/releases](https://github.com/discord/discord-rpc/releases)
4. Put `discord-rpc\win64-dynamic\bin\discord-rpc.dll` into `Craftopia\Craftopia_Data\Plugins`
5. Rename `discord-rpc.dll` to `0discord-rpc.dll`.

## Usage

Play Craftopia, That's all.

## Build

I am building using Visual Studio 2019.

1. Copy `AD__Overcraft.dll`, `Assembly-CSharp.dll`, `UnityEngine.dll`, `UnityEngine.CoreModule.dll` and `UniRx.dll` from `Craftopia\Craftopia_Data\Managed` to `./Libs`.
2. Copy `BepInEx.dll` from `Craftopia\BepInEx\core` to `./Libs`.
3. Now you should be able to build.

## License

MIT License

Most of the code was copied from [Weilbyte/RWRichPresence](https://github.com/Weilbyte/RWRichPresence), licensed under the MIT license. I'd like to take this opportunity to thank you for Weilbyte.
