using ArcardnoidContent;
using ArcardnoidShared.Framework.ServiceProvider;
using System;

GameServiceProvider.RegisterService(new ArcardnoidGame());
using var game = new arcardnoid.ArCardNoidGame();
game.Run();
Environment.Exit(0);