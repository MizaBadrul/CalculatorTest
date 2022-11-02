using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public bool hasData = false;
        public string calInput = "";
        public double ans = 0;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            hasData = true;
            calInput = Request.Form["calInput"];

            // CALL CALCULATOR FUNCTION
            ans = Calculate_Stack(calInput);

        }

        public double Calculate_Stack(string sum)
        {
            double answer = 0;                                          // calculation answer
            int i = 0;                                                  // loop counter

            Stack<String> stack = new Stack<String>();

            // SPLIT KEY IN INPUT BY SPACE
            string[] val = sum.Split(' ');

            foreach (var valStr in val)
            {
                i++;

                // CALCULATE INNER BRACKET FIRST
                if (valStr.Equals("("))
                {
                    string innerExp = "";
                    int bracketCount = 0;

                    // ONLY TAKE VALUE AFTER (
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

                    double z = Calculate_Stack(innerExp);
                    stack.Push(z.ToString());

                }
                else if (valStr.Equals("+")) stack.Push(valStr);
                else if (valStr.Equals("-")) stack.Push(valStr);
                else if (valStr.Equals("*")) stack.Push(valStr);
                else if (valStr.Equals("/")) stack.Push(valStr);
                else if (!Double.IsNaN(Convert.ToDouble(valStr)) && valStr != "")   // Check if valStr is digit
                {
                    stack.Push(valStr);

                }

            }

            // RUN THE CALCULATION HERE
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                //stack

                if (op == "+") answer = left + right;
                else if (op == "-") answer = left - right;
                else if (op == "*") answer = left * right;
                else if (op == "/") answer = left / right;

                stack.Push(answer.ToString());
            }

            return Convert.ToDouble(stack.Pop());
        }

    }
}