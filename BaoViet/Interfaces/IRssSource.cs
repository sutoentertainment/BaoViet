﻿using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.Interfaces
{
    public interface IRssSource
    {
        Task<IEnumerable<FeedItem>> GetFeed(string url);
    }
}