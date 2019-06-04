using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _8085_Simulator
{
    public partial class main : Form
    {
        private List<string> errors;

        public class Register
        {
            public bool[] data = new bool[8];
            public Register()
            {
                Clear();
            }
            public string getBinaryString()
            {
                string data_string = "";
                for (int i = 0; i < 8; i++)
                {
                    if (data[i] == true)
                        data_string += '1';
                    else
                        data_string += '0';
                }
                return data_string;
            }
            public int getInt()
            {
                int data = Convert.ToInt32(getBinaryString(), 2);
                return data;
            }
            public string getHex()
            {
                string hex = Convert.ToString(getInt(), 16).ToUpper();
                return hex;
            }
            public void setData(int data)
            {
                if (data < 256)
                {
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
            public bool auxilary = new bool();
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
                address[3] = auxilary;
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

            public void Clear()
            {
                sign = zero = auxilary = parity = carry = false;
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


        Register a = new Register();
        Register b = new Register();
        Register c = new Register();
        Register d = new Register();
        Register e = new Register();
        Register h = new Register();
        Register l = new Register();

        Flags f = new Flags();

        Stack<int> sp;

        ProgramCounter pc;

        string psw;
        string m;


        private List<string> memory;

        public main()
        {
            InitializeComponent();

        }

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
            flaga.Text = f.auxilary ? "1" : "0";
            flagp.Text = f.parity ? "1" : "0";
            psw = $"{a.getHex().PadLeft(2, '0')}{f.getHex().PadLeft(2, '0')}";
            m = $"{h.getHex().PadLeft(2, '0')}{l.getHex().PadLeft(2, '0')}";
            pswreg.Text = psw.ToString();
            mreg.Text = m.ToString();
            pcreg.Text = pc.getAddress().PadLeft(4, '0');
            if (sp.Count > 0)
                spreg.Text = sp.Peek().ToString("X");
            else
                spreg.Text = "0";
            spreg.Text = spreg.Text.PadLeft(4, '0');
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
            sp.Clear();
            update_variables();
        }

        public void refresh_memory()
        {
            memorybox.Items.Clear();
            ListViewItem item;
            ListViewItem[] items = new ListViewItem[memory.Capacity];
            for (int i = 0; i < items.Length; i++)
            {
                memory[i] = "0";
                item = new ListViewItem($"{i.ToString("X")}", i);
                item.SubItems.Add($"{memory[i]}");
                items[i] = item;
                Application.DoEvents();
            }
            memorybox.Items.AddRange(items);
        }

        public int load_into_memory()
        {
            int for_pc;
            int start_location = for_pc = 0;
            string opcode = "";
            foreach (string line in codeEditor.Lines)
            {
                if (line == "")
                {
                    continue;
                }
                string[] word = line.ToLower().Split(' ');
                if (word[0] == "mov")
                {
                    if (word[1] == "a")
                    {
                        if (word[2] == "a")
                            opcode = "7F";
                        if (word[2] == "b")
                            opcode = "78";
                        if (word[2] == "c")
                            opcode = "79";
                        if (word[2] == "d")
                            opcode = "7A";
                        if (word[2] == "e")
                            opcode = "7B";
                        if (word[2] == "h")
                            opcode = "7C";
                        if (word[2] == "l")
                            opcode = "7D";
                        if (word[2] == "m")
                            opcode = "7E";
                    }
                    if (word[1] == "b")
                    {
                        if (word[2] == "a")
                            opcode = "47";
                        if (word[2] == "b")
                            opcode = "40";
                        if (word[2] == "c")
                            opcode = "41";
                        if (word[2] == "d")
                            opcode = "42";
                        if (word[2] == "e")
                            opcode = "43";
                        if (word[2] == "h")
                            opcode = "44";
                        if (word[2] == "l")
                            opcode = "45";
                        if (word[2] == "m")
                            opcode = "46";
                    }

                    if (word[1] == "c")
                    {
                        if (word[2] == "a")
                            opcode = "4F";
                        if (word[2] == "b")
                            opcode = "48";
                        if (word[2] == "c")
                            opcode = "49";
                        if (word[2] == "d")
                            opcode = "4A";
                        if (word[2] == "e")
                            opcode = "4B";
                        if (word[2] == "h")
                            opcode = "4C";
                        if (word[2] == "l")
                            opcode = "4D";
                        if (word[2] == "m")
                            opcode = "4E";
                    }

                    if (word[1] == "d")
                    {
                        if (word[2] == "a")
                            opcode = "57";
                        if (word[2] == "b")
                            opcode = "50";
                        if (word[2] == "c")
                            opcode = "51";
                        if (word[2] == "d")
                            opcode = "52";
                        if (word[2] == "e")
                            opcode = "53";
                        if (word[2] == "h")
                            opcode = "54";
                        if (word[2] == "l")
                            opcode = "55";
                        if (word[2] == "m")
                            opcode = "56";
                    }
                    if (word[1] == "e")
                    {
                        if (word[2] == "a")
                            opcode = "5F";
                        if (word[2] == "b")
                            opcode = "58";
                        if (word[2] == "c")
                            opcode = "59";
                        if (word[2] == "d")
                            opcode = "5A";
                        if (word[2] == "e")
                            opcode = "5B";
                        if (word[2] == "h")
                            opcode = "5C";
                        if (word[2] == "l")
                            opcode = "5D";
                        if (word[2] == "m")
                            opcode = "5E";
                    }
                    if (word[1] == "h")
                    {
                        if (word[2] == "a")
                            opcode = "67";
                        if (word[2] == "b")
                            opcode = "60";
                        if (word[2] == "c")
                            opcode = "61";
                        if (word[2] == "d")
                            opcode = "62";
                        if (word[2] == "e")
                            opcode = "63";
                        if (word[2] == "h")
                            opcode = "64";
                        if (word[2] == "l")
                            opcode = "65";
                        if (word[2] == "m")
                            opcode = "66";
                    }

                    if (word[1] == "l")
                    {
                        if (word[2] == "a")
                            opcode = "6F";
                        if (word[2] == "b")
                            opcode = "68";
                        if (word[2] == "c")
                            opcode = "69";
                        if (word[2] == "d")
                            opcode = "6A";
                        if (word[2] == "e")
                            opcode = "6B";
                        if (word[2] == "h")
                            opcode = "6C";
                        if (word[2] == "l")
                            opcode = "6D";
                        if (word[2] == "m")
                            opcode = "6E";
                    }
                    if (word[1] == "m")
                    {
                        if (word[2] == "a")
                            opcode = "77";
                        if (word[2] == "b")
                            opcode = "70";
                        if (word[2] == "c")
                            opcode = "71";
                        if (word[2] == "d")
                            opcode = "72";
                        if (word[2] == "e")
                            opcode = "73";
                        if (word[2] == "h")
                            opcode = "74";
                        if (word[2] == "l")
                            opcode = "75";
                    }
                    memory[start_location] = opcode;
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                }
                else if (word[0] == "mvi")
                {
                    if (word[1] == "a")
                        opcode = "3E";
                    if (word[1] == "b")
                        opcode = "6";
                    if (word[1] == "c")
                        opcode = "E";
                    if (word[1] == "d")
                        opcode = "16";
                    if (word[1] == "e")
                        opcode = "1E";
                    if (word[1] == "h")
                        opcode = "26";
                    if (word[1] == "l")
                        opcode = "2E";
                    if (word[1] == "m")
                        opcode = "36";

                    if (word[2].EndsWith("h"))
                    {
                        word[2] = word[2].Remove(word[2].Length - 1);
                    }
                    else
                    {
                        int data = Convert.ToInt32(word[2]);
                        word[2] = data.ToString("X");
                    }

                    memory[start_location] = opcode;
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                    memory[start_location] = word[2];
                    memorybox.Items[start_location].SubItems[1].Text = word[2];
                    start_location++;
                }
                else if (word[0] == "lxi")
                {
                    //not done yet
                }
                else if (word[0] == "ldax")
                {
                    //not done yet
                }
                else if (word[0] == "lda")
                {
                    opcode = "3A";
                    memory[start_location] = opcode;
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                    if (word[1].EndsWith("h"))
                        word[1] = word[1].Remove(word[1].Length - 1);
                    else
                    {
                        int data = Convert.ToInt32(word[1], 16);
                        word[1] = data.ToString("X");
                    }
                    word[1] = word[1].PadLeft(4, '0');
                    memory[start_location] = word[1].Substring(2, 2);
                    memorybox.Items[start_location].SubItems[1].Text = word[1].Substring(2, 2);
                    start_location++;
                    memory[start_location] = word[1].Substring(0, 2);
                    memorybox.Items[start_location].SubItems[1].Text = word[1].Substring(0, 2);
                    start_location++;
                }
                else if (word[0] == "sta")
                {
                    //not done yet
                }
                else if (word[0] == "lhld")
                {
                    //not done yet
                }
                else if (word[0] == "shld")
                {
                    //not done yet
                }
                else if (word[0] == "hlt")
                {
                    opcode = "76";
                    memory[start_location] = opcode;
                    memorybox.Items[start_location].SubItems[1].Text = opcode;
                    start_location++;
                }

            }
            return for_pc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            code_inspect();
        }

        private void code_inspect()
        {
            clear_registers();
            errors.Clear();
            output_box.Items.Clear();
            int error_level = 0;
            int line_number = 1;
            bool halt = false;
            string[] lines = codeEditor.Lines;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    line_number++;
                    continue;
                }
                if (line.ToLower() == "hlt")
                    halt = true;
                string[] code = line.Split(' ');
                string error_string = check_error(code);
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
                errors.Add("No end to the program detected, did you forget HLT?");
                error_level++;
            }
            if (error_level > 0)
            {
                foreach (string error in errors)
                {
                    output_box.Items.Add(error);
                }
            }
            else
            {
                pc.counter = load_into_memory();
                while (memory[pc.counter] != "76")
                {
                    code_execute(memory[pc.counter]);
                    pc.increment();
                    update_variables();
                }
                //code execute here
            }
        }

        private void code_execute(string hex)
        {
            if (hex == "CE") //ACI data8
            {

            }
            else if (hex == "8F") // ADC A
            {

            }
            else if (hex == "88") // ADC B
            {

            }
            else if (hex == "89") // ADC C
            {

            }
            else if (hex == "8A") // ADC D
            {

            }
            else if (hex == "8B") // ADC E
            {

            }
            else if (hex == "8C") // ADC H
            {

            }
            else if (hex == "8D") // ADC L
            {

            }
            else if (hex == "8E") // ADC M
            {

            }
            else if (hex == "87") // ADD A
            {

            }
            else if (hex == "80") // ADD B
            {

            }
            else if (hex == "81") // ADD C
            {

            }
            else if (hex == "82") // ADD D
            {

            }
            else if (hex == "83") // ADD E
            {

            }
            else if (hex == "84") // ADD H
            {

            }
            else if (hex == "85") // ADD L
            {

            }
            else if (hex == "86") // ADD M
            {

            }
            else if (hex == "C6") // ADI data8
            {

            }
            else if (hex == "A7") // ANA A
            {

            }
            else if (hex == "A0") // ANA B
            {

            }
            else if (hex == "A1") // ANA C
            {

            }
            else if (hex == "A2") // ANA D
            {

            }
            else if (hex == "A3") // ANA E
            {

            }
            else if (hex == "A4") // ANA H
            {

            }
            else if (hex == "A5") // ANA L
            {

            }
            else if (hex == "A6") // ANA M
            {

            }
            else if (hex == "E6") // ANI data8
            {

            }
            else if (hex == "CD") // CALL label16
            {

            }
            else if (hex == "DC") // CC label16
            {

            }
            else if (hex == "FC") // CM label16
            {

            }
            else if (hex == "2F") // CMA
            {

            }
            else if (hex == "3F") // CMC
            {

            }
            else if (hex == "BF") // CMP A
            {

            }
            else if (hex == "B8") // CMP B
            {

            }
            else if (hex == "B9") // CMP C
            {

            }
            else if (hex == "BA") // CMP D
            {

            }
            else if (hex == "BB") // CMP E
            {

            }
            else if (hex == "BC") // CMP H
            {

            }
            else if (hex == "BD") // CMP L
            {

            }
            else if (hex == "BE") // CMP M
            {

            }
            else if (hex == "D4") // CNC label16
            {

            }
            else if (hex == "C4") // CNZ label16
            {

            }
            else if (hex == "F4") // CP label16
            {

            }
            else if (hex == "EC") // CPE label16
            {

            }
            else if (hex == "FE") // CPI data8
            {

            }
            else if (hex == "E4") // CPO label16
            {

            }
            else if (hex == "CC") // CZ label16
            {

            }
            else if (hex == "27") // DAA
            {

            }
            else if (hex == "9") // DAD B [C]
            {

            }
            else if (hex == "19") // DAD D [E]
            {

            }
            else if (hex == "29")  // DAD H [L]
            {

            }
            else if (hex == "39") // DAD SP
            {

            }
            else if (hex == "3D") // DCR A
            {

            }
            else if (hex == "5") // DCR B
            {

            }
            else if (hex == "D") // DCR C
            {

            }
            else if (hex == "15") // DCR D
            {

            }
            else if (hex == "1D") // DCR E
            {

            }
            else if (hex == "25") // DCR H
            {

            }
            else if (hex == "2D") // DCR L
            {

            }
            else if (hex == "35") // DCR M
            {

            }
            else if (hex == "B") // DCX B [C]
            {

            }
            else if (hex == "1B") // DCX D [E]
            {

            }
            else if (hex == "2B") // DCX H [L]
            {

            }
            else if (hex == "3B") // DCX SP
            {

            }
            else if (hex == "F3") // DI
            {

            }
            else if (hex == "FB") // EI
            {

            }
            else if (hex == "76") // HLT
            {

            }
            else if (hex == "DB") // IN Port8
            {
                //useless
            }
            else if (hex == "3C") // INR A
            {

            }
            else if (hex == "4") // INR B
            {

            }
            else if (hex == "C") // INR C
            {

            }
            else if (hex == "14") // INR D
            {

            }
            else if (hex == "1C") // INR E
            {

            }
            else if (hex == "24") // INR H
            {

            }
            else if (hex == "2C") // INR L
            {

            }
            else if (hex == "34") // INR M
            {

            }
            else if (hex == "3") // INX B [C]
            {

            }
            else if (hex == "13") // INX D [E]
            {

            }
            else if (hex == "23") // INX H [L]
            {

            }
            else if (hex == "33") // INX SP
            {

            }
            else if (hex == "DA") // JC label16
            {

            }
            else if (hex == "FA") // JM label16
            {

            }
            else if (hex == "C3") // JMP label16
            {

            }
            else if (hex == "D2") // JNC label16
            {

            }
            else if (hex == "C2") // JNZ label16
            {

            }
            else if (hex == "F2") // JP label16
            {

            }
            else if (hex == "EA") // JPE label16
            {

            }
            else if (hex == "D2") // JPO label16
            {

            }
            else if (hex == "CA") // JZ label16
            {

            }
            else if (hex == "3A") // LDA address16
            {
                string addr = "";
                addr += memory[pc.counter + 2];
                addr += memory[pc.counter + 1];
                int index = Convert.ToInt32(addr,16);
                a.setData(memory[index]);
                pc.incrementBy(2);
            }
            else if (hex == "A") // LDAX B [C]
            {

            }
            else if (hex == "1A") // LDAX D [E]
            {

            }
            else if (hex == "2A") // LHLD address16
            {

            }
            else if (hex == "1") // LXI B [C]
            {

            }
            else if (hex == "11") // LXI D [E]
            {

            }
            else if (hex == "21") // LXI H [L]
            {

            }
            else if (hex == "31") // LXI SP
            {

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

            }
            else if (hex == "77") // MOV M A
            {

            }
            else if (hex == "70") // MOV M B
            {

            }
            else if (hex == "71") // MOV M C
            {

            }
            else if (hex == "72") // MOV M D
            {

            }
            else if (hex == "73") // MOV M E
            {

            }
            else if (hex == "74") // MOV M H
            {

            }
            else if (hex == "75") // MOV M L
            {

            }
            else if (hex == "3E") // MVI A data8
            {
                a.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "6") // MVI B data8
            {
                b.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "E") // MVI C data8
            {
                c.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "16") // MVI D data8
            {
                d.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "1E") // MVI E data8
            {
                e.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "26") // MVI H data8
            {
                h.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "2E") // MVI L data8
            {
                l.setData(memory[pc.counter + 1]);
                pc.incrementBy(1);
            }
            else if (hex == "36") // MVI M data8
            {

            }
            else if (hex == "0") // NOP
            {

            }
            else if (hex == "B7") // ORA A
            {

            }
            else if (hex == "B0") // ORA B
            {

            }
            else if (hex == "B1") // ORA C
            {

            }
            else if (hex == "B2") // ORA D
            {

            }
            else if (hex == "B3") // ORA E
            {

            }
            else if (hex == "B4") // ORA H
            {

            }
            else if (hex == "B5") // ORA L
            {

            }
            else if (hex == "B6") // ORA M
            {

            }
            else if (hex == "F6") // ORI data8
            {

            }
            else if (hex == "D3") // OUT port8
            {

            }
            else if (hex == "E9") // PCHL
            {

            }
            else if (hex == "C1") // POP B [C]
            {

            }
            else if (hex == "D1") // POP D [E]
            {

            }
            else if (hex == "E1") // POP H [L]
            {

            }
            else if (hex == "F1") // POP PSW
            {

            }
            else if (hex == "C5") // PUSH B [C]
            {

            }
            else if (hex == "D5") // PUSH D [E]
            {

            }
            else if (hex == "E5") // PUSH H [L]
            {

            }
            else if (hex == "F5") // PUSH PSW
            {

            }
            else if (hex == "17") // RAL
            {

            }
            else if (hex == "1F") // RAR
            {

            }
            else if (hex == "D8") // RC
            {

            }
            else if (hex == "C9") // RET
            {

            }
            else if (hex == "20") // RIM
            {

            }
            else if (hex == "7") // RLC
            {

            }
            else if (hex == "F8") // RM
            {

            }
            else if (hex == "D0") // RNC
            {

            }
            else if (hex == "C0") // RNZ
            {

            }
            else if (hex == "F0") // RP
            {

            }
            else if (hex == "E8") // RPE
            {

            }
            else if (hex == "E0") // RPO
            {

            }
            else if (hex == "F") // RRC
            {

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

            }
            else if (hex == "30") // SIM
            {

            }
            else if (hex == "F9") // SPHL
            {

            }
            else if (hex == "32") // STA address16
            {

            }
            else if (hex == "2") // STAX B [C]
            {

            }
            else if (hex == "12") // STAX D [E]
            {

            }
            else if (hex == "37") // STC
            {

            }
            else if (hex == "97") // SUB A
            {

            }
            else if (hex == "90") // SUB B
            {

            }
            else if (hex == "91") // SUB C
            {

            }
            else if (hex == "92") // SUB D
            {

            }
            else if (hex == "93") // SUB E
            {

            }
            else if (hex == "94") // SUB H
            {

            }
            else if (hex == "95") // SUB L
            {

            }
            else if (hex == "96") // SUB M
            {

            }
            else if (hex == "D6") // SUI data8
            {

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
                        code[1] == "m")
                        {
                            if (code[1] == "m" && code[2] == "m")
                                error_string = $"cannot do self assignment to \"m\"";
                        }
                        else
                            error_string = $"cannot identify \"{code[2]}\"";
                    }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                }
                else
                    error_string = "not enough parameter";

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
                        if (code[2].EndsWith("h") || code[2].EndsWith("H"))
                        {
                            code[2] = code[2].Remove(code[2].Length - 1);
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
                        error_string = $"cannot identify \"{code[1]}\"";
                }
                else
                    error_string = "not enough parameter";


            }  //done
            else if (code[0] == "add" || code[0] == "sub" || code[0] == "adc" || code[0] == "sbb" || code[0] == "inr" || code[0] == "dcr" || code[0] == "ana" || code[0] == "ora" || code[0] == "xra" || code[0] == "cmp")
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
                        error_string = $"cannot identify \"{code[1]}\"";
                else
                    error_string = error_string = "not enough parameter";

            } //done
            else if (code[0] == "adi" || code[0] == "aci" || code[0] == "sui" || code[0] == "sbi" || code[0] == "ani" || code[0] == "xri" || code[0] == "ori" || code[0] == "cpi")
            {
                if (code.Length > 1)
                    if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                    {
                        code[1] = code[1].Remove(code[1].Length - 1);
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
                            if (Int32.Parse(code[1]) > 255)
                                error_string = $"\"{code[1]}\" is not valid value";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                else
                    error_string = "not enough parameter";
            } //done
            else if (code[0] == "stc" || code[0] == "cma" || code[0] == "daa" || code[0] == "cmc" || code[0] == "rlc" || code[0] == "rrc" || code[0] == "ral" || code[0] == "rar" || code[0] == "ret" || code[0] == "rnz" || code[0] == "rz" || code[0] == "rnc" || code[0] == "rc" || code[0] == "rpo" || code[0] == "rpe" || code[0] == "rp" || code[0] == "rm" || code[0] == "pchl" || code[0] == "xthl" || code[0] == "sphl" || code[0] == "ei" || code[0] == "di" || code[0] == "rim" || code[0] == "sim" || code[0] == "nop" || code[0] == "hlt" || code[0] == "xchg") { } //done
            else if (code[0] == "lxi")
            {
                if (code.Length > 2)
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "sp")
                        if (code[2].EndsWith("h") || code[2].EndsWith("H"))
                        {
                            code[2] = code[2].Remove(code[2].Length - 1);
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
                        error_string = $"cannot identify \"{code[1]}\"";
                else
                    error_string = "not enough parameters";
            } //done
            else if (code[0] == "ldax" || code[0] == "stax")
            {
                if (code.Length > 1)
                    if (code[1] == "b" ||
                        code[1] == "d") { }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                else
                    error_string = "not enough parameters";
            } //done
            else if (code[0] == "lda" || code[0] == "sta" || code[0] == "lhld" || code[0] == "shld")
            {
                if (code.Length > 1)
                    if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                        try
                        {
                            code[1] = code[1].Remove(code[1].Length - 1);
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
                            if (Int32.Parse(code[1]) > 65335)
                                error_string = $"\"{code[1]}\" is not valid value";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                else
                    error_string = "not enough parameters";
            } //done
            else if (code[0] == "dad" || code[0] == "inx" || code[0] == "dcx")
            {
                if (code.Length > 1)
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "sp") { }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                else
                    error_string = "not enough parameters";
            } //done
            else if (code[0] == "jmp" || code[0] == "jnz" || code[0] == "jz" || code[0] == "jnc" || code[0] == "jc" || code[0] == "jpo" || code[0] == "jpe" || code[0] == "jp" || code[0] == "jm" || code[0] == "call" || code[0] == "cnz" || code[0] == "cz" || code[0] == "cnc" || code[0] == "cc" || code[0] == "cpo" || code[0] == "cpe" || code[0] == "cp" || code[0] == "cm")
            {
                if (code.Length > 1)
                    if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                        try
                        {
                            code[1] = code[1].Remove(code[1].Length - 1);
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
                            if (Int32.Parse(code[1]) > 65535)
                                error_string = $"\"{code[1]}\" is not valid value";
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                else
                    error_string = "not enough parameters";
            }
            else if (code[0] == "rst")
            {
                if (code.Length > 1)
                    try
                    {
                        if (Convert.ToInt32(code[1], 16) > 7 || Convert.ToInt32(code[1], 16) < 1)
                            error_string = $"\"{code[1]}\" is not valid value";
                    }
                    catch
                    {
                        error_string = $"\"{code[1]}\" is not valid value";
                    }
                else
                    error_string = "not enough parameters";
            }
            else if (code[0] == "push" || code[0] == "pop")
            {
                if (code.Length > 1)
                    if (code[1] == "b" ||
                        code[1] == "d" ||
                        code[1] == "h" ||
                        code[1] == "psw") { }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                else
                    error_string = "not enough parameters";
            }
            else if (code[0].StartsWith(";")) { } //done
            else
                error_string = $"\"{code[0]}\" is incompatible instruction";

            return error_string;
        }

        private void code_editor_key_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Back)
            {
                lines_indicator.Text = "";
                for (int i = 0; i < codeEditor.Lines.Length; i++)
                    lines_indicator.Text += $"{i + 1}{Environment.NewLine}";
                lines_indicator.SelectionStart = lines_indicator.Text.Length;
                lines_indicator.ScrollToCaret();
            }
        }

        private void closing(object sender, FormClosingEventArgs e)
        {

        }

        private void loading(object sender, EventArgs e)
        {
            errors = new List<string>();
            memory = new List<string>(new string[65535]);
            sp = new Stack<int>();
            pc = new ProgramCounter();
            lines_indicator.Font = codeEditor.Font;
            refresh_memory();
            update_variables();
        }

        private void find_address_Click(object sender, EventArgs e)
        {
            ListViewItem f = null;
            f = memorybox.FindItemWithText(address_to_find.Text);
            if (f != null)
            {
                memorybox.TopItem = f;
            }
        }

        //Menu section

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = codeEditor.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                codeEditor.Font = fd.Font;
                lines_indicator.Font = fd.Font;
            }
        }

        private void clearMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh_memory();
        }

        private void clearRegistersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_registers();
        }

        private void clearEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditor.Clear();
        }



        //key events

        private void address_box_press(object sender, KeyPressEventArgs e)
        {
            if (memorybox.SelectedItems.Count > 0)
            {
                if (e.KeyChar >= (char)Keys.D0 && e.KeyChar <= (char)Keys.D9)
                {
                    memorybox.SelectedItems[0].SubItems[1].Text += e.KeyChar;
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)Keys.Return)
                {
                    int data = Convert.ToInt32(memorybox.SelectedItems[0].SubItems[1].Text);
                    memory[Convert.ToInt32(memorybox.SelectedItems[0].SubItems[0].Text,16)] = memorybox.SelectedItems[0].SubItems[1].Text = data.ToString("X");
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)Keys.Back)
                {
                    if (memorybox.SelectedItems[0].SubItems[1].Text.Length > 0)
                        memorybox.SelectedItems[0].SubItems[1].Text = memorybox.SelectedItems[0].SubItems[1].Text.Remove(memorybox.SelectedItems[0].SubItems[1].Text.Length - 1);
                }
                else
                    e.Handled = false;
            }
            
        }

    }
}
