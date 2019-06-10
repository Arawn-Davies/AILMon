using System;
using System.Data.SqlTypes;
using Apollo_IL;

namespace AILMon
{
    class Program
    {
        private static bool vmode = false;

        public static byte[] HelloWorldHex =
        {
            0x41, 0xf5, 0x02, 0x00, 0x00, 0x00, 0x41, 0xfd, 0xfa, 0x00, 0x00, 0x00,
            0xfa, 0x45, 0xfa, 0x00, 0x00, 0x00, 0xfa, 0x6e, 0xfb, 0x00, 0x00, 0x00,
            0xfa, 0x74, 0xfc, 0x00, 0x00, 0x00, 0xfa, 0x65, 0xfd, 0x00, 0x00, 0x00,
            0xfa, 0x72, 0xfe, 0x00, 0x00, 0x00, 0xfa, 0xff, 0x00, 0x00, 0x00, 0xfa,
            0x59, 0x00, 0x01, 0x00, 0x00, 0xfa, 0x6f, 0x01, 0x01, 0x00, 0x00, 0xfa,
            0x75, 0x02, 0x01, 0x00, 0x00, 0xfa, 0x72, 0x03, 0x01, 0x00, 0x00, 0xfa,
            0x04, 0x01, 0x00, 0x00, 0xfa, 0x4e, 0x05, 0x01, 0x00, 0x00, 0xfa, 0x61,
            0x06, 0x01, 0x00, 0x00, 0xfa, 0x6d, 0x07, 0x01, 0x00, 0x00, 0xfa, 0x65,
            0x08, 0x01, 0x00, 0x00, 0xfa, 0x3a, 0x01, 0x00, 0x00, 0xfa, 0x01, 0x00,
            0x00, 0x41, 0xf7, 0x11, 0x00, 0x00, 0x00, 0xeb, 0x01, 0x00, 0x00, 0x00,
            0x00, 0x41, 0xf5, 0x04, 0x00, 0x00, 0x00, 0x41, 0xfd, 0xf4, 0x01, 0x00,
            0x00, 0xeb, 0x01, 0x00, 0x00, 0x00, 0x00, 0xfa, 0x48, 0xed, 0x01, 0x00,
            0x00, 0xfa, 0x65, 0xee, 0x01, 0x00, 0x00, 0xfa, 0x6c, 0xef, 0x01, 0x00,
            0x00, 0xfa, 0x6c, 0xf0, 0x01, 0x00, 0x00, 0xfa, 0x6f, 0xf1, 0x01, 0x00,
            0x00, 0xfa, 0x2c, 0xf2, 0x01, 0x00, 0x00, 0xfa, 0xf3, 0x01, 0x00, 0x00,
            0x41, 0xfd, 0xed, 0x01, 0x00, 0x00, 0x44, 0xf7, 0x07, 0x00, 0x00, 0x00,
            0x41, 0xf5, 0x02, 0x00, 0x00, 0x00, 0xeb, 0x01, 0x00, 0x00, 0x00, 0x00,
            0x41, 0xf5, 0x01, 0x00, 0x00, 0x00, 0x41, 0xf6, 0x00, 0x00, 0x00, 0xeb,
            0x01, 0x00, 0x00, 0x00, 0x00, 0xd0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public static byte[] HelloWorld =
        {
            065, 245, 002, 000, 000, 000, 065, 253, 250, 000, 000, 000,
            250, 069, 250, 000, 000, 000, 250, 110, 251, 000, 000, 000,
            250, 116, 252, 000, 000, 000, 250, 101, 253, 000, 000, 000,
            250, 114, 254, 000, 000, 000, 250, 255, 000, 000, 000, 250,
            089, 000, 001, 000, 000, 250, 111, 001, 001, 000, 000, 250,
            117, 002, 001, 000, 000, 250, 114, 003, 001, 000, 000, 250,
            004, 001, 000, 000, 250, 078, 005, 001, 000, 000, 250, 097,
            006, 001, 000, 000, 250, 109, 007, 001, 000, 000, 250, 101,
            008, 001, 000, 000, 250, 058, 001, 000, 000, 250, 001, 000,
            000, 065, 247, 017, 000, 000, 000, 235, 001, 000, 000, 000,
            000, 065, 245, 004, 000, 000, 000, 065, 253, 244, 001, 000,
            000, 235, 001, 000, 000, 000, 000, 250, 072, 237, 001, 000,
            000, 250, 101, 238, 001, 000, 000, 250, 108, 239, 001, 000,
            000, 250, 108, 240, 001, 000, 000, 250, 111, 241, 001, 000,
            000, 250, 044, 242, 001, 000, 000, 250, 243, 001, 000, 000,
            065, 253, 237, 001, 000, 000, 068, 247, 007, 000, 000, 000,
            065, 245, 002, 000, 000, 000, 235, 001, 000, 000, 000, 000,
            065, 245, 001, 000, 000, 000, 065, 246, 000, 000, 000, 235,
            001, 000, 000, 000, 000, 208, 000, 000, 000, 000, 000, 000
        };

        private static VM virtualMachine;
        static void Main(string[] args)
        {
            Console.SetWindowSize((80), (25));
            Console.SetBufferSize((80), 25);
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

            if (cmd.StartsWith("d"))
            {
                if (args.Length == 1)
                {
                    int val = Globals.DebugMode ? 1 : 0;
                    Console.WriteLine(val);
                }
                else if (args.Length == 2)
                {
                    switch (args[1])
                    {
                        case "1":
                        case "true":
                            Globals.DebugMode = true;
                            break;
                        case "0":
                        case "false":
                            Globals.DebugMode = false;
                            break;
                        default:
                            int val = Globals.DebugMode ? 1 : 0;
                            Console.WriteLine(val);
                            break;
                    }
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

                    if (end <= virtualMachine.ram.memory.Length)
                    {
                        for (int i = addr; i < (end + 1); i++)
                        {
                            virtualMachine.ram.memory[i] = (byte) val;
                        }
                    }
                }
            }
            else if (cmd.StartsWith("h"))
            {
                if (args.Length == 1)
                {
                    int val = vmode ? 1 : 0;
                    Console.WriteLine(val);
                }
                else if (args.Length == 2)
                {
                    switch (args[1])
                    {
                        case "1":
                        case "true":
                            vmode = true;
                            break;
                        case "0":
                        case "false":
                            vmode = false;
                            break;
                        default:
                            int val = vmode ? 1 : 0;
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
            else if (cmd.StartsWith("m"))
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
                        if (i % 10 == 0)
                        {
                            Console.Write("\n");
                            if (vmode == true)
                            {

                                Console.Write(i + "\t= " + (int)virtualMachine.ram.memory[i] + ", ");
                            }
                            else
                            {
                                Console.Write(i.ToString("X4") + "\t= " + virtualMachine.ram.memory[i].ToString("X4") + ", ");
                            }
                        }
                        else
                        {
                            if (vmode == true)
                            {
                                Console.Write(i + ", ");
                            }
                            else
                            {
                                Console.Write(i.ToString("X4") + ", ");
                            }
                            
                        }
                    }
                    Console.WriteLine("\n");
                }
                if (args.Length == 2)
                {
                    ushort addr = HexToInt(args[1]);
                    
                    if (vmode == true)
                    {
                        Console.WriteLine(addr + " = " + (int) virtualMachine.ram.memory[addr]);
                    }
                    else
                    {
                        Console.WriteLine(addr.ToString("X4") + " = " + virtualMachine.ram.memory[addr].ToString("X4"));
                    }
                }
                else if (args.Length == 3)
                {
                    ushort addr = HexToInt(args[1]);
                    ushort end = HexToInt(args[2]);
                    byte[] arr = new byte[(end - addr) + 1];
                    Array.Copy(virtualMachine.ram.memory, arr, (end - addr) + 1);

                    //for (int b = 0; b < arr.Length; b++)
                    //{
                    //    Console.WriteLine("0x" + (addr + b) + " = " + "0x" + virtualMachine.ram.memory[addr + b].ToString("X4"));
                    //}

                    
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i % 10 == 0)
                        {
                            Console.Write("\n");
                            if (vmode == true)
                            {
                                
                                Console.Write(i + "\t= " + (int) virtualMachine.ram.memory[i] + ", ");
                            }
                            else
                            {
                                Console.Write(i.ToString("X4") + "\t= " + virtualMachine.ram.memory[i].ToString("X4") + ", ");
                            }
                        }
                        else
                        {
                            if (vmode == true)
                            {
                                Console.Write(i + ", ");
                            }
                            else
                            {
                                Console.Write(i.ToString("X4") + ", ");
                            }
                        }
                    }
                    Console.WriteLine("\n");
                }
            }
            else if (cmd.StartsWith("?"))
            {
                string h = "// D - Debug\t- turn debugging on/off - returns 0 if off, 1 if on\n//" +
"\n// E - Execute\t- begin execution of the virtual machine," +
"\n//           \t  optionally at the specified memory address\n//" +
"\n// F - Fill\t- fill the range of the specified memory addresses" +
"\n//           \t  with the specified value\n//" +
"\n// H - Hex\t- switches view mode to represent memory addresses" +
"\n//           \t  as hexadecimal or decimal\n//" +
"\n// M - Modify\t- modify the value of a specific memory address\n//" +
"\n// V - View\t- view the value of a specific memory address\n//";

                Console.WriteLine(h);
            }

            else if (cmd == "")
            {

            }
            else
            {
                Console.WriteLine("?");
            }
        }


        // D - view Debug bit - turn debugging on/off - returns 0 if off, 1 if on
        // E - Execute - begin execution of the virtual machine,
        //               optionally at the specified memory address
        // F - Fill - fill the range of the specified memory addresses with the specified value
        // H - Hex - switches view mode to represent memory addresses as hexadecimal
        // M - Modify - modify the value of a specific memory address
        // V - View - view the value of a specific memory address

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
