using System;
using System.Collections.Generic;

namespace BaoVietCore.Interfaces
{
    public interface ICrittercismService
    {
        void CancelFlow(string flowName);
        void EndFlow(string flowName);
        void FailFlow(string flowName);
        List<string> GetCurrentFlow();
        bool GetOptOutStatus();
        void Init();
        void Log(string bread);
        void LogExceptions(Exception e);
        void SetOptOutStatus(bool status);
        void SetUsername(string username);
        void StartFlow(string flowName);
    }
}