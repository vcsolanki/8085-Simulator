using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 
 * have to do flag resgisters work
 * 
 * add more instructions
 * 
 * add HL pair support in instruction
 * 
 */

namespace _8085_Simulator
{
    public partial class main : Form
    {
        private List<string> errors;


        //-----------------------------

        //accumalator
        
        private byte a = new byte();

        //common registers

        private byte b = new byte();
        private byte c = new byte();
        private byte d = new byte();
        private byte e = new byte();

        //reference registers

        private byte h = new byte();
        private byte l = new byte();

        //stacks and counter

        private byte[] pc = new byte[2];
        private byte[] sp = new byte[2];

        //flags

        private bool sign = new bool();
        private bool zero = new bool();
        private bool auxilary = new bool();
        private bool parity = new bool();
        private bool carry = new bool();

        //-----------------------------

        // memory

        private char[,] memroy = new char[4096,4];

        //-----------------------------

        public main()
        {
            InitializeComponent();
            errors = new List<string>();
        }

        public void update_variables()
        {
            areg.Text = a.ToString("X");
            breg.Text = b.ToString("X");
            creg.Text = c.ToString("X");
            dreg.Text = d.ToString("X");
            ereg.Text = e.ToString("X");
            hreg.Text = h.ToString("X");
            lreg.Text = l.ToString("X");
            flagc.Text = carry ? "1" : "0";
            flagz.Text = zero ? "1" : "0";
            flags.Text = sign ? "1" : "0";
            flaga.Text = auxilary ? "1" : "0";
            flagp.Text = parity ? "1" : "0";
        }

        public void clear_registers()
        {
            a = b = c = d = e = h = l = 0;
            carry = zero = sign = auxilary = parity = false;
            update_variables();
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
            string[] lines = codeEditor.Lines;
            foreach(string line in lines)
            {
                if (line == "")
                {
                    line_number++;
                    continue;
                }
                string[] code = line.Split(' ');
                string error_string = check_error(code);
                if(error_string!="")
                {
                    if(error_level>=50)
                        break;
                    errors.Add($"{error_string} on line {line_number}!");
                    error_level++;
                }
                line_number++;
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
                line_number = 1;
                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        line_number++;
                        continue;
                    }
                    code_execute(line.Split(' '));
                }
            }
        }

        private void code_execute(string[] code)
        {
            for (int i = 0; i < code.Length; i++)
                code[i] = code[i].ToLower();

            if (code[0] == "mov")
                mov(code);
            else if (code[0] == "mvi")
                mvi(code);
            else if (code[0] == "add")
                add(code);
            else if (code[0] == "sub")
                sub(code);
            else if (code[0] == "adi")
                adi(code);
            else if (code[0] == "stc")
                stc();
            update_variables();
        }

        private void mov(string[] code)
        {
            if(code[1]=="a")
            {
                if (code[2] == "b")
                    a = b;
                if (code[2] == "c")
                    a = c;
                if (code[2] == "d")
                    a = d;
                if (code[2] == "e")
                    a = e;
                if (code[2] == "h")
                    a = h;
                if (code[2] == "l")
                    a = l;
            }
            if (code[1] == "b")
            {
                if (code[2] == "a")
                    b = a;
                if (code[2] == "c")
                    b = c;
                if (code[2] == "d")
                    b = d;
                if (code[2] == "e")
                    b = e;
                if (code[2] == "h")
                    b = h;
                if (code[2] == "l")
                    b = l;
            }
            if (code[1] == "c")
            {
                if (code[2] == "b")
                    c = b;
                if (code[2] == "a")
                    c = a;
                if (code[2] == "d")
                    c = d;
                if (code[2] == "e")
                    c = e;
                if (code[2] == "h")
                    c = h;
                if (code[2] == "l")
                    c = l;
            }
            if (code[1] == "d")
            {
                if (code[2] == "b")
                    d = b;
                if (code[2] == "c")
                    d = c;
                if (code[2] == "a")
                    d = a;
                if (code[2] == "e")
                    d = e;
                if (code[2] == "h")
                    d = h;
                if (code[2] == "l")
                    d = l;
            }
            if (code[1] == "e")
            {
                if (code[2] == "b")
                    e = b;
                if (code[2] == "c")
                    e = c;
                if (code[2] == "d")
                    e = d;
                if (code[2] == "a")
                    e = a;
                if (code[2] == "h")
                    e = h;
                if (code[2] == "l")
                    e = l;
            }
            if (code[1] == "h")
            {
                if (code[2] == "b")
                    h = b;
                if (code[2] == "c")
                    h = c;
                if (code[2] == "d")
                    h = d;
                if (code[2] == "a")
                    h = a;
                if (code[2] == "e")
                    h = e;
                if (code[2] == "l")
                    h = l;
            }
            if (code[1] == "l")
            {
                if (code[2] == "b")
                    l = b;
                if (code[2] == "c")
                    l = c;
                if (code[2] == "d")
                    l = d;
                if (code[2] == "a")
                    l = a;
                if (code[2] == "h")
                    l = h;
                if (code[2] == "e")
                    l = e;
            }
        }
        private void mvi(string[] code)
        {
            if (code[2].EndsWith("h") || code[2].EndsWith("H"))
            {
                code[2] = code[2].Remove(code[2].Length - 1);
                if (code[1] == "a")
                    a = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "b")
                    b = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "c")
                    c = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "d")
                    d = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "e")
                    e = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "h")
                    h = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
                if (code[1] == "l")
                    l = byte.Parse(Convert.ToInt32(code[2], 16).ToString());
            }
            else
            {
                if (code[1] == "a") 
                    a = byte.Parse(code[2]);
                if (code[1] == "b")
                    b = byte.Parse(code[2]);
                if (code[1] == "c")
                    c = byte.Parse(code[2]);
                if (code[1] == "d")
                    d = byte.Parse(code[2]);
                if (code[1] == "e")
                    e = byte.Parse(code[2]);
                if (code[1] == "h")
                    h = byte.Parse(code[2]);
                if (code[1] == "l")
                    l = byte.Parse(code[2]);
            }
        }
        private void add(string[] code)
        {
            int sum = 0;
            if (code[1] == "b")
            {
                sum = a + b;
                a = byte.Parse(sum.ToString());
                //if (sum < a || sum < b)
                //    carry = true;
            }
            if (code[1] == "c")
            {
                sum = a + c;
                a = byte.Parse(sum.ToString());
            }
            if (code[1] == "d")
            {
                sum = a + d;
                a = byte.Parse(sum.ToString());
            }
            if (code[1] == "e")
            {
                sum = a + e;
                a = byte.Parse(sum.ToString());
            }
        }
        private void sub(string[] code)
        {
            int sub = 0;
            if (code[1] == "b")
            {
                sub = a - b;
                a = byte.Parse(sub.ToString());
            }
            if (code[1] == "c")
            {
                sub = a - c;
                a = byte.Parse(sub.ToString());
            }
            if (code[1] == "d")
            {
                sub = a - d;
                a = byte.Parse(sub.ToString());
            }
            if (code[1] == "e")
            {
                sub = a - e;
                a = byte.Parse(sub.ToString());
            }
        }
        private void adi(string[] code)
        {
            if (code[1].EndsWith("h") || code[1].EndsWith("H"))
            {
                code[1] = code[1].Remove(code[1].Length - 1);
                a = byte.Parse(Convert.ToInt32(code[1], 16).ToString());
            }
            else
                    a = byte.Parse(code[1]);
        }
        private void stc()
        {
            carry = true;
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
                        code[1] == "l")
                    {
                        if (code[2] == "a" ||
                        code[2] == "b" ||
                        code[2] == "c" ||
                        code[2] == "d" ||
                        code[2] == "e" ||
                        code[2] == "h" ||
                        code[2] == "l")
                        {

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
                        code[1] == "l")
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
            else if (code[0] == "add")
            {
                if (code.Length > 1)
                {
                    if (code[1] == "b" ||
                        code[1] == "c" ||
                        code[1] == "d" ||
                        code[1] == "e" ||
                        code[1] == "h" ||
                        code[1] == "l")
                    {

                    }
                    else if (code[1] == "a")
                    {
                        error_string = $"You cant add \"a\" itself";
                    }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                }
                else
                    error_string = error_string = "not enough parameter";

            } //done
            else if (code[0] == "sub")
            {
                if (code.Length > 1)
                {
                    if (code[1] == "a" ||
                        code[1] == "b" ||
                        code[1] == "c" ||
                        code[1] == "d" ||
                        code[1] == "e" ||
                        code[1] == "h" ||
                        code[1] == "l")
                    {

                    }
                    else if (code[1] == "a")
                    {
                        error_string = $"You cant sub \"a\" itself";
                    }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                }
                else
                    error_string = error_string = "not enough parameter";

            } //done
            else if (code[0] == "adi")
            {
                if (code.Length > 1)
                {
                    if (code[1].EndsWith("h") || code[1].EndsWith("H"))
                    {
                        code[1] = code[1].Remove(code[1].Length - 1);
                        try
                        {
                            Convert.ToInt32(code[1], 16);
                            if (code[1].Length > 2)
                            {
                                error_string = $"\"{code[1]}\" is not valid value";
                            }
                        }
                        catch
                        {
                            error_string = $"\"{code[1]}\" is not valid value";
                        }
                    }
                    else
                        error_string = $"cannot identify \"{code[1]}\"";
                }
                else
                    error_string = "not enough parameter";
            } //done
            else if (code[0] == "stc") { } //done
            else
                error_string = $"\"{code[0]}\" is incompatible instruction";

            return error_string;
        }

        private void code_editor_key_press(object sender, KeyPressEventArgs e)
        {
            lines_indicator.Text = "";
            for (int i = 0; i < codeEditor.Lines.Length; i++)
                lines_indicator.Text += $"{i+1}{Environment.NewLine}";
            lines_indicator.SelectionStart = lines_indicator.Text.Length;
            lines_indicator.ScrollToCaret();
        }
    }
}
