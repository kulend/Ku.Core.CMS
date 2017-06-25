using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Vino.Core.CMS.Core.Exceptions;

namespace Vino.Core.CMS.Core.Helper
{
    /// <summary>
    /// ID 生成帮助类
    /// </summary>
    public class ID
    {
        //数据中心ID
        private static long datacenterId;
        //机器ID
        private static long workerId;
        private static long sequence = 0L;

        private static long twepoch = 687888001020L; //唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳      

        private static readonly int workerIdBits = 5; //机器码字节数。4个字节用来保存机器码
        private static readonly int datacenterIdBits = 3;
        private static readonly long maxWorkerId = -1L ^ -1L << workerIdBits; //最大机器ID
        private static readonly long maxDatacenterId = -1L ^ (-1L << datacenterIdBits);//最大数据中心ID
        private static readonly int sequenceBits = 10; //计数器字节数，10个字节用来保存计数码

        private static readonly int workerIdShift = sequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
        private static readonly int datacenterIdShift = sequenceBits + workerIdBits;
        private static readonly int timestampLeftShift = sequenceBits + workerIdBits + datacenterIdBits; //时间戳左移动位数就是机器码和计数器总字节数  
        private static readonly long sequenceMask = -1L ^ -1L << sequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成  

        private long lastTimestamp = -1L;
        private static object syncRoot = new object();

        private static ID instance = null;

        public static void Initialize(IConfiguration configuration)
        {
            var section = configuration.GetSection("IdWorker");
            if (section == null)
            {
                throw new VinoException("IdHelper配置出错!");
            }

            //从配置文件中获取
            var zoneSetting = section["Zone"];
            if (string.IsNullOrEmpty(zoneSetting))
            {
                throw new VinoException("IdWorker生成规则中的Zone未进行配置。");
            }
            var datacenterId = int.Parse(zoneSetting);
            if (datacenterId > maxDatacenterId || datacenterId < 0)
            {
                throw new VinoException($"IdWorker生成规则中的Zone配置应在0~{maxDatacenterId}之间。");
            }
            var machineSetting = section["Machine"];
            var workerId = int.Parse(machineSetting);
            if (string.IsNullOrEmpty(machineSetting))
            {
                throw new VinoException("IdWorker生成规则中的Machine未进行配置。");
            }
            if (workerId > maxWorkerId || workerId < 0)
            {
                throw new VinoException($"IdWorker生成规则中的Machine配置应在0~{maxWorkerId}之间。");
            }

            ID.datacenterId = datacenterId;
            ID.workerId = workerId;
        }

        public static long NewID()
        {
            if (instance == null)
            {
                instance = new ID();
            }

            return instance.nextId();
        }

        private long nextId()
        {
            lock (syncRoot)
            {
                long timestamp = timeGen();
                if (this.lastTimestamp == timestamp)
                { 
                    //同一微妙中生成ID
                    ID.sequence = (ID.sequence + 1) & ID.sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限  
                    if (ID.sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙  
                        timestamp = tillNextMillis(this.lastTimestamp);
                    }
                }
                else
                { 
                    //不同微秒生成ID  
                    ID.sequence = 0; //计数清0
                }
                if (timestamp < lastTimestamp)
                {
                    //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过  
                    throw new VinoException(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                        this.lastTimestamp - timestamp));
                }
                this.lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳  
                long nextId = (timestamp - twepoch << timestampLeftShift) | (ID.datacenterId << datacenterIdShift) | ID.workerId << ID.workerIdShift | ID.sequence;
                return nextId;
            }
        }

        /// <summary>  
        /// 获取下一微秒时间戳  
        /// </summary>  
        /// <param name="lastTimestamp"></param>  
        /// <returns></returns>  
        private long tillNextMillis(long lastTimestamp)
        {
            long timestamp = timeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = timeGen();
            }
            return timestamp;
        }

        /// <summary>  
        /// 生成当前时间戳  
        /// </summary>  
        /// <returns></returns>  
        private long timeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
