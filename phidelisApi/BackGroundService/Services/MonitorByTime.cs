using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackGroundService.Services
{
    public abstract class MonitorByTime : IHostedService, IDisposable
    {
        private Task _executingTask;
        
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            if (_executingTask.IsCompleted)
                return _executingTask;

            return Task.CompletedTask;
        }


        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
                return;

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                _stoppingCts.Dispose();
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected bool IsExecutionTime(string specifiedTime)
        {
            var pt_Br = new System.Globalization.CultureInfo("pt-BR");
            var timeNow = DateTime.Now.ToString("t", pt_Br);

            if (timeNow == specifiedTime)
                return true;

            return false;
        }

        public void Dispose()
        {
            _stoppingCts.Dispose();
            _stoppingCts.Cancel();
        }
    }
}
