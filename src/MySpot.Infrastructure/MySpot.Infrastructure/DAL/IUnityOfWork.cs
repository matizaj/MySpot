﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL
{
    internal interface IUnityOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
