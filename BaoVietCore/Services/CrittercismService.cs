using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrittercismSDK;
using System.Threading.Tasks;

namespace BaoVietCore.Services
{
    public class CrittercismService : ServiceBase, ICrittercismService
    {
        //b1b4014cbf2a4f4a849e0d6a50eeb0b000444503
        string appID = "";
        public CrittercismService(Manager man, string AppID) : base(man)
        {
            appID = AppID;
        }

        public void Init()
        {
            Crittercism.Init(appID);
        }

        public void Log(string bread)
        {
            Crittercism.LeaveBreadcrumb(bread);
        }

        public void LogExceptions(Exception e)
        {
            Crittercism.LogHandledException(e);
        }

        public void SetUsername(string username)
        {
            Crittercism.SetUsername(username);
        }

        List<string> FlowNames = new List<string>();

        public void StartFlow(string flowName)
        {
            if (!FlowNames.Any(f => f == flowName))
                FlowNames.Add(flowName);
            Crittercism.BeginUserflow(flowName);
        }

        public void EndFlow(string flowName)
        {
            FlowNames.Remove(flowName);
            Crittercism.EndUserflow(flowName);
        }

        public void CancelFlow(string flowName)
        {
            FlowNames.Remove(flowName);
            Crittercism.CancelUserflow(flowName);
        }

        public void FailFlow(string flowName)
        {
            FlowNames.Remove(flowName);
            Crittercism.FailUserflow(flowName);
        }

        public List<string> GetCurrentFlow()
        {
            return FlowNames;
        }

        public void SetOptOutStatus(bool status)
        {
            Crittercism.SetOptOutStatus(status);
        }

        public bool GetOptOutStatus()
        {
            return Crittercism.GetOptOutStatus();
        }
    }
}
