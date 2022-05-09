﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Snap.Hutao.Control.Cancellable;
using Snap.Hutao.Core;
using Snap.Hutao.Core.Threading;
using Snap.Hutao.Factory.Abstraction;
using Snap.Hutao.Service.Abstraction;
using Snap.Hutao.Web.Hoyolab.Hk4e.Common.Announcement;
using System.Windows.Input;

namespace Snap.Hutao.ViewModel;

/// <summary>
/// 公告视图模型
/// </summary>
[Injection(InjectAs.Transient)]
internal class AnnouncementViewModel : ObservableObject, ISupportCancellation
{
    private readonly IAnnouncementService announcementService;
    private readonly INavigationService navigationService;
    private readonly IInfoBarService infoBarService;
    private readonly ILogger<AnnouncementViewModel> logger;

    private AnnouncementWrapper? announcement;

    /// <summary>
    /// 构造一个公告视图模型
    /// </summary>
    /// <param name="announcementService">公告服务</param>
    /// <param name="navigationService">导航服务</param>
    /// <param name="asyncRelayCommandFactory">异步命令工厂</param>
    /// <param name="infoBarService">信息条服务</param>
    /// <param name="logger">日志器</param>
    public AnnouncementViewModel(
        IAnnouncementService announcementService,
        INavigationService navigationService,
        IAsyncRelayCommandFactory asyncRelayCommandFactory,
        IInfoBarService infoBarService,
        ILogger<AnnouncementViewModel> logger)
    {
        this.announcementService = announcementService;
        this.navigationService = navigationService;
        this.infoBarService = infoBarService;
        this.logger = logger;

        OpenUICommand = asyncRelayCommandFactory.Create(OpenUIAsync);
        OpenAnnouncementUICommand = new RelayCommand<string>(OpenAnnouncementUI);
    }

    /// <inheritdoc/>
    public CancellationToken CancellationToken { get; set; }

    /// <summary>
    /// 公告
    /// </summary>
    public AnnouncementWrapper? Announcement
    {
        get => announcement;

        set => SetProperty(ref announcement, value);
    }

    /// <summary>
    /// 打开界面监视器
    /// </summary>
    public Watcher OpeningUI { get; } = new(false);

    /// <summary>
    /// 打开界面触发的命令
    /// </summary>
    public ICommand OpenUICommand { get; }

    /// <summary>
    /// 打开公告UI触发的命令
    /// </summary>
    public ICommand OpenAnnouncementUICommand { get; }

    private async Task OpenUIAsync()
    {
        using (OpeningUI.Watch())
        {
            try
            {
                Announcement = await announcementService.GetAnnouncementsAsync(OpenAnnouncementUICommand, CancellationToken);
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation("Open UI cancelled");
            }
        }
    }

    private void OpenAnnouncementUI(string? content)
    {
        logger.LogInformation("Open Announcement Command Triggered");

        if (WebView2Helper.IsSupported)
        {
            navigationService.Navigate<View.Page.AnnouncementContentPage>(data: content);
        }
        else
        {
            infoBarService.Warning("尚未安装 WebView2 运行时。");
        }
    }
}