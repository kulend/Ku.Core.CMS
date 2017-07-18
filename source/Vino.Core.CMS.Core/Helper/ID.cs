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
        /// <summary>
        /// zone 占据long（64位）中的位数
        /// </summary>
        private const int ZONEBITS = 5;
        /// <summary>
        /// machine 占据long（64位）中的位数
        /// </summary>
        private const int MACHINEBITS = 5;
        /// <summary>
        /// 流水号占据long（64位）中的位数
        /// </summary>
        private const int SEQUENCEBITS = 12;
        /// <summary>
        /// zone允许的最大值
        /// </summary>
        private const int MAXZONEID = -1 ^ (-1 << ZONEBITS);
        /// <summary>
        /// machine允许的最大值
        /// </summary>
        private const int MAXMACHINEID = -1 ^ (-1 << MACHINEBITS);
        /// <summary>
        /// 流水号的最大值
        /// </summary>
        private const int MAXSEQUENCE = -1 ^ (-1 << SEQUENCEBITS);
        /// <summary>
        /// 时间左移位数
        /// </summary>
        private const int TICKSSHIFT = ZONEBITS + MACHINEBITS + SEQUENCEBITS;
        /// <summary>
        /// 区域左移位数
        /// </summary>
        private const int ZONESHIFT = MACHINEBITS + SEQUENCEBITS;
        /// <summary>
        /// 机器左移位数
        /// </summary>
        private const int MACHINESHIFT = SEQUENCEBITS;
        /// <summary>
        /// 起始时间的ticks
        /// [2017-06-14 15:50:47] - new DateTime(1970, 1, 1, 0, 0, 0).Ticks /10000
        /// </summary>
        private const long STARTTICKS = 1497455447582L;
        /// <summary>
        /// 1970-1-1 的ticks
        /// </summary>
        private const long TICKS1970 = 621355968000000000L;

        private static object syncRoot = new object();

        private long _currentSequence = 0L;
        private long _lastTicks = 0L;
        private long _zoneMachineValue = 0L;

        private static int _zoneId;
        private static int _machineId;
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
            var zoneId = int.Parse(zoneSetting);
            if (zoneId > MAXZONEID || zoneId < 0)
            {
                throw new VinoException($"IdWorker生成规则中的Zone配置应在0~{MAXZONEID}之间。");
            }
            var machineSetting = section["Machine"];
            if (string.IsNullOrEmpty(machineSetting))
            {
                throw new VinoException("IdWorker生成规则中的Machine未进行配置。");
            }
            var machineId = int.Parse(machineSetting);
            if (machineId > MAXMACHINEID || machineId < 0)
            {
                throw new VinoException($"IdWorker生成规则中的Machine配置应在0~{MAXMACHINEID}之间。");
            }

            ID._zoneId = zoneId;
            ID._machineId = machineId;
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
                var currentTicks = GetNowTicks();
                if (currentTicks < _lastTicks)
                {
                    throw new VinoException("系统时间出现错误");
                }

                if (currentTicks == _lastTicks)
                {
                    _currentSequence = (_currentSequence + 1) & MAXSEQUENCE;
                    if (_currentSequence == 0) //超过了流水号的最大值，则重置时间
                    {
                        currentTicks = UtilNextTicks();
                    }
                }
                else
                {
                    _currentSequence = 0L;
                }
                _lastTicks = currentTicks;

                return (currentTicks - STARTTICKS) << TICKSSHIFT | _zoneMachineValue | _currentSequence;
            }
        }

        private long UtilNextTicks()
        {
            long ticks = GetNowTicks();
            while (ticks <= _lastTicks)
            {
                ticks = GetNowTicks();
            }
            return ticks;
        }

        private long GetNowTicks()
        {
            return (DateTime.Now.Ticks - TICKS1970) / 10000;
        }
    }
}
