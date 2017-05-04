using System;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace VPhotoLoader
{
    public class TaskProgressReporter : ITaskProgress
    {
        public TaskProgressReporter() { }

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        protected virtual void OnReport(int value, object state)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(value, state));
            }
        }

        void ITaskProgress.Report(int value) { OnReport(value, null); }

        void ITaskProgress.Report(int value, object state) { OnReport(value, state); }

    }
}