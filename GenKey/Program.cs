using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Management;

namespace GenKey
{
    class Program
    {
        static void Main(string[] args)
        {
            string key64 = RNGCrypto_MachineKey.getRandomKey(64);
            Debug.WriteLine("KEY64: |"+key64);
            string key32 = RNGCrypto_MachineKey.getRandomKey(32);
            Debug.WriteLine("KEY32: |" + key32);
            string cpuInfo = String.Empty;
            
        }
    }
}
