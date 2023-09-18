﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.View.Page;

namespace Snap.Hutao.Service.Navigation;

[Injection(InjectAs.Singleton, typeof(IDocumentationProvider))]
[ConstructorGenerated]
internal sealed partial class DocumentationProvider : IDocumentationProvider
{
    private const string Home = "https://hut.ao";

    private static readonly Dictionary<Type, string> TypeDocumentations = new()
    {
        [typeof(AchievementPage)] = "https://hut.ao/features/achievements.html",
        [typeof(AnnouncementPage)] = "https://hut.ao/features/dashboard.html",
        [typeof(AvatarPropertyPage)] = "https://hut.ao/features/character-data.html",
        [typeof(CultivationPage)] = "https://hut.ao/features/develop-plan.html",
        [typeof(DailyNotePage)] = "https://hut.ao/features/real-time-notes.html",
        [typeof(GachaLogPage)] = "https://hut.ao/features/wish-export.html",
        [typeof(HutaoDatabasePage)] = "https://hut.ao/features/hutao-API.html",
        [typeof(HutaoPassportPage)] = "https://hut.ao/zh/features/hutao-settings.html#%E8%83%A1%E6%A1%83%E5%B8%90%E5%8F%B7",
        [typeof(LaunchGamePage)] = "https://hut.ao/features/game-launcher.html",
        [typeof(LoginHoyoverseUserPage)] = "https://hut.ao/features/mhy-account-switch.html",
        [typeof(LoginMihoyoUserPage)] = "https://hut.ao/features/mhy-account-switch.html",
        [typeof(SettingPage)] = "https://hut.ao/features/hutao-settings.html",
        [typeof(SpiralAbyssRecordPage)] = "https://hut.ao/features/dashboard.html",
        [typeof(TestPage)] = Home,
        [typeof(WikiAvatarPage)] = "https://hut.ao/features/character-wiki.html",
        [typeof(WikiMonsterPage)] = "https://hut.ao/features/monster-wiki.html",
        [typeof(WikiWeaponPage)] = "https://hut.ao/features/weapon-wiki.html",
    };

    private readonly INavigationService navigationService;

    public string GetDocumentation()
    {
        if (navigationService.Current is { } type)
        {
            return TypeDocumentations[type];
        }

        return Home;
    }
}