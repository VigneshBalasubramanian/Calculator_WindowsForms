using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator_WindowsForms
{
    public partial class form_Calculator : Form
    {
        Double Number = 0.0;
        String Operator = "";
        bool infinity_Check = false;
        bool is_Number = false;
        int operator_Count = 0;
        int Equal_Pressed = 0;
        Double buffer = 0.0;
        Double repeat_Operand = 0.0;
        string root_Value = "";
        bool is_Operator = false;
        bool is_Operation = false;
        bool is_Sqrt = false;
        bool is_Replaced_Operator = false;
        bool is_Repeat = false;

        public form_Calculator()
        {
            InitializeComponent();
            AcceptButton = button_Equals_To;                    // Enter Key press //
            Button enter_Key = (Button)this.AcceptButton;
            enter_Key.Select();
        }

        private void button_Number_Click(object sender, EventArgs e)
        {
            button_Decimal.Enabled = true;
            button_Add.Enabled = true;           
            button_Divide.Enabled = true;
            button_Subtract.Enabled = true;
            button_Multiply.Enabled = true;
            button_Sq_Root.Enabled = true;
            infinity_Check = false;            
            is_Repeat = false;            
            is_Replaced_Operator = false;
            is_Sqrt = false;

            if ((text_Display.Text == "0") || (is_Operation) || (is_Operator))
                text_Display.Text = "";
            is_Operator = false;


            if (is_Operation == true && is_Number == false && operator_Count == 0)
                Number = 0;
            operator_Count = 0;
            is_Operation = false;                                      
            
            Button input_Value = (Button)sender;
            
            if (input_Value.Text == ".")
            {
                if(!text_Display.Text.Contains("."))
                    if(text_Display.Text == "")
                        text_Display.Text = "0" + input_Value.Text;
                    else
                        text_Display.Text = text_Display.Text + input_Value.Text;
            }
            else
                text_Display.Text = text_Display.Text + input_Value.Text;
            repeat_Operand = Double.Parse(text_Display.Text);
            button_Equals_To.Focus();
            Equal_Pressed = 0;
        }

        private void button_Clear_Entry(object sender, EventArgs e)
        {
            text_Display.Text = "0";
            button_Equals_To.Focus();
        }

        private void button_Operator_Click(object sender, EventArgs e)
        {            
            Button input_Operator = (Button)sender;
            operator_Count++;
            is_Repeat = false;
            Equal_Pressed = 0;
            is_Number = true;            

            if (operator_Count > 1 && is_Sqrt == false && input_Operator.Text != "√" && (!is_Operation))
            {
                label_Full_Operation.Text = label_Full_Operation.Text.Substring(0, label_Full_Operation.Text.Length - 2);
                is_Replaced_Operator = true;
            }

            if (operator_Count > 1 && is_Sqrt == false && input_Operator.Text != "√" && (!is_Replaced_Operator))
            {
                label_Full_Operation.Text = label_Full_Operation.Text.Substring(0, label_Full_Operation.Text.Length - 2);
                is_Replaced_Operator = true;
            }
            
            if (Number != 0)
            {
                if(is_Sqrt == true)
                    label_Full_Operation.Text = label_Full_Operation.Text.Substring(0, label_Full_Operation.Text.Length - root_Value.Length - 3);
                if (input_Operator.Text == "√")
                {
                    root_Value = text_Display.Text;
                    label_Full_Operation.Text = label_Full_Operation.Text + " " + ("√" + "(" + (text_Display.Text) + ")").ToString();
                    text_Display.Text = (Math.Sqrt(Double.Parse(text_Display.Text))).ToString();﻿                    
                    is_Sqrt = true;
                }
                else
                {
                    if(!is_Replaced_Operator && !is_Operation)
                        button_Equals_To.PerformClick();
                    is_Operator = true;
                    Operator = input_Operator.Text;
                    label_Full_Operation.Text = Number + " " + Operator;
                    is_Sqrt = false;
                }
            }
            else if (input_Operator.Text == "√")
            {
                if (is_Sqrt == true)
                    label_Full_Operation.Text = label_Full_Operation.Text.Substring(0, label_Full_Operation.Text.Length - root_Value.Length - 3); 
                label_Full_Operation.Text = label_Full_Operation.Text + " " + ("√" + "(" + (text_Display.Text) + ")").ToString();
                text_Display.Text = (Math.Sqrt(Double.Parse(text_Display.Text))).ToString();
                is_Sqrt = true;
            }
            else
            {
                Operator = input_Operator.Text;
                Number = Double.Parse(text_Display.Text);
                is_Operator = true;
                label_Full_Operation.Text = Number + " " + Operator;
            }
            buffer = Double.Parse(text_Display.Text);
            button_Equals_To.Focus();
        }

        private void button_Equals(object sender, EventArgs e)
        {
            this.button_Equals_To.DialogResult = System.Windows.Forms.DialogResult.OK;
            label_Full_Operation.Text = "";
            Equal_Pressed++;
            if (infinity_Check == true)
            {
                text_Display.Text = "0";
                button_Decimal.Enabled = true;
                button_Add.Enabled = true;
                button_Divide.Enabled = true;
                button_Subtract.Enabled = true;
                button_Multiply.Enabled = true;
                button_Sq_Root.Enabled = true;
            }            
            
            else
            {
                
                if (Double.Parse(text_Display.Text) == buffer && is_Sqrt == false)
                    is_Repeat = true;
                switch (Operator)
                {
                    case "+":
                        if (is_Repeat == true)
                            text_Display.Text = (Double.Parse(text_Display.Text) + buffer).ToString();
                        else if (Equal_Pressed > 1)
                            text_Display.Text = (repeat_Operand + Double.Parse(text_Display.Text)).ToString();
                        else
                            text_Display.Text = (Number + Double.Parse(text_Display.Text)).ToString();
                        break;
                    case "-":
                        if (is_Repeat == true)
                            text_Display.Text = (Double.Parse(text_Display.Text) - buffer).ToString();
                        else if (Equal_Pressed > 1)
                            text_Display.Text = (repeat_Operand - Double.Parse(text_Display.Text)).ToString();
                        else
                            text_Display.Text = (Number - Double.Parse(text_Display.Text)).ToString();
                        break;
                    case "*":
                        if (is_Repeat == true)
                            text_Display.Text = (Double.Parse(text_Display.Text) * buffer).ToString();
                        else if (Equal_Pressed > 1)
                            text_Display.Text = (repeat_Operand * Double.Parse(text_Display.Text)).ToString();
                        else
                            text_Display.Text = (Number * Double.Parse(text_Display.Text)).ToString();
                        break;
                    case "/":
                        if (is_Repeat == true)
                            text_Display.Text = (Double.Parse(text_Display.Text) / buffer).ToString();
                        else if (Equal_Pressed > 1)
                            text_Display.Text = (repeat_Operand / Double.Parse(text_Display.Text)).ToString();
                        else
                            text_Display.Text = (Number / Double.Parse(text_Display.Text)).ToString();
                        break;
                    default:
                        break;
                }
            }
            if (text_Display.Text == "∞")
            {
                text_Display.Text = "Cannot divide by zero";
                button_Decimal.Enabled = false;
                button_Add.Enabled = false;
                button_Divide.Enabled = false;
                button_Subtract.Enabled = false;
                button_Multiply.Enabled = false;                
                button_Sq_Root.Enabled = false;
                infinity_Check = true;
            }
            else if (text_Display.Text == "NaN")
            {
                text_Display.Text = "Result is undefined";
                button_Decimal.Enabled = false;
                button_Add.Enabled = false;
                button_Divide.Enabled = false;
                button_Subtract.Enabled = false;
                button_Multiply.Enabled = false;
                button_Sq_Root.Enabled = false;
                infinity_Check = true;
            }
            else
            {
                if (infinity_Check == true)
                {
                    text_Display.Text = "0";
                    Number = Double.Parse(text_Display.Text);
                    button_Decimal.Enabled = true;
                    button_Add.Enabled = true;
                    button_Divide.Enabled = true;
                    button_Subtract.Enabled = true;
                    button_Multiply.Enabled = true;
                    button_Sq_Root.Enabled = true;
                    infinity_Check = false;
                }
                else
                    Number = Double.Parse(text_Display.Text);
            }            
            //is_Operator = true;
            is_Sqrt = false;
            is_Number = false;
            is_Operation = true;                        
        }

        private void button_Clear(object sender, EventArgs e)
        {
            text_Display.Text = "0";
            label_Full_Operation.Text = "";
            Number = 0.0;
            button_Equals_To.Focus();
        }

        private void keypress_Calculator(object sender, KeyPressEventArgs e)
        {            
            switch (e.KeyChar.ToString())
            {
                case "0":
                    button_0.PerformClick();
                    break;
                case "1":
                    button_1.PerformClick();
                    break;
                case "2":
                    button_2.PerformClick();
                    break;
                case "3":
                    button_3.PerformClick();
                    break;
                case "4":
                    button_4.PerformClick();
                    break;
                case "5":
                    button_5.PerformClick();
                    break;
                case "6":
                    button_6.PerformClick();
                    break;
                case "7":
                    button_7.PerformClick();
                    break;
                case "8":
                    button_8.PerformClick();
                    break;
                case "9":
                    button_9.PerformClick();
                    break;
                case ".":
                    button_Decimal.PerformClick();
                    break;
                case "+":
                    button_Add.PerformClick();                    
                    break;
                case "-":
                    button_Subtract.PerformClick();                    
                    break;
                case "*":
                    button_Multiply.PerformClick();                    
                    break;
                case "/":
                    button_Divide.PerformClick();                    
                    break;
                case "=":
                    button_Equals_To.PerformClick();
                    break;
                default:
                    break;
            }            
        }

        private void keydown_Calculator(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                text_Display.Text = "0";
                label_Full_Operation.Text = "";
                Number = 0.0;
                button_Equals_To.Focus();
            }
        }
    }
}
