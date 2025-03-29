using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = "";      // To hold the current input (number/operator)
        private double result = 0;             // To hold the result of the operations
        private string currentOperator = "";  // To hold the current operator
        private bool operatorPressed = false; // To prevent appending multiple operators

        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for number button clicks (1, 2, 3, 4, etc.)
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string buttonContent = button.Content.ToString();
                if (operatorPressed)
                {
                    // Clear the current input if an operator was pressed
                    currentInput = "";
                    operatorPressed = false;
                }

                // Append the pressed button's content (number) to currentInput
                if (!(buttonContent == "0" && currentInput == "0"))  // Prevent appending multiple zeros
                {
                    currentInput += buttonContent;
                }

                Operation.Text = currentInput;
            }
        }

        // Event handler for operator button clicks (+, -, *, /)
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string operatorSymbol = button.Content.ToString();

                // If we already have an operator, calculate the previous result
                if (currentOperator != "")
                {
                    CalculateResult();
                }

                currentOperator = operatorSymbol;
                result = double.Parse(currentInput);
                operatorPressed = true;
                currentInput = "";  // Clear current input for the next number
            }
        }

        // Event handler for equals button click (=)
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentOperator != "" && currentInput != "")
            {
                CalculateResult();
                Operation.Text = result.ToString();
                currentInput = result.ToString();  // Allow continuing from the result
                currentOperator = ""; // Reset operator
            }
        }

        // Method to perform calculations based on the operator
        private void CalculateResult()
        {
            // Check if currentInput is empty. If it is, treat it as 0 for calculation
            if (string.IsNullOrEmpty(currentInput))
            {
                currentInput = "0";  // Treat empty input as zero
            }

            // Now safely parse the currentInput into a double
            if (double.TryParse(currentInput, out double num))
            {
                // Perform the calculation based on the operator
                switch (currentOperator)
                {
                    case "+":
                        result += num;
                        break;
                    case "-":
                        result -= num;
                        break;
                    case "X":
                        result *= num;
                        break;
                    case "/":
                        if (num != 0)
                        {
                            result /= num;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero!");
                        }
                        break;
                    default:
                        break;
                }

                // Display the result
                Operation.Text = result.ToString();
                currentInput = result.ToString();  // Allow continuing from the result
                currentOperator = ""; // Reset operator
            }
            else
            {
                MessageBox.Show("Invalid input!");
            }
        }

        // Clear button logic (reset all)
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            currentInput = "";
            currentOperator = "";
            Operation.Text = "0";
        }
    }
}