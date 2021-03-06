﻿using System;
using MediaBrowser.Controller.Entities;
using Trakt.Api.DataContracts.BaseModel;

namespace Trakt.Api.DataContracts.Users
{
    public class TraktListsItem
    {
        public int rank { get; set; } 
        public int id { get; set; } 
        public DateTime listedat { get; set; } 
        public object notes { get; set; } 
        public string type { get; set; } 
        public TraktMovie movie { get; set; } 
        public BaseItem EmbyMovie { get; set; }
    }
}