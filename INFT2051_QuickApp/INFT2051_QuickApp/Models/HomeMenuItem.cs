﻿using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051_QuickApp.Models
{
    public enum MenuItemType
    {
        User,
        BrowseSimple,
        MyApp,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
