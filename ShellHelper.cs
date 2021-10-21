using System;
using System.Diagnostics;
using System.Threading.Tasks;
using log4net;

namespace BashCallDemoHelpers
{
    public static class ShellHelper
    {
        public static Task<int> Bash(this string cmd, ILog log, String consoleOut)
        {
            var source = new TaskCompletionSource<int>();
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, args) =>
              {
                  log.Warn(process.StandardError.ReadToEnd());
                  log.Info(process.StandardOutput.ReadToEnd());
                  if (process.ExitCode == 0)
                  {
                      source.SetResult(0);
                  }
                  else
                  {
                      source.SetException(new Exception($"Command `{cmd}` failed with exit code `{process.ExitCode}`"));
                  }

                  process.Dispose();
              };

            try
            {
                process.Start();
                consoleOut = process.StandardOutput.ReadToEnd();
            }
            catch (Exception e)
            {
                log.Error($"Command {cmd} failed");
                source.SetException(e);
            }

            return source.Task;
        }
    }
}