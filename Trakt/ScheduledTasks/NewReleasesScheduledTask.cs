using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Model.Tasks;
using Trakt.Configuration;

namespace Trakt.ScheduledTasks
{
    public class NewReleasesScheduledTask : IScheduledTask, IConfigurableScheduledTask
    {
        private ITaskManager TaskManager { get; set; }

        public NewReleasesScheduledTask(ITaskManager taskMan)
        {
            TaskManager = taskMan;
        }
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
           foreach (var t in TaskManager.ScheduledTasks)
           {
               if (t.Name == "Refresh Internet Channels")
               {
                   await TaskManager.Execute(t, new TaskOptions());
               }
           }
        }

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
        {
            return new[]
            {
                new TaskTriggerInfo
                {
                    Type          = TaskTriggerInfo.TriggerInterval,
                    IntervalTicks = TimeSpan.FromDays(1).Ticks
                }
            };
        }

        public string Name        => "New Releases Monthly Premiere Update";
        public string Key         => "New Releases";
        public string Description => "Increment the New Releases Minimum Premiere Date Monthly.";
        public string Category    => "Library";
        public bool IsHidden      => true;
        public bool IsEnabled     => true;
        public bool IsLogged      => true;
    }
}