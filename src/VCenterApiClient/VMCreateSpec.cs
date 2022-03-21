namespace VCenterApiClient
{
    public class VMCreateSpec
    {
        public Boot boot { get; set; }
        public Boot_Devices[] boot_devices { get; set; }
        public Cdrom[] cdroms { get; set; }
        public Cpu cpu { get; set; }
        public Disk[] disks { get; set; }
        public Floppy[] floppies { get; set; }
        public string guest_OS { get; set; }
        public string hardware_version { get; set; }
        public Memory memory { get; set; }
        public string name { get; set; }
        public Nic[] nics { get; set; }
        public Nvme_Adapters[] nvme_adapters { get; set; }
        public Parallel_Ports[] parallel_ports { get; set; }
        public Placement placement { get; set; }
        public Sata_Adapters[] sata_adapters { get; set; }
        public Scsi_Adapters[] scsi_adapters { get; set; }
        public Serial_Ports[] serial_ports { get; set; }
        public Storage_Policy storage_policy { get; set; }

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

        public class Cpu
        {
            public int cores_per_socket { get; set; }
            public int count { get; set; }
            public bool hot_add_enabled { get; set; }
            public bool hot_remove_enabled { get; set; }
        }

        public class Memory
        {
            public bool hot_add_enabled { get; set; }
            public int size_MiB { get; set; }
        }

        public class Placement
        {
            public string cluster { get; set; }
            public string datastore { get; set; }
            public string folder { get; set; }
            public string host { get; set; }
            public string resource_pool { get; set; }
        }

        public class Storage_Policy
        {
            public string policy { get; set; }
        }

        public class Boot_Devices
        {
            public string type { get; set; }
        }

        public class Cdrom
        {
            public bool allow_guest_control { get; set; }
            public Backing backing { get; set; }
            public Ide ide { get; set; }
            public Sata sata { get; set; }
            public bool start_connected { get; set; }
            public string type { get; set; }

            public class Backing
            {
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

        public class Disk
        {
            public Backing backing { get; set; }
            public Ide ide { get; set; }
            public New_Vmdk new_vmdk { get; set; }
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

            public class New_Vmdk
            {
                public int capacity { get; set; }
                public string name { get; set; }
                public Storage_Policy storage_policy { get; set; }

                public class Storage_Policy
                {
                    public string policy { get; set; }
                }
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

        public class Floppy
        {
            public bool allow_guest_control { get; set; }
            public Backing backing { get; set; }
            public bool start_connected { get; set; }

            public class Backing
            {
                public string host_device { get; set; }
                public string image_file { get; set; }
                public string type { get; set; }
            }
        }

        public class Nic
        {
            public bool allow_guest_control { get; set; }
            public Backing backing { get; set; }
            public string mac_address { get; set; }
            public string mac_type { get; set; }
            public int pci_slot_number { get; set; }
            public bool start_connected { get; set; }
            public string type { get; set; }
            public bool upt_compatibility_enabled { get; set; }
            public bool wake_on_lan_enabled { get; set; }

            public class Backing
            {
                public string distributed_port { get; set; }
                public string network { get; set; }
                public string type { get; set; }
            }
        }

        public class Nvme_Adapters
        {
            public int bus { get; set; }
            public int pci_slot_number { get; set; }
        }

        public class Parallel_Ports
        {
            public bool allow_guest_control { get; set; }
            public Backing backing { get; set; }
            public bool start_connected { get; set; }

            public class Backing
            {
                public string file { get; set; }
                public string host_device { get; set; }
                public string type { get; set; }
            }
        }

        public class Sata_Adapters
        {
            public int bus { get; set; }
            public int pci_slot_number { get; set; }
            public string type { get; set; }
        }

        public class Scsi_Adapters
        {
            public int bus { get; set; }
            public int pci_slot_number { get; set; }
            public string sharing { get; set; }
            public string type { get; set; }
        }

        public class Serial_Ports
        {
            public bool allow_guest_control { get; set; }
            public Backing backing { get; set; }
            public bool start_connected { get; set; }
            public bool yield_on_poll { get; set; }
            public class Backing
            {
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
}

