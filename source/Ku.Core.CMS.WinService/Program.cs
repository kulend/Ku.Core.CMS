using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using NLog.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ku.Core.CMS.WinService
{
    /// <summary>
    /// 参照页面：https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-2.1
    /// </summary>
    public class Program
    {
        //使用 sc.exe 命令行工具创建服务。 binPath 值是应用的可执行文件的路径，其中包括可执行文件的文件名。 
        //等于号和路径开头的引号字符之间需要添加空格。
        //sc create Ku.Core.CMS.WinService binPath= "D:\wwwroots\official_website\WinService\Ku.Core.CMS.WinService.exe"


        //在服务之外运行时更便于进行测试和调试，因此通常仅在特定情况下添加调用 RunAsService 的代码。 
        //例如，应用可以使用 --console 命令行参数或在已附加调试器时作为控制台应用运行：
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("程序启动");

                var isService = !(Debugger.IsAttached || args.Contains("--console"));
                var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    builder.UseContentRoot(pathToContentRoot);
                }

                var host = builder.Build();

                if (isService)
                {
                    host.RunAsService();
                }
                else
                {
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
        }
    }
}
