# XNB Reader Inspector

Read-only inspector for XNB type reader tables. Includes the LZX decoder previously supplied in the user's XNB repair tooling. Requires .NET 9 SDK. Does not edit input files.

Run from PowerShell:

```powershell
cd C:\path\to\XnbReaderInspector
dotnet run -c Release --project .\XnbReaderInspector.csproj -- 'D:\indie-game-archive\$1 Zombie Game, The (World) (XBLIG)\Content\Content.repo_0.xnb'
```

Shows reader table, shared-resource count and root reader index. A non-root registration may still be used by nested objects.
