using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Intrinsics.X86;

namespace WebApplication1.Pages
{
    public class CalculatorModel : PageModel
    {

        public bool hasData = false;
        public string calInput = "";
        public double ans = 0;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;
            calInput = Request.Form["calInput"];

            // CALL CALCULATOR FUNCTION
            //ans = Calculate(calInput);
            //ans = Evaluate(calInput);
            ans = Calculate_Stack(calInput);

        }

        public double Calculate(string sum)
        {
            double answer = 0;                                          // calculation answer
            string[] operators = { "+", "-", "*", "/", "(", ")" };      // operators
            int i = 0;                                                  // loop counter

            
            // SPLIT KEY IN INPUT BY SPACE
            string[] val = sum.Split(' ');

            foreach (var valStr in val)
            {

                
                double a = 0;

                // IF THERE IS AN ANSWER TO PREVIOUS CALCULATION
                if (answer == 0 & i > 0)
                {
                    a = double.Parse(val[i - 1]);
                }
                else
                {
                    a = answer;
                }
                
                // SET RULES WHICH OPERATORS TO RUN FIRST

                // CHECK WHICH OPERATOR TO BE USED
                if (valStr == "+")
                {
                    double b = double.Parse(val[i + 1]);
                    answer = add(a, b);
                }

                else if (valStr == "*")
                {
                    double b = double.Parse(val[i + 1]);
                    answer = times(a, b);
                }

                else if (valStr == "/")
                {
                    double b = double.Parse(val[i + 1]);
                    answer = division(a, b);

                }

                i++;
                
            }

            
            return answer;
        }

        public double add(double a, double b)
        {
            return a + b;
        }

        public double minus(double a, double b)
        {
            return a - b;
        }

        public double times(double a, double b)
        {
            return a * b;
        }

        public double division(double a, double b)
        {
            return a / b;
        }


        public double Calculate_Stack(string sum)
        {
            double answer = 0;                                          // calculation answer
            int i = 0;                                                  // loop counter

            Stack<String> stack = new Stack<String>();
            string value = "";

            // SPLIT KEY IN INPUT BY SPACE
            string[] val = sum.Split(' ');

            foreach (var valStr in val)
            {
                i++;
                /*
                if (Double.IsNaN(Convert.ToDouble(valStr)) && value != "")
                {
                    stack.Push(value);
                    value = "";
                }*/

                if (valStr.Equals("("))
                {
                    string innerExp = "";
                    //i++; //Fetch Next Character
                    int bracketCount = 0;

                    var innerVal = val.Skip(i);
                    foreach (var innervalStr in innerVal)
                    {
                        if (innervalStr.Equals("("))
                            bracketCount++;

                        if (innervalStr.Equals(")"))
                            if (bracketCount == 0)
                            {
                                innerExp = innerExp.Remove(innerExp.Length - 1);
                                break;
                            }
                            else
                                bracketCount--;


                        innerExp = innerExp + innervalStr + " ";
                    }


                    stack.Push(Calculate_Stack(innerExp).ToString());

                }
                else if (valStr.Equals("+")) stack.Push(valStr);
                else if (valStr.Equals("-")) stack.Push(valStr);
                else if (valStr.Equals("*")) stack.Push(valStr);
                else if (valStr.Equals("/")) stack.Push(valStr);
                else if (valStr.Equals("sqrt")) stack.Push(valStr);
                else if (valStr.Equals(")"))
                {
                }
                else if (!Double.IsNaN(Convert.ToDouble(valStr)) && valStr != "")
                {
                    stack.Push(valStr);

                }

                //i++;

            }


            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (op == "+") answer = left + right;
                else if (op == "+") answer = left + right;
                else if (op == "-") answer = left - right;
                else if (op == "*") answer = left * right;
                else if (op == "/") answer = left / right;

                stack.Push(answer.ToString());
            }

            return Convert.ToDouble(stack.Pop());
        }


        public static double Evaluate(String expr)
        {

            Stack<String> stack = new Stack<String>();

            string value = "";
            for (int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                char chr = s.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }

                if (s.Equals("("))
                {

                    string innerExp = "";
                    i++; //Fetch Next Character
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExp += s;
                    }

                    stack.Push(Evaluate(innerExp).ToString());

                }
                else if (s.Equals("+")) stack.Push(s);
                else if (s.Equals("-")) stack.Push(s);
                else if (s.Equals("*")) stack.Push(s);
                else if (s.Equals("/")) stack.Push(s);
                else if (s.Equals("sqrt")) stack.Push(s);
                else if (s.Equals(")"))
                {
                }
                else if (char.IsDigit(chr) || chr == '.')
                {
                    value += s;

                    if (value.Split('.').Length > 2)
                        throw new Exception("Invalid decimal.");

                    if (i == (expr.Length - 1))
                        stack.Push(value);

                }
                else
                    throw new Exception("Invalid character.");

            }


            double result = 0;
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (op == "+") result = left + right;
                else if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "*") result = left * right;
                else if (op == "/") result = left / right;

                stack.Push(result.ToString());
            }


            return Convert.ToDouble(stack.Pop());
        }


    }
}
