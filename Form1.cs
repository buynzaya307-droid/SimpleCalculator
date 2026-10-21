using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        private double resultValue = 0;
        private string operationPerformed = "";
        private bool isOperationPerformed = false;
        private double memoryValue = 0;

        public Form1()
        {
            InitializeComponent();
            AttachEvents(); // Товчлууруудыг автоматаар холбох функц
        }

        private void AttachEvents()
        {
            // Дизайнер дээрх бүх товчлууруудыг шалгаад текрээр нь холбох
            foreach (Control c in this.Controls)
            {
                if (c is Button btn)
                {
                    if (btn.Text == "+" || btn.Text == "-" || btn.Text == "x" || btn.Text == "÷")
                    {
                        btn.Click += new EventHandler(Operator_Click);
                    }
                    else if (btn.Text == "=")
                    {
                        btn.Click += new EventHandler(BTN_EQUAL_Click);
                    }
                    else if (btn.Text == "C")
                    {
                        btn.Click += new EventHandler(BTN_C_Click);
                    }
                    else if (btn.Text == "CE")
                    {
                        btn.Click += new EventHandler(BTN_CE_Click);
                    }
                    else if (btn.Text.Contains("Back") || btn.Text.Contains("Space"))
                    {
                        btn.Click += new EventHandler(BTN_BackSpace_Click);
                    }
                    else if (btn.Text == "MC")
                    {
                        btn.Click += new EventHandler(BTN_MC_Click);
                    }
                    else if (btn.Text == "MR")
                    {
                        btn.Click += new EventHandler(BTN_MR_Click);
                    }
                    else if (btn.Text == "MS")
                    {
                        btn.Click += new EventHandler(BTN_MS_Click);
                    }
                    else if (btn.Text == "M+")
                    {
                        btn.Click += new EventHandler(BTN_MPlus_Click);
                    }
                    else if (btn.Text != "") // Тоонууд (0-9) болон цэг (.)
                    {
                        btn.Click += new EventHandler(Button_Click);
                    }
                }
            }
        }

        // 1. Тоо болон цэг дарагдах үед
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if ((TXT_Current.Text == "0") || (isOperationPerformed))
                TXT_Current.Clear();

            isOperationPerformed = false;

            if (button.Text == ".")
            {
                if (!TXT_Current.Text.Contains("."))
                    TXT_Current.Text += button.Text;
            }
            else
            {
                TXT_Current.Text += button.Text;
            }
        }

        // 2. Үйлдэл (+, -, x, ÷) дарагдах үед
        private void Operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0)
            {
                BTN_EQUAL_Click(null, null);
                operationPerformed = button.Text;
                TXT_previous.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                operationPerformed = button.Text;
                double.TryParse(TXT_Current.Text, out resultValue);
                TXT_previous.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
        }

        // 3. C товчлуур (Бүхнийг цэвэрлэх)
        private void BTN_C_Click(object sender, EventArgs e)
        {
            TXT_Current.Text = "0";
            TXT_previous.Text = "";
            resultValue = 0;
            operationPerformed = "";
        }

        // 4. CE товчлуур (Одоогийн утгыг цэвэрлэх)
        private void BTN_CE_Click(object sender, EventArgs e)
        {
            TXT_Current.Text = "0";
        }

        // 5. BackSpace товчлуур (Сүүлийн үсгийг устгах)
        private void BTN_BackSpace_Click(object sender, EventArgs e)
        {
            if (TXT_Current.Text.Length > 0)
            {
                TXT_Current.Text = TXT_Current.Text.Remove(TXT_Current.Text.Length - 1, 1);
            }

            if (TXT_Current.Text == "")
            {
                TXT_Current.Text = "0";
            }
        }

        // 6. = (Тэнцүү) товчлуур
        private void BTN_EQUAL_Click(object sender, EventArgs e)
        {
            double currentValue = 0;
            double.TryParse(TXT_Current.Text, out currentValue);

            switch (operationPerformed)
            {
                case "+":
                    TXT_Current.Text = (resultValue + currentValue).ToString();
                    break;
                case "-":
                    TXT_Current.Text = (resultValue - currentValue).ToString();
                    break;
                case "x":
                    TXT_Current.Text = (resultValue * currentValue).ToString();
                    break;
                case "÷":
                    if (currentValue != 0)
                        TXT_Current.Text = (resultValue / currentValue).ToString();
                    else
                        TXT_Current.Text = "Error";
                    break;
                default:
                    break;
            }

            double.TryParse(TXT_Current.Text, out resultValue);
            TXT_previous.Text = "";
            operationPerformed = "";
        }

        // 7. Санах ойн товчлуурууд (MC, MR, MS, M+)
        private void BTN_MC_Click(object sender, EventArgs e)
        {
            memoryValue = 0;
        }

        private void BTN_MR_Click(object sender, EventArgs e)
        {
            TXT_Current.Text = memoryValue.ToString();
        }

        private void BTN_MS_Click(object sender, EventArgs e)
        {
            double.TryParse(TXT_Current.Text, out memoryValue);
        }

        private void BTN_MPlus_Click(object sender, EventArgs e)
        {
            double currentValue = 0;
            double.TryParse(TXT_Current.Text, out currentValue);
            memoryValue += currentValue;
        }
    }
}