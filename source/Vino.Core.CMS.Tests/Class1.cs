using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using Ku.Core.CMS.Domain.Enum.WeChat;
using Xunit;
using Xunit.Abstractions;

namespace Ku.Core.CMS.Tests
{
    public class Class1 : BaseTest
    {
        public Class1(ITestOutputHelper outPut) : base(outPut)
        {
        }

        [Fact]
        public void run()
        {
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                EmWxAccountType value = EmWxAccountType.Service;
                var field = value.GetType().GetField("Service");
                var attr = field?.GetCustomAttribute<DisplayAttribute>();
            }
            watch.Stop();
            OutPut.WriteLine($"used {watch.ElapsedMilliseconds} ms.");
        }
    }
}
