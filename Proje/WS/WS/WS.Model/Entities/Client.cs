﻿using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Entities
{
    public class Client : IEntity
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? PhotoPath { get; set; }  
        public bool? IsActive { get; set; }
        public Order? Orders { get; set; }

    }
}
