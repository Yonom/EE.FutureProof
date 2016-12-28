# EE.FutureProof

Download from [NuGet](https://www.nuget.org/packages/EE.FutureProof/) and call ```.FutureProof(fromVersion)``` on your connection. ```fromVersion``` is the version of the game your bot was written for.

Example:

```csharp
FutureProofConnection c = cl.Multiplayer.CreateJoinRoom([...]).FutureProof(215);
```


## Performance

Using the above syntax, FutureProof has to login as a guest user in order to check the current game version. If the game version is already known, this step can be skipped by supplying the target version:

```csharp
FutureProofConnection c = cl.Multiplayer.CreateJoinRoom([...]).FutureProof(fromVersion, toVersion);
```

---

FutureProof downloads a signed dll (located in bin/EE.FutureProof.Bridge.dll) from GitHub the first time it is called. This step can be skipped by regularly downloading this file and placing it in the same folder as your assembly or in the GAC.

# Contributing

Upgraders are classes that convert messages from one version to another. Upgraders that upgrade the version by one are preferred because they can be stacked with other Upgraders to support a wide range of version numbers.
Anyone is free to create a new Upgrader. However, Upgraders of very old versions are not always welcome, so make sure to discuss it with me if the versions you are targetting are older than a few months.
