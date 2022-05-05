using DocumentFormat.OpenXml.Spreadsheet;
using ReGen.Configuration;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCenterApiClient;

namespace ReGen.CLI
{
    public static class ReportOperations
    {
        public class JobResult : IComparable<JobResult>
        {
            public string Target;

            public string JobName;

            public Worksheet Worksheet;

            public int CompareTo(JobResult other)
            {
                if (this.Target != other.Target)
                    return this.Target.CompareTo(other.Target);
                else if (this.JobName != other.JobName)
                    return this.JobName.CompareTo(other.JobName);
                else
                    return 0;
            }
        }

        public static Task<JobResult>[] Execute(this ProgramConfiguration config)
        {
            List<Task<JobResult>> results = new List<Task<JobResult>>();

            foreach (var target in config.Work)
            {
                var categories = target.Jobs.Select((item) => item.Category);
                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.Custom))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.Custom);
                    var subResults = ExecuteSSHJob(config, target, jobs);
                    results.AddRange(subResults);
                }

                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.McAfee))
                {
                    //var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.McAfee);
                    throw new NotSupportedException();
                }

                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.Acronis))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.Acronis);

                }

                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.vCenter))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.vCenter);
                    var subResults = ExecuteVCenterJob(config, target, jobs);
                    results.AddRange(subResults);
                }
            }

            return results.ToArray();
        }

        public static List<Task<JobResult>> ExecuteSSHJob(ProgramConfiguration config, ProgramConfiguration.Target target, IEnumerable<ProgramConfiguration.Target.Job> jobs)
        {
            List<Task<JobResult>> subResults = new List<Task<JobResult>>();

            foreach (var job in jobs)
            {
                subResults.Add(Task.Run(() => SSHReport(config, target, job)));
            }

            return subResults;
        }

        private static JobResult SSHReport(ProgramConfiguration config, ProgramConfiguration.Target target, ProgramConfiguration.Target.Job job)
        {
            var users = config.Keys
                   .Where(item => job.Keys.Contains(item.ID))
                   .Select(key => key.User);

            List<Row> rows = new List<Row>();
            Row headerRow = new Row(new Cell[]
                {
                    new Cell()
                    {
                        CellReference = "A1",
                        DataType = CellValues.String,
                        CellValue = new CellValue("Job Name")
                    },
                    new Cell()
                    {
                        CellReference = "B1",
                        DataType = CellValues.String,
                        CellValue = new CellValue("Return Value")
                    },
                    new Cell()
                    {
                        CellReference = "C1",
                        DataType = CellValues.String,
                        CellValue = new CellValue("Execution Result")
                    }
                });

            rows.Add(headerRow);

            int rowOffset = 2;

            foreach (string user in users)
            {
                try
                {
                    using (var client = new SshClient(
                    host: target.Host,
                    username: user,
                    port: (int)job.Port,

                    keyFiles: config.KeyBytes
                        .Where(item => job.Keys.Contains(item.Key))
                        .Select(key => new PrivateKeyFile(new MemoryStream(key.Value)))
                        .ToArray()
                        ))
                    {

                        client.Connect();

                        var commands = config.ScriptBytes
                            .Where(bytes => job.Scripts.Contains(bytes.Key))
                            .Select(pair => (Key: pair.Key, Value: client.CreateCommand(Encoding.UTF8.GetString(pair.Value)))).ToArray();

                        var commandOutput = commands.Select((item) => (Key: item.Key, Value: item.Value.Execute()));
                        var res = commands.Select((item) => new KeyValuePair<ushort, int>(item.Key, item.Value.ExitStatus));


                        for (int i = 0; i < job.Scripts.Length; i++)
                        {
                            Row resultRow = new Row(new Cell[]
                            {
                                new Cell()
                                {
                                    CellReference = $"A{rowOffset}",
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(config.Scripts.Single(item => item.ID ==  job.Scripts[i]).Name)
                                },
                                new Cell()
                                {
                                    CellReference = $"B{rowOffset}",
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(res.Single(item => item.Key == job.Scripts[i]).Value)
                                },
                                new Cell()
                                {
                                    CellReference = $"C{rowOffset}",
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(commandOutput.Single(item => item.Key == job.Scripts[i]).Value)
                                }
                            });
                            rows.Add(resultRow);
                            rowOffset++;
                        }

                        foreach (var item in commands) item.Value.Dispose();

                        break;
                    }
                }
                catch (Exception ex)
                {
                    Row resultRow = new Row(new Cell[]
                    {
                        new Cell()
                        {
                            CellReference = $"A{rowOffset}",
                            DataType = CellValues.String,
                            CellValue = new CellValue($"SSH: {user}@{target.Host}")
                        },
                        new Cell()
                        {
                            CellReference = $"B{rowOffset}",
                            DataType = CellValues.Number,
                            CellValue = new CellValue(1)
                        },
                        new Cell()
                        {
                            CellReference = $"C{rowOffset}",
                            DataType = CellValues.String,
                            CellValue = new CellValue($"{ex.GetType().FullName}: {ex.Message}")
                        }
                    });

                    rows.Add(resultRow);
                    rowOffset++;
                }

            }

            SheetData resultData = new SheetData(rows.ToArray());
            JobResult result = new JobResult() { Target = target.Host, JobName = job.Name, Worksheet = new Worksheet(resultData) };

            return result;
        }

        public static List<Task<JobResult>> ExecuteVCenterJob(ProgramConfiguration config, ProgramConfiguration.Target target, IEnumerable<ProgramConfiguration.Target.Job> jobs)
        {
            foreach (var job in jobs)
            {
                var keys =
                    from key in config.Keys
                    where job.Keys.Contains(key.ID)
                    select key.Value;

                foreach (var key in keys)
                {
                    try
                    {
                        var client = new VCenterClient(new Uri($"https://{target.Host}:{job.Port}/"), key);
                        client.CreateSession();
                        var vm_arr = client.ListVm();

                        break;
                    }
                    catch (Exception)
                    {
                       
                    }
                }
            }   
            throw new NotImplementedException();
        }
    }
}
