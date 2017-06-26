using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string operatorPressed = "";
        bool operatorIsClicked = false; 
        double number1,number2,result;
        bool isDot = false;     //to check if there is already a dot


        private void OnNumberClicked(object sender, EventArgs e)
        {
            Clear.Text = "C";
            Button button = (Button)sender;
            if (Result.Text == "0" || operatorIsClicked) // if user clicked operator or cleaned the number, he'll write totally new one
            {
                Result.Text = button.Text;
                operatorIsClicked = false;
                isDot = false;
            }
            else Result.Text += button.Text;
        }

        private void Dot_Clicked(object sender, EventArgs e)
        {
            if (!(isDot))
            {
                Result.Text += Dot.Text;
                isDot = true;
            }

        }

        private void Clear_Clicked(object sender, EventArgs e)
        {  
            if (Clear.Text == "AC")
            {
                number1 = 0;
                number2 = 0;
                operatorIsClicked = false;
                operatorPressed = "";
                isDot = false;
            }
            else 
            {
                Result.Text = "0";
                Clear.Text = "AC";
            }
        }

        private void Sign_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Result.Text, out result))
                Result.Text = ((-1) * result).ToString();

        }

        private void Percent_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Result.Text, out result))
                Result.Text = (result / 100).ToString();
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (Result.Text == "0" && button.Text == "-") // so that user can enter negative number
            {
                Result.Text = button.Text;
                Clear.Text = "C";
            }
            else
            {
                if (operatorIsClicked) // if true, the user didn't enter new number, so he want just to change operator
                    operatorPressed = "";
                operatorIsClicked = true; // enable the possibility to enter new number
                if (double.TryParse(Result.Text, out result)) // check if it is the first operator
                {
                    if (operatorPressed == "")
                    {
                        operatorPressed = button.Text;
                        number1 = result;
                    }
                    else // if not, the program calculates the previous one and save it in number1
                    {
                        number2 = result;
                        number1 = Calculate(number1, number2, operatorPressed);
                        Result.Text = number1.ToString();
                        operatorPressed = button.Text;
                    }

                }
            }

        }

        private void Equals_Clicked(object sender, EventArgs e)
        {
            if(double.TryParse(Result.Text, out number2) && !(operatorPressed=="") && !(operatorIsClicked))//if true, the user didn't enter the second number that program need for calculating
            {
                result = Calculate(number1, number2, operatorPressed);
                Result.Text = result.ToString();
                operatorPressed = "";
                number1 = 0;
                number2 = 0;
                operatorIsClicked = false;
                if (result - (int)result == 0)
                    isDot = false;
                else isDot = true;
            }
        }

        private double Calculate(double num1,double num2, string operation)
        {
            double output = 0;

            switch (operation)
            {
                case "-":
                    output = num1 - num2;
                    break;
                case "+":
                    output = num1 + num2;
                    break;
                case "÷":
                    output = num1 / num2;
                    break;
                case "×":
                    output = num1 * num2;
                    break;
            }
            return output;
        }
     
    }
}
