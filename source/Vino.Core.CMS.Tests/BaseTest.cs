using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace Vino.Core.CMS.Tests
{

    public class BaseTest
    {
        protected ITestOutputHelper OutPut { get; }

        public BaseTest(ITestOutputHelper outPut)
        {
            OutPut = outPut;
        }

        public void ComputeTime(Action action, string name = "", int testNum = 1)
        {
            var watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1; i++)
            {
                action();
            }

            watch.Stop();
            OutPut.WriteLine($"{name} after test num {testNum}, used {watch.ElapsedMilliseconds} ms.");
        }

    }
}
