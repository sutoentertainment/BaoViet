using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Services
{
    public abstract class ServiceBase : IService
    {
        public Manager Manager
        {
            get; set;
        }

        public ServiceBase(Manager manager)
        {
            Manager = manager;
        }

        public ServiceBase()
        {

        }
    }
}
