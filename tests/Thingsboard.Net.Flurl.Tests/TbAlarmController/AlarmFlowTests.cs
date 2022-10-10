﻿using Thingsboard.Net.TbAlarmController;
using Thingsboard.Net.TbDeviceController;

namespace Thingsboard.Net.Tests.TbAlarmController;

public class AlarmFlowTests
{
    [Fact]
    public async Task TestCompletedFlow()
    {
        var newAlarm = await CreateAlarmAsync();
    }

    private async Task<TbAlarm> CreateAlarmAsync()
    {
        // arrange
        using var service = new TbTestService();
        var       api     = service.GetRequiredService<ITbAlarmApi>();
        var       device  = await service.GetDeviceByNameAsync("Test Device A1");

        // act
        var newAlarm = new TbAlarm
        {
            Id                     = null,
            TenantId               = null,
            CustomerId             = null,
            CreatedTime            = DateTime.Now,
            Name                   = "Test alarm",
            Type                   = "Test alarm type",
            Originator             = device.Id,
            Severity               = TbAlarmSeverity.CRITICAL,
            Status                 = TbAlarmStatus.ACTIVE_UNACK,
            StartTs                = DateTime.Now,
            EndTs                  = null,
            AckTs                  = null,
            ClearTs                = null,
            Details                = new Dictionary<string, object>() {["prop"] = "value"},
            Propagate              = false,
            PropagateToOwner       = false,
            PropagateToTenant      = false,
            PropagateRelationTypes = null,
        };
        await api.SaveAlarmAsync(newAlarm);

        // assert
        Assert.True(true);

        return newAlarm;
    }

    [Fact]
    public async Task WhenDeviceNotFound()
    {
        // arrange
        using var service = new TbTestService();
        var       api     = service.GetRequiredService<ITbDeviceClient>();

        // act
        var deviceId = Guid.Empty;
        var device   = await api.GetDeviceByIdAsync(deviceId);

        Assert.Null(device);
    }
}
