﻿using OrchidCavalry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry
{
    public class AlertService : IAlertService
    {
        public void DisplayAlert(string message, string header = "")
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.DisplayAlert(message, header, "Cancel");
        }
    }
}