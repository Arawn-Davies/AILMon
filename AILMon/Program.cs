using System;
using Apollo_IL;

namespace AILMon
{
    class Program
    {
        public static byte[] HelloWorld =
        {
            65, 245, 2, 0, 0, 0,
            65, 253, 250, 0, 0, 0,
            250, 69, 250, 0, 0, 0,
            250, 110, 251, 0, 0, 0,
            250, 116, 252, 0, 0, 0,
            250, 101, 253, 0, 0, 0,
            250, 114, 254, 0, 0, 0,
            250, 32, 255, 0, 0, 0,
            250, 89, 0, 1, 0, 0,
            250, 111, 1, 1, 0, 0,
            250, 117, 2, 1, 0, 0,
            250, 114, 3, 1, 0, 0,
            250, 32, 4, 1, 0, 0,
            250, 78, 5, 1, 0, 0,
            250, 97, 6, 1, 0, 0,
            250, 109, 7, 1, 0, 0,
            250, 101, 8, 1, 0, 0,
            250, 58, 9, 1, 0, 0,
            250, 32, 10, 1, 0, 0,
            65, 247, 17, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            65, 245, 4, 0, 0, 0,
            65, 253, 244, 1, 0, 0,
            235, 1, 0, 0, 0, 0,
            250, 72, 237, 1, 0, 0,
            250, 101, 238, 1, 0, 0,
            250, 108, 239, 1, 0, 0,
            250, 108, 240, 1, 0, 0,
            250, 111, 241, 1, 0, 0,
            250, 44, 242, 1, 0, 0,
            250, 32, 243, 1, 0, 0,
            65, 253, 237, 1, 0, 0,
            68, 247, 7, 0, 0, 0,
            65, 245, 2, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            65, 245, 1, 0, 0, 0,
            65, 246, 10, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            208, 0, 0, 0, 0, 0,
        };

        private static VM virtualMachine;
        static void Main(string[] args)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            
            // Set the monitor to use current console
            Globals.console = new IOimpl();
            // Clear the application ROM, set to 65535 bytes
            byte[] rom = new byte[65535];
            // Make sure the ROM array is empty
            Array.Fill(rom, (byte)0);



            //Create a virtual machine, with the specified empty ROM, with an equal amount of RAM
            virtualMachine = new VM(HelloWorld, HelloWorld.Length + 1024);

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
                if (args.Length == 1)
                {
                    for (int i = 0; i < virtualMachine.ram.memory.Length; i++)
                    {
                        if (((byte) virtualMachine.ram.memory[i] / 10) == 0)
                        Console.WriteLine("");
                    }
                }
                if (args.Length == 2)
                {
                    ushort addr = HexToInt(args[1]);
                    Console.WriteLine("0x" + addr + " = " + "0x" + virtualMachine.ram.memory[addr].ToString("X4"));
                }
                else if (args.Length == 3)
                {
                    ushort addr = HexToInt(args[1]);
                    ushort end = HexToInt(args[2]);
                    byte[] arr = new byte[(end - addr) + 1];
                    Array.Copy(virtualMachine.ram.memory, arr, (end - addr) + 1);

                    for (int i = 0; i < arr.Length; i++)
                    {
                        Console.WriteLine("0x" + (addr + i) + " = " + "0x" + virtualMachine.ram.memory[addr + i].ToString("X4"));
                    }
                }
            }
            else if (cmd.StartsWith("f"))
            {
                if (args.Length == 4)
                {
                    // Beginning address
                    ushort addr = HexToInt(args[1]);
                    // Ending address
                    ushort end = HexToInt(args[2]);
                    // Value to replace as
                    ushort val = HexToInt(args[3]);

                    byte[] arr = new byte[(end - addr) + 1];

                    for (int i = addr; i < (end + 1); i++)
                    {
                        virtualMachine.ram.memory[i] = (byte) val;
                    }

                    /*
                    Array.Copy(virtualMachine.ram.memory, arr, (end - addr) + 1);
                    foreach (byte b in arr)
                    {
                        arr[b] = (byte) val;
                    }



                    Array.Copy(arr, 0, virtualMachine.ram.memory, addr, arr.Length);
                    */
                }
            }
            else if (cmd.StartsWith("e"))
            {
                if (args.Length == 1)
                {
                    virtualMachine.Execute();
                }
                else if (args.Length == 2)
                {
                    ushort addr = HexToInt(args[1]);
                    virtualMachine.ExecuteAtAddress((byte) addr);
                }
            }
            else if (cmd == "")
            {

            }
            else
            {
                Console.WriteLine("?");
            }
        }

        // M - modify
        // F - fill
        // V - view
        // E - execute

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
