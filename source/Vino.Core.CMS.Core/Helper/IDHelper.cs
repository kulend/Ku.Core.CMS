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
    public static class IdHelper
    {
        private static string _baseIdFilePath;
        private static int _sequenceStep = 100;
        private static IdWorker _defaultIdWorker;

        public static void Initialize(IConfiguration configuration, IHostingEnvironment env)
        {
            var section = configuration.GetSection("IdHelper");
            if (section == null)
            {
                throw new VinoException("IdHelper配置出错!");
            }

            //从配置文件中获取
            var zoneSetting = section["zone"];
            if (string.IsNullOrEmpty(zoneSetting))
            {
                throw new VinoException("IdHelper生成规则中的Zone未进行配置。");
            }
            var machineSetting = section["machine"];
            if (string.IsNullOrEmpty(machineSetting))
            {
                throw new VinoException("IdHelper生成规则中的Machine未进行配置。");
            }
            var seqStepSetting = section["step"];
            if (string.IsNullOrEmpty(seqStepSetting))
            {
                throw new VinoException("IdHelper生成规则中的Step未进行配置。");
            }
            var zone = int.Parse(zoneSetting);
            var machine = int.Parse(machineSetting);

            string idFilePath = section["file"];
            idFilePath = string.IsNullOrEmpty(idFilePath) ? "id_files" : idFilePath;
            _baseIdFilePath = Path.Combine(env.ContentRootPath, idFilePath);  
            if (!Directory.Exists(_baseIdFilePath))
            {
                Directory.CreateDirectory(_baseIdFilePath);
            }

            _sequenceStep = int.Parse(seqStepSetting);
            //创建默认的Id生成器
            _defaultIdWorker = new IdWorker(zone, machine);
        }

        /// <summary>
        /// 获取下个ID
        /// </summary>
        public static long GetNextId()
        {
            return IdHelper._defaultIdWorker.GetNextId();
        }

        public class IdWorker
        {
            /*
				10:16  time(day) 32768		2014-2103
				26:28 sequence (268435456) 256M	3106/s
				54:4 (zone)		16	
				58:6 (machine)	256	
			*/
            private const int ZONEBITS = 4;
            private const int MAXZONEID = (2 << ZONEBITS) - 1;//31
            private const int MACHINEBITS = 6;
            private const int MAXMACHINEID = (2 << MACHINEBITS) - 1;//127
            private const int SEQUENCEBITS = 28;
            private const int DAYSHIFT = SEQUENCEBITS + ZONEBITS + MACHINEBITS;//28+4+6=38
            private const int SEQUENCESHIFT = ZONEBITS + MACHINEBITS;//4+6=10
            private const int ZONESHIFT = MACHINEBITS;//6
            private const long dayTicks = 86400L * 1000 * 10000;//一天等于多少100毫微秒
            private static readonly long startTime = (new DateTime(2014, 1, 1)).Ticks;
            private long _curDay = startTime / dayTicks;
            private object lockObj = new object();
            private int _zone;
            private int _machine;
            private long _zoneMachine;
            private long _sequence = 0L;
            private long _maxSequence = 0L;
            private string _seedFilePath;

            public IdWorker(int zone, int machine)
            {
                if (zone > MAXZONEID)
                {
                    throw new VinoException("IdHelper配置参数zone不能大于" + MAXZONEID);
                }
                if (machine > MAXMACHINEID)
                {
                    throw new VinoException("IdHelper配置参数machine不能大于" + MAXMACHINEID);
                }
                this._zone = zone;
                this._machine = machine;
                this._zoneMachine = (long)((zone << ZONESHIFT) | machine);
                this._seedFilePath = Path.Combine(IdHelper._baseIdFilePath, string.Concat(this._zone, "-", this._machine, ".id"));
            }

            public long GetNextId()
            {
                var day = (DateTime.UtcNow.Ticks - startTime) / dayTicks;
                long rd;
                lock (lockObj)
                {
                    if (this._sequence >= this._maxSequence || day != this._curDay)
                    {
                        this.ReadSeedBuffer((int)day);
                        _curDay = day;
                    }
                    rd = this._sequence++;
                }
                return (day << DAYSHIFT) | (rd << SEQUENCESHIFT) | this._zoneMachine;
            }

            private void ReadSeedBuffer(int day)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        using (var fs = new FileStream(
                            this._seedFilePath,
                            FileMode.OpenOrCreate,
                            FileAccess.ReadWrite,
                            FileShare.Read
                        ))
                        {
                            int seed;
                            int curDay;
                            var buf = new byte[256];
                            int l = fs.Read(buf, 0, buf.Length);
                            if (l == 0)
                            {
                                curDay = 0;
                                seed = 0;//Math.Abs((int)DateTime.Now.Ticks) & random_mask;
                            }
                            else
                            {
                                var s = Encoding.UTF8.GetString(buf, 0, l).Trim();
                                var idx = s.Split('|');
                                if (idx.Length == 1)
                                {
                                    seed = int.Parse(s);
                                    curDay = day;
                                }
                                else
                                {
                                    seed = int.Parse(idx[0].Trim());
                                    curDay = int.Parse(idx[1].Trim());
                                }
                            }
                            if (curDay != day)
                            {
                                curDay = day;
                                seed = 0;
                            }
                            this._sequence = seed;
                            this._maxSequence = seed + IdHelper._sequenceStep;
                            //留空主要为了不出现截断情况
                            string refreshContent = string.Format("{0}|{1}                                 "
                                , this._maxSequence.ToString()
                                , curDay.ToString());
                            fs.Seek(0, SeekOrigin.Begin);
                            l = Encoding.UTF8.GetBytes(refreshContent, 0, refreshContent.Length, buf, 0);
                            fs.Write(buf, 0, l);
                            fs.Flush();
                        }
                        return;
                    }
                    catch (IOException e)
                    {
                        if (i == 9)
                        {
                            //sw.log.write("ident", e);
                            throw e;
                        }
                    }
                    catch (System.Exception e)
                    {
                        //sw.log.write("ident", e);
                        throw e;
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
