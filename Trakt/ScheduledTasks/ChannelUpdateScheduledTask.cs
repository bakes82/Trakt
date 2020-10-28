using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Model.Tasks;
using Trakt.Configuration;

namespace Trakt.ScheduledTasks
{
    public class ChannelUpdateScheduledTask : IScheduledTask, IConfigurableScheduledTask
    {
        private ITaskManager TaskManager { get; set; }

        public ChannelUpdateScheduledTask(ITaskManager taskMan)
        {
            TaskManager = taskMan;
        }
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
           foreach (var t in TaskManager.ScheduledTasks)
           {
               if (t.Name == "Update Channel" + ListConfig.ChannelName)
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

        public string Name        => ListConfig.ChannelName + "Channel";
        public string Key         => "Update " + ListConfig.ChannelName;
        public string Description => "Create/Update channel from trakt list.";
        public string Category    => "Library";
        public bool IsHidden      => true;
        public bool IsEnabled     => true;
        public bool IsLogged      => true;
    }
}