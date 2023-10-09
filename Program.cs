using System;
using System.IO.Ports;
using System.Text.Json;
using System.Threading;
using System.Data.Common;
using System.Web;
using System.Text;

class Program
{
    // Create the serial port with basic settings 
    static SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);

    static void Main(string[] args)
    {
        // Instatiate this 
        SerialPortProgram();
    }

    public static byte[] ConvertToByteArray(string str, Encoding encoding)
    {
        return encoding.GetBytes(str);
    }

    public static String ToBinary(Byte[] data)
    {
        return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
    }


    private static void SerialPortProgram()
    {
        Console.WriteLine("Incoming Data:");
        // Attach a method to be called when there
        // is data waiting in the port's buffer 
        port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        // Begin communications 
        port.Open();
        
        for(;;)
        {
            continue;
        }
    }

    private static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        string input = port.ReadExisting();

        //Credits to @.luxus for suggesting this
        byte[] hex_bytes = ConvertToByteArray(input, Encoding.Default);
        string binary = ToBinary(hex_bytes);
        string hex_string = "";

        for (int i = 0; i < hex_bytes.Length; i++)
        {
            hex_string += hex_bytes[i];
            hex_string += " ";

        }

        Console.WriteLine();

        switch (binary)
        {
            case ("00101000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00101001"):
            {
                Console.WriteLine("No Input");
                break;
            }
            default:
            {
                Console.WriteLine($"Input Detected: {binary}\n{hex_string}");
                break;
            }
        }
        
    }
}