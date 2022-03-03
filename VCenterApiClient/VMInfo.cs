using System;
using System.Collections.Generic;
using System.Text;

namespace VCenterApiClient
{
    public class VMInfo
    {
        public Boot boot { get; set; }
        public Boot_Devices[] boot_devices { get; set; }
        public Cdroms cdroms { get; set; }
        public Cpu cpu { get; set; }
        public Disks disks { get; set; }
        public Floppies floppies { get; set; }
        public string guest_OS { get; set; }
        public VmHardwareInfo hardware { get; set; }
        public Identity identity { get; set; }
        public bool instant_clone_frozen { get; set; }
        public Memory memory { get; set; }
        public string name { get; set; }
        public Nics nics { get; set; }
        public Nvme_Adapters nvme_adapters { get; set; }
        public Parallel_Ports parallel_ports { get; set; }
        public string power_state { get; set; }
        public Sata_Adapters sata_adapters { get; set; }
        public Scsi_Adapters scsi_adapters { get; set; }
        public Serial_Ports serial_ports { get; set; }

        public class Boot
        {
            public int delay { get; set; }
            public bool efi_legacy_boot { get; set; }
            public bool enter_setup_mode { get; set; }
            public string network_protocol { get; set; }
            public bool retry { get; set; }
            public int retry_delay { get; set; }
            public string type { get; set; }
        }

        public class Cdroms
        {
            public Key key { get; set; }
            public class Key
            {
                public bool allow_guest_control { get; set; }
                public Backing backing { get; set; }
                public Ide ide { get; set; }
                public string label { get; set; }
                public Sata sata { get; set; }
                public bool start_connected { get; set; }
                public string state { get; set; }
                public string type { get; set; }
                public class Backing
                {
                    public bool auto_detect { get; set; }
                    public string device_access_type { get; set; }
                    public string host_device { get; set; }
                    public string iso_file { get; set; }
                    public string type { get; set; }
                }

                public class Ide
                {
                    public bool master { get; set; }
                    public bool primary { get; set; }
                }

                public class Sata
                {
                    public int bus { get; set; }
                    public int unit { get; set; }
                }
            }
        }

        public class Cpu
        {
            public int cores_per_socket { get; set; }
            public int count { get; set; }
            public bool hot_add_enabled { get; set; }
            public bool hot_remove_enabled { get; set; }
        }

        public class Disks
        {
            public Key key { get; set; }
            public class Key
            {
                public Backing backing { get; set; }
                public int capacity { get; set; }
                public Ide ide { get; set; }
                public string label { get; set; }
                public Nvme nvme { get; set; }
                public Sata sata { get; set; }
                public Scsi scsi { get; set; }
                public string type { get; set; }
                public class Backing
                {
                    public string type { get; set; }
                    public string vmdk_file { get; set; }
                }
                public class Ide
                {
                    public bool master { get; set; }
                    public bool primary { get; set; }
                }

                public class Nvme
                {
                    public int bus { get; set; }
                    public int unit { get; set; }
                }

                public class Sata
                {
                    public int bus { get; set; }
                    public int unit { get; set; }
                }

                public class Scsi
                {
                    public int bus { get; set; }
                    public int unit { get; set; }
                }
            }
        }

        public class Floppies
        {
            public Key key { get; set; }
            public class Key
            {
                public bool allow_guest_control { get; set; }
                public Backing backing { get; set; }
                public string label { get; set; }
                public bool start_connected { get; set; }
                public string state { get; set; }

                public class Backing
                {
                    public bool auto_detect { get; set; }
                    public string host_device { get; set; }
                    public string image_file { get; set; }
                    public string type { get; set; }
                }
            }
        }

        public class VmHardwareInfo
        {
            public Upgrade_Error upgrade_error { get; set; }
            public string upgrade_policy { get; set; }
            public string upgrade_status { get; set; }
            public string upgrade_version { get; set; }
            public string version { get; set; }
            public class Upgrade_Error
            {
            }
        }

        public class Identity
        {
            public string bios_uuid { get; set; }
            public string instance_uuid { get; set; }
            public string name { get; set; }
        }

        public class Memory
        {
            public bool hot_add_enabled { get; set; }
            public int hot_add_increment_size_MiB { get; set; }
            public int hot_add_limit_MiB { get; set; }
            public int size_MiB { get; set; }
        }

        public class Nics
        {
            public Key key { get; set; }
            public class Key
            {
                public bool allow_guest_control { get; set; }
                public Backing backing { get; set; }
                public string label { get; set; }
                public string mac_address { get; set; }
                public string mac_type { get; set; }
                public int pci_slot_number { get; set; }
                public bool start_connected { get; set; }
                public string state { get; set; }
                public string type { get; set; }
                public bool upt_compatibility_enabled { get; set; }
                public bool wake_on_lan_enabled { get; set; }

                public class Backing
                {
                    public int connection_cookie { get; set; }
                    public string distributed_port { get; set; }
                    public string distributed_switch_uuid { get; set; }
                    public string host_device { get; set; }
                    public string network { get; set; }
                    public string network_name { get; set; }
                    public string opaque_network_id { get; set; }
                    public string opaque_network_type { get; set; }
                    public string type { get; set; }
                }
            }

        }
        public class Nvme_Adapters
        {
            public Key key { get; set; }

            public class Key
            {
                public int bus { get; set; }
                public string label { get; set; }
                public int pci_slot_number { get; set; }
            }
        }

        public class Parallel_Ports
        {
            public Key key { get; set; }
            public class Key
            {
                public bool allow_guest_control { get; set; }
                public Backing backing { get; set; }
                public string label { get; set; }
                public bool start_connected { get; set; }
                public string state { get; set; }
                public class Backing
                {
                    public bool auto_detect { get; set; }
                    public string file { get; set; }
                    public string host_device { get; set; }
                    public string type { get; set; }
                }
            }

        }

        public class Sata_Adapters
        {
            public Key key { get; set; }
            public class Key
            {
                public int bus { get; set; }
                public string label { get; set; }
                public int pci_slot_number { get; set; }
                public string type { get; set; }
            }
        }

        public class Scsi_Adapters
        {
            public Key key { get; set; }
            public class Key
            {
                public string label { get; set; }
                public int pci_slot_number { get; set; }
                public Scsi scsi { get; set; }
                public string sharing { get; set; }
                public string type { get; set; }
                public class Scsi
                {
                    public int bus { get; set; }
                    public int unit { get; set; }
                }
            }
        }
        public class Serial_Ports
        {
            public Key key { get; set; }
            public class Key
            {
                public bool allow_guest_control { get; set; }
                public Backing backing { get; set; }
                public string label { get; set; }
                public bool start_connected { get; set; }
                public string state { get; set; }
                public bool yield_on_poll { get; set; }
                public class Backing
                {
                    public bool auto_detect { get; set; }
                    public string file { get; set; }
                    public string host_device { get; set; }
                    public string network_location { get; set; }
                    public bool no_rx_loss { get; set; }
                    public string pipe { get; set; }
                    public string proxy { get; set; }
                    public string type { get; set; }
                }
            }
        }

        public class Boot_Devices
        {
            public string[] disks { get; set; }
            public string nic { get; set; }
            public string type { get; set; }
        }
    }
}
