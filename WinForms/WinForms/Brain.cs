using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms
{
    public delegate void DisplayMessage(string text);

    public class Brain
    {
        public DisplayMessage displayMessage;
        public Brain(DisplayMessage displayMessageDelegate)
        {
            this.displayMessage = displayMessageDelegate;
        }


        string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] nonZeroDigits = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] zero = { "0" };
        string[] operations = { "+", "-", "*", "/", "%", "^", "log" };
        string[] equals = { "=" };
        string[] separators = { "," };
        string[] clear = { "C" };

        public enum State
        {
            Zero,
            AccumulateDigit,
            ComputePending,
<<<<<<< HEAD
            Compute,
=======
            Compute, 
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
            Separator,
            Clear,
        }

        public string currentNumber = "";
        public string previousNumber = "";

        // Polish notation
        public Stack<string> stack_of_numbers = new Stack<string>();
        public Stack<string> stack_of_operations = new Stack<string>();

        public string currentOperation = "";
        public State currentState = State.Zero;
        public void ProcessSignal(string message)
        {
            switch (currentState)
            {
                case State.Zero:
                    ProcessZeroState(message, false);
                    break;
                case State.AccumulateDigit:
                    ProcessAccumulateDigitState(message, false);
                    break;
                case State.ComputePending:
                    ProcessComputePendingState(message, false);
                    break;
                case State.Compute:
                    ProcessAccumulateDigitState(message, false);
                    break;
                case State.Separator:
                    ProcessSeparatorState(message, false);
                    break;
                case State.Clear:
                    ProcessClearState(message, false);
                    break;
                default:
                    break;
            }

        }

        public void ProcessClearState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.Clear;
                stack_of_numbers.Clear();
                stack_of_operations.Clear();
                displayMessage("0");
            }
            else
            {
                ProcessZeroState(msg, false);
            }
        }

<<<<<<< HEAD
        public void ProcessZeroState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.Zero;
=======
        public void ProcessZeroState(string msg, bool incoming) {
            if (incoming)
            {
                currentState = State.Zero;   
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
            }
            else
            {
                if (nonZeroDigits.Contains(msg))
                {
                    ProcessAccumulateDigitState(msg, true);
                }
            }
        }

        public void ProcessAccumulateDigitState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.AccumulateDigit;
                if (stack_of_numbers.Count() == 0)
                {
                    string number = msg;
                    stack_of_numbers.Push(number);
                }
                else
                {
                    string number = stack_of_numbers.Peek() + msg;
                    stack_of_numbers.Pop();
                    stack_of_numbers.Push(number);
                }
                displayMessage(stack_of_numbers.Peek());
            }
            else
            {
                if (digits.Contains(msg))
                {
                    ProcessAccumulateDigitState(msg, true);
                }
                else if (operations.Contains(msg))
                {
                    ProcessComputePendingState(msg, true);
                }
                else if (equals.Contains(msg))
                {
                    ProcessComputeState(msg, true);
                }
                else if (separators.Contains(msg))
                {
                    ProcessSeparatorState(msg, true);
                }
                else if (clear.Contains(msg))
                {
                    ProcessClearState(msg, true);
                }
            }
        }

        public void ProcessComputePendingState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.ComputePending;
                stack_of_numbers.Push("");
                stack_of_operations.Push(msg);
            }
            else
            {
                if (digits.Contains(msg))
                {
                    ProcessAccumulateDigitState(msg, true);
                }
                if (clear.Contains(msg))
                {
                    ProcessClearState(msg, true);
                }
            }
        }

        public void ProcessSeparatorState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.Separator;
                if (!stack_of_numbers.Peek().Contains(separators[0]))
                {

                    string number = stack_of_numbers.Peek() + msg;
                    stack_of_numbers.Pop();
                    stack_of_numbers.Push(number);
                    displayMessage(stack_of_numbers.Peek() + "0");
                }
            }
            else
            {
                ProcessAccumulateDigitState(msg, false);
            }
        }

        public void ProcessComputeState(string msg, bool incoming)
        {
            if (incoming)
            {
                currentState = State.Compute;
                if (stack_of_numbers.Count() <= 1)
                {
                    return;
                }
                Stack<string> remaining_numbers = new Stack<string>();
                Stack<string> remaining_opearation = new Stack<string>();
                while (stack_of_numbers.Count > 1)
                {
                    double b = Double.Parse(stack_of_numbers.Peek());
                    stack_of_numbers.Pop();
                    double a = Double.Parse(stack_of_numbers.Peek());
                    stack_of_numbers.Pop();
                    String currentNumber = default;
                    if (stack_of_operations.Peek() == "*")
                    {
                        currentNumber = (a * b).ToString();
                    }
                    else if (stack_of_operations.Peek() == "/")
                    {
                        currentNumber = (a / b).ToString();
                    }
                    else if (stack_of_operations.Peek() == "^")
                    {
                        currentNumber = (Math.Pow(a, b)).ToString();
                    }
                    else if (stack_of_operations.Peek() == "%")
                    {
                        currentNumber = (a % b).ToString();
                    }
                    else if (stack_of_operations.Peek() == "log")
                    {
                        currentNumber = (Math.Log(a, b)).ToString();
<<<<<<< HEAD
                    }
=======
                    } 
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
                    else
                    {
                        remaining_numbers.Push(b.ToString());
                        stack_of_numbers.Push(a.ToString());
                        remaining_opearation.Push(stack_of_operations.Peek());
<<<<<<< HEAD
                        stack_of_operations.Pop();
=======
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
                        continue;
                    }
                    stack_of_numbers.Push(currentNumber);
                    stack_of_operations.Pop();
                }
                remaining_numbers.Push(stack_of_numbers.Peek());
                stack_of_numbers.Pop();
                while (remaining_numbers.Count() > 1)
                {
                    double b = Double.Parse(remaining_numbers.Peek());
                    remaining_numbers.Pop();
                    double a = Double.Parse(remaining_numbers.Peek());
                    remaining_numbers.Pop();
                    string currentNumber = default;
                    if (remaining_opearation.Peek() == "+")
                    {
                        currentNumber = (a + b).ToString();
                    }
                    else if (remaining_opearation.Peek() == "-")
                    {
                        currentNumber = (b - a).ToString();
                    }
                    remaining_numbers.Push(currentNumber);
                    remaining_opearation.Pop();
                }
                stack_of_numbers.Push(remaining_numbers.Peek());
                stack_of_operations.Clear();
                displayMessage(stack_of_numbers.Peek());
            }
            else
            {

            }
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
