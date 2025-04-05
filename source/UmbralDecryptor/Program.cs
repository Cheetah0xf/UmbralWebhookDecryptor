using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;


namespace UmbralDecryptor
{
    internal class Program
    {
        static string input;
        static bool isRunning = true;
        static void Main(string[] args)
        {

            Console.Title = Encoding.UTF8.GetString(Convert.FromBase64String("VW1icmFsIFN0ZWFsZXIgV2ViaG9vayBEZWNyeXB0b3JAQ2hlZXRhaDB4Zg=="));


            Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\t ██████╗██╗  ██╗ █████╗ ██████╗ ");
                    Console.WriteLine("\t██╔════╝██║  ██║██╔══██╗██╔══██╗");
                    Console.WriteLine("\t██║     ███████║███████║██║  ██║");
                    Console.WriteLine("\t██║     ██╔══██║██╔══██║██║  ██║");
                    Console.WriteLine("\t╚██████╗██║  ██║██║  ██║██████╔╝");
                    Console.WriteLine("\t ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ");
            Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String("VW1icmFsIFN0ZWFsZXIgV2ViaG9vayBEZWNyeXB0b3JAQ2hlZXRhaDB4Zg==")) +
                $" >> {Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9kaXNjb3JkLmdnL0VaWnJUdk1uNVg="))}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (args.Length != 0)
                input = args[0].Replace("\"", string.Empty);

            while (!File.Exists(input))
            {
                Console.WriteLine("\nEnter valid file path: ");
                input = Console.ReadLine().Replace("\"", string.Empty);
            }

            ModuleDefMD module = ModuleDefMD.Load(input);
            Assembly runtimeAssembly = Assembly.LoadFile(input);

            foreach (var type in module.GetTypes())
            {
                foreach (var method in type.Methods)
                {
                    if (!method.HasBody || !method.Body.HasInstructions)
                        continue;
                    var instructions = method.Body.Instructions;
                    for (int i = 0; i < instructions.Count; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Ldstr &&
                            instructions[i + 1].IsStloc() &&
                            instructions[i + 2].OpCode == OpCodes.Ldstr &&
                            instructions[i + 3].IsStloc() &&
                            instructions[i + 4].OpCode == OpCodes.Ldstr &&
                            instructions[i + 5].OpCode == OpCodes.Ldstr &&
                            instructions[i + 6].IsStloc() &&
                            instructions[i + 7].OpCode == OpCodes.Ldstr &&
                            instructions[i + 8].IsStloc()
                            
                            
                            )
                        {
                           
                                
                                byte[] text = Convert.FromBase64String(instructions[i].Operand.ToString());
                                byte[] text2 = Convert.FromBase64String(instructions[i + 2].Operand.ToString());
                                byte[] text3 = Convert.FromBase64String(instructions[i+4].Operand.ToString());

                                string Webhook = smethod_0(text3,text,text2);
                                Console.WriteLine("\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Found Webhook: {Webhook}");
                                Console.ResetColor();
                            
                            }
                        }
                }
            }
            Console.ReadKey();
        }
        private static string smethod_0(byte[] encryptedData, byte[] key, byte[] iv)
        {
            byte[] array = encryptedData.Take(encryptedData.Length - 16).ToArray<byte>();
            byte[] array2 = encryptedData.Skip(encryptedData.Length - 16).ToArray<byte>();
            Class33 class33 = new Class33();
            byte[] array3 = class33.method_0(key, iv, null, array, array2);
            return Encoding.UTF8.GetString(array3);
        }
   

    }
}
