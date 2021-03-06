﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Power;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BaoViet.VisualStateTriggers
{
    public class BatteryTrigger : StateTriggerBase
    {
        public bool Charging { get; set; }
        //public double MinPercentage { get; set; }

        public BatteryTrigger()
        {
            Windows.Devices.Power.Battery.AggregateBattery.ReportUpdated
                += async (sender, args) => await UpdateStatus();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            UpdateStatus();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }


        private async Task UpdateStatus()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var batteryReport = Windows.Devices.Power.Battery.AggregateBattery.GetReport();
                var percentage = (batteryReport.RemainingCapacityInMilliwattHours.Value /
                                  (double)batteryReport.FullChargeCapacityInMilliwattHours.Value);

                switch (batteryReport.Status)
                {
                    case BatteryStatus.Charging:
                        SetActive(this.Charging);
                        break;
                    case BatteryStatus.Discharging:
                        SetActive(!this.Charging);
                        break;
                }
                // e.g: 0, current 56
                // e.g: 0, current 56

                //if(MinPercentage >= percentage){}
            });
        }
    }
}
