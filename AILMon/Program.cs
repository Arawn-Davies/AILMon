using System;
using Apollo_IL;

namespace AILMon
{
    class Program
    {
        private static VM virtualMachine;
        static void Main(string[] args)
        {
            // Set the monitor to use current console
            Globals.console = new IOimpl();
            // Clear the application ROM, set to 65535 bytes
            byte[] rom = new byte[65535];
            // Make sure the ROM array is empty
            Array.Fill(rom, (byte)0);

            //Create a virtual machine, with the specified empty ROM, with an equal amount of RAM
            virtualMachine = new VM(rom, rom.Length + 1024);

            while (true)
            {
                Console.Write("#");
                string op = Console.ReadLine();
                Monitor(op);

            }
        }

        public static void Monitor(string op)
        {
            string cmd = op.ToLower();
            string[] args = cmd.Split(' ');
            if (cmd.StartsWith("m"))
            {
                if (args.Length == 2)
                {
                    ushort addr = HexToInt(args[1]);
                    virtualMachine.ram.memory[addr] = 0;
                }
                else if (args.Length == 3)
                {
                    ushort addr = HexToInt(args[1]);
                    ushort val = HexToInt(args[2]);
                    virtualMachine.ram.memory[addr] = (byte)val;
                }
            }
            else if (cmd.StartsWith("v"))
            {
                if (args.Length == 2)
                {
                    ushort addr = HexToInt(args[1]);
                    Console.WriteLine("0x" + virtualMachine.ram.memory[addr].ToString("X4"));
                }
                else if (args.Length == 3)
                {
                    ushort addr = HexToInt(args[1]);
                    ushort end = HexToInt(args[2]);
                    byte[] arr = new byte[(end - addr) + 1];
                    Array.Copy(virtualMachine.ram.memory, arr, (end - addr) + 1);

                    for (int i = 0; i < arr.Length; i++)
                    {
                        Console.WriteLine("0x" + virtualMachine.ram.memory[addr + i].ToString("X4"));
                    }
                }
            }
            else if (cmd.StartsWith("f"))
            {
                if (args.Length == 3)
                {

                }
            }
        }

        // M - modify
        // F - fill
        // V - view
        // 
        private static ushort HexToInt(string hex)
        {
            int v = 0;
            int digit = 0;
            int pwr = 0;
            int answer = 0;

            for (int i = hex.Length - 1; i > -1; i--)
            {
                char c = hex[i];

                switch (c)
                {
                    case '0': v = 0; break;
                    case '1': v = 1; break;
                    case '2': v = 2; break;
                    case '3': v = 3; break;
                    case '4': v = 4; break;
                    case '5': v = 5; break;
                    case '6': v = 6; break;
                    case '7': v = 7; break;
                    case '8': v = 8; break;
                    case '9': v = 9; break;
                    case 'A': v = 10; break;
                    case 'B': v = 11; break;
                    case 'C': v = 12; break;
                    case 'D': v = 13; break;
                    case 'E': v = 14; break;
                    case 'F': v = 15; break;
                }

                pwr = 1;

                for (int p = 0; p < digit; p++)
                    pwr = pwr * 16;

                answer = answer + (v * pwr);
                digit++;

            }
            return (ushort)answer;
        }

    }
}
