using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


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
            public string getBinaryString(bool not=false)
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
            public int getInt()
            {
                int data = Convert.ToInt32(getBinaryString(), 2);
                return data;
            }
            public int getNInt()
            {
                int data = Convert.ToInt32(getBinaryString(true), 2);
                return data;
            }
            public int get2SInt()
            {
                int data = Convert.ToInt32(getBinaryString(true), 2);
                data += 1;
                if (data > 255)
                    overflowed = 1;
                else
                    overflowed = 0;
                return data;
            }
            public string getHex()
            {
                string hex = Convert.ToString(getInt(), 16).ToUpper().PadLeft(2,'0');
                return hex;
            }
            public void setData(int data)
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
            public void setData(string hex)
            {
                if (Convert.ToInt32(hex, 16) < 256)
                {
                    int data = Convert.ToInt32(hex, 16);
                    setData(data);
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
            public int getInt()
            {
                int data = Convert.ToInt32(getBinaryString(), 2);
                return data;
            }
            public string getBinaryString()
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
            public string getHex()
            {
                string hex = Convert.ToString(getInt(), 16).ToUpper();
                return hex;
            }
            public void setData(int data)
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
            public void checkParity(string binary)
            {
                int eo = 0;
                foreach(char c in binary)
                {
                    if(c=='1')
                    {
                        eo++;
                    }
                }
                if ((eo % 2) == 0)
                    parity = true;
                else
                    parity = false;
            }
            public void checkAuxiliary(string f1, string f2)
            {
                int a = Convert.ToInt32(f1.Substring(4), 2);
                int b = Convert.ToInt32(f2.Substring(4), 2);
                if ((a + b) >= 16)
                    auxiliary = true;
                else
                    auxiliary = false;
            }
            public void checkSign(string binary)
            {
                if (binary[0] == '1')
                    sign = true;
                if (binary[0] == '0')
                    sign = false;
            }
            public void update(Register temp)
            {
                sign = temp.data[0];
                if (temp.getInt() == 0)
                    zero = true;
                else
                    zero = false;
                checkParity(temp.getBinaryString());
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
            public void increment()
            {
                counter++;
            }
            public void incrementBy(int by)
            {
                counter += by;
            }
            public void decrement()
            {
                counter--;
            }
            public void decrementBy(int by)
            {
                counter -= by;
            }
            public string getAddress()
            {
                string hexadr = counter.ToString("X");
                return hexadr;
            }
            public void setAddress(string hex)
            {
                counter = Convert.ToInt32(hex, 16);
            }
            public void setLowerByte(string hex)
            {

            }
            public string getLowerByte()
            {
                return "";
            }
            public void setHigherByte()
            {

            }
            public string getHigherByte()
            {
                return "";
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

        Register a = new Register();
        Register b = new Register();
        Register c = new Register();
        Register d = new Register();
        Register e = new Register();
        Register h = new Register();
        Register l = new Register();

        Flags f = new Flags();

        ProgramCounter pc;

        string psw;
        string m;

        bool stop_run = true;

        private List<ushort> memory;
        private List<ushort> port;

        private List<ushort> stack;
        int sp = 65535;
        int selected_index = 0;

        string orignal_code;
        string formatted_code;

        private List<string> errors;
        private List<LabelAddress> labels;

        ListViewItem[] memory_items;
        ListViewItem[] stack_items;
        ListViewItem[] port_items;
        int m_first;
        int s_first;
        int p_first;

        ListViewItem[] m_items;

        public main()
        {
            InitializeComponent();
        }

        //General functions

        public void update_variables()
        {
            areg.Text = a.getHex().PadLeft(2, '0');
            breg.Text = b.getHex().PadLeft(2, '0');
            creg.Text = c.getHex().PadLeft(2, '0');
            dreg.Text = d.getHex().PadLeft(2, '0');
            ereg.Text = e.getHex().PadLeft(2, '0');
            hreg.Text = h.getHex().PadLeft(2, '0');
            lreg.Text = l.getHex().PadLeft(2, '0');
            flagc.Text = f.carry ? "1" : "0";
            flagz.Text = f.zero ? "1" : "0";
            flags.Text = f.sign ? "1" : "0";
            flaga.Text = f.auxiliary ? "1" : "0";
            flagp.Text = f.parity ? "1" : "0";
            psw = $"{a.getHex().PadLeft(2, '0')}{f.getHex().PadLeft(2, '0')}";
            m = $"{h.getHex().PadLeft(2, '0')}{l.getHex().PadLeft(2, '0')}";
            pswreg.Text = psw.ToString();
            mreg.Text = m.ToString();
            pcreg.Text = pc.getAddress().PadLeft(4, '0');
            spreg.Text = sp.ToString("X").PadLeft(4, '0');
            memorybox.Invalidate();
            stackbox.Invalidate();
            portbox.Invalidate();
        }

        public void clear_registers()
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
            update_variables();
        }

        public void reset_memory()
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
        }

        public void reset_stack()
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
        }

        public void reset_port()
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
        }

        public int load_into_memory()
        {
            int for_pc;
            int start_location = for_pc = 0;
            string opcode = "";
            string lineF = "";
            foreach (Line line in codeEditor.Lines)
            {
                lineF = Regex.Replace(line.Text, @",+", " ");
                lineF = Regex.Replace(lineF, @"\s+", " ");
                lineF = lineF.Trim(' ');
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
                    memorybox.Items[start_location].SubItems[1].Text = code[2].ToUpper();
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
                        int data = Convert.ToInt32(code[2], 16);
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
                        int data = Convert.ToInt32(code[1], 16);
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
                        int data = Convert.ToInt32(code[1], 16);
                        code[1] = data.ToString("X");
                    }

                    memory[start_location] = Convert.ToByte(code[1], 16);
                    memorybox.Items[start_location].SubItems[1].Text = code[1].ToUpper();
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
                }

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
            return for_pc;
        }

        private void run_click(object sender, EventArgs e)
        {
            code_inspect(true);
        }

        public void step_next_code()
        {

        }

        public void read_labels()
        {
            labels.Clear();
            int start_location = 0;
            string lineF = "";
            foreach (Line line in codeEditor.Lines)
            {
                lineF = Regex.Replace(line.Text, @",+", " ");
                lineF = Regex.Replace(lineF, @"\s+", " ");
                lineF = lineF.Trim(' ');
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

        public bool isLabel(string lbl)
        {
            foreach (LabelAddress ad in labels)
                if (ad.name == lbl)
                    return true;
            return false;
        }

        private bool code_inspect(bool run_code)
        {
            read_labels();
            clear_registers();
            errors.Clear();
            output_box.Items.Clear();
            int error_level = 0;
            int line_number = 1;
            bool halt = false;
            string lineF = "";
            foreach (Line line in codeEditor.Lines)
            {
                lineF = Regex.Replace(line.Text, @",+", " ");
                lineF = Regex.Replace(lineF, @"\s+", " ");
                lineF = lineF.Trim(' ');
                if (lineF == "")
                {
                    line_number++;
                    continue;
                }
                if (lineF.ToLower() == "hlt")
                    halt = true;

                string[] code = lineF.Split(' ');
                string error_string = "";
                if (code[0].EndsWith(":"))
                {
                    code[0] = code[0].Remove(code[0].Length - 1);
                    for (int i = 1; i < code.Length; i++)
                    {
                        code[i - 1] = code[i];
                    }
                }
                error_string = check_error(code);
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
                errors.Add("No end to the program detected, did you forget \"HLT?\"");
                error_level++;
            }
            if (error_level > 0)
            {
                foreach (string error in errors)
                {
                    output_box.Items.Add(error);
                }
                codeEditor.Enabled = true;
                stop_button.Enabled = false;
                step_button.Enabled = true;
                play_button.Enabled = true;
                stop_run = true;
                return false;
            }
            else
            {
                if (run_code == true)
                {
                    pc.counter = load_into_memory();
                    while (memory[pc.counter] != 118)
                    {
                        code_execute(memory[pc.counter].ToString("X"));
                        pc.increment();
                        update_variables();
                        if (sp == 0)
                            sp += 1;
                        stackbox.TopItem = stackbox.Items[sp-1];
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
                    play_button.Enabled = true;
                    stop_run = true;
                    output_box.Items.Add("Program successfully terminated!");
                }
                else
                    output_box.Items.Add("No errors found in the code!");
                return true;
            }
        }

        private void code_execute(string hex)
        {
            if (hex == "CE") //ACI data8
            {
                int data = memory[pc.counter + 1];
                Register temp = new Register();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
                pc.incrementBy(1);
            }
            else if (hex == "8F") // ADC A
            {
                Register temp = new Register();
                temp.setData(a.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "88") // ADC B
            {
                Register temp = new Register();
                temp.setData(b.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "89") // ADC C
            {
                Register temp = new Register();
                temp.setData(c.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "8A") // ADC D
            {
                Register temp = new Register();
                temp.setData(d.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "8B") // ADC E
            {
                Register temp = new Register();
                temp.setData(e.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "8C") // ADC H
            {
                Register temp = new Register();
                temp.setData(h.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "8D") // ADC L
            {
                Register temp = new Register();
                temp.setData(l.getInt());
                int data = temp.getInt();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "8E") // ADC M
            {
                int index = Convert.ToInt32(m, 16);
                int data = memory[index];
                Register temp = new Register();
                if (f.carry == true)
                {
                    data += 1;
                    f.carry = false;
                }
                temp.setData(data);
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                a.setData(data);
                f.update(a);
            }
            else if (hex == "87") // ADD A
            {
                Register temp = new Register();
                int data = a.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "80") // ADD B
            {
                Register temp = new Register();
                int data = b.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "81") // ADD C
            {
                Register temp = new Register();
                int data = c.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "82") // ADD D
            {
                Register temp = new Register();
                int data = d.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "83") // ADD E
            {
                Register temp = new Register();
                int data = e.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "84") // ADD H
            {
                Register temp = new Register();
                int data = h.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "85") // ADD L
            {
                Register temp = new Register();
                int data = l.getInt();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "86") // ADD M
            {
                int index = Convert.ToInt32(m, 16);
                int data = memory[index];
                Register temp = new Register();
                temp.setData(data);
                data += a.getInt();
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "C6") // ADI data8
            {
                int data = Convert.ToInt32(memory[pc.counter + 1]);
                Register temp = new Register();
                temp.setData(data);
                data = a.getInt() + data;
                if (data > 255)
                    f.carry = true;
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
                pc.incrementBy(1);
            }
            else if (hex == "A7") // ANA A
            {
                Register temp = new Register();
                int data = a.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);

            }
            else if (hex == "A0") // ANA B
            {
                Register temp = new Register();
                int data = b.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A1") // ANA C
            {
                Register temp = new Register();
                int data = c.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A2") // ANA D
            {
                Register temp = new Register();
                int data = d.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A3") // ANA E
            {
                Register temp = new Register();
                int data = e.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A4") // ANA H
            {
                Register temp = new Register();
                int data = h.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A5") // ANA L
            {
                Register temp = new Register();
                int data = l.getInt();
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "A6") // ANA M
            {
                Register temp = new Register();
                int index = Convert.ToInt32(m, 16);
                int data = memory[index];
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                //ana resets carry flag by default
                f.carry = false;
                //ana sets auxiliary flag by default
                f.auxiliary = true;
                //
                a.setData(data);
                f.update(a);
            }
            else if (hex == "E6") // ANI data8
            {
                Register temp = new Register();
                int data = memory[pc.counter + 1];
                temp.setData(data);
                data = data & a.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
                //ani resets carry flag by default
                f.carry = false;
                //ani sets auxiliary carry by default
                f.auxiliary = true;
                //
                pc.incrementBy(1);
            }
            else if (hex == "CD") // CALL label16
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
            }
            else if (hex == "DC") // CC label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "FC") // CM label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "2F") // CMA
            {
                a.setData(a.getNInt());
            }
            else if (hex == "3F") // CMC
            {
                f.carry = !f.carry;
            }
            else if (hex == "BF") // CMP A
            {
                Register temp = new Register();
                temp.setData(a.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "B8") // CMP B
            {
                Register temp = new Register();
                temp.setData(b.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "B9") // CMP C
            {
                Register temp = new Register();
                temp.setData(c.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "BA") // CMP D
            {
                Register temp = new Register();
                temp.setData(d.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "BB") // CMP E
            {
                Register temp = new Register();
                temp.setData(e.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "BC") // CMP H
            {
                Register temp = new Register();
                temp.setData(h.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "BD") // CMP L
            {
                Register temp = new Register();
                temp.setData(l.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "BE") // CMP M
            {
                int index = Convert.ToInt32(m, 16);
                int data = Convert.ToInt32(memory[index]);
                Register temp = new Register();
                temp.setData(data);
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
            }
            else if (hex == "D4") // CNC label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "C4") // CNZ label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "F4") // CP label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "EC") // CPE label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "FE") // CPI data8
            {
                int data = Convert.ToInt32(memory[pc.counter + 1]);
                Register temp = new Register();
                temp.setData(data);
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                temp.setData(a.getInt() + temp.getInt());
                f.update(temp);
                pc.incrementBy(1);
            }
            else if (hex == "E4") // CPO label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "CC") // CZ label16
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
                }
                else
                    pc.incrementBy(2);
            }
            else if (hex == "27") // DAA
            {

            }
            else if (hex == "9") // DAD B [C]
            {
                string t1 = b.getHex() + c.getHex();
                string t2 = h.getHex() + l.getHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if(data>65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.setData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.setData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            else if (hex == "19") // DAD D [E]
            {
                string t1 = d.getHex() + e.getHex();
                string t2 = h.getHex() + l.getHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.setData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.setData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            else if (hex == "29")  // DAD H [L]
            {
                string t1 = h.getHex() + l.getHex();
                string t2 = h.getHex() + l.getHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.setData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.setData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            else if (hex == "39") // DAD SP
            {
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                string t2 = h.getHex() + l.getHex();
                long data = Convert.ToUInt32(t1, 16) + Convert.ToUInt32(t2, 16);
                if (data > 65535)
                {
                    data -= 65536;
                    f.carry = true;
                }
                h.setData(data.ToString("X").PadLeft(4, '0').Substring(0, 2));
                l.setData(data.ToString("X").PadLeft(4, '0').Substring(2, 2));
            }
            else if (hex == "3D") // DCR A
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = a.getInt() + temp.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "5") // DCR B
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = b.getInt() + temp.getInt();
                f.checkAuxiliary(b.getBinaryString(), temp.getBinaryString());
                b.setData(data);
                f.update(b);
            }
            else if (hex == "D") // DCR C
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = c.getInt() + temp.getInt();
                f.checkAuxiliary(c.getBinaryString(), temp.getBinaryString());
                c.setData(data);
                f.update(c);
            }
            else if (hex == "15") // DCR D
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = d.getInt() + temp.getInt();
                f.checkAuxiliary(d.getBinaryString(), temp.getBinaryString());
                d.setData(data);
                f.update(d);
            }
            else if (hex == "1D") // DCR E
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = e.getInt() + temp.getInt();
                f.checkAuxiliary(e.getBinaryString(), temp.getBinaryString());
                e.setData(data);
                f.update(e);
            }
            else if (hex == "25") // DCR H
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = h.getInt() + temp.getInt();
                f.checkAuxiliary(h.getBinaryString(), temp.getBinaryString());
                h.setData(data);
                f.update(h);
            }
            else if (hex == "2D") // DCR L
            {
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int data = l.getInt() + temp.getInt();
                f.checkAuxiliary(l.getBinaryString(), temp.getBinaryString());
                l.setData(data);
                f.update(l);
            }
            else if (hex == "35") // DCR M
            {
                int index = Convert.ToInt32(m, 16);
                Register data = new Register();
                data.setData(Convert.ToInt32(memory[index]));
                Register temp = new Register();
                temp.setData(1);
                temp.setData(temp.get2SInt());
                int res = data.getInt() + temp.getInt();
                f.checkAuxiliary(data.getBinaryString(), temp.getBinaryString());
                data.setData(res);
                f.update(data);
                memorybox.Items[index].SubItems[1].Text = data.getHex();
            }
            else if (hex == "B") // DCX B [C]
            {
                string t1 = b.getHex() + c.getHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                b.setData(t1.Substring(0, 2));
                c.setData(t1.Substring(2, 2));
            }
            else if (hex == "1B") // DCX D [E]
            {
                string t1 = d.getHex() + e.getHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                d.setData(t1.Substring(0, 2));
                e.setData(t1.Substring(2, 2));
            }
            else if (hex == "2B") // DCX H [L]
            {
                string t1 = h.getHex() + l.getHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                t1 = data.ToString("X").PadLeft(4, '0');
                h.setData(t1.Substring(0, 2));
                l.setData(t1.Substring(2, 2));
            }
            else if (hex == "3B") // DCX SP
            {
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                sp = data;
            }
            else if (hex == "F3") // DI
            {

            }
            else if (hex == "FB") // EI
            {

            }
            else if (hex == "76") // HLT
            {
                //end og life
            }
            else if (hex == "DB") // IN Port8
            {
                //useless
            }
            else if (hex == "3C") // INR A
            {
                Register temp = new Register();
                temp.setData(1);
                int data = a.getInt() + temp.getInt();
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "4") // INR B
            {
                Register temp = new Register();
                temp.setData(1);
                int data = b.getInt() + temp.getInt();
                f.checkAuxiliary(b.getBinaryString(), temp.getBinaryString());
                b.setData(data);
                f.update(b);
            }
            else if (hex == "C") // INR C
            {
                Register temp = new Register();
                temp.setData(1);
                int data = c.getInt() + temp.getInt();
                f.checkAuxiliary(c.getBinaryString(), temp.getBinaryString());
                c.setData(data);
                f.update(c);
            }
            else if (hex == "14") // INR D
            {
                Register temp = new Register();
                temp.setData(1);
                int data = d.getInt() + temp.getInt();
                f.checkAuxiliary(d.getBinaryString(), temp.getBinaryString());
                d.setData(data);
                f.update(d);
            }
            else if (hex == "1C") // INR E
            {
                Register temp = new Register();
                temp.setData(1);
                int data = e.getInt() + temp.getInt();
                f.checkAuxiliary(e.getBinaryString(), temp.getBinaryString());
                e.setData(data);
                f.update(e);
            }
            else if (hex == "24") // INR H
            {
                Register temp = new Register();
                temp.setData(1);
                int data = h.getInt() + temp.getInt();
                f.checkAuxiliary(h.getBinaryString(), temp.getBinaryString());
                h.setData(data);
                f.update(h);
            }
            else if (hex == "2C") // INR L
            {
                Register temp = new Register();
                temp.setData(1);
                int data = l.getInt() + temp.getInt();
                f.checkAuxiliary(l.getBinaryString(), temp.getBinaryString());
                l.setData(data);
                f.update(l);
            }
            else if (hex == "34") // INR M
            {
                int index = Convert.ToInt32(m, 16);
                Register data = new Register();
                data.setData(Convert.ToInt32(memory[index]));
                Register temp = new Register();
                temp.setData(1);
                int res = data.getInt() + temp.getInt();
                f.checkAuxiliary(data.getBinaryString(), temp.getBinaryString());
                data.setData(res);
                f.update(data);
                memorybox.Items[index].SubItems[1].Text = data.getHex();
            }
            else if (hex == "3") // INX B [C]
            {
                string t1 = b.getHex() + c.getHex();
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                b.setData(t1.Substring(0, 2));
                c.setData(t1.Substring(2, 2));
            }
            else if (hex == "13") // INX D [E]
            {
                string t1 = d.getHex() + e.getHex();
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                d.setData(t1.Substring(0, 2));
                e.setData(t1.Substring(2, 2));
            }
            else if (hex == "23") // INX H [L]
            {
                string t1 = h.getHex() + l.getHex();
                int data = Convert.ToInt32(t1, 16);
                data -= 1;
                if (data > 65535)
                    data -= 65536;
                t1 = data.ToString("X").PadLeft(4, '0');
                h.setData(t1.Substring(0, 2));
                l.setData(t1.Substring(2, 2));
            }
            else if (hex == "33") // INX SP
            {
                string t1 = Convert.ToString(sp, 16).PadLeft(4, '0');
                int data = Convert.ToInt32(t1, 16);
                data += 1;
                if (data > 65535)
                    data -= 65536;
                sp = data;
            }
            else if (hex == "DA") // JC label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "FA") // JM label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "C3") // JMP label16
            {
                string label = "";
                label += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                label += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                pc.counter = Convert.ToInt32(label, 16);
                pc.counter -= 1;
            }
            else if (hex == "D2") // JNC label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "C2") // JNZ label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "F2") // JP label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "EA") // JPE label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "D2") // JPO label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "CA") // JZ label16
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
                    pc.incrementBy(2);
            }
            else if (hex == "3A") // LDA address16
            {
                string addr = "";
                addr += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                addr += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                int index = Convert.ToInt32(addr, 16);
                a.setData(Convert.ToInt32(memory[index]));
                pc.incrementBy(2);
            }
            else if (hex == "A") // LDAX B [C]
            {
                string addr = "";
                addr += b.getHex();
                addr += c.getHex();
                int index = Convert.ToInt32(addr, 16);
                a.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "1A") // LDAX D [E]
            {
                string addr = "";
                addr += d.getHex();
                addr += e.getHex();
                int index = Convert.ToInt32(addr, 16);
                a.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "2A") // LHLD address16
            {
                int index = memory[pc.counter + 2] + memory[pc.counter + 1];
                int ad1 = memory[index + 1];
                int ad2 = memory[index];
                h.setData(ad1);
                l.setData(ad2);
                pc.incrementBy(2);
            }
            else if (hex == "1") // LXI B [C] data16
            {
                b.setData(Convert.ToInt32(memory[pc.counter + 2]));
                c.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(2);
            }
            else if (hex == "11") // LXI D [E] data16
            {
                d.setData(Convert.ToInt32(memory[pc.counter + 2]));
                e.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(2);
            }
            else if (hex == "21") // LXI H [L] data16
            {
                h.setData(Convert.ToInt32(memory[pc.counter + 2]));
                l.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(2);
            }
            else if (hex == "31") // LXI SP data16
            {
                string data = "";
                data += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                data += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                sp = Convert.ToInt32(data, 16);
                pc.incrementBy(2);
            }
            else if (hex == "7F") // MOV A A
            {
                a.setData(a.getInt());
            }
            else if (hex == "78") // MOV A B
            {
                a.setData(b.getInt());
            }
            else if (hex == "79") // MOV A C
            {
                a.setData(c.getInt());
            }
            else if (hex == "7A") // MOV A D
            {
                a.setData(d.getInt());
            }
            else if (hex == "7B") // MOV A E
            {
                a.setData(e.getInt());
            }
            else if (hex == "7C") // MOV A H
            {
                a.setData(h.getInt());
            }
            else if (hex == "7D") // MOV A L
            {
                a.setData(l.getInt());
            }
            else if (hex == "7E") // MOV A M
            {
                int index = Convert.ToInt32(m, 16);
                a.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "47") // MOV B A
            {
                b.setData(a.getInt());
            }
            else if (hex == "40") // MOV B B
            {
                b.setData(b.getInt());
            }
            else if (hex == "41") // MOV B C
            {
                b.setData(c.getInt());
            }
            else if (hex == "42") // MOV B D
            {
                b.setData(d.getInt());
            }
            else if (hex == "43") // MOV B E
            {
                b.setData(e.getInt());
            }
            else if (hex == "44") // MOV B H
            {
                b.setData(h.getInt());
            }
            else if (hex == "45") // MOV B L
            {
                b.setData(l.getInt());
            }
            else if (hex == "46") // MOV B M
            {
                int index = Convert.ToInt32(m, 16);
                b.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "4F") // MOV C A
            {
                c.setData(a.getInt());
            }
            else if (hex == "48") // MOV C B
            {
                c.setData(b.getInt());
            }
            else if (hex == "49") // MOV C C
            {
                c.setData(c.getInt());
            }
            else if (hex == "4A") // MOV C D
            {
                c.setData(d.getInt());
            }
            else if (hex == "4B") // MOV C E
            {
                c.setData(l.getInt());
            }
            else if (hex == "4C") // MOV C H
            {
                c.setData(l.getInt());
            }
            else if (hex == "4D") // MOV C L
            {
                c.setData(l.getInt());
            }
            else if (hex == "4E") // MOV C M
            {
                int index = Convert.ToInt32(m, 16);
                c.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "57") // MOV D A
            {
                d.setData(a.getInt());
            }
            else if (hex == "50") // MOV D B
            {
                d.setData(b.getInt());
            }
            else if (hex == "51") // MOV D C
            {
                d.setData(c.getInt());
            }
            else if (hex == "52") // MOV D D
            {
                a.setData(a.getInt());
            }
            else if (hex == "53") // MOV D E
            {
                d.setData(e.getInt());
            }
            else if (hex == "54") // MOV D H
            {
                d.setData(h.getInt());
            }
            else if (hex == "55") // MOV D L
            {
                d.setData(l.getInt());
            }
            else if (hex == "56") // MOV D M
            {
                int index = Convert.ToInt32(m, 16);
                d.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "5F") // MOV E A
            {
                e.setData(a.getInt());
            }
            else if (hex == "58") // MOV E B
            {
                e.setData(b.getInt());
            }
            else if (hex == "59") // MOV E C
            {
                e.setData(c.getInt());
            }
            else if (hex == "5A") // MOV E D
            {
                e.setData(d.getInt());
            }
            else if (hex == "5B") // MOV E E
            {
                e.setData(e.getInt());
            }
            else if (hex == "5C") // MOV E H
            {
                e.setData(h.getInt());
            }
            else if (hex == "5D") // MOV E L
            {
                e.setData(l.getInt());
            }
            else if (hex == "5E") // MOV E M
            {
                int index = Convert.ToInt32(m, 16);
                e.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "67") // MOV H A
            {
                h.setData(a.getInt());
            }
            else if (hex == "60") // MOV H B
            {
                h.setData(b.getInt());
            }
            else if (hex == "61") // MOV H C
            {
                h.setData(c.getInt());
            }
            else if (hex == "62") // MOV H D
            {
                h.setData(d.getInt());
            }
            else if (hex == "63") // MOV H E
            {
                h.setData(e.getInt());
            }
            else if (hex == "64") // MOV H H
            {
                h.setData(h.getInt());
            }
            else if (hex == "65") // MOV H L
            {
                h.setData(l.getInt());
            }
            else if (hex == "66") // MOV H M
            {
                int index = Convert.ToInt32(m, 16);
                h.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "6F") // MOV L A
            {
                l.setData(a.getInt());
            }
            else if (hex == "68") // MOV L B
            {
                l.setData(b.getInt());
            }
            else if (hex == "69") // MOV L C
            {
                l.setData(c.getInt());
            }
            else if (hex == "6A") // MOV L D
            {
                l.setData(d.getInt());
            }
            else if (hex == "6B") // MOV L E
            {
                l.setData(e.getInt());
            }
            else if (hex == "6C") // MOV L H
            {
                l.setData(h.getInt());
            }
            else if (hex == "6D") // MOV L L
            {
                l.setData(l.getInt());
            }
            else if (hex == "6E") // MOV L M
            {
                int index = Convert.ToInt32(m, 16);
                l.setData(Convert.ToInt32(memory[index]));
            }
            else if (hex == "77") // MOV M A
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(a.getInt());
                memorybox.Items[index].SubItems[1].Text = a.getHex();
            }
            else if (hex == "70") // MOV M B
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(b.getInt());
                memorybox.Items[index].SubItems[1].Text = b.getHex();
            }
            else if (hex == "71") // MOV M C
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(c.getInt());
                memorybox.Items[index].SubItems[1].Text = c.getHex();
            }
            else if (hex == "72") // MOV M D
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(d.getInt());
                memorybox.Items[index].SubItems[1].Text = d.getHex();
            }
            else if (hex == "73") // MOV M E
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(e.getInt());
                memorybox.Items[index].SubItems[1].Text = e.getHex();
            }
            else if (hex == "74") // MOV M H
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(h.getInt());
                memorybox.Items[index].SubItems[1].Text = h.getHex();
            }
            else if (hex == "75") // MOV M L
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = Convert.ToByte(l.getInt());
                memorybox.Items[index].SubItems[1].Text = l.getHex();
            }
            else if (hex == "3E") // MVI A data8
            {
                a.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "6") // MVI B data8
            {
                b.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "E") // MVI C data8
            {
                c.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "16") // MVI D data8
            {
                d.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "1E") // MVI E data8
            {
                e.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "26") // MVI H data8
            {
                h.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "2E") // MVI L data8
            {
                l.setData(Convert.ToInt32(memory[pc.counter + 1]));
                pc.incrementBy(1);
            }
            else if (hex == "36") // MVI M data8
            {
                int index = Convert.ToInt32(m, 16);
                memory[index] = memory[pc.counter + 1];
                memorybox.Items[index].SubItems[1].Text = memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                pc.incrementBy(1);
            }
            else if (hex == "0") // NOP
            {
                //NO MEANS NO
            }
            else if (hex == "B7") // ORA A
            {
                int ora = a.getInt() | a.getInt();
                a.setData(ora);
            }
            else if (hex == "B0") // ORA B
            {
                int ora = a.getInt() | b.getInt();
                a.setData(ora);
            }
            else if (hex == "B1") // ORA C
            {
                int ora = a.getInt() | c.getInt();
                a.setData(ora);
            }
            else if (hex == "B2") // ORA D
            {
                int ora = a.getInt() | d.getInt();
                a.setData(ora);
            }
            else if (hex == "B3") // ORA E
            {
                int ora = a.getInt() | e.getInt();
                a.setData(ora);
            }
            else if (hex == "B4") // ORA H
            {
                int ora = a.getInt() | h.getInt();
                a.setData(ora);
            }
            else if (hex == "B5") // ORA L
            {
                int ora = a.getInt() | l.getInt();
                a.setData(ora);
            }
            else if (hex == "B6") // ORA M
            {
                int ora = a.getInt() | Convert.ToInt32(memory[Convert.ToInt32(m, 16)]);
                a.setData(ora);
            }
            else if (hex == "F6") // ORI data8
            {
                int ora = a.getInt() | Convert.ToInt32(memory[pc.counter + 1]);
                a.setData(ora);
            }
            else if (hex == "D3") // OUT port8
            {
                //useless
            }
            else if (hex == "E9") // PCHL
            {
                int data = Convert.ToInt32(h.getInt()) + Convert.ToInt32(l.getInt());
                pc.counter = data-1;
            }
            else if (hex == "C1") // POP B [C]
            {
                if (sp <= 65533)
                {
                    b.setData(stack[sp]);
                    sp++;
                    c.setData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            else if (hex == "D1") // POP D [E]
            {
                if (sp <= 65533)
                {
                    d.setData(stack[sp]);
                    sp++;
                    e.setData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            else if (hex == "E1") // POP H [L]
            {
                if (sp <= 65533)
                {
                    h.setData(stack[sp]);
                    sp++;
                    l.setData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            else if (hex == "F1") // POP PSW
            {
                if (sp <= 65533)
                {
                    a.setData(stack[sp]);
                    sp++;
                    f.setData(stack[sp]);
                    sp++;
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            else if (hex == "C5") // PUSH B [C]
            {
                sp--;
                stack[sp] = Convert.ToByte(c.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(b.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
            }
            else if (hex == "D5") // PUSH D [E]
            {
                sp--;
                stack[sp] = Convert.ToByte(e.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(d.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
            }
            else if (hex == "E5") // PUSH H [L]
            {
                sp--;
                stack[sp] = Convert.ToByte(l.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(h.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
            }
            else if (hex == "F5") // PUSH PSW
            {
                sp--;
                stack[sp] = Convert.ToByte(f.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
                sp--;
                stack[sp] = Convert.ToByte(a.getInt());
                stackbox.Items[sp].SubItems[1].Text = stack[sp].ToString("X").PadLeft(2, '0');
            }
            else if (hex == "17") // RAL
            {
                bool temp = a.data[0];
                for (int i = 1; i < 8; i++)
                    a.data[i - 1] = a.data[i];
                a.data[7] = f.carry;
                f.carry = temp;
            }
            else if (hex == "1F") // RAR
            {
                bool temp = a.data[7];
                for (int i = 7; i > 0; i--)
                    a.data[i] = a.data[i-1];
                a.data[0] = f.carry;
                f.carry = temp;
            }
            else if (hex == "D8") // RC
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "C9") // RET
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
                }
                else
                {
                    stop_run = true;
                    errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                }
            }
            else if (hex == "20") // RIM
            {

            }
            else if (hex == "7") // RLC
            {
                bool temp = a.data[0];
                for (int i = 1; i < 8; i++)
                    a.data[i - 1] = a.data[i];
                a.data[7] = temp;
                f.carry = temp;
            }
            else if (hex == "F8") // RM
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "D0") // RNC
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "C0") // RNZ
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "F0") // RP
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "E8") // RPE
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "E0") // RPO
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "F") // RRC
            {
                bool temp = a.data[7];
                for (int i = 8; i > 1; i--)
                    a.data[i] = a.data[i-1];
                a.data[0] = temp;
                f.carry = temp;
            }
            else if (hex == "C7") // RST 0
            {

            }
            else if (hex == "CF") // RST 1
            {

            }
            else if (hex == "D7") // RST 2
            {

            }
            else if (hex == "DF") // RST 3
            {

            }
            else if (hex == "E7") // RST 4
            {

            }
            else if (hex == "EF") // RST 5
            {

            }
            else if (hex == "F7") // RST 6
            {

            }
            else if (hex == "FF") // RST 7
            {

            }
            else if (hex == "C8") // RZ
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
                    }
                    else
                    {
                        stop_run = true;
                        errors.Add($"Runtime Error at {pc.counter.ToString("X").PadLeft(4, '0')} : Not enough value in stack!");
                    }
                }
            }
            else if (hex == "9F") // SBB A
            {

            }
            else if (hex == "98") // SBB B
            {

            }
            else if (hex == "99") // SBB C
            {

            }
            else if (hex == "9A") // SBB D
            {

            }
            else if (hex == "9B") // SBB E
            {

            }
            else if (hex == "9C") // SBB H
            {

            }
            else if (hex == "9D") // SBB L
            {

            }
            else if (hex == "9E") // SBB M
            {

            }
            else if (hex == "DE") // SBI data8
            {

            }
            else if (hex == "22") // SHLD address16
            {
                int index = memory[pc.counter + 2] + memory[pc.counter + 1];
                memory[index] = Convert.ToByte(l.getInt());
                memory[index + 1] = Convert.ToByte(h.getInt());
                memorybox.Items[index].SubItems[1].Text = l.getHex();
                memorybox.Items[index + 1].SubItems[1].Text = h.getHex();
            }
            else if (hex == "30") // SIM
            {

            }
            else if (hex == "F9") // SPHL
            {

            }
            else if (hex == "32") // STA address16
            {
                string addr = "";
                addr += memory[pc.counter + 2].ToString("X").PadLeft(2, '0');
                addr += memory[pc.counter + 1].ToString("X").PadLeft(2, '0');
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.getInt());
                memorybox.Items[index].SubItems[1].Text = a.getHex();
                pc.incrementBy(2);
            }
            else if (hex == "2") // STAX B [C]
            {
                string addr = "";
                addr += b.getHex();
                addr += c.getHex();
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.getInt());
                memorybox.Items[index].SubItems[1].Text = a.getHex();
            }
            else if (hex == "12") // STAX D [E]
            {
                string addr = "";
                addr += d.getHex();
                addr += e.getHex();
                int index = Convert.ToInt32(addr, 16);
                memory[index] = Convert.ToByte(a.getInt());
                memorybox.Items[index].SubItems[1].Text = a.getHex();
            }
            else if (hex == "37") // STC
            {
                f.carry = true;
            }
            else if (hex == "97") // SUB A
            {
                /*
                 * "sub a", sets carry when "a" is 0
                 * but sub a, resets carry if set when "a" is not 0
                 * why?
                 * 
                 * */
                Register temp = new Register();
                temp.setData(a.getInt());
                int data = a.getInt() + temp.get2SInt();
                if (data > 255)
                    f.carry = true;
                else
                    f.carry = false;
                f.carry = !f.carry;
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "90") // SUB B
            {
                Register temp = new Register();
                temp.setData(b.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "91") // SUB C
            {
                Register temp = new Register();
                temp.setData(c.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "92") // SUB D
            {
                Register temp = new Register();
                temp.setData(d.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "93") // SUB E
            {
                Register temp = new Register();
                temp.setData(e.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "94") // SUB H
            {
                Register temp = new Register();
                temp.setData(h.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "95") // SUB L
            {
                Register temp = new Register();
                temp.setData(l.getInt());
                if (temp.getInt() > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                int data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "96") // SUB M
            {
                int index = Convert.ToInt32(m, 16);
                int data = Convert.ToInt32(memory[index]);
                Register temp = new Register();
                temp.setData(data);
                if (data > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
            }
            else if (hex == "D6") // SUI data8
            {
                int data = memory[pc.counter + 1];
                Register temp = new Register();
                temp.setData(data);
                if (data > a.getInt())
                    f.carry = true;
                else
                    f.carry = false;
                data = a.getInt() + temp.get2SInt();
                temp.setData(temp.get2SInt());
                f.checkAuxiliary(a.getBinaryString(), temp.getBinaryString());
                a.setData(data);
                f.update(a);
                pc.incrementBy(1);
            }
            else if (hex == "EB") // XCHG
            {

            }
            else if (hex == "AF") // XRA A
            {

            }
            else if (hex == "A8") // XRA B
            {

            }
            else if (hex == "A9") // XRA C
            {

            }
            else if (hex == "AA") // XRA D
            {

            }
            else if (hex == "AB") // XRA E
            {

            }
            else if (hex == "AC") // XRA H
            {

            }
            else if (hex == "AD") // XRA L
            {

            }
            else if (hex == "AE") // XRA M
            {

            }
            else if (hex == "EE") // XRI data8
            {

            }
            else if (hex == "E3") // XTHL
            {

            }
        }

        private string check_error(string[] code)
        {
            for (int i = 0; i < code.Length; i++)
                code[i] = code[i].ToLower();

            string error_string = "";

            if (code[0] == "mov")
            {
                if (code.Length > 2)
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
                if (code.Length > 2)
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
                        if(code[2].StartsWith("-"))
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
                                    error_string = $"\"{code[2]}\" is not valid value";
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
                                    error_string = $"\"{code[2]}\" is not valid value";
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
                if (code.Length > 1)
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
                    error_string = error_string = "Need 1 parameter";

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
                if (code.Length > 1)
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
                                error_string = $"\"{code[1]}\" is not valid value";
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
                                error_string = $"\"{code[1]}\" is not valid value";
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
                if (code.Length > 2)
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
                                    error_string = $"\"{code[2]}\" is not valid value";
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
                                    error_string = $"\"{code[2]}\" is not valid value";
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
                if (code.Length > 1)
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
                if (code.Length > 1)
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
                            if (Convert.ToInt32(code[1], 16) > 65335)
                                error_string = $"\"{code[1]}\" is not valid value";
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
                            if (Int32.Parse(code[1]) > 65335)
                                error_string = $"\"{code[1]}\" is not valid value";
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
                if (code.Length > 1)
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
                if (code.Length > 1)
                {
                    if (isLabel(code[1])) { }
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
                            if (Convert.ToInt32(code[1], 16) > 65535)
                                error_string = $"\"{code[1]}\" is not valid value";
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
                            if (Int32.Parse(code[1]) > 65535)
                                error_string = $"\"{code[1]}\" is not valid value";
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
                if (code.Length > 1)
                    try
                    {
                        if (code[1].StartsWith("0"))
                            code[1] = code[1].TrimStart('0');
                        if (Convert.ToInt32(code[1], 16) > 7 || Convert.ToInt32(code[1], 16) < 1)
                            error_string = $"\"{code[1]}\" is not valid value";
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
                if (code.Length > 1)
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

        private void closing(object sender, FormClosingEventArgs e)
        {

        }

        private void loading(object sender, EventArgs e)
        {

            labels = new List<LabelAddress>();
            errors = new List<string>();

            memory = new List<ushort>(new ushort[65535]);
            stack = new List<ushort>(new ushort[65535]);
            port = new List<ushort>(new ushort[255]);


            pc = new ProgramCounter();

            reset_memory();
            reset_stack();
            reset_port();
            update_variables();

            WindowState = FormWindowState.Maximized;



            codeEditor.Lexer = ScintillaNET.Lexer.Asm;
            codeEditor.Styles[Style.Default].Font = "Consolas";
            codeEditor.Styles[Style.Default].Size = 14;
            codeEditor.Styles[Style.Asm.Comment].ForeColor = Color.Green;
            codeEditor.Styles[Style.Asm.CpuInstruction].ForeColor = Color.DarkOrange;
            codeEditor.Styles[Style.Asm.Number].ForeColor = Color.Blue;
            codeEditor.Styles[Style.Asm.Register].ForeColor = Color.MediumVioletRed;
            codeEditor.Styles[Style.Asm.Operator].ForeColor = Color.Purple;
            codeEditor.Styles[Style.Asm.DirectiveOperand].ForeColor = Color.DarkSeaGreen;
            codeEditor.SetKeywords(0, "mov mvi lxi ldax stax lda sta lhld shld xchg add adc sub sbb adi aci sui sbi dad inr dcr inx dcx daa cma cmc stc adi aci sui sbi ana ora xra cmp rlc rrc ral rar ani xri ori cpi jmp jnz jz jnc jc jpo jpe jp jm call cnz cz cnc cc cpo cpe cp cm ret rnz rz rnc rc rpo rpe rp rm pchl in out push pop xthl sphl ei di rim sim nop hlt rst");
            codeEditor.SetKeywords(2, "a b c d e h l m psw sp");
            codeEditor.Margins[0].Width = 40;
            codeEditor.Margins[1].Width = 10;

            codeEditor.Markers[1].Symbol = MarkerSymbol.Background;
            codeEditor.Markers[1].SetBackColor(Color.LightGray);

            codeEditor.Select();
        }


        //Menu section

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void clearMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset_memory();
        }

        private void clearRegistersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_registers();
        }

        private void clearEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditor.ClearAll();
        }

        //key events

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            codeEditor.Enabled = false;
            stop_button.Enabled = true;
            play_button.Enabled = false;
            step_button.Enabled = false;
            stop_run = false;
            code_inspect(true);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("8085 Simulator - ALPHA\nDeveloped by Vishal Solanki\n\nThis is Project for my 5th semester, I poured all my dedication to this project, hope it helps!\n\nE-Mail : vcsolanki.vs@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void stepNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            step_next_code();
        }

        private void show_conv_tooltip(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            reg_name.Text = temp.Tag.ToString();
            int data = Convert.ToInt32(temp.Text, 16);
            conv_lbl.Text = $"BIN : {Convert.ToString(data, 2).PadLeft(8, '0')}\nHEX : {temp.Text}\nDEC : {data}";
        }

        private void checkErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code_inspect(false);
        }
      
        private void warning_click(object sender, EventArgs e)
        {
            MessageBox.Show("Software is in ALPHA state!\nCan't guarantee software will work in every situation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void run_program(object sender, EventArgs e)
        {
                codeEditor.Enabled = false;
                stop_button.Enabled = true;
                play_button.Enabled = false;
                step_button.Enabled = false;
                stop_run = false;
                code_inspect(true);
        }

        private void stop_program(object sender, EventArgs e)
        {
            play_button.Enabled = true;
            stop_button.Enabled = false;
            selected_index = 0;
            stop_run = true;
            codeEditor.Enabled = true;
            codeEditor.Text = orignal_code;
            formatted_code = "";
        }

        private void format_code(ref string str)
        {
            int start_location = 0;
            string lineF = "";
            foreach (Line line in codeEditor.Lines)
            {
                lineF = Regex.Replace(line.Text, @",+", " ");
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
                    code[code.Length-1] = "";
                    lineF = "";
                    foreach(string word in code)
                    {
                        if(word!="")
                        lineF += word + ' ';
                    }
                    str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF}\n";
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
                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF}\n";
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
                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF}\n";
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
                        if (isLabel(code[1]))
                        {
                            foreach (LabelAddress la in labels)
                                if (code[1] == la.name)
                                    code[1] = la.address.ToString("X").PadLeft(4, '0');
                            lineF = "";
                            foreach (string word in code)
                                lineF += word + " ";
                        }

                        str += $"{start_location.ToString("X").PadLeft(4, '0')} : {lineF}\n";
                        start_location += 3;
                    }
                }
            }
        }

        private void step_program(object sender, EventArgs e)
        {
            if (stop_run == true && code_inspect(false))
            {
                pc.counter = load_into_memory();
                orignal_code = codeEditor.Text;
                format_code(ref formatted_code);
                codeEditor.Text = formatted_code;
                play_button.Enabled = false;
                stop_button.Enabled = true;
                stop_run = false;
                codeEditor.Enabled = false;
            }
            else
                if (memory[pc.counter].ToString("X") != "76")
                {
                code_execute(memory[pc.counter].ToString("X"));
                pc.increment();
                update_variables();
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
                    selected_index = 0;
                    stop_run = true;
                    codeEditor.Enabled = true;
                    codeEditor.Text = orignal_code;
                    formatted_code = "";
                }
        }

        private void code_editor_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar<32)
            {
                e.Handled = true;
            }
        }

        private void find_text_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                memorybox.TopItem = memorybox.FindItemWithText(address_to_find.Text.ToUpper());
                e.Handled = true;
            }
        }
        private void find_address_Click(object sender, EventArgs e)
        {
            memorybox.TopItem = memorybox.FindItemWithText(address_to_find.Text.ToUpper());
        }

        private void memory_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (memory_items != null && e.ItemIndex >= m_first && e.ItemIndex < m_first + memory_items.Length)
                e.Item = memory_items[e.ItemIndex - m_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void memory_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (memory_items != null && e.StartIndex >= m_first && e.EndIndex <= m_first + memory_items.Length)
                return;
            m_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            memory_items = new ListViewItem[length];
            int x = 0;
            for (int i = 0; i < length; i++)
            {
                x = (i + m_first) * (i + m_first);
                memory_items[i] = new ListViewItem(x.ToString());
            }
        }

        private void stack_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (stack_items != null && e.ItemIndex >= s_first && e.ItemIndex < s_first + stack_items.Length)
                e.Item = stack_items[e.ItemIndex - s_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void stack_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (stack_items != null && e.StartIndex >= s_first && e.EndIndex <= s_first + stack_items.Length)
                return;
            s_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            stack_items = new ListViewItem[length];
            int x = 0;
            for (int i = 0; i < length; i++)
            {
                x = (i + s_first) * (i + s_first);
                stack_items[i] = new ListViewItem(x.ToString());
            }
        }

        private void port_retrieve_items(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (port_items != null && e.ItemIndex >= p_first && e.ItemIndex < p_first + port_items.Length)
                e.Item = port_items[e.ItemIndex - p_first];
            else
            {
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(x.ToString());
            }
        }
        private void port_cache_items(object sender, CacheVirtualItemsEventArgs e)
        {
            if (port_items != null && e.StartIndex >= p_first && e.EndIndex <= p_first + port_items.Length)
                return;
            p_first = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            port_items = new ListViewItem[length];
            int x = 0;
            for (int i = 0; i < length; i++)
            {
                x = (i + p_first) * (i + p_first);
                port_items[i] = new ListViewItem(x.ToString());
            }
        }

        private void memory_search_item(object sender, SearchForVirtualItemEventArgs e)
        {
            int x = 0;
            foreach(ListViewItem it in m_items)
            {
                if (it.Text.Contains(e.Text))
                {
                    e.Index = x;
                    return;
                }
                x++;
            }
        }

        private void memory_box_keypress(object sender, KeyPressEventArgs e)
        {
        }

        private void memory_double_click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection lic = memorybox.SelectedIndices;
            address_editbox address_Editbox = new address_editbox(memorybox.Items[lic[0]].SubItems[1].Text);
            address_Editbox.Text = memorybox.Items[lic[0]].Text;
            if (address_Editbox.ShowDialog() == DialogResult.OK)
            {
                memory[Convert.ToInt32(memorybox.Items[lic[0]].Text, 16)] = Convert.ToByte(address_Editbox.int_value);
                memorybox.Items[lic[0]].SubItems[1].Text = memory[Convert.ToInt32(memorybox.Items[lic[0]].Text, 16)].ToString("X").PadLeft(2, '0');
                memorybox.Invalidate();
            }
        }
    }
}
