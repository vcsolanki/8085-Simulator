using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace _8085_Simulator
{
    public partial class main : Form
    {

        //Custom class

        public class Register
        {
            public bool[] data = new bool[8];
            public int overflowed = 0;
            public Register()
            {
                Clear();
            }
            public string GetBinaryString(bool not = false)
            {
                string data_string = "";
                for (int i = 0; i < 8; i++)
                {
                    if (not)
                    {
                        if (data[i] == false)
                            data_string += '1';
                        else
                            data_string += '0';
                    }
                    else
                    {
                        if (data[i] == true)
                            data_string += '1';
                        else
                            data_string += '0';
                    }
                }
                return data_string;
            }
            public int GetInt()
            {
                int data = Convert.ToInt32(GetBinaryString(), 2);
                return data;
            }
            public int GetNInt()
            {
                int data = Convert.ToInt32(GetBinaryString(true), 2);
                return data;
            }
            public int Get2SInt()
            {
                int data = Convert.ToInt32(GetBinaryString(true), 2);
                data += 1;
                if (data > 255)
                    overflowed = 1;
                else
                    overflowed = 0;
                return data;
            }
            public string GetHex()
            {
                string hex = Convert.ToString(GetInt(), 16).ToUpper().PadLeft(2, '0');
                return hex;
            }
            public void SetData(int data)
            {
                Clear();
                if (data > 255)
                {
                    data -= 256;
                    overflowed = 1;
                }
                else if (data < 0)
                {
                    data += 256;
                }
                else
                    overflowed = 0;
                int i = 7;
                foreach (char c in Convert.ToString(data, 2).Reverse<char>())
                {
                    if (c == '1')
                        this.data[i] = true;
                    else
                        this.data[i] = false;
                    i--;
                }
            }
            public void SetData(string hex)
            {
                if (Convert.ToInt32(hex, 16) < 256)
                {
                    int data = Convert.ToInt32(hex, 16);
                    SetData(data);
                }
            }
            public void Clear()
            {
                for (int i = 0; i < 8; i++)
                    data[i] = false;
            }
        };
        public class Flags
        {
            public bool sign = new bool();
            public bool zero = new bool();
            public bool auxiliary = new bool();
            public bool parity = new bool();
            public bool carry = new bool();
            public bool[] address;
            public Flags()
            {
                address = new bool[8];
            }
            public int GetInt()
            {
                int data = Convert.ToInt32(GetBinaryString(), 2);
                return data;
            }
            public string GetBinaryString()
            {
                string data_string = "";
                address[7] = carry;
                address[5] = parity;
                address[3] = auxiliary;
                address[1] = zero;
                address[0] = sign;
                for (int i = 0; i < 8; i++)
                {
                    if (address[i] == true)
                        data_string += '1';
                    else
                        data_string += '0';
                }
                return data_string;
            }
            public string GetHex()
            {
                string hex = Convert.ToString(GetInt(), 16).ToUpper();
                return hex;
            }
            public void SetData(int data)
            {
                Clear();
                if (data > 255)
                    data -= 256;
                else if (data < 0)
                    data += 256;
                int i = 7;
                foreach (char c in Convert.ToString(data, 2).Reverse<char>())
                {
                    if (c == '1')
                        address[i] = true;
                    else
                        address[i] = false;
                    i--;
                }
                carry = address[7];
                parity = address[5];
                auxiliary = address[3];
                zero = address[1];
                sign = address[0];
            }
            public void CheckParity(string binary)
            {
                int eo = 0;
                foreach (char c in binary)
                {
                    if (c == '1')
                    {
                        eo++;
                    }
                }
                if ((eo % 2) == 0)
                    parity = true;
                else
                    parity = false;
            }
            public void CheckAuxiliary(string f1, string f2)
            {
                int a = Convert.ToInt32(f1.Substring(4), 2);
                int b = Convert.ToInt32(f2.Substring(4), 2);
                if ((a + b) >= 16)
                    auxiliary = true;
                else
                    auxiliary = false;
            }
            public void CheckSign(string binary)
            {
                if (binary[0] == '1')
                    sign = true;
                if (binary[0] == '0')
                    sign = false;
            }
            public void Update(Register temp)
            {
                sign = temp.data[0];
                if (temp.GetInt() == 0)
                    zero = true;
                else
                    zero = false;
                CheckParity(temp.GetBinaryString());
            }
            public void Clear()
            {
                for (int i = 0; i < 8; i++)
                    address[i] = false;
                sign = zero = auxiliary = parity = carry = false;
            }
        };
        public class ProgramCounter
        {
            public int counter;
            public ProgramCounter()
            {
                counter = 0;
            }
            public void Increment()
            {
                counter++;
            }
            public void IncrementBy(int by)
            {
                counter += by;
            }
            public void Decrement()
            {
                counter--;
            }
            public void DecrementBy(int by)
            {
                counter -= by;
            }
            public string GetAddress()
            {
                string hexadr = counter.ToString("X");
                return hexadr;
            }
            public void SetAddress(string hex)
            {
                counter = Convert.ToInt32(hex, 16);
            }
            public void Clear()
            {
                counter = 0;
            }
        };
        public class LabelAddress
        {
            public string name = "";
            public int address = 0;
            public LabelAddress(string name, int address)
            {
                this.name = name;
                this.address = address;
            }
        };

        //Variables

        readonly Register a = new Register();
        readonly Register b = new Register();
        readonly Register c = new Register();
        readonly Register d = new Register();
        readonly Register e = new Register();
        readonly Register h = new Register();
        readonly Register l = new Register();

        readonly Flags f = new Flags();

        private ProgramCounter pc;

        private string psw;
        private string m;

        private bool stop_run = true;

        private List<ushort> memory;
        private List<ushort> port;
        private List<ushort> stack;

        private int sp = 65535;
        private int selected_index = 0;

        private string orignal_code;
        private string formatted_code;

        private List<string> errors;
        private List<LabelAddress> labels;

        private ListViewItem[] memory_items;
        private ListViewItem[] stack_items;
        private ListViewItem[] port_items;
        private int m_first;
        private int s_first;
        private int p_first;

        private ListViewItem[] m_items;

        //File management avriables

        private bool isFileSaved = false;
        private string filePath = "";

        // main

        public main()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        //General functions

        private void Update_variables()
        {
            areg.Text = a.GetHex().PadLeft(2, '0');
            breg.Text = b.GetHex().PadLeft(2, '0');
            creg.Text = c.GetHex().PadLeft(2, '0');
            dreg.Text = d.GetHex().PadLeft(2, '0');
            ereg.Text = e.GetHex().PadLeft(2, '0');
            hreg.Text = h.GetHex().PadLeft(2, '0');
            lreg.Text = l.GetHex().PadLeft(2, '0');
            flagc.Text = f.carry ? "1" : "0";
            flagz.Text = f.zero ? "1" : "0";
            flags.Text = f.sign ? "1" : "0";
            flaga.Text = f.auxiliary ? "1" : "0";
            flagp.Text = f.parity ? "1" : "0";
            psw = $"{a.GetHex().PadLeft(2, '0')}{f.GetHex().PadLeft(2, '0')}";
            m = $"{h.GetHex().PadLeft(2, '0')}{l.GetHex().PadLeft(2, '0')}";
            pswreg.Text = psw.ToString();
            mreg.Text = m.ToString();
            pcreg.Text = pc.GetAddress().PadLeft(4, '0');
            spreg.Text = sp.ToString("X").PadLeft(4, '0');
        }
        private void Clear_registers()
        {
            a.Clear();
            b.Clear();
            c.Clear();
            d.Clear();
            e.Clear();
            h.Clear();
            l.Clear();
            m = "0";
            psw = "0";
            f.Clear();
            pc.Clear();
            sp = 65535;
            Update_variables();
        }
        private void Reset_memory()
        {
            ListViewItem item;
            memorybox.VirtualListSize = memory.Count;
            memory_items = new ListViewItem[memory.Count];
            m_items = new ListViewItem[memory.Count];
            for (int i = 0; i < memory_items.Length; i++)
            {
                memory[i] = Convert.ToByte(0);
                item = new ListViewItem($"{i.ToString("X").PadLeft(4, '0')}", i);
                item.SubItems.Add($"{memory[i].ToString("X").PadLeft(2, '0')}");
                memory_items[i] = m_items[i] = item;
            }
            memorybox.Invalidate();
        }
        private void Reset_stack()
        {
            ListViewItem item;
            stackbox.VirtualListSize = stack.Count;
            stack_items = new ListViewItem[stack.Count];
            for (int i = stack_items.Length - 1; i >= 0; i--)
            {
                stack[i] = Convert.ToByte(0);
                item = new ListViewItem($"{i.ToString("X").PadLeft(4, '0')}", i);
                item.SubItems.Add($"{stack[i].ToString("X").PadLeft(2, '0')}");
                stack_items[i] = item;
            }
            stackbox.Invalidate();
        }
        private void Reset_port()
        {
            ListViewItem item;
            portbox.VirtualListSize = port.Count;
            port_items = new ListViewItem[port.Count];
            for (int i = 0; i < port_items.Length; i++)
            {
                port[i] = Convert.ToByte(0);
                item = new ListViewItem($"{i.ToString("X").PadLeft(2, '0')}", i);
                item.SubItems.Add($"{port[i].ToString("X").PadLeft(2, '0')}");
                port_items[i] = item;
            }
            portbox.Update();
        }
        private int Load_into_memory()
        {
            int for_pc;
            int start_location = for_pc = 0;
            string opcode = "";
            foreach (Line line in codeEditor.Lines)
            {
                string lineF = line.Text;
                FixLineFormat(ref lineF);
                if (lineF == "")
                    continue;
                string[] code = lineF.ToLower().Split(' ');

                if (code[0].EndsWith(":"))
                    for (int i = 1; i < code.Length; i++)
                        code[i - 1] = code[i];

                if (code[0] == "mov")
                {
                    if (code[1] == "a")
                    {
                        if (code[2] == "a")
                            opcode = "7F";
                        if (code[2] == "b")
                            opcode = "78";
                        if (code[2] == "c")
                            opcode = "79";
                        if (code[2] == "d")
                            opcode = "7A";
                        if (code[2] == "e")
                            opcode = "7B";
                        if (code[2] == "h")
                            opcode = "7C";
                        if (code[2] == "l")
                            opcode = "7D";
                        if (code[2] == "m")
                            opcode = "7E";
                    }
                    if (code[1] == "b")
                    {
                        if (code[2] == "a")
                            opcode = "47";
                        if (code[2] == "b")
                            opcode = "40";
                        if (code[2] == "c")
                            opcode = "41";
                        if (code[2] == "d")
                            opcode = "42";
                        if (code[2] == "e")
                            opcode = "43";
                        if (code[2] == "h")
                            opcode = "44";
                        if (code[2] == "l")
                            opcode = "45";
                        if (code[2] == "m")
                            opcode = "46";
                    }

                    if (code[1] == "c")
                    {
                        if (code[2] == "a")
                            opcode = "4F";
                        if (code[2] == "b")
                            opcode = "48";
                        if (code[2] == "c")
                            opcode = "49";
                        if (code[2] == "d")
                            opcode = "4A";
                        if (code[2] == "e")
                            opcode = "4B";
                        if (code[2] == "h")
                            opcode = "4C";
                        if (code[2] == "l")
                            opcode = "4D";
                        if (code[2] == "m")
                            opcode = "4E";
                    }

                    if (code[1] == "d")
                    {
                        if (code[2] == "a")
                            opcode = "57";
                        if (code[2] == "b")
                            opcode = "50";
                        if (code[2] == "c")
                            opcode = "51";
                        if (code[2] == "d")
                            opcode = "52";
                        if (code[2] == "e")
                            opcode = "53";
                        if (code[2] == "h")
                            opcode = "54";
                        if (code[2] == "l")
                            opcode = "55";
                        if (code[2] == "m")
                            opcode = "56";
                    }
                    if (code[1] == "e")
                    {
                        if (code[2] == "a")
                            opcode = "5F";
                        if (code[2] == "b")
                            opcode = "58";
                        if (code[2] == "c")
                            opcode = "59";
                        if (code[2] == "d")
                            opcode = "5A";
                        if (code[2] == "e")
                            opcode = "5B";
                        if (code[2] == "h")
                            opcode = "5C";
                        if (code[2] == "l")
                            opcode = "5D";
                        if (code[2] == "m")
                            opcode = "5E";
                    }
                    if (code[1] == "h")
                    {
                        if (code[2] == "a")
                            opcode = "67";
                        if (code[2] == "b")
                            opcode = "60";
                        if (code[2] == "c")
                            opcode = "61";
                        if (code[2] == "d")
                            opcode = "62";
                        if (code[2] == "e")
                            opcode = "63";
                        if (code[2] == "h")
                            opcode = "64";
                        if (code[2] == "l")
                            opcode = "65";
                        if (code[2] == "m")
                            opcode = "66";
                    }

                    if (code[1] == "l")
                    {
                        if (code[2] == "a")
                            opcode = "6F";
                        if (code[2] == "b")
                            opcode = "68";
                        if (code[2] == "c")
                            opcode = "69";
                        if (code[2] == "d")
                            opcode = "6A";
                        if (code[2] == "e")
                            opcode = "6B";
                        if (code[2] == "h")
                            opcode = "6C";
                        if (code[2] == "l")
                            opcode = "6D";
                        if (code[2] == "m")
                            opcode = "6E";
                    }
                    if (code[1] == "m")
                    {
                        if (code[2] == "a")
                            opcode = "77";
                        if (code[2] == "b")
                            opcode = "70";
                        if (code[2] == "c")
                            opcode = "71";
                        if (code[2] == "d")
                            opcode = "72";
                        if (code[2] == "e")
                            opcode = "73";
                        if (code[2] == "h")
                            opcode = "74";
                        if (code[2] == "l")
                            opcode = "75";
                    }
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                } //done

                else if (code[0] == "mvi")
                {
                    if (code[1] == "a")
                        opcode = "3E";
                    if (code[1] == "b")
                        opcode = "06";
                    if (code[1] == "c")
                        opcode = "0E";
                    if (code[1] == "d")
                        opcode = "16";
                    if (code[1] == "e")
                        opcode = "1E";
                    if (code[1] == "h")
                        opcode = "26";
                    if (code[1] == "l")
                        opcode = "2E";
                    if (code[1] == "m")
                        opcode = "36";



                    if (code[2].EndsWith("h"))
                    {
                        code[2] = code[2].Remove(code[2].Length - 1);
                        if (code[2].StartsWith("0"))
                        {
                            code[2] = code[2].TrimStart('0');
                            if (code[2] == "") code[2] = "0";
                        }
                    }
                    else
                    {
                        if (code[2].StartsWith("0"))
                        {
                            code[2] = code[2].TrimStart('0');
                            if (code[2] == "") code[2] = "0";
                        }
                        int data = Convert.ToInt32(code[2]);
                        code[2] = data.ToString("X");
                    }
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                    memory[start_location] = Convert.ToByte(code[2].ToUpper(), 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[2].ToUpper().PadLeft(2,'0');
                    start_location++;
                } //done

                else if (code[0] == "lxi")
                {
                    if (code[1] == "b")
                        opcode = "01";
                    if (code[1] == "d")
                        opcode = "11";
                    if (code[1] == "h")
                        opcode = "21";
                    if (code[1] == "sp")
                        opcode = "31";
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;

                    if (code[2].EndsWith("h"))
                    {
                        code[2] = code[2].Remove(code[2].Length - 1);
                        if (code[2].StartsWith("0"))
                        {
                            code[2] = code[2].TrimStart('0');
                            if (code[2] == "") code[2] = "0";
                        }
                    }
                    else
                    {
                        if (code[2].StartsWith("0"))
                        {
                            code[2] = code[2].TrimStart('0');
                            if (code[2] == "") code[2] = "0";
                        }
                        int data = Convert.ToInt32(code[2]);
                        code[2] = data.ToString("X");
                    }
                    code[2] = code[2].PadLeft(4, '0');
                    memory[start_location] = Convert.ToByte(code[2].Substring(2, 2), 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[2].Substring(2, 2).ToUpper();
                    start_location++;
                    memory[start_location] = Convert.ToByte(code[2].Substring(0, 2), 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[2].Substring(0, 2).ToUpper();
                    start_location++;
                } //done

                else if (code[0] == "ldax" ||
                        code[0] == "stax")
                {
                    if (code[0] == "ldax")
                        if (code[1] == "b")
                            opcode = "0A";
                        else if (code[1] == "d")
                            opcode = "1A";
                    if (code[0] == "stax")
                        if (code[1] == "b")
                            opcode = "02";
                        else if (code[1] == "d")
                            opcode = "12";
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                } //done

                else if (code[0] == "lda" ||
                        code[0] == "sta" ||
                        code[0] == "lhld" ||
                        code[0] == "shld" ||
                        code[0] == "jmp" ||
                        code[0] == "jnz" ||
                        code[0] == "jz" ||
                        code[0] == "jnc" ||
                        code[0] == "jc" ||
                        code[0] == "jpo" ||
                        code[0] == "jpe" ||
                        code[0] == "jp" ||
                        code[0] == "jm" ||
                        code[0] == "call" ||
                        code[0] == "cnz" ||
                        code[0] == "cz" ||
                        code[0] == "cnc" ||
                        code[0] == "cc" ||
                        code[0] == "cpo" ||
                        code[0] == "cpe" ||
                        code[0] == "cp" ||
                        code[0] == "cm"
                        )
                {
                    if (code[0] == "lda")
                        opcode = "3A";
                    if (code[0] == "sta")
                        opcode = "32";
                    if (code[0] == "lhld")
                        opcode = "2A";
                    if (code[0] == "shld")
                        opcode = "22";
                    if (code[0] == "jmp")
                        opcode = "C3";
                    if (code[0] == "jnz")
                        opcode = "C2";
                    if (code[0] == "jz")
                        opcode = "CA";
                    if (code[0] == "jnc")
                        opcode = "D2";
                    if (code[0] == "jc")
                        opcode = "DA";
                    if (code[0] == "jpo")
                        opcode = "E2";
                    if (code[0] == "jpe")
                        opcode = "EA";
                    if (code[0] == "jp")
                        opcode = "F2";
                    if (code[0] == "jm")
                        opcode = "FA";
                    if (code[0] == "call")
                        opcode = "CD";
                    if (code[0] == "cnz")
                        opcode = "C4";
                    if (code[0] == "cz")
                        opcode = "CC";
                    if (code[0] == "cnc")
                        opcode = "D4";
                    if (code[0] == "cc")
                        opcode = "DC";
                    if (code[0] == "cpo")
                        opcode = "E4";
                    if (code[0] == "cpe")
                        opcode = "EC";
                    if (code[0] == "cp")
                        opcode = "F4";
                    if (code[0] == "cm")
                        opcode = "FC";

                    foreach (LabelAddress ad in labels)
                        if (ad.name == code[1])
                        {
                            code[1] = ad.address.ToString("X").PadLeft(4, '0');
                        }

                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;

                    if (code[1].EndsWith("h"))
                    {
                        code[1] = code[1].Remove(code[1].Length - 1);
                        if (code[1].StartsWith("0"))
                        {
                            code[1] = code[1].TrimStart('0');
                            if (code[1] == "") code[1] = "0";
                        }
                    }
                    else
                    {
                        if (code[1].StartsWith("0"))
                        {
                            code[1] = code[1].TrimStart('0');
                            if (code[1] == "") code[1] = "0";
                        }
                        int data = Convert.ToInt32(code[1]);
                        code[1] = data.ToString("X");
                    }
                    code[1] = code[1].PadLeft(4, '0');
                    memory[start_location] = Convert.ToByte(code[1].Substring(2, 2), 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[1].Substring(2, 2).ToUpper();
                    start_location++;
                    memory[start_location] = Convert.ToByte(code[1].Substring(0, 2), 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[1].Substring(0, 2).ToUpper();
                    start_location++;
                } //done

                else if (code[0] == "hlt" ||
                        code[0] == "stc" ||
                        code[0] == "rlc" ||
                        code[0] == "rrc" ||
                        code[0] == "ral" ||
                        code[0] == "rar" ||
                        code[0] == "ret" ||
                        code[0] == "rnz" ||
                        code[0] == "rz" ||
                        code[0] == "rnc" ||
                        code[0] == "rc" ||
                        code[0] == "rpo" ||
                        code[0] == "rpe" ||
                        code[0] == "rp" ||
                        code[0] == "rm" ||
                        code[0] == "daa" ||
                        code[0] == "cma" ||
                        code[0] == "cmc" ||
                        code[0] == "pchl" ||
                        code[0] == "xchg" ||
                        code[0] == "xthl" ||
                        code[0] == "sphl" ||
                        code[0] == "ei" ||
                        code[0] == "di" ||
                        code[0] == "rim" ||
                        code[0] == "sim" ||
                        code[0] == "nop"
                        )
                {
                    if (code[0] == "hlt")
                        opcode = "76";
                    if (code[0] == "rlc")
                        opcode = "07";
                    if (code[0] == "rrc")
                        opcode = "0F";
                    if (code[0] == "ral")
                        opcode = "17";
                    if (code[0] == "rar")
                        opcode = "1F";
                    if (code[0] == "ret")
                        opcode = "C9";
                    if (code[0] == "rnz")
                        opcode = "C0";
                    if (code[0] == "rz")
                        opcode = "C8";
                    if (code[0] == "rnc")
                        opcode = "D0";
                    if (code[0] == "rc")
                        opcode = "D8";
                    if (code[0] == "rpo")
                        opcode = "E0";
                    if (code[0] == "rpe")
                        opcode = "E8";
                    if (code[0] == "rp")
                        opcode = "F0";
                    if (code[0] == "rm")
                        opcode = "F8";
                    if (code[0] == "daa")
                        opcode = "27";
                    if (code[0] == "cma")
                        opcode = "2F";
                    if (code[0] == "cmc")
                        opcode = "3F";
                    if (code[0] == "stc")
                        opcode = "37";
                    if (code[0] == "xchg")
                        opcode = "EB";
                    if (code[0] == "pchl")
                        opcode = "E9";
                    if (code[0] == "xthl")
                        opcode = "E3";
                    if (code[0] == "sphl")
                        opcode = "F9";
                    if (code[0] == "ei")
                        opcode = "FB";
                    if (code[0] == "di")
                        opcode = "F3";
                    if (code[0] == "rim")
                        opcode = "20";
                    if (code[0] == "sim")
                        opcode = "30";
                    if (code[0] == "nop")
                        opcode = "00";

                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                } //done

                else if (code[0] == "ora" ||
                        code[0] == "ana" ||
                        code[0] == "xra" ||
                        code[0] == "add" ||
                        code[0] == "adc" ||
                        code[0] == "sub" ||
                        code[0] == "sbb" ||
                        code[0] == "inr" ||
                        code[0] == "dcr" ||
                        code[0] == "cmp")
                {
                    if (code[0] == "ora")
                    {
                        if (code[1] == "a")
                            opcode = "B7";
                        if (code[1] == "b")
                            opcode = "B0";
                        if (code[1] == "c")
                            opcode = "B1";
                        if (code[1] == "d")
                            opcode = "B2";
                        if (code[1] == "e")
                            opcode = "B3";
                        if (code[1] == "h")
                            opcode = "B4";
                        if (code[1] == "l")
                            opcode = "B5";
                        if (code[1] == "m")
                            opcode = "B6";
                    }
                    if (code[0] == "ana")
                    {
                        if (code[1] == "a")
                            opcode = "A7";
                        if (code[1] == "b")
                            opcode = "A0";
                        if (code[1] == "c")
                            opcode = "A1";
                        if (code[1] == "d")
                            opcode = "A2";
                        if (code[1] == "e")
                            opcode = "A3";
                        if (code[1] == "h")
                            opcode = "A4";
                        if (code[1] == "l")
                            opcode = "A5";
                        if (code[1] == "m")
                            opcode = "A6";
                    }
                    if (code[0] == "xra")
                    {
                        if (code[1] == "a")
                            opcode = "AF";
                        if (code[1] == "b")
                            opcode = "A8";
                        if (code[1] == "c")
                            opcode = "A9";
                        if (code[1] == "d")
                            opcode = "AA";
                        if (code[1] == "e")
                            opcode = "AB";
                        if (code[1] == "h")
                            opcode = "AC";
                        if (code[1] == "l")
                            opcode = "AD";
                        if (code[1] == "m")
                            opcode = "AE";
                    }
                    if (code[0] == "add")
                    {
                        if (code[1] == "a")
                            opcode = "87";
                        if (code[1] == "b")
                            opcode = "80";
                        if (code[1] == "c")
                            opcode = "81";
                        if (code[1] == "d")
                            opcode = "82";
                        if (code[1] == "e")
                            opcode = "83";
                        if (code[1] == "h")
                            opcode = "84";
                        if (code[1] == "l")
                            opcode = "85";
                        if (code[1] == "m")
                            opcode = "86";
                    }
                    if (code[0] == "adc")
                    {
                        if (code[1] == "a")
                            opcode = "8F";
                        if (code[1] == "b")
                            opcode = "88";
                        if (code[1] == "c")
                            opcode = "89";
                        if (code[1] == "d")
                            opcode = "8A";
                        if (code[1] == "e")
                            opcode = "8B";
                        if (code[1] == "h")
                            opcode = "8C";
                        if (code[1] == "l")
                            opcode = "8D";
                        if (code[1] == "m")
                            opcode = "8E";
                    }
                    if (code[0] == "sub")
                    {
                        if (code[1] == "a")
                            opcode = "97";
                        if (code[1] == "b")
                            opcode = "90";
                        if (code[1] == "c")
                            opcode = "91";
                        if (code[1] == "d")
                            opcode = "92";
                        if (code[1] == "e")
                            opcode = "93";
                        if (code[1] == "h")
                            opcode = "94";
                        if (code[1] == "l")
                            opcode = "95";
                        if (code[1] == "m")
                            opcode = "96";
                    }
                    if (code[0] == "sbb")
                    {
                        if (code[1] == "a")
                            opcode = "9F";
                        if (code[1] == "b")
                            opcode = "98";
                        if (code[1] == "c")
                            opcode = "99";
                        if (code[1] == "d")
                            opcode = "9A";
                        if (code[1] == "e")
                            opcode = "9B";
                        if (code[1] == "h")
                            opcode = "9C";
                        if (code[1] == "l")
                            opcode = "9D";
                        if (code[1] == "m")
                            opcode = "9E";
                    }
                    if (code[0] == "inr")
                    {
                        if (code[1] == "a")
                            opcode = "3C";
                        if (code[1] == "b")
                            opcode = "04";
                        if (code[1] == "c")
                            opcode = "0C";
                        if (code[1] == "d")
                            opcode = "14";
                        if (code[1] == "e")
                            opcode = "1C";
                        if (code[1] == "h")
                            opcode = "24";
                        if (code[1] == "l")
                            opcode = "2C";
                        if (code[1] == "m")
                            opcode = "34";
                    }
                    if (code[0] == "dcr")
                    {
                        if (code[1] == "a")
                            opcode = "3D";
                        if (code[1] == "b")
                            opcode = "05";
                        if (code[1] == "c")
                            opcode = "0D";
                        if (code[1] == "d")
                            opcode = "15";
                        if (code[1] == "e")
                            opcode = "1D";
                        if (code[1] == "h")
                            opcode = "25";
                        if (code[1] == "l")
                            opcode = "2D";
                        if (code[1] == "m")
                            opcode = "35";
                    }
                    if (code[0] == "cmp")
                    {
                        if (code[1] == "a")
                            opcode = "BF";
                        if (code[1] == "b")
                            opcode = "B8";
                        if (code[1] == "c")
                            opcode = "B9";
                        if (code[1] == "d")
                            opcode = "BA";
                        if (code[1] == "e")
                            opcode = "BB";
                        if (code[1] == "h")
                            opcode = "BC";
                        if (code[1] == "l")
                            opcode = "BD";
                        if (code[1] == "m")
                            opcode = "BE";
                    }
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                } //done

                else if (code[0] == "ani" ||
                        code[0] == "xri" ||
                        code[0] == "cpi" ||
                        code[0] == "ori" ||
                        code[0] == "adi" ||
                        code[0] == "sbi" ||
                        code[0] == "sui" ||
                        code[0] == "aci" ||
                        code[0] == "in" ||
                        code[0] == "out")
                {
                    if (code[0] == "ori")
                        opcode = "F6";
                    if (code[0] == "adi")
                        opcode = "C6";
                    if (code[0] == "sbi")
                        opcode = "DE";
                    if (code[0] == "sui")
                        opcode = "D6";
                    if (code[0] == "aci")
                        opcode = "CE";
                    if (code[0] == "ani")
                        opcode = "E6";
                    if (code[0] == "xri")
                        opcode = "EE";
                    if (code[0] == "cpi")
                        opcode = "FE";
                    if (code[0] == "in")
                        opcode = "DB";
                    if (code[0] == "out")
                        opcode = "D3";

                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;



                    if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                    {
                        code[1] = code[1].Remove(code[1].Length - 1);
                        if (code[1].StartsWith("0"))
                        {
                            code[1] = code[1].TrimStart('0');
                            if (code[1] == "") code[1] = "0";
                        }
                    }
                    else
                    {
                        if (code[1].StartsWith("0"))
                        {
                            code[1] = code[1].TrimStart('0');
                            if (code[1] == "") code[1] = "0";
                        }
                        int data = Convert.ToInt32(code[1]);
                        code[1] = data.ToString("X");
                    }

                    memory[start_location] = Convert.ToByte(code[1], 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[1].ToUpper().PadLeft(2, '0');
                    start_location++;
                } //done

                else if (code[0] == "dad" ||
                        code[0] == "inx" ||
                        code[0] == "dcx" ||
                        code[0] == "push" ||
                        code[0] == "pop")
                {
                    if (code[0] == "dad")
                    {
                        if (code[1] == "b")
                            opcode = "09";
                        if (code[1] == "d")
                            opcode = "19";
                        if (code[1] == "h")
                            opcode = "29";
                        if (code[1] == "sp")
                            opcode = "39";
                    }
                    if (code[0] == "inx")
                    {
                        if (code[1] == "b")
                            opcode = "03";
                        if (code[1] == "d")
                            opcode = "13";
                        if (code[1] == "h")
                            opcode = "23";
                        if (code[1] == "sp")
                            opcode = "33";
                    }
                    if (code[0] == "dcx")
                    {
                        if (code[1] == "b")
                            opcode = "0B";
                        if (code[1] == "d")
                            opcode = "1B";
                        if (code[1] == "h")
                            opcode = "2B";
                        if (code[1] == "sp")
                            opcode = "3B";
                    }
                    if (code[0] == "push")
                    {
                        if (code[1] == "b")
                            opcode = "C5";
                        if (code[1] == "d")
                            opcode = "D5";
                        if (code[1] == "h")
                            opcode = "E5";
                        if (code[1] == "psw")
                            opcode = "F5";
                    }
                    if (code[0] == "pop")
                    {
                        if (code[1] == "b")
                            opcode = "C1";
                        if (code[1] == "d")
                            opcode = "D1";
                        if (code[1] == "h")
                            opcode = "E1";
                        if (code[1] == "psw")
                            opcode = "F1";
                    }
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                }  //done

                else if (code[0] == "rst")
                {
                    if (code[1] == "0")
                        opcode = "C7";
                    if (code[1] == "1")
                        opcode = "CF";
                    if (code[1] == "2")
                        opcode = "D7";
                    if (code[1] == "3")
                        opcode = "DF";
                    if (code[1] == "4")
                        opcode = "E7";
                    if (code[1] == "5")
                        opcode = "EF";
                    if (code[1] == "6")
                        opcode = "F7";
                    if (code[1] == "7")
                        opcode = "FF";
                    memory[start_location] = Convert.ToByte(opcode, 16);
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                } //done
            }
            memorybox.Invalidate();
            return for_pc;
        }
        private void Read_labels()
        {
            labels.Clear();
            int start_location = 0;
            foreach (Line line in codeEditor.Lines)
            {
                string lineF = line.Text;
                FixLineFormat(ref lineF);
                if (lineF == "")
                    continue;
                string[] code = lineF.ToLower().Split(' ');
                if (code[0].EndsWith(":"))
                {
                    code[0] = code[0].Remove(code[0].Length - 1);
                    labels.Add(new LabelAddress(code[0], start_location));
                    start_location += 1;
                }
                else
                {
                    if (code[0] == "mov" ||
                        code[0] == "add" ||
                        code[0] == "sub" ||
                        code[0] == "adc" ||
                        code[0] == "sbb" ||
                        code[0] == "inr" ||
                        code[0] == "dcr" ||
                        code[0] == "ana" ||
                        code[0] == "ora" ||
                        code[0] == "xra" ||
                        code[0] == "cmp" ||
                        code[0] == "stc" ||
                        code[0] == "cma" ||
                        code[0] == "daa" ||
                        code[0] == "cmc" ||
                        code[0] == "rlc" ||
                        code[0] == "rrc" ||
                        code[0] == "ral" ||
                        code[0] == "rar" ||
                        code[0] == "ret" ||
                        code[0] == "rnz" ||
                        code[0] == "rz" ||
                        code[0] == "rnc" ||
                        code[0] == "rc" ||
                        code[0] == "rpo" ||
                        code[0] == "rpe" ||
                        code[0] == "rp" ||
                        code[0] == "rm" ||
                        code[0] == "pchl" ||
                        code[0] == "xthl" ||
                        code[0] == "sphl" ||
                        code[0] == "ei" ||
                        code[0] == "di" ||
                        code[0] == "rim" ||
                        code[0] == "sim" ||
                        code[0] == "nop" ||
                        code[0] == "hlt" ||
                        code[0] == "xchg" ||
                        code[0] == "ldax" ||
                        code[0] == "stax" ||
                        code[0] == "dad" ||
                        code[0] == "inx" ||
                        code[0] == "dcx" ||
                        code[0] == "rst" ||
                        code[0] == "push" ||
                        code[0] == "pop")
                        start_location += 1;

                    else if (code[0] == "mvi" ||
                        code[0] == "adi" ||
                        code[0] == "aci" ||
                        code[0] == "sui" ||
                        code[0] == "sbi" ||
                        code[0] == "ani" ||
                        code[0] == "xri" ||
                        code[0] == "ori" ||
                        code[0] == "cpi" ||
                        code[0] == "in" ||
                        code[0] == "out"
                        )
                        start_location += 2;

                    else if (code[0] == "jmp" ||
                        code[0] == "jnz" ||
                        code[0] == "jz" ||
                        code[0] == "jnc" ||
                        code[0] == "jc" ||
                        code[0] == "jpo" ||
                        code[0] == "jpe" ||
                        code[0] == "jp" ||
                        code[0] == "jm" ||
                        code[0] == "call" ||
                        code[0] == "cnz" ||
                        code[0] == "cz" ||
                        code[0] == "cnc" ||
                        code[0] == "lda" ||
                        code[0] == "sta" ||
                        code[0] == "cc" ||
                        code[0] == "cpo" ||
                        code[0] == "cpe" ||
                        code[0] == "cp" ||
                        code[0] == "cm" ||
                        code[0] == "lxi" ||
                        code[0] == "lhld" ||
                        code[0] == "shld")
                        start_location += 3;
                }
            }
        }
        private bool IsLabel(string lbl)
        {
            foreach (LabelAddress ad in labels)
                if (ad.name == lbl)
                    return true;
            return false;
        }
        private void FixLineFormat(ref string lineF)
        {
            if(lineF.Split(' ')[0].Contains(":"))
                lineF = Regex.Replace(lineF, @":+", ": ");
            lineF = Regex.Replace(lineF, @",+", " ");
            lineF = Regex.Replace(lineF, @";+", " ;");
            lineF = Regex.Replace(lineF, @"\s+", " ");
            lineF = lineF.Trim(' ');
        }
        private bool Code_inspect(bool run_code)
        {
            Read_labels();
            Clear_registers();
            errors.Clear();
            output_box.Items.Clear();
            int error_level = 0;
            int line_number = 1;
            bool halt = false;
            foreach (Line line in codeEditor.Lines)
            {
                string lineF = line.Text;
                FixLineFormat(ref lineF);
                if (lineF == "")
                {
                    line_number++;
                    continue;
                }
                if (lineF.ToLower().Split(' ')[0] == "hlt")
                    halt = true;

                string[] code = lineF.Split(' ');
                string error_string = "";
                if (code[0].EndsWith(":"))
                {
                    code[0] = code[0].Remove(code[0].Length - 1);
                    if (code.Length < 2)
                    {
                        error_string = $"\"{code[0]}\" cannot be alone! You need to atleast add \"nop\" at the end";
                        errors.Add($"{error_string} on line {line_number}");
                        error_level++;
                        continue;
                    }
                    for (int i = 1; i < code.Length; i++)
                    {
                        code[i - 1] = code[i];
                    }
                }
                error_string = Check_error(code);
                if (error_string != "")
                {
                    if (error_level >= 50)
                        break;
                    errors.Add($"{error_string} on line {line_number}");
                    error_level++;
                }
                line_number++;
            }
            if (halt == false)
            {
                errors.Add("No end to the program detected, did you forget \"HLT\"?");
                error_level++;
            }
            if (error_level > 0)
            {
                foreach (string error in errors)
                {
                    output_box.Items.Add(error);
                }
                blinker.Start();
                codeEditor.Enabled = true;
                stop_button.Enabled = false;
                step_button.Enabled = true;
                runToolStripMenuItem1.Enabled = true;
                stepNextToolStripMenuItem.Enabled = true;
                checkErrorsToolStripMenuItem.Enabled = true;
                clearEditorToolStripMenuItem.Enabled = true;
                clearMemoryToolStripMenuItem.Enabled = true;
                clearRegistersToolStripMenuItem.Enabled = true;
                newASMFileToolStripMenuItem.Enabled = true;
                openASMToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                status_lbl.Text = "Program Stopped, " + $"Errors :{error_level}";
                status_lbl.ForeColor = Color.Red;
                play_button.Enabled = true;
                stop_run = true;
                return false;
            }
            else
            {
                if (run_code == true)
                {
                    pc.counter = Load_into_memory();
                    while (memory[pc.counter] != 118)
                    {
                        Code_execute(memory[pc.counter].ToString("X"));
                        pc.Increment();
                        Update_variables();
                        if (sp == 0)
                            sp += 1;
                        stackbox.TopItem = stackbox.Items[sp - 1];
                        if (stop_run == true)
                        {
                            break;
                        }
                        Application.DoEvents();
                    }
                    if (stop_run)
                    {
                        foreach (string error in errors)
                        {
                            output_box.Items.Add(error);
                        }
                    }
                    codeEditor.Enabled = true;
                    stop_button.Enabled = false;
                    step_button.Enabled = true;
                    runToolStripMenuItem1.Enabled = true;
                    stepNextToolStripMenuItem.Enabled = true;
                    checkErrorsToolStripMenuItem.Enabled = true;
                    clearEditorToolStripMenuItem.Enabled = true;
                    clearMemoryToolStripMenuItem.Enabled = true;
                    clearRegistersToolStripMenuItem.Enabled = true;
                    newASMFileToolStripMenuItem.Enabled = true;
                    openASMToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                    stopToolStripMenuItem.Enabled = false;
                    status_lbl.Text = "Program Stopped";
                    status_lbl.ForeColor = Color.Red;
                    play_button.Enabled = true;
                    stop_run = true;
                    output_box.Items.Add("Program successfully terminated!");
                }
                else
                    output_box.Items.Add("No errors found in the code!");
                return true;
            }
        }
        private void Code_execute(string hex)
        {
            // ACI data8
            if (hex == "CE")
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1] + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
                pc.IncrementBy(1);
            }
            // ADC A
            else if (hex == "8F")
            {
                Register temp = new Register();
                int data = a.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC B
            else if (hex == "88")
            {
                Register temp = new Register();
                int data = b.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC C
            else if (hex == "89")
            {
                Register temp = new Register();
                int data = c.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC D
            else if (hex == "8A")
            {
                Register temp = new Register();
                int data = d.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC E
            else if (hex == "8B")
            {
                Register temp = new Register();
                int data = e.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC H
            else if (hex == "8C")
            {
                Register temp = new Register();
                int data = h.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC L
            else if (hex == "8D")
            {
                Register temp = new Register();
                int data = l.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADC M
            else if (hex == "8E")
            {
                Register temp = new Register();
                int data = memory[Convert.ToInt32(m, 16)] + (f.carry ? 1 : 0);
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // ADD A
            else if (hex == "87")
            {
                Register temp = new Register();
                int data = a.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD B
            else if (hex == "80")
            {
                Register temp = new Register();
                int data = b.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD C
            else if (hex == "81")
            {
                Register temp = new Register();
                int data = c.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD D
            else if (hex == "82")
            {
                Register temp = new Register();
                int data = d.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD E
            else if (hex == "83")
            {
                Register temp = new Register();
                int data = e.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD H
            else if (hex == "84")
            {
                Register temp = new Register();
                int data = h.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD L
            else if (hex == "85")
            {
                Register temp = new Register();
                int data = l.GetInt();
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // ADD M
            else if (hex == "86")
            {
                Register temp = new Register();
                int data = memory[Convert.ToInt32(m, 16)];
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
                f.Update(a);
            }
            // ADI data8
            else if (hex == "C6")
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1];
                temp.SetData(data);
                data = a.GetInt() + temp.GetInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
                pc.IncrementBy(1);
            }
            // ANA A
            else if (hex == "A7")
            {
                Register temp = new Register();
                int data = a.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA B
            else if (hex == "A0")
            {
                Register temp = new Register();
                int data = b.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA C
            else if (hex == "A1")
            {
                Register temp = new Register();
                int data = c.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA D
            else if (hex == "A2")
            {
                Register temp = new Register();
                int data = d.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA E
            else if (hex == "A3")
            {
                Register temp = new Register();
                int data = e.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA H
            else if (hex == "A4")
            {
                Register temp = new Register();
                int data = h.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA L
            else if (hex == "A5")
            {
                Register temp = new Register();
                int data = l.GetInt();
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANA M
            else if (hex == "A6")
            {
                Register temp = new Register();
                int index = Convert.ToInt32(m, 16);
                int data = memory[index];
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.SetData(data);
                f.Update(a);
            }
            // ANI data8
            else if (hex == "E6")
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1];
                temp.SetData(data);
                data &= a.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
                //ani resets carry flag by default
                f.carry = false;
                //ani sets auxiliary carry by default
                f.auxiliary = true;
                //
                pc.IncrementBy(1);
            }
            // CALL label16
            else if (hex == "CD")
            {
                string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                sp--;
                stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                string address = "";
                address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                pc.counter = Convert.ToInt32(address) - 1;
                stackbox.Invalidate();
            }
            // CC label16
            else if (hex == "DC")
            {
                if (f.carry == true)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CM label16
            else if (hex == "FC")
            {
                if (f.sign == true)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CMA
            else if (hex == "2F")
            {
                a.SetData(a.GetNInt());
            }
            // CMC
            else if (hex == "3F")
            {
                f.carry = !f.carry;
            }
            // CMP A
            else if (hex == "BF")
            {
                Register temp = new Register();
                temp.SetData(a.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP B
            else if (hex == "B8")
            {
                Register temp = new Register();
                temp.SetData(b.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP C
            else if (hex == "B9")
            {
                Register temp = new Register();
                temp.SetData(c.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP D
            else if (hex == "BA")
            {
                Register temp = new Register();
                temp.SetData(d.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP E
            else if (hex == "BB")
            {
                Register temp = new Register();
                temp.SetData(e.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP H
            else if (hex == "BC")
            {
                Register temp = new Register();
                temp.SetData(h.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP L
            else if (hex == "BD")
            {
                Register temp = new Register();
                temp.SetData(l.GetInt());
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CMP M
            else if (hex == "BE")
            {
                int index = Convert.ToInt32(m, 16);
                int data = Convert.ToInt32(memory[index]);
                Register temp = new Register();
                temp.SetData(data);
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                else
                    f.carry = false;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
            }
            // CNC label16
            else if (hex == "D4")
            {
                if (f.carry == false)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CNZ label16
            else if (hex == "C4")
            {
                if (f.zero == false)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CP label16
            else if (hex == "F4")
            {
                if (f.sign == false)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CPE label16
            else if (hex == "EC")
            {
                if (f.parity == true)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CPI data8
            else if (hex == "FE")
            {
                int data = Convert.ToInt32(memory[pc.counter + 1]);
                Register temp = new Register();
                temp.SetData(data);
                if (temp.GetInt() > a.GetInt())
                    f.carry = true;
                temp.SetData(temp.Get2SInt());
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                temp.SetData(a.GetInt() + temp.GetInt());
                f.Update(temp);
                pc.IncrementBy(1);
            }
            // CPO label16
            else if (hex == "E4")
            {
                if (f.parity == false)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // CZ label16
            else if (hex == "CC")
            {
                if (f.zero == true)
                {
                    string pchex = Convert.ToString(pc.counter + 2, 16).PadLeft(4, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(2, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp--;
                    stack[sp] = Convert.ToByte(pchex.Substring(0, 2), 16);
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    string address = "";
                    address += memory[pc.counter + 2].ToString().PadLeft(2, '0');
                    address += memory[pc.counter + 1].ToString().PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(address) - 1;
                    stackbox.Invalidate();
                }
                else
                    pc.IncrementBy(2);
            }
            // DAA
            else if (hex == "27")
            {
                string ar = a.GetHex();
                int al = Convert.ToInt32(ar.Substring(1, 1), 16);
                int ah = Convert.ToInt32(ar.Substring(0, 1), 16);
                if(f.auxiliary==true)
                {
                    al += 6;
                    if (al > 15)
                    {
                        f.auxiliary = true;
                        al -= 16;
                        ah += 1;
                    }
                    else
                        f.auxiliary = false;
                }
                else
                {
                    if (al > 9)
                    {
                        al += 6;
                        if (al > 15)
                        {
                            f.auxiliary = true;
                            al -= 16;
                            ah += 1;
                        }
                        else
                            f.auxiliary = false;
                    }
                }
                if(f.carry==true)
                {
                    ah += 6;
                    if (ah > 15)
                    {
                        f.carry = true;
                        ah -= 16;
                    }
                    else
                        f.carry = false;
                }
                else
                {
                    if (ah > 9)
                    {
                        ah += 6;
                        if (ah > 15)
                        {
                            ah -= 16;
                            f.carry = true;
                        }
                        else
                            f.carry = false;
                    }
                }
                a.SetData(ah.ToString("X") + al.ToString("X"));
                f.Update(a);
            }
            // DAD B [C]
            else if (hex == "9")
            {
                string t1 = b.GetHex() + c.GetHex();
                string t2 = h.GetHex() + l.GetHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.SetData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.SetData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            // DAD D [E]
            else if (hex == "19")
            {
                string t1 = d.GetHex() + e.GetHex();
                string t2 = h.GetHex() + l.GetHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.SetData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.SetData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            // DAD H [L]
            else if (hex == "29")
            {
                string t1 = h.GetHex() + l.GetHex();
                string t2 = h.GetHex() + l.GetHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.SetData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.SetData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            // DAD SP
            else if (hex == "39")
            {
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                string t2 = h.GetHex() + l.GetHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.SetData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.SetData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            // DCR A
            else if (hex == "3D")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = a.GetInt() + temp.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // DCR B
            else if (hex == "5")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = b.GetInt() + temp.GetInt();
                f.CheckAuxiliary(b.GetBinaryString(), temp.GetBinaryString());
                b.SetData(data);
                f.Update(b);
            }
            // DCR C
            else if (hex == "D")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = c.GetInt() + temp.GetInt();
                f.CheckAuxiliary(c.GetBinaryString(), temp.GetBinaryString());
                c.SetData(data);
                f.Update(c);
            }
            // DCR D
            else if (hex == "15")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = d.GetInt() + temp.GetInt();
                f.CheckAuxiliary(d.GetBinaryString(), temp.GetBinaryString());
                d.SetData(data);
                f.Update(d);
            }
            // DCR E
            else if (hex == "1D")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = e.GetInt() + temp.GetInt();
                f.CheckAuxiliary(e.GetBinaryString(), temp.GetBinaryString());
                e.SetData(data);
                f.Update(e);
            }
            // DCR H
            else if (hex == "25")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = h.GetInt() + temp.GetInt();
                f.CheckAuxiliary(h.GetBinaryString(), temp.GetBinaryString());
                h.SetData(data);
                f.Update(h);
            }
            // DCR L
            else if (hex == "2D")
            {
                //dcr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int data = l.GetInt() + temp.GetInt();
                f.CheckAuxiliary(l.GetBinaryString(), temp.GetBinaryString());
                l.SetData(data);
                f.Update(l);
            }
            // DCR M
            else if (hex == "35")
            {
                //dcr ignores carry flag
                int index = Convert.ToInt32(m, 16);
                Register data = new Register();
                data.SetData(Convert.ToInt32(memory[index]));
                Register temp = new Register();
                temp.SetData(1);
                temp.SetData(temp.Get2SInt());
                int res = data.GetInt() + temp.GetInt();
                f.CheckAuxiliary(data.GetBinaryString(), temp.GetBinaryString());
                data.SetData(res);
                f.Update(data);
                memorybox.Items[index].SubItems[1].Text = data.GetHex();
                memorybox.Invalidate();
            }
            // DCX B [C]
            else if (hex == "B")
            {
                //dcx cannot set any flags
                string t1 = b.GetHex() + c.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                b.SetData(t1.Substring(0, 2));
                c.SetData(t1.Substring(2, 2));
            }
            // DCX D [E]
            else if (hex == "1B")
            {
                //dcx cannot set any flags
                string t1 = d.GetHex() + e.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                d.SetData(t1.Substring(0, 2));
                e.SetData(t1.Substring(2, 2));
            }
            // DCX H [L]
            else if (hex == "2B")
            {
                //dcx cannot set any flags
                string t1 = h.GetHex() + l.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                h.SetData(t1.Substring(0, 2));
                l.SetData(t1.Substring(2, 2));
            }
            // DCX SP
            else if (hex == "3B")
            {
                //dcx cannot set any flags
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                sp = data;
            }
            // DI
            else if (hex == "F3")
            {

            }
            // EI
            else if (hex == "FB")
            {
 
            }
            // HLT
            else if (hex == "76")
            {
                //end of life
            }
            // IN Port8
            else if (hex == "DB")
            {
                a.SetData(Convert.ToInt32(port[memory[pc.counter + 1]]));
                pc.IncrementBy(1);
            }
            // INR A
            else if (hex == "3C")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = a.GetInt() + temp.GetInt();
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(data);
                f.Update(a);
            }
            // INR B
            else if (hex == "4")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = b.GetInt() + temp.GetInt();
                f.CheckAuxiliary(b.GetBinaryString(), temp.GetBinaryString());
                b.SetData(data);
                f.Update(b);
            }
            // INR C
            else if (hex == "C")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = c.GetInt() + temp.GetInt();
                f.CheckAuxiliary(c.GetBinaryString(), temp.GetBinaryString());
                c.SetData(data);
                f.Update(c);
            }
            // INR D
            else if (hex == "14")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = d.GetInt() + temp.GetInt();
                f.CheckAuxiliary(d.GetBinaryString(), temp.GetBinaryString());
                d.SetData(data);
                f.Update(d);
            }
            // INR E
            else if (hex == "1C")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = e.GetInt() + temp.GetInt();
                f.CheckAuxiliary(e.GetBinaryString(), temp.GetBinaryString());
                e.SetData(data);
                f.Update(e);
            }
            // INR H
            else if (hex == "24")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = h.GetInt() + temp.GetInt();
                f.CheckAuxiliary(h.GetBinaryString(), temp.GetBinaryString());
                h.SetData(data);
                f.Update(h);
            }
            // INR L
            else if (hex == "2C")
            {
                //inr ignores carry flag
                Register temp = new Register();
                temp.SetData(1);
                int data = l.GetInt() + temp.GetInt();
                f.CheckAuxiliary(l.GetBinaryString(), temp.GetBinaryString());
                l.SetData(data);
                f.Update(l);
            }
            // INR M
            else if (hex == "34")
            {
                //inr ignores carry flag
                int index = Convert.ToInt32(m, 16);
                Register data = new Register();
                data.SetData(Convert.ToInt32(memory[index]));
                Register temp = new Register();
                temp.SetData(1);
                int res = data.GetInt() + temp.GetInt();
                f.CheckAuxiliary(data.GetBinaryString(), temp.GetBinaryString());
                data.SetData(res);
                f.Update(data);
                memorybox.Items[index].SubItems[1].Text = data.GetHex();
                memorybox.Invalidate();
            }
            // INX B [C]
            else if (hex == "3")
            {
                //inx cannot set any flags
                string t1 = b.GetHex() + c.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                b.SetData(t1.Substring(0, 2));
                c.SetData(t1.Substring(2, 2));
            }
            // INX D [E]
            else if (hex == "13")
            {
                //inx cannot set any flags
                string t1 = d.GetHex() + e.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                d.SetData(t1.Substring(0, 2));
                e.SetData(t1.Substring(2, 2));
            }
            // INX H [L]
            else if (hex == "23")
            {
                //inx cannot set any flags
                string t1 = h.GetHex() + l.GetHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                h.SetData(t1.Substring(0, 2));
                l.SetData(t1.Substring(2, 2));
            }
            // INX SP
            else if (hex == "33")
            {
                //inx cannot set any flags
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                sp = data;
            }
            // JC label16
            else if (hex == "DA")
            {
                if (f.carry == true)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JM label16
            else if (hex == "FA")
            {
                if (f.sign == true)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JMP label16
            else if (hex == "C3")
            {
                string label = "";
                label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                pc.counter = Convert.ToInt32(label, 16);
                pc.counter -= 1;
            }
            // JNC label16
            else if (hex == "D2")
            {
                if (f.carry == false)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JNZ label16
            else if (hex == "C2")
            {
                if (f.zero == false)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JP label16
            else if (hex == "F2")
            {
                if (f.sign == false)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JPE label16
            else if (hex == "EA")
            {
                if (f.parity == true)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JPO label16
            else if (hex == "D2")
            {
                if (f.parity == false)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // JZ label16
            else if (hex == "CA")
            {
                if (f.zero == true)
                {
                    string label = "";
                    label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                    label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                    pc.counter = Convert.ToInt32(label, 16);
                    pc.counter -= 1;
                }
                else
                    pc.IncrementBy(2);
            }
            // LDA address16
            else if (hex == "3A")
            {
                string addr = "";
                addr += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                addr += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                int index = Convert.ToInt32(addr, 16);
                a.SetData(Convert.ToInt32(memory[index]));
                pc.IncrementBy(2);
            }
            // LDAX B [C]
            else if (hex == "A")
            {
                string addr = "";
                addr += b.GetHex();
                addr += c.GetHex();
                int index = Convert.ToInt32(addr, 16);
                a.SetData(Convert.ToInt32(memory[index]));
            }
            // LDAX D [E]
            else if (hex == "1A")
            {
                string addr = "";
                addr += d.GetHex();
                addr += e.GetHex();
                int index = Convert.ToInt32(addr, 16);
                a.SetData(Convert.ToInt32(memory[index]));
            }
            // LHLD address16
            else if (hex == "2A")
            {
                string addr = memory[pc.counter + 2].ToString("X").PadLeft(2, '0') + memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                int ad1 = memory[Convert.ToInt32(addr, 16) + 1];
                int ad2 = memory[Convert.ToInt32(addr, 16)];
                h.SetData(ad1);
                l.SetData(ad2);
                pc.IncrementBy(2);
            }
            // LXI B [C] data16
            else if (hex == "1")
            {
                b.SetData(Convert.ToInt32(memory[pc.counter + 2]));
                c.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(2);
            }
            // LXI D [E] data16
            else if (hex == "11")
            {
                d.SetData(Convert.ToInt32(memory[pc.counter + 2]));
                e.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(2);
            }
            // LXI H [L] data16
            else if (hex == "21")
            {
                h.SetData(Convert.ToInt32(memory[pc.counter + 2]));
                l.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(2);
            }
            // LXI SP data16
            else if (hex == "31")
            {
                string data = "";
                data += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                data += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                sp = Convert.ToInt32(data, 16);
                pc.IncrementBy(2);
            }
            // MOV A A
            else if (hex == "7F")
            {
                a.SetData(a.GetInt());
            }
            // MOV A B
            else if (hex == "78")
            {
                a.SetData(b.GetInt());
            }
            // MOV A C
            else if (hex == "79")
            {
                a.SetData(c.GetInt());
            }
            // MOV A D
            else if (hex == "7A")
            {
                a.SetData(d.GetInt());
            }
            // MOV A E
            else if (hex == "7B")
            {
                a.SetData(e.GetInt());
            }
            // MOV A H
            else if (hex == "7C")
            {
                a.SetData(h.GetInt());
            }
            // MOV A L
            else if (hex == "7D")
            {
                a.SetData(l.GetInt());
            }
            // MOV A M
            else if (hex == "7E")
            {
                int index = Convert.ToInt32(m, 16);
                a.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV B A
            else if (hex == "47")
            {
                b.SetData(a.GetInt());
            }
            // MOV B B
            else if (hex == "40")
            {
                b.SetData(b.GetInt());
            }
            // MOV B C
            else if (hex == "41")
            {
                b.SetData(c.GetInt());
            }
            // MOV B D
            else if (hex == "42")
            {
                b.SetData(d.GetInt());
            }
            // MOV B E
            else if (hex == "43")
            {
                b.SetData(e.GetInt());
            }
            // MOV B H
            else if (hex == "44")
            {
                b.SetData(h.GetInt());
            }
            // MOV B L
            else if (hex == "45")
            {
                b.SetData(l.GetInt());
            }
            // MOV B M
            else if (hex == "46")
            {
                int index = Convert.ToInt32(m, 16);
                b.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV C A
            else if (hex == "4F")
            {
                c.SetData(a.GetInt());
            }
            // MOV C B
            else if (hex == "48")
            {
                c.SetData(b.GetInt());
            }
            // MOV C C
            else if (hex == "49")
            {
                c.SetData(c.GetInt());
            }
            // MOV C D
            else if (hex == "4A")
            {
                c.SetData(d.GetInt());
            }
            // MOV C E
            else if (hex == "4B")
            {
                c.SetData(l.GetInt());
            }
            // MOV C H
            else if (hex == "4C")
            {
                c.SetData(l.GetInt());
            }
            // MOV C L
            else if (hex == "4D")
            {
                c.SetData(l.GetInt());
            }
            // MOV C M
            else if (hex == "4E")
            {
                int index = Convert.ToInt32(m, 16);
                c.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV D A
            else if (hex == "57")
            {
                d.SetData(a.GetInt());
            }
            // MOV D B
            else if (hex == "50")
            {
                d.SetData(b.GetInt());
            }
            // MOV D C
            else if (hex == "51")
            {
                d.SetData(c.GetInt());
            }
            // MOV D D
            else if (hex == "52")
            {
                a.SetData(a.GetInt());
            }
            // MOV D E
            else if (hex == "53")
            {
                d.SetData(e.GetInt());
            }
            // MOV D H
            else if (hex == "54")
            {
                d.SetData(h.GetInt());
            }
            // MOV D L
            else if (hex == "55")
            {
                d.SetData(l.GetInt());
            }
            // MOV D M
            else if (hex == "56")
            {
                int index = Convert.ToInt32(m, 16);
                d.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV E A
            else if (hex == "5F")
            {
                e.SetData(a.GetInt());
            }
            // MOV E B
            else if (hex == "58")
            {
                e.SetData(b.GetInt());
            }
            // MOV E C
            else if (hex == "59")
            {
                e.SetData(c.GetInt());
            }
            // MOV E D
            else if (hex == "5A")
            {
                e.SetData(d.GetInt());
            }
            // MOV E E
            else if (hex == "5B")
            {
                e.SetData(e.GetInt());
            }
            // MOV E H
            else if (hex == "5C")
            {
                e.SetData(h.GetInt());
            }
            // MOV E L
            else if (hex == "5D")
            {
                e.SetData(l.GetInt());
            }
            // MOV E M
            else if (hex == "5E")
            {
                int index = Convert.ToInt32(m, 16);
                e.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV H A
            else if (hex == "67")
            {
                h.SetData(a.GetInt());
            }
            // MOV H B
            else if (hex == "60")
            {
                h.SetData(b.GetInt());
            }
            // MOV H C
            else if (hex == "61")
            {
                h.SetData(c.GetInt());
            }
            // MOV H D
            else if (hex == "62")
            {
                h.SetData(d.GetInt());
            }
            // MOV H E
            else if (hex == "63")
            {
                h.SetData(e.GetInt());
            }
            // MOV H H
            else if (hex == "64")
            {
                h.SetData(h.GetInt());
            }
            // MOV H L
            else if (hex == "65")
            {
                h.SetData(l.GetInt());
            }
            // MOV H M
            else if (hex == "66")
            {
                int index = Convert.ToInt32(m, 16);
                h.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV L A
            else if (hex == "6F")
            {
                l.SetData(a.GetInt());
            }
            // MOV L B
            else if (hex == "68")
            {
                l.SetData(b.GetInt());
            }
            // MOV L C
            else if (hex == "69")
            {
                l.SetData(c.GetInt());
            }
            // MOV L D
            else if (hex == "6A")
            {
                l.SetData(d.GetInt());
            }
            // MOV L E
            else if (hex == "6B")
            {
                l.SetData(e.GetInt());
            }
            // MOV L H
            else if (hex == "6C")
            {
                l.SetData(h.GetInt());
            }
            // MOV L L
            else if (hex == "6D")
            {
                l.SetData(l.GetInt());
            }
            // MOV L M
            else if (hex == "6E")
            {
                int index = Convert.ToInt32(m, 16);
                l.SetData(Convert.ToInt32(memory[index]));
            }
            // MOV M A
            else if (hex == "77")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(a.GetInt());
                memorybox.Items[index].SubItems[1].Text = a.GetHex();
                memorybox.Invalidate();
            }
            // MOV M B
            else if (hex == "70")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(b.GetInt());
                memorybox.Items[index].SubItems[1].Text = b.GetHex();
                memorybox.Invalidate();
            }
            // MOV M C
            else if (hex == "71")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(c.GetInt());
                memorybox.Items[index].SubItems[1].Text = c.GetHex();
                memorybox.Invalidate();
            }
            // MOV M D
            else if (hex == "72")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(d.GetInt());
                memorybox.Items[index].SubItems[1].Text = d.GetHex();
                memorybox.Invalidate();
            }
            // MOV M E
            else if (hex == "73")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(e.GetInt());
                memorybox.Items[index].SubItems[1].Text = e.GetHex();
                memorybox.Invalidate();
            }
            // MOV M H
            else if (hex == "74")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(h.GetInt());
                memorybox.Items[index].SubItems[1].Text = h.GetHex();
                memorybox.Invalidate();
            }
            // MOV M L
            else if (hex == "75")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(l.GetInt());
                memorybox.Items[index].SubItems[1].Text = l.GetHex();
                memorybox.Invalidate();
            }
            // MVI A data8
            else if (hex == "3E")
            {
                a.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI B data8
            else if (hex == "6")
            {
                b.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI C data8
            else if (hex == "E")
            {
                c.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI D data8
            else if (hex == "16")
            {
                d.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI E data8
            else if (hex == "1E")
            {
                e.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI H data8
            else if (hex == "26")
            {
                h.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI L data8
            else if (hex == "2E")
            {
                l.SetData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.IncrementBy(1);
            }
            // MVI M data8
            else if (hex == "36")
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = memory[pc.counter + 1];
                memorybox.Items[index].SubItems[1].Text = memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                memorybox.Invalidate();
                pc.IncrementBy(1);
            }
            // NOP
            else if (hex == "0")
            {
                //NO MEANS NO
            }
            // ORA A
            else if (hex == "B7")
            {
                int ora = a.GetInt() | a.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA B
            else if (hex == "B0")
            {
                int ora = a.GetInt() | b.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA C
            else if (hex == "B1")
            {
                int ora = a.GetInt() | c.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA D
            else if (hex == "B2")
            {
                int ora = a.GetInt() | d.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA E
            else if (hex == "B3")
            {
                int ora = a.GetInt() | e.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA H
            else if (hex == "B4")
            {
                int ora = a.GetInt() | h.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA L
            else if (hex == "B5")
            {
                int ora = a.GetInt() | l.GetInt();
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORA M
            else if (hex == "B6")
            {
                int ora = a.GetInt() | Convert.ToInt32(memory[Convert.ToInt32(m, 16)]);
                a.SetData(ora);
                //ora reset carry by default
                f.carry = false;
                //ora reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // ORI data8
            else if (hex == "F6")
            {
                int ora = a.GetInt() | Convert.ToInt32(memory[pc.counter + 1]);
                a.SetData(ora);
                //ori reset carry by default
                f.carry = false;
                //ori reset auxiliary by default
                f.auxiliary = false;
                //
                f.Update(a);
            }
            // OUT port8
            else if (hex == "D3")
            {
                port[memory[pc.counter + 1]] = Convert.ToByte(a.GetInt());
                portbox.Items[memory[pc.counter + 1]].SubItems[1].Text = a.GetHex();
                portbox.Invalidate();
            }
            // PCHL
            else if (hex == "E9")
            {
                int data = Convert.ToInt32(h.GetInt()) + Convert.ToInt32(l.GetInt());
                pc.counter = data - 1;
            }
            // POP B [C]
            else if (hex == "C1")
            {
                if (sp <= 65533)
                {
                    b.SetData(stack[sp]);
                    sp++;
                    c.SetData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            // POP D [E]
            else if (hex == "D1")
            {
                if (sp <= 65533)
                {
                    d.SetData(stack[sp]);
                    sp++;
                    e.SetData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            // POP H [L]
            else if (hex == "E1")
            {
                if (sp <= 65533)
                {
                    h.SetData(stack[sp]);
                    sp++;
                    l.SetData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            // POP PSW
            else if (hex == "F1")
            {
                if (sp <= 65533)
                {
                    a.SetData(stack[sp]);
                    sp++;
                    f.SetData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            // PUSH B [C]
            else if (hex == "C5")
            {
                sp--;
                stack[sp] = Convert.ToByte(c.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(b.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
            // PUSH D [E]
            else if (hex == "D5")
            {
                sp--;
                stack[sp] = Convert.ToByte(e.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(d.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
            // PUSH H [L]
            else if (hex == "E5")
            {
                sp--;
                stack[sp] = Convert.ToByte(l.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(h.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
            // PUSH PSW
            else if (hex == "F5")
            {
                sp--;
                stack[sp] = Convert.ToByte(f.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(a.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
            // RAL
            else if (hex == "17")
            {
                bool temp = a.data[0];
                for (int i = 1; i < 8; i++)
                    a.data[i - 1] = a.data[i];
                a.data[7] = f.carry;
                f.carry = temp;
            }
            // RAR
            else if (hex == "1F")
            {
                bool temp = a.data[7];
                for (int i = 7; i > 0; i--)
                    a.data[i] = a.data[i - 1];
                a.data[0] = f.carry;
                f.carry = temp;
            }
            // RC
            else if (hex == "D8")
            {
                if (f.carry == true)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RET
            else if (hex == "C9")
            {
                if (sp <= 65533)
                {
                    string address = "";
                    address += stack[sp].ToString().PadLeft(2, '0');
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp++;
                    address += stack[sp].ToString().PadLeft(2, '0');
                    stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                    sp++;
                    pc.counter = Convert.ToInt32(address, 16);
                    stackbox.Invalidate();
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            // RIM
            else if (hex == "20")
            {
               
            }
            // RLC
            else if (hex == "7")
            {
                bool temp = a.data[0];
                for (int i = 1; i < 8; i++)
                    a.data[i - 1] = a.data[i];
                a.data[7] = temp;
                f.carry = temp;
            }
            // RM
            else if (hex == "F8")
            {
                if (f.sign == true)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RNC
            else if (hex == "D0")
            {
                if (f.carry == false)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RNZ
            else if (hex == "C0")
            {
                if (f.zero == false)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RP
            else if (hex == "F0")
            {
                if (f.sign == false)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RPE
            else if (hex == "E8")
            {
                if (f.parity == true)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RPO
            else if (hex == "E0")
            {
                if (f.parity == false)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // RRC
            else if (hex == "F")
            {
                bool temp = a.data[7];
                for (int i = 8; i > 1; i--)
                    a.data[i] = a.data[i - 1];
                a.data[0] = temp;
                f.carry = temp;
            }
            // RST 0
            else if (hex == "C7")
            {

            }
            // RST 1
            else if (hex == "CF")
            {

            }
            // RST 2
            else if (hex == "D7")
            {

            }
            // RST 3
            else if (hex == "DF")
            {

            }
            // RST 4
            else if (hex == "E7")
            {

            }
            // RST 5
            else if (hex == "EF")
            {

            }
            // RST 6
            else if (hex == "F7")
            {

            }
            // RST 7
            else if (hex == "FF")
            {

            }
            // RZ
            else if (hex == "C8")
            {
                if (f.zero == true)
                {
                    if (sp <= 65533)
                    {
                        string address = "";
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        address += stack[sp].ToString().PadLeft(2, '0');
                        stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                        sp++;
                        pc.counter = Convert.ToInt32(address, 16);
                        stackbox.Invalidate();
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            // SBB A
            else if (hex == "9F")
            {
                Register temp = new Register();
                int data = a.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB B
            else if (hex == "98")
            {
                Register temp = new Register();
                int data = b.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB C
            else if (hex == "99")
            {
                Register temp = new Register();
                int data = c.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB D
            else if (hex == "9A")
            {
                Register temp = new Register();
                int data = d.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB E
            else if (hex == "9B")
            {
                Register temp = new Register();
                int data = e.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB H
            else if (hex == "9C")
            {
                Register temp = new Register();
                int data = h.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB L
            else if (hex == "9D")
            {
                Register temp = new Register();
                int data = l.GetInt() + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBB M
            else if (hex == "9E")
            {
                Register temp = new Register();
                int data = memory[Convert.ToInt32(m, 16)] + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SBI data8
            else if (hex == "DE")
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1] + (f.carry ? 1 : 0);
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
                pc.IncrementBy(1);
            }
            // SHLD address16
            else if (hex == "22")
            {
                int index = memory[pc.counter + 2] + memory[pc.counter + 1];
                memory[index] = Convert.ToByte(l.GetInt());
                memory[index + 1] = Convert.ToByte(h.GetInt());
                memorybox.Items[index].SubItems[1].Text = l.GetHex();
                memorybox.Items[index + 1].SubItems[1].Text = h.GetHex();
                pc.IncrementBy(2);
                memorybox.Invalidate();
            }
            // SIM
            else if (hex == "30")
            {

            }
            // SPHL
            else if (hex == "F9")
            {
                sp--;
                stack[sp] = Convert.ToByte(l.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(h.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
            // STA address16
            else if (hex == "32")
            {
                string addr = "";
                addr += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                addr += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.GetInt());
                memorybox.Items[index].SubItems[1].Text = a.GetHex();
                memorybox.Invalidate();
                pc.IncrementBy(2);
            }
            // STAX B [C]
            else if (hex == "2")
            {
                string addr = "";
                addr += b.GetHex();
                addr += c.GetHex();
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.GetInt());
                memorybox.Items[index].SubItems[1].Text = a.GetHex();
                memorybox.Invalidate();
            }
            // STAX D [E]
            else if (hex == "12")
            {
                string addr = "";
                addr += d.GetHex();
                addr += e.GetHex();
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.GetInt());
                memorybox.Items[index].SubItems[1].Text = a.GetHex();
                memorybox.Invalidate();
            }
            // STC
            else if (hex == "37")
            {
                f.carry = true;
            }
            // SUB A
            else if (hex == "97")
            {
                Register temp = new Register();
                int data = a.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB B
            else if (hex == "90")
            {
                Register temp = new Register();
                int data = b.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB C
            else if (hex == "91")
            {
                Register temp = new Register();
                int data = c.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB D
            else if (hex == "92")
            {
                Register temp = new Register();
                int data = d.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB E
            else if (hex == "93")
            {
                Register temp = new Register();
                int data = e.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB H
            else if (hex == "94")
            {
                Register temp = new Register();
                int data = h.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB L
            else if (hex == "95")
            {
                Register temp = new Register();
                int data = l.GetInt();
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUB M
            else if (hex == "96")
            {
                Register temp = new Register();
                int data = memory[Convert.ToInt32(m, 16)];
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
            }
            // SUI data8
            else if (hex == "D6")
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1];
                temp.SetData(data);
                temp.SetData(temp.Get2SInt());
                data += temp.GetInt();
                if (data > 255)
                    f.carry = false;
                else
                    f.carry = true;
                f.CheckAuxiliary(a.GetBinaryString(), temp.GetBinaryString());
                a.SetData(a.GetInt() + temp.GetInt());
                f.Update(a);
                pc.IncrementBy(1);
            }
            // XCHG
            else if (hex == "EB")
            {
                int temp = d.GetInt();
                d.SetData(h.GetInt());
                h.SetData(temp);
                temp = e.GetInt();
                e.SetData(l.GetInt());
                l.SetData(temp);
            }
            // XRA A
            else if (hex == "AF")
            {
                int data = a.GetInt() ^ a.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA B
            else if (hex == "A8")
            {
                int data = a.GetInt() ^ b.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA C
            else if (hex == "A9")
            {
                int data = a.GetInt() ^ c.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA D
            else if (hex == "AA")
            {
                int data = a.GetInt() ^ d.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA E
            else if (hex == "AB")
            {
                int data = a.GetInt() ^ e.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA H
            else if (hex == "AC")
            {
                int data = a.GetInt() ^ h.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA L
            else if (hex == "AD")
            {
                int data = a.GetInt() ^ l.GetInt();
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRA M
            else if (hex == "AE")
            {
                int data = a.GetInt() ^ memory[Convert.ToInt32(m, 16)];
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
            }
            // XRI data8
            else if (hex == "EE")
            {
                int data = a.GetInt() ^ memory[pc.counter + 1];
                //xra resets carry flag by default
                f.carry = false;
                //xra resets auxiliary flag by default
                f.auxiliary = false;
                //
                a.SetData(data);
                f.Update(a);
                pc.IncrementBy(1);
            }
            // XTHL
            else if (hex == "E3")
            {
                string addh = stack[sp].ToString("X").PadLeft(2, '0');
                sp++;
                string addl = stack[sp].ToString("X").PadLeft(2, '0');
                stack[sp] = Convert.ToByte(l.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(h.GetInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                h.SetData(addh);
                l.SetData(addl);
                stackbox.Invalidate();
            }
        }
        private string Check_error(string[] code)
        {
            for (int i = 0; i < code.Length; i++)
                code[i] = code[i].ToLower();

            string error_string = "";

            if (code[0] == "mov")
            {
                if (code.Length > 2 && !code[1].Contains(";") && !code[2].Contains(";"))
                {
                    if (code[1] == "a" ||
                        code[1] == "b" ||
                        code[1] == "c" ||
                        code[1] == "d" ||
                        code[1] == "e" ||
                        code[1] == "h" ||
                        code[1] == "l" ||
                        code[1] == "m")
                    {
                        if (code[2] == "a" ||
                        code[2] == "b" ||
                        code[2] == "c" ||
                        code[2] == "d" ||
                        code[2] == "e" ||
                        code[2] == "h" ||
                        code[2] == "l" ||
                        code[2] == "m")
                        {
                            if (code[1] == "m" && code[2] == "m")
                                error_string = $"Cannot do self assignment to 'm'";
                        }
                        else
                            error_string = $"Only 'a','b','c','d','e','h','l', or 'm' can be used! Cannot identify \"{code[1]}\"";
                    }
                    else
                        error_string = $"Only 'a','b','c','d','e','h','l', or 'm' can be used! Cannot identify \"{code[1]}\"";
                }
                else
                    error_string = "Need 2 parameters";

            }  //done
            else if (code[0] == "mvi")
            {
                if (code.Length > 2 && !code[1].Contains(";") && !code[2].Contains(";"))
                {
                    if (code[1] == "a" ||
                        code[1] == "b" ||
                        code[1] == "c" ||
                        code[1] == "d" ||
                        code[1] == "e" ||
                        code[1] == "h" ||
                        code[1] == "l" ||
                        code[1] == "m")
                    {
                        if (code[2].StartsWith("-"))
                        {
                            error_string = $"\"{code[2]}\" is not valid value";
                        }
                        else if (code[2].EndsWith("h") || code[2].EndsWith("H"))
                        {
                            code[2] = code[2].Remove(code[2].Length - 1);
                            if (code[2].StartsWith("0"))
                            {
                                code[2] = code[2].TrimStart('0');
                                if (code[2] == "") code[2] += "0";
                            }
                            try
                            {
                                Convert.ToInt32(code[2], 16);
                                if (code[2].Length > 2)
                                {
                                    error_string = $"\"{code[2]}\" is bigger than \"FF\"";
                                }
                            }
                            catch
                            {
                                error_string = $"\"{code[2]}\" is not valid value";
                            }
                        }
                        else
                        {
                            try
                            {
                                if (code[2].StartsWith("0"))
                                {
                                    code[2] = code[2].TrimStart('0');
                                    if (code[2] == "") code[2] += "0";
                                }
                                Int32.Parse(code[2]);
                                if (Int32.Parse(code[2]) > 255)
                                {
                                    error_string = $"\"{code[2]}\" is bigger than \"65535\"";
                                }
                            }
                            catch
                            {
                                error_string = $"\"{code[2]}\" is not valid value";
                            }
                        }

                    }
                    else
                        error_string = $"Only 'a','b','c','d','e','h','l', or 'm' can be used! Cannot identify \"{code[1]}\"";
                }
                else
                    error_string = "Need 2 parameters";


            }  //done
            else if (code[0] == "add" ||
                code[0] == "sub" ||
                code[0] == "adc" ||
                code[0] == "sbb" ||
                code[0] == "inr" ||
                code[0] == "dcr" ||
                code[0] == "ana" ||
                code[0] == "ora" ||
                code[0] == "xra" ||
                code[0] == "cmp")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (
                        code[1] == "a" ||
                        code[1] == "b" ||
                        code[1] == "c" ||
                        code[1] == "d" ||
                        code[1] == "e" ||
                        code[1] == "h" ||
                        code[1] == "l" ||
                        code[1] == "m") { }
                    else
                        error_string = $"Only 'a','b','c','d','e','h','l', or 'm' can be used! Cannot identify \"{code[1]}\"";
                else
                    error_string = "Need 1 parameter";

            } //done

            else if (code[0] == "adi" ||
                code[0] == "aci" ||
                code[0] == "sui" ||
                code[0] == "sbi" ||
                code[0] == "ani" ||
                code[0] == "xri" ||
                code[0] == "ori" ||
                code[0] == "cpi" ||
                code[0] == "in" ||
                code[0] == "out")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (code[1].StartsWith("-"))
                    {
                        error_string = $"\"{code[2]}\" is not valid value";
                    }
                    else if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                    {
                        code[1] = code[1].Remove(code[1].Length - 1);
                        if (code[1].StartsWith("0"))
                        {
                            code[1] = code[1].TrimStart('0');
                            if (code[1] == "") code[1] += "0";
                        }
                        try
                        {
                            if (Convert.ToInt32(code[1], 16) > 255)
                                error_string = $"\"{code[1]}\" is bigger than \"FF\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                    }
                    else
                        try
                        {
                            if (code[1].StartsWith("0"))
                            {
                                code[1] = code[1].TrimStart('0');
                                if (code[1] == "") code[1] += "0";
                            }
                            if (Int32.Parse(code[1]) > 255)
                                error_string = $"\"{code[1]}\" is bigger than \"255\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0] == "stc" ||
                code[0] == "cma" ||
                code[0] == "daa" ||
                code[0] == "cmc" ||
                code[0] == "rlc" ||
                code[0] == "rrc" ||
                code[0] == "ral" ||
                code[0] == "rar" ||
                code[0] == "ret" ||
                code[0] == "rnz" ||
                code[0] == "rz" ||
                code[0] == "rnc" ||
                code[0] == "rc" ||
                code[0] == "rpo" ||
                code[0] == "rpe" ||
                code[0] == "rp" ||
                code[0] == "rm" ||
                code[0] == "pchl" ||
                code[0] == "xthl" ||
                code[0] == "sphl" ||
                code[0] == "ei" ||
                code[0] == "di" ||
                code[0] == "rim" ||
                code[0] == "sim" ||
                code[0] == "nop" ||
                code[0] == "hlt" ||
                code[0] == "xchg") { } //done

            else if (code[0] == "lxi")
            {
                if (code.Length > 2 && !code[1].Contains(";") && !code[2].Contains(";"))
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "sp")
                        if (code[2].StartsWith("-"))
                        {
                            error_string = $"\"{code[2]}\" is not valid value";
                        }
                        else if (code[2].EndsWith("h") || code[2].EndsWith("H"))
                        {
                            code[2] = code[2].Remove(code[2].Length - 1);
                            if (code[2].StartsWith("0"))
                            {
                                code[2] = code[2].TrimStart('0');
                                if (code[2] == "") code[2] += "0";
                            }
                            try
                            {
                                if (Convert.ToInt32(code[2], 16) > 65535)
                                {
                                    error_string = $"\"{code[2]}\" is bigger than \"FFFF\"";
                                }
                            }
                            catch
                            {
                                error_string = $"\"{code[2]}\" is not valid value";
                            }
                        }
                        else
                            try
                            {
                                if (code[2].StartsWith("0"))
                                {
                                    code[2] = code[2].TrimStart('0');
                                    if (code[2] == "") code[2] += "0";
                                }
                                if (Int32.Parse(code[2]) > 65535)
                                {
                                    error_string = $"\"{code[2]}\" is bigger than \"65535\"";
                                }
                            }
                            catch
                            {
                                error_string = $"\"{code[2]}\" is not valid value";
                            }
                    else
                        error_string = $"Only 'b','d','h', or \"sp\" can be used! Cannot identify \"{code[1]}\"";
                else
                    error_string = "Need 2 parameters";
            } //done

            else if (code[0] == "ldax" ||
                code[0] == "stax")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (code[1] == "b" ||
                        code[1] == "d") { }
                    else
                        error_string = $"Only 'b' or 'd' can be used! Cannot identify \"{code[1]}\"";
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0] == "lda" ||
                code[0] == "sta" ||
                code[0] == "lhld" ||
                code[0] == "shld")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (code[1].StartsWith("-"))
                    {
                        error_string = $"\"{code[1]}\" is not valid value";
                    }
                    else if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                        try
                        {
                            code[1] = code[1].Remove(code[1].Length - 1);
                            if (code[1].StartsWith("0"))
                            {
                                code[1] = code[1].TrimStart('0');
                                if (code[1] == "") code[1] += "0";
                            }
                            if (Convert.ToInt32(code[1], 16) > 65334)
                                error_string = $"\"{code[1]}\" is bigger than \"FFFE\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                    else
                        try
                        {
                            if (code[1].StartsWith("0"))
                            {
                                code[1] = code[1].TrimStart('0');
                                if (code[1] == "") code[1] += "0";
                            }
                            if (Int32.Parse(code[1]) > 65334)
                                error_string = $"\"{code[1]}\" is bigger than \"65534\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0] == "dad" ||
                code[0] == "inx" ||
                code[0] == "dcx")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "sp") { }
                    else
                        error_string = $"Only 'b','d','h', or \"sp\" can be used! Cannot identify \"{code[1]}\"";
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0] == "jmp" ||
                code[0] == "jnz" ||
                code[0] == "jz" ||
                code[0] == "jnc" ||
                code[0] == "jc" ||
                code[0] == "jpo" ||
                code[0] == "jpe" ||
                code[0] == "jp" ||
                code[0] == "jm" ||
                code[0] == "call" ||
                code[0] == "cnz" ||
                code[0] == "cz" ||
                code[0] == "cnc" ||
                code[0] == "cc" ||
                code[0] == "cpo" ||
                code[0] == "cpe" ||
                code[0] == "cp" ||
                code[0] == "cm")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                {
                    if (IsLabel(code[1])) { }
                    else if (code[1].StartsWith("-"))
                    {
                        error_string = $"\"{code[1]}\" is not valid value";
                    }
                    else if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                        try
                        {
                            code[1] = code[1].Remove(code[1].Length - 1);
                            if (code[1].StartsWith("0"))
                            {
                                code[1] = code[1].TrimStart('0');
                                if (code[1] == "") code[1] += "0";
                            }
                            if (Convert.ToInt32(code[1], 16) > 65534)
                                error_string = $"\"{code[1]}\" is bigger than \"FFFE\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                    else
                        try
                        {
                            if (code[1].StartsWith("0"))
                            {
                                code[1] = code[1].TrimStart('0');
                                if (code[1] == "") code[1] += "0";
                            }
                            if (Int32.Parse(code[1]) > 65534)
                                error_string = $"\"{code[1]}\" is bigger than \"65534\"";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                }
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0] == "rst")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    try
                    {
                        if (code[1].StartsWith("0"))
                            code[1] = code[1].TrimStart('0');
                        if (Convert.ToInt32(code[1], 16) > 7 || Convert.ToInt32(code[1], 16) < 1)
                            error_string = $"\"{code[1]}\" is not between '1' to '7'";
                    }
                    catch
                    {
                        error_string = $"\"{code[1]}\" is not valid value";
                    }
                else
                    error_string = "Need 1 parameter";
            }

            else if (code[0] == "push" ||
                code[0] == "pop")
            {
                if (code.Length > 1 && !code[1].Contains(";"))
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "psw") { }
                    else
                        error_string = $"Only 'b','d','h', or \"psw\" can be used! Cannot identify \"{code[1]}\"";
                else
                    error_string = "Need 1 parameter";
            } //done

            else if (code[0].StartsWith(";")) { } //done
            else
                error_string = $"\"{code[0]}\" is incompatible instruction";

            return error_string;
        }
        private void Window_Closing(object sender, FormClosingEventArgs e)
        {
            if (isFileSaved == false)
            {
                DialogResult dm = MessageBox.Show("Do you wanna save this file?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dm == DialogResult.Yes)
                    SaveFile(this, null);
                else if (dm == DialogResult.No) { }
                else if (dm == DialogResult.Cancel)
                    e.Cancel = true;
            }
            Properties.Settings.Default.WindowState = WindowState;
            Properties.Settings.Default.Bounds = Bounds;
            Properties.Settings.Default.LastFile = filePath;
            Properties.Settings.Default.Save();
        }
        private void Window_Loading(object sender, EventArgs e)
        {
            //init the arrays and variables
            labels = new List<LabelAddress>();
            errors = new List<string>();
            memory = new List<ushort>(new ushort[65535]);
            stack = new List<ushort>(new ushort[65535]);
            port = new List<ushort>(new ushort[255]);
            pc = new ProgramCounter();


            //reset to load variables
            Reset_memory();
            Reset_stack();
            Reset_port();
            Update_variables();

            
            //setting window position and state from last run
            WindowState = Properties.Settings.Default.WindowState;
            if (WindowState != FormWindowState.Maximized)
                Bounds = Properties.Settings.Default.Bounds;

            //setting up code editor
            codeEditor.Lexer = ScintillaNET.Lexer.Asm;
            codeEditor.Styles[Style.Default].Font = "Consolas";
            codeEditor.Styles[Style.Default].Size = 14;
            codeEditor.Styles[Style.Asm.Comment].ForeColor = Color.Green;
            codeEditor.Styles[Style.Asm.CpuInstruction].ForeColor = Color.DarkOrange;
            codeEditor.Styles[Style.Asm.Number].ForeColor = Color.Blue;
            codeEditor.Styles[Style.Asm.Register].ForeColor = Color.MediumVioletRed;
            codeEditor.Styles[Style.Asm.Operator].ForeColor = Color.Purple;
            codeEditor.Styles[Style.Asm.DirectiveOperand].ForeColor = Color.DarkSeaGreen;
            codeEditor.SetKeywords(0, "mov mvi lxi ldax stax lda sta lhld shld xchg add adc sub sbb adi aci sui sbi dad inr dcr inx dcx daa cma cmc stc ana ora xra cmp rlc rrc ral rar ani xri ori cpi jmp jnz jz jnc jc jpo jpe jp jm call cnz cz cnc cc cpo cpe cp cm ret rnz rz rnc rc rpo rpe rp rm pchl in out push pop xthl sphl ei di rim sim nop hlt rst");
            codeEditor.SetKeywords(2, "a b c d e h l m psw sp");
            codeEditor.Margins[0].Width = 60;
            codeEditor.Margins[1].Width = 10;
            codeEditor.Markers[1].Symbol = MarkerSymbol.Background;
            codeEditor.Markers[1].SetBackColor(Color.LightGray);


            //setting up autolist
            auto_list.TargetControlWrapper = new ScintillaWrapper(codeEditor);
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("mov",0,"mov","Help", "Copies data from destination to source!\n\nMOV R1, R2\n\nR1 = Destination Register\nR2 = Source Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("mvi", 0, "mvi", "Help", "Copies 8-bit value to destination!\n\nMVI R, Data\n\nR = Destination Register\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("lxi", 0, "lxi", "Help", "Copies 16-bit value to destination!\n\nLXI RP, Data\n\nRP = Destination Register Pair\nData = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ldax", 0, "ldax", "Help", "Copies value to accumulator from address pointed by register pair!\n\nLDAX RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("stax", 0, "stax", "Help", "Copies value to address from accumulator pointed by register pair!\n\nSTAX RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("lda", 0, "lda", "Help", "Copies value to accumulator from 16-bit address value!\n\nLDA Address\n\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sta", 0, "sta", "Help", "Copies value to 16-bit address value from accumulator!\n\nSTA Address\n\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("lhld", 0, "lhld", "Help", "Copies value to HL register pair from 16-bit address value!\n\nLHLD Address\n\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("shld", 0, "shld", "Help", "Copies value to 16-bit address from HL register pair!\n\nSHLD Address\n\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("xchg", 0, "xchg", "Help", "Exchange 16-bit value between DE and HL register pair\n\nXCHG"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("add", 0, "add", "Help", "Add register to accumulator!\n\nADD R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("adc", 0, "adc", "Help", "Add register to accumulator with carry!\n\nADC R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sub", 0, "sub", "Help", "Subtract register from accumulator!\n\nSUB R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sbb", 0, "sbb", "Help", "Subtract register from accumulator with borrow!\n\nSBB R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("adi", 0, "adi", "Help", "Add 8-bit value to accumulator!\n\nADI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("aci", 0, "aci", "Help", "Add 8-bit value to accumulator with carry!\n\nACI Data\n\nData = 8-bit valule"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sui", 0, "sui", "Help", "Subtract 8-bit value from accumulator!\n\nSUI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sbi", 0, "sbi", "Help", "Subtract 8-bit value from accumulator with borrow!\n\nSBI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("dad", 0, "dad", "Help", "Add register pair to HL register pair!\n\nDAD RP\n\nRP = Source Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("inr", 0, "inr", "Help", "Increment register!\n\nINR R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("dcr", 0, "dcr", "Help", "Decrement register!\n\nDCR R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("inx", 0, "inx", "Help", "Increment register pair!\n\nINX RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("dcx", 0, "dcx", "Help", "Decrement register pair!\n\nDCX RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("daa", 0, "daa", "Help", "Convert accumulator value to BCD!\n\nDAA\n\nBCD = Binary Coded Decimal"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cma", 0, "cma", "Help", "Complement value of accumulator!\n\nCMA"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cmc", 0, "cmc", "Help", "Complement value of carry flag!\n\nCMC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("stc", 0, "stc", "Help", "Set value of carry flag!\n\nSTC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ana", 0, "ana", "Help", "AND operation between accumulator and register!\n\nANA R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ora", 0, "ora", "Help", "OR operation between accumulator and register!\n\nORA R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("xra", 0, "xra", "Help", "XOR operation between accumulator and register!\n\nXRA R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cmp", 0, "cmp", "Help", "Compare accumulator and register!\n\nCMP R\n\nR = Destination Register"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rlc", 0, "rlc", "Help", "Rotate accumulator bits to left with carry flag!\n\nRLC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rrc", 0, "rrc", "Help", "Rotate accumulator bits to right with carry flag!\n\nRRC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ral", 0, "ral", "Help", "Rotate accumulator bits to left through carry flag!\n\nRAL"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rar", 0, "rar", "Help", "Rotate accumulator bits to right through carry flag!\n\nRAR"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ani", 0, "ani", "Help", "AND operation between accumulator and 8-bit value!\n\nANI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ori", 0, "ori", "Help", "OR operation between accumulator and 8-bit value!\n\nORI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("xri", 0, "xri", "Help", "XOR operation between accumulator and 8-bit value!\n\nXRI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cpi", 0, "cpi", "Help", "Compare accumulator and 8-bit value!\n\nCPI Data\n\nData = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jmp", 0, "jmp", "Help", "Jump to label or address!\n\nJMP Lable/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jnz", 0, "jnz", "Help", "Jump to label or address if zero flag is reset!\n\nJNZ Lable/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jz", 0, "jz", "Help", "Jump to label or address if zero flag is set!\n\nJZ Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jnc", 0, "jnc", "Help", "Jump to label or address if carry flag is reset!\n\nJNC Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jc", 0, "jc", "Help", "Jump to label or address if carry flag is set!\n\nJC Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jpo", 0, "jpo", "Help", "Jump to label or address if parity flag is reset!\n\nJPO Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jpe", 0, "jpe", "Help", "Jump to label or address if parity flag is set!\n\nJPE Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jp", 0, "jp", "Help", "Jump to label or address if accumulator value is positive!\n\nJP Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("jm", 0, "jm", "Help", "Jump to label or address if accumulator value is negative!\n\nJM Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("call", 0, "call", "Help", "Goto subroutine to label or address!\n\nCALL Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cnz", 0, "cnz", "Help", "Goto subroutine to label or address if zero flag is reset!\n\nCNZ Label/Addreess\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cz", 0, "cz", "Help", "Goto subroutine to label or address if zero flag is set!\n\nCZ Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cc", 0, "cc", "Help", "Goto subroutine to label or address if carry flag is set!\n\nCC Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cnc", 0, "cnc", "Help", "Goto subroutine to label or address if carry flag is reset!\n\nCNC Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cpo", 0, "cpo", "Help", "Goto subroutine to label or address if parity flag is reset!\n\nCPO Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cpe", 0, "cpe", "Help", "Goto subroutine to label or address if parity flag is set!\n\nCPE Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cp", 0, "cp", "Help", "Goto subroutine to label or address if accumulator value is positive!\n\nCP Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("cm", 0, "cm", "Help", "Goto subroutine to label or address if accumulator value is negative!\n\nCM Label/Address\n\nLabel = Name specified in code\nAddress = 16-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ret", 0, "ret", "Help", "Return to address from stack!\n\nRET"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rnz", 0, "rnz", "Help", "Return to address from stack if zero flag is reset!\n\nRNZ"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rz", 0, "rz", "Help", "Return to address from stack if zero flag is set!\n\nRZ"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rnc", 0, "rnc", "Help", "Return to address from stack if carry falg is reset!\n\nRNC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rc", 0, "rc", "Help", "Retrun to address from stack if carry flag is set!\n\nRC"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rpo", 0, "rpo", "Help", "Return to address from stack if parity flag is reset!\n\nRPO"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rpe", 0, "rpe", "Help", "Return to address from stack if parity flag is set!\n\nRPE"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rp", 0, "rp", "Help", "Return to address from stack if accumulator value is positive!\n\nRP"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rm", 0, "rm", "Help", "Return to address from stack if accumulator value is negative!\n\nRM"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("pchl", 0, "pchl", "Help", "Replace PC with HL register pair!\n\nPCHL\n\nPC = Program Counter"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("in", 0, "in", "Help", "Copies 8-bit value from specified port address!\n\nIN Address\n\nAddress = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("out", 0, "out", "Help", "Copies 8-bit value from accumulator to port address!\n\nOUT Address\n\nAddress = 8-bit value"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("push", 0, "push", "Help", "Insert register pair to stack!\n\nPUSH RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("pop", 0, "pop", "Help", "Store value to register pair and delete from stack!\n\nPOP RP\n\nRP = Destination Register Pair"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("xthl", 0, "xthl", "Help", "Exchange top value of stack with HL register pair!\n\nXTHL"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sphl", 0, "sphl", "Help", "Insert HL register pair to stack!\n\nSPHL"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("ei", 0, "ei", "Help", "Enable interrupt!\n\nEI"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("di", 0, "di", "Help", "Disbale interrupt\n\n\nDI"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rim", 0, "rim", "Help", "Copies interrupt value to accumulator!\n\nRIM"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("sim", 0, "sim", "Help", "Copies interrupt value from accumulator!\n\nSIM"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("nop", 0, "nop", "Help", "Ignore and continue!\n\nNOP"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("hlt", 0, "hlt", "Help", "Stops processor execution!\n\nHLT"));
            auto_list.AddItem(new AutocompleteMenuNS.AutocompleteItem("rst", 0, "rst", "Help", "Set restart values!\n\nRST Data\n\nData = 1 to 7 value"));

            //loading recent file if opened last
            if (Properties.Settings.Default.LastFile != "")
                filePath = Properties.Settings.Default.LastFile;
            if (File.Exists(filePath))
            {
                using (FileStream file = File.OpenRead(filePath))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        codeEditor.Text = reader.ReadToEnd();
                        isFileSaved = true;
                        Text = "8085 Simulator - " + Path.GetFileName(filePath);
                        reader.Close();
                    }
                    file.Close();
                }
            }
            else
                filePath = "";

            //getting focus on editor
            codeEditor.Select();
        }

        //Menu section

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                codeEditor.Styles[Style.Asm.Default].Font = Font.Name;
                codeEditor.Styles[Style.Asm.Default].SizeF = Font.Size;
                codeEditor.Styles[Style.Asm.Default].Bold = Font.Bold;
                codeEditor.Styles[Style.Asm.Default].Italic = Font.Italic;
                codeEditor.Styles[Style.Asm.Default].Underline = Font.Underline;
            }
        }
        private void ClearMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset_memory();
        }
        private void ClearRegistersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_registers();
        }
        private void ClearEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditor.ClearAll();
        }
        private void CheckErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code_inspect(false);
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("8085 Simulator - ALPHA\nDeveloped by Vishal Solanki\n\nThis is Project for my 5th semester, I poured all my dedication to this project, hope it helps!\n\nE-Mail : vcsolanki.vs@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //key events

        private void Show_conv_tooltip(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            reg_name.Text = temp.Tag.ToString();
            int data = Convert.ToInt32(temp.Text, 16);
            conv_lbl.Text = $"BIN : {Convert.ToString(data, 2).PadLeft(8, '0')}\nHEX : {temp.Text}\nDEC : {data}";
        }
        private void Warning_click(object sender, EventArgs e)
        {
            MessageBox.Show("Software is in ALPHA state!\nCan't guarantee software will work in every situation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Run_program(object sender, EventArgs e)
        {
            orignal_code = codeEditor.Text;
            blinker.Stop();
            status_lbl.BackColor = Color.WhiteSmoke;
            codeEditor.Enabled = false;
            stop_button.Enabled = true;
            play_button.Enabled = false;
            step_button.Enabled = false;
            runToolStripMenuItem1.Enabled = false;
            stepNextToolStripMenuItem.Enabled = false;
            checkErrorsToolStripMenuItem.Enabled = false;
            clearEditorToolStripMenuItem.Enabled = false;
            clearMemoryToolStripMenuItem.Enabled = false;
            clearRegistersToolStripMenuItem.Enabled = false;
            newASMFileToolStripMenuItem.Enabled = false;
            openASMToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            stop_run = false;
            status_lbl.Text = "Program Running, You can't edit code right now!";
            status_lbl.ForeColor = Color.DarkGreen;
            Code_inspect(true);
        }
        private void Stop_program(object sender, EventArgs e)
        {
            play_button.Enabled = true;
            stop_button.Enabled = false;
            blinker.Stop();
            selected_index = 0;
            stop_run = true;
            runToolStripMenuItem1.Enabled = true;
            stepNextToolStripMenuItem.Enabled = true;
            checkErrorsToolStripMenuItem.Enabled = true;
            clearEditorToolStripMenuItem.Enabled = true;
            clearMemoryToolStripMenuItem.Enabled = true;
            clearRegistersToolStripMenuItem.Enabled = true;
            newASMFileToolStripMenuItem.Enabled = true;
            openASMToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            codeEditor.Text = orignal_code;
            codeEditor.Enabled = true;
            status_lbl.Text = "Program Stopped";
            status_lbl.ForeColor = Color.Red;
            formatted_code = "";
        }
        private void Format_code(ref string str)
        {
            int start_location = 0;
            foreach (Line line in codeEditor.Lines)
            {
                string lineF = Regex.Replace(line.Text, @",+", " ");
                lineF = Regex.Replace(lineF, @"\s+", " ");
                lineF = lineF.Trim(' ');
                if (lineF == "")
                    continue;
                string[] code = lineF.ToLower().Split(' ');
                if (code[0].EndsWith(":"))
                {
                    code[0] = code[0].Remove(code[0].Length - 1);
                    labels.Add(new LabelAddress(code[0], start_location));
                    for (int i = 1; i < code.Length; i++)
                        code[i - 1] = code[i];
                    code[code.Length - 1] = "";
                    lineF = "";
                    foreach (string word in code)
                    {
                        if (word != "")
                            lineF += word + ' ';
                    }
                    str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF.Split(';')[0]}\n";
                    start_location += 1;
                }
                else
                {
                    if (code[0] == "mov" ||
                        code[0] == "add" ||
                        code[0] == "sub" ||
                        code[0] == "adc" ||
                        code[0] == "sbb" ||
                        code[0] == "inr" ||
                        code[0] == "dcr" ||
                        code[0] == "ana" ||
                        code[0] == "ora" ||
                        code[0] == "xra" ||
                        code[0] == "cmp" ||
                        code[0] == "stc" ||
                        code[0] == "cma" ||
                        code[0] == "daa" ||
                        code[0] == "cmc" ||
                        code[0] == "rlc" ||
                        code[0] == "rrc" ||
                        code[0] == "ral" ||
                        code[0] == "rar" ||
                        code[0] == "ret" ||
                        code[0] == "rnz" ||
                        code[0] == "rz" ||
                        code[0] == "rnc" ||
                        code[0] == "rc" ||
                        code[0] == "rpo" ||
                        code[0] == "rpe" ||
                        code[0] == "rp" ||
                        code[0] == "rm" ||
                        code[0] == "pchl" ||
                        code[0] == "xthl" ||
                        code[0] == "sphl" ||
                        code[0] == "ei" ||
                        code[0] == "di" ||
                        code[0] == "rim" ||
                        code[0] == "sim" ||
                        code[0] == "nop" ||
                        code[0] == "hlt" ||
                        code[0] == "xchg" ||
                        code[0] == "ldax" ||
                        code[0] == "stax" ||
                        code[0] == "dad" ||
                        code[0] == "inx" ||
                        code[0] == "dcx" ||
                        code[0] == "rst" ||
                        code[0] == "push" ||
                        code[0] == "pop")
                    {
                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF.Split(';')[0]}\n";
                        start_location += 1;
                    }

                    else if (code[0] == "mvi" ||
                        code[0] == "adi" ||
                        code[0] == "aci" ||
                        code[0] == "sui" ||
                        code[0] == "sbi" ||
                        code[0] == "ani" ||
                        code[0] == "xri" ||
                        code[0] == "ori" ||
                        code[0] == "cpi" ||
                        code[0] == "in" ||
                        code[0] == "out"
                        )
                    {
                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF.Split(';')[0]}\n";
                        start_location += 2;
                    }

                    else if (code[0] == "jmp" ||
                        code[0] == "jnz" ||
                        code[0] == "jz" ||
                        code[0] == "jnc" ||
                        code[0] == "jc" ||
                        code[0] == "jpo" ||
                        code[0] == "jpe" ||
                        code[0] == "jp" ||
                        code[0] == "jm" ||
                        code[0] == "call" ||
                        code[0] == "cnz" ||
                        code[0] == "cz" ||
                        code[0] == "cnc" ||
                        code[0] == "lda" ||
                        code[0] == "sta" ||
                        code[0] == "cc" ||
                        code[0] == "cpo" ||
                        code[0] == "cpe" ||
                        code[0] == "cp" ||
                        code[0] == "cm" ||
                        code[0] == "lxi" ||
                        code[0] == "lhld" ||
                        code[0] == "shld")
                    {
                        if (IsLabel(code[1]))
                        {
                            foreach (LabelAddress la in labels)
                                if (code[1] == la.name)
                                    code[1] = la.address.ToString("X").PadLeft(4, '0');
                            lineF = "";
                            foreach (string word in code)
                                lineF += word + " ";
                        }

                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF.Split(';')[0]}\n";
                        start_location += 3;
                    }
                }
            }
        }
        private void Step_program(object sender, EventArgs e)
        {
            if (stop_run == true && Code_inspect(false))
            {
                pc.counter = Load_into_memory();
                orignal_code = codeEditor.Text;
                Format_code(ref formatted_code);
                codeEditor.Text = formatted_code;
                blinker.Stop();
                play_button.Enabled = false;
                stop_button.Enabled = true;
                runToolStripMenuItem1.Enabled = false;
                stepNextToolStripMenuItem.Enabled = true;
                checkErrorsToolStripMenuItem.Enabled = false;
                clearEditorToolStripMenuItem.Enabled = false;
                clearMemoryToolStripMenuItem.Enabled = false;
                clearRegistersToolStripMenuItem.Enabled = false;
                newASMFileToolStripMenuItem.Enabled = false;
                openASMToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                status_lbl.Text = "Program Stepping, You can't edit code right now!";
                status_lbl.ForeColor = Color.DarkGreen;
                stop_run = false;
                codeEditor.Enabled = false;
            }
            else
            {
                if (stop_run != true)
                {
                    if (memory[pc.counter].ToString("X") != "76")
                    {
                        Code_execute(memory[pc.counter].ToString("X"));
                        pc.Increment();
                        Update_variables();
                        if (sp == 0)
                            sp += 1;
                        for (int i = 0; i < codeEditor.Lines.Count; i++)
                            codeEditor.Lines[i].MarkerDelete(1);
                        codeEditor.Lines[selected_index].MarkerAdd(1);
                        int ln = 0;
                        foreach (Line line in codeEditor.Lines)
                        {
                            if (line.Text.Split(' ')[0] == pc.counter.ToString("X").PadLeft(4, '0'))
                            {
                                selected_index = ln;
                                break;
                            }
                            ln++;
                        }
                        stackbox.TopItem = stackbox.Items[sp - 1];
                        memorybox.Invalidate();
                    }
                    else
                    {
                        output_box.Items.Add("Program successfully terminated!");
                        play_button.Enabled = true;
                        stop_button.Enabled = false;
                        blinker.Stop();
                        selected_index = 0;
                        stop_run = true;
                        runToolStripMenuItem1.Enabled = true;
                        stepNextToolStripMenuItem.Enabled = true;
                        checkErrorsToolStripMenuItem.Enabled = true;
                        clearEditorToolStripMenuItem.Enabled = true;
                        clearMemoryToolStripMenuItem.Enabled = true;
                        clearRegistersToolStripMenuItem.Enabled = true;
                        newASMFileToolStripMenuItem.Enabled = true;
                        openASMToolStripMenuItem.Enabled = true;
                        saveAsToolStripMenuItem.Enabled = true;
                        saveToolStripMenuItem.Enabled = true;
                        stopToolStripMenuItem.Enabled = false;
                        status_lbl.Text = "Program Stopped";
                        status_lbl.ForeColor = Color.Red;
                        codeEditor.Text = orignal_code;
                        codeEditor.Enabled = true;
                        formatted_code = "";
                    }
                }
            }
        }
        private void Code_editor_keypress(object sender, KeyPressEventArgs e)
        {
                isFileSaved = false;
                Text = "8085 Simulator - " + Path.GetFileName(filePath) + "*";
        }


        private void Find_text_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                memorybox.TopItem = memorybox.FindItemWithText(address_to_find.Text.ToUpper());
                e.Handled = true;
            }
        }
        private void Find_address_Click(object sender, EventArgs e)
        {
            memorybox.TopItem = memorybox.FindItemWithText(address_to_find.Text.ToUpper());
        }
        private void Memory_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (memory_items != null && e.ItemIndex >= m_first && e.ItemIndex < m_first + memory_items.Length)
                e.Item = memory_items[e.ItemIndex - m_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void Memory_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (memory_items != null && e.StartIndex >= m_first && e.EndIndex <= m_first + memory_items.Length)
                return;
            m_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            memory_items = new ListViewItem[length];
            for (int i = 0; i < length; i++)
            {
                int x = (i + m_first) * (i + m_first);
                memory_items[i] = new ListViewItem(x.ToString());
            }
        }
        private void Stack_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (stack_items != null && e.ItemIndex >= s_first && e.ItemIndex < s_first + stack_items.Length)
                e.Item = stack_items[e.ItemIndex - s_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void Stack_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (stack_items != null && e.StartIndex >= s_first && e.EndIndex <= s_first + stack_items.Length)
                return;
            s_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            stack_items = new ListViewItem[length];
            for (int i = 0; i < length; i++)
            {
                int x = (i + s_first) * (i + s_first);
                stack_items[i] = new ListViewItem(x.ToString());
            }
        }
        private void Port_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (port_items != null && e.ItemIndex >= p_first && e.ItemIndex < p_first + port_items.Length)
                e.Item = port_items[e.ItemIndex - p_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void Port_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (port_items != null && e.StartIndex >= p_first && e.EndIndex <= p_first + port_items.Length)
                return;
            p_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            port_items = new ListViewItem[length];
            for (int i = 0; i < length; i++)
            {
                int x = (i + p_first) * (i + p_first);
                port_items[i] = new ListViewItem(x.ToString());
            }
        }
        private void Memory_search_item(object sender, SearchForVirtualItemEventArgs e)
        {
            int x = 0;
            foreach (ListViewItem it in m_items)
            {
                if (it.Text.Contains(e.Text))
                {
                    e.Index = x;
                    return;
                }
                x++;
            }
        }
        private void Memory_double_click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection lic = memorybox.SelectedIndices;
            value_editbox address_Editbox = new value_editbox(memorybox.Items[lic[0]].SubItems[1].Text)
            {
                Text = memorybox.Items[lic[0]].Text
            };
            if (address_Editbox.ShowDialog() == DialogResult.OK)
            {
                memory[Convert.ToInt32(memorybox.Items[lic[0]].Text, 16)] = Convert.ToByte(address_Editbox.int_value);
                memorybox.Items[lic[0]].SubItems[1].Text = memory[Convert.ToInt32(memorybox.Items[lic[0]].Text, 16)].ToString("X").PadLeft(2, '0');
                memorybox.Invalidate();
            }
        }
        private void Create_NewFile(object sender, EventArgs e)
        {
            if(isFileSaved==false)
            {
                DialogResult dm = MessageBox.Show("Do you wanna save this file?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dm == DialogResult.Yes)
                    SaveFile(this, null);
                else if (dm == DialogResult.No) { }

                else if (dm == DialogResult.Cancel)
                    return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                CheckPathExists = true,
                SupportMultiDottedExtensions = true,
                AddExtension = true,
                Title = "Create New ASM File...",
                Filter = "ASM File (*.asm)|*.asm|All Files (*.*)|*.*",
                InitialDirectory = Properties.Settings.Default.RecentPath
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                isFileSaved = true;
                FileStream file = File.Create(filePath);
                file.Close();
                Properties.Settings.Default.RecentPath = Path.GetDirectoryName(filePath);
                Text = "8085 Simulator - " + Path.GetFileName(filePath);
            }
            Properties.Settings.Default.Save();
            codeEditor.Select();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            if (isFileSaved == false)
            {
                DialogResult dm = MessageBox.Show("Do you wanna save this file?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dm == DialogResult.Yes)
                    SaveFile(this, null);
                else if (dm == DialogResult.No) { }

                else if (dm == DialogResult.Cancel)
                    return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                CheckPathExists = true,
                CheckFileExists = true,
                Title = "Open ASM File...",
                Filter = "ASM File (*.asm)|*.asm|All Files (*.*)|*.*",
                InitialDirectory = Properties.Settings.Default.RecentPath
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                isFileSaved = true;
                using (FileStream file = File.Open(@filePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        codeEditor.Text = reader.ReadToEnd();
                        reader.Close();
                    }
                    file.Close();
                }
                Properties.Settings.Default.RecentPath = Path.GetDirectoryName(filePath);
                Text = "8085 Simulator - " + Path.GetFileName(filePath);
            }
            Properties.Settings.Default.Save();
            codeEditor.Select();
        }
        private void SaveFile(object sender, EventArgs e)
        {
            if (!isFileSaved)
            {
                if (filePath == "")
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        CheckPathExists = true,
                        SupportMultiDottedExtensions = true,
                        AddExtension = true,
                        Title = "Save ASM File As...",
                        Filter = "ASM File (*.asm)|*.asm|All Files (*.*)|*.*",
                        InitialDirectory = Properties.Settings.Default.RecentPath
                    };
                    if (sfd.ShowDialog()==DialogResult.OK)
                    filePath = sfd.FileName;
                }
                if (filePath != "")
                {
                    isFileSaved = true;
                    using (FileStream file = File.Create(filePath))
                    {
                        using (StreamWriter write = new StreamWriter(file))
                        {
                        write.Write(codeEditor.Text);
                        write.Close();
                        }
                        file.Close();
                    }
                    Properties.Settings.Default.RecentPath = Path.GetDirectoryName(filePath);
                    Text = "8085 Simulator - " + Path.GetFileName(filePath);
                }
            }
            Properties.Settings.Default.Save();
            codeEditor.Select();
        }
        private void SaveFileAs(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                CheckPathExists = true,
                SupportMultiDottedExtensions = true,
                AddExtension = true,
                Title = "Save ASM File As...",
                Filter = "ASM File (*.asm)|*.asm|All Files (*.*)|*.*",
                InitialDirectory = Properties.Settings.Default.RecentPath
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                isFileSaved = true;
                using (FileStream file = File.Create(filePath))
                {
                    using (StreamWriter write = new StreamWriter(file))
                    {
                        write.Write(codeEditor.Text);
                        write.Close();
                    }
                    file.Close();
                }
                Properties.Settings.Default.RecentPath = Path.GetDirectoryName(filePath);
                Text = "8085 Simulator - " + Path.GetFileName(filePath);
            }
            codeEditor.Select();
        }
        private void Output_box_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string slt = output_box.SelectedItem.ToString();
                string[] words = slt.Split(' ');
                int line = Convert.ToInt32(words.Last<string>());
                codeEditor.Lines[line - 1].MarkerAdd(1);
            }
            catch { }
        }
        private void CodeEditor_click(object sender, EventArgs e)
        {
            if(stop_run==true)
            {
                codeEditor.MarkerDeleteAll(1);
            }
        }
        private void Stack_double_click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection lic = stackbox.SelectedIndices;
            value_editbox address_Editbox = new value_editbox(stackbox.Items[lic[0]].SubItems[1].Text)
            {
                Text = stackbox.Items[lic[0]].Text
            };
            if (address_Editbox.ShowDialog() == DialogResult.OK)
            {
                stack[Convert.ToInt32(stackbox.Items[lic[0]].Text, 16)] = Convert.ToByte(address_Editbox.int_value);
                stackbox.Items[lic[0]].SubItems[1].Text = stack[Convert.ToInt32(stackbox.Items[lic[0]].Text, 16)].ToString("X").PadLeft(2, '0');
                stackbox.Invalidate();
            }
        }
        private void Port_double_click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection lic = portbox.SelectedIndices;
            value_editbox address_Editbox = new value_editbox(portbox.Items[lic[0]].SubItems[1].Text)
            {
                Text = portbox.Items[lic[0]].Text
            };
            if (address_Editbox.ShowDialog() == DialogResult.OK)
            {
                port[Convert.ToInt32(portbox.Items[lic[0]].Text, 16)] = Convert.ToByte(address_Editbox.int_value);
                portbox.Items[lic[0]].SubItems[1].Text = port[Convert.ToInt32(portbox.Items[lic[0]].Text, 16)].ToString("X").PadLeft(2, '0');
                portbox.Invalidate();
            }
        }
        private void DoubleClickFlagSet(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            if (temp.Tag.ToString() == "cf")
                f.carry = !f.carry;
            else if (temp.Tag.ToString() == "sf")
                f.sign = !f.sign;
            else if (temp.Tag.ToString() == "pf")
                f.parity = !f.parity;
            else if (temp.Tag.ToString() == "af")
                f.auxiliary = !f.auxiliary;
            else if (temp.Tag.ToString() == "zf")
                f.zero = !f.zero;
            Update_variables();
            
        }
        private void DoubleClickRSet(object sender, EventArgs e)
        {
            value_editbox address_Editbox;
            Label temp = (Label)sender;
            if (temp.Tag.ToString() == "A Register")
            {
                address_Editbox = new value_editbox(a.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    a.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "B Register")
            {
                address_Editbox = new value_editbox(b.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    b.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "C Register")
            {
                address_Editbox = new value_editbox(c.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    c.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "D Register")
            {
                address_Editbox = new value_editbox(d.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    d.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "E Register")
            {
                address_Editbox = new value_editbox(this.e.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    this.e.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "H Register")
            {
                address_Editbox = new value_editbox(h.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    h.SetData(address_Editbox.int_value);
            }
            else if (temp.Tag.ToString() == "L Register")
            {
                address_Editbox = new value_editbox(l.GetHex());
                address_Editbox.Text = temp.Tag.ToString();
                if (address_Editbox.ShowDialog() == DialogResult.OK)
                    l.SetData(address_Editbox.int_value);
            }
            Update_variables();
        }
        private void Blink(object sender, EventArgs e)
        {
            if (status_lbl.BackColor == Color.White)
            {
                status_lbl.BackColor = Color.Red;
                status_lbl.ForeColor = Color.White;
            }
            else
            {
                status_lbl.BackColor = Color.White;
                status_lbl.ForeColor = Color.Red;
            }
        }
        private void Status_text_click(object sender, EventArgs e)
        {
            blinker.Stop();
            status_lbl.ForeColor = Color.Red;
            status_lbl.BackColor = Color.WhiteSmoke;
        }

        private void auto_word_selected(object sender, AutocompleteMenuNS.SelectedEventArgs e)
        {

        }
    }
}
