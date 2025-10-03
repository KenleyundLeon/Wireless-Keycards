using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace Wireless_Keycards;

public class Main : Plugin<Config>
{
    public static Main Instance { get; private set; }
    public Events events = new();
    public override string Name => "Wireless-Keycards";
    public override string Description => "Allows the usage of Wireless keycards";
    public override string Author => "Kenley M.";
    public override Version Version => new(1, 1, 5);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    public string githubRepo = "KenleyundLeon/Wireless-Keycards";
    public override void Enable()
    {
        Instance = this;
        Logger.Info("WirelessCard Enabled.");
        Logger.Info(githubRepo);
        if (!Config.Enabled) return;
        CustomHandlersManager.RegisterEventsHandler(events);
    }

    public override void Disable()
    {
        Instance = null;
        Logger.Info("WirelessCard Disabled.");
        if (!Config.Enabled) return;
        CustomHandlersManager.UnregisterEventsHandler(events);
    }
}

