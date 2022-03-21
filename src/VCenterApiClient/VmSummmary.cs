using System;
using System.Collections.Generic;
using System.Text;

namespace VCenterApiClient
{
    public class VMSummmary
    {
        public int cpu_count { get; set; }
        public int memory_size_MiB { get; set; }
        public string name { get; set; }
        public string power_state { get; set; }
        public string vm { get; set; }
    }




}

