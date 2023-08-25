﻿using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Client
{
    public class ClientPutDto : IDto
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        public string? Address { get; set; }
        public string? PhotoPath { get; set; }

    }
}
