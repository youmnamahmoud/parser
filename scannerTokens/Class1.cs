using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace scannerTokens
{
    public class CompilerScanner
    {
        enum R{If, Then, Else, End, Repeat, Until, Read, Write}
        enum T{EndFile, Error,Identifier, Number,Comment,R,Char} 

        struct SpecialSymbols
        {
            public string Symbol,Name;
            public SpecialSymbols(string symbol, string name)
            {
                Symbol = symbol;
                Name = name;
            }
        };
        
        SpecialSymbols[] special = { 
                                               new SpecialSymbols("+","Plus"), 
                                               new SpecialSymbols("-","Minus"), 
                                               new SpecialSymbols("*","Times"), 
                                               new SpecialSymbols("=","Equal"),
                                               new SpecialSymbols("/","Division"), 
                                               new SpecialSymbols(">","GreaterThan"), 
                                               new SpecialSymbols("<","LessThan"), 
                                               new SpecialSymbols("(","LeftParentheses"),
                                               new SpecialSymbols(")","RightParentheses") ,
                                               new SpecialSymbols(";","SemiColon"), 
                                               new SpecialSymbols(":=","Assign"), 
                                               new SpecialSymbols("<=","LessThanOrEqual") , 
                                               new SpecialSymbols(">=","GreaterThanOrEqual"), 
                                               new SpecialSymbols("!=","NotEqual")
                                           };

        public void StartScanner(string[] FileData, ref List<KeyValuePair<string, string>> ScannerData)
        {
            string output="", result;
            bool Error,IsComment=false;
            foreach (string line in FileData)
            {
                if (line.Length > 0)
                {
                    for (int index = 0; index < line.Length; index++)
                    {
                       //---------------for comments {   }-------------------------
                        if (IsComment)
                        {
                            if (index  < line.Length && line[index] == '}')
                            {
                                index++;
                                IsComment = false;
                                output += "}";
                                result = T.Comment.ToString();
                            }
                            else
                            { output += line[index]; }
                            continue;
                        }
                        else
                        {
                            if (index  < line.Length && line[index] == '{')
                            {
                                index++;
                                IsComment = true;
                                output = "{";
                                continue;
                            }
                        }
                        //-----------------for comments /** **/-------------------
                        if (IsComment)
                        {
                            if (index + 2 < line.Length && line[index] == '*' && line[index + 1] == '*' && line[index + 2] == '/')
                            //End of Comment "**/"
                            {
                                index++;
                                IsComment = false;
                                output += "**/";
                                result = T.Comment.ToString();
                            }
                            else
                            //Continue in Comment
                            { output += line[index]; }
                            continue;
                        }
                        else
                        {
                            if (index + 1 < line.Length && line[index] == '/' && line[index + 1] == '*' && line[index + 2] == '*')//Start of Comment "/**"
                            {
                                index++;
                                IsComment = true;
                                output = "/**";
                                continue;
                            }
                        }
                        

                        //---------------------char--------------------
                        if (index+2 < line.Length &&line[index] == '\'' && line[index+2] == '\'')
                        {
                            output = "";
                            output += line[index];
                            output += line[index + 1];
                            output += line[index + 2];
                            result = T.Char.ToString();
                            AddScannerList(ref ScannerData, output, result);
                            index += 2;
                            continue;
                        }
                        //--------------------Identifier Or Reserved Word----------------------------
                        output = Identifier(line, ref index);//Get Identifier or empty
                        result = T.Identifier.ToString();
                        if (output != "")
                        {
                            if (Enum.IsDefined(typeof(R), output))//If Identifier is Reserved Word 
                            { result = T.R.ToString(); }
                            AddScannerList(ref ScannerData, output, result);
                            index--;
                            continue;
                        }
                        //---------------------------Number Or Error-------------------
                        Error = false;
                        output = Number(line, ref index, ref Error);//Get Number(with Error or not) or empty
                        result = T.Number.ToString();
                        if (Error)//If error number contains letter
                            result = T.Error.ToString();
                        if (output != "")
                        {
                            AddScannerList(ref ScannerData, output, result);
                            index--;
                            continue;
                        }
                        //---------------------------SpecialSymbols--------------------
                        if (index + 1 < line.Length)//If this is Special Symbol with 2 char
                        {
                            string Symbol = line[index].ToString() + line[index + 1].ToString();
                            string SymbolName = GetSpecialSymbol(Symbol);
                            if (SymbolName != "")
                            {
                                index++;
                                AddScannerList(ref ScannerData, Symbol, SymbolName);
                                continue;
                            }
                        }
                        {//If this is Special Symbol with 1 char
                            string Symbol = line[index].ToString();
                            string SymbolName = GetSpecialSymbol(Symbol);
                            if (SymbolName != "")
                            {
                                AddScannerList(ref ScannerData, Symbol, SymbolName);
                                continue;
                            }
                            
                        }
                        //------------------------Errors-----------------------
                        if (line[index] != ' ' && line[index] != '\t')
                        {
                            AddScannerList(ref ScannerData, line[index].ToString(), T.Error.ToString());
                        }
                    }
                }
            }
            if (IsComment)//Error in the comment
            {
                AddScannerList(ref ScannerData, output, T.Error.ToString());
            }
            AddScannerList(ref ScannerData, "", T.EndFile.ToString());
        }

        private bool IsChar(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                return true;
            return false;
        }

        private bool IsDigit(char ch)
        {
            if (ch >= '0' && ch <= '9') return true;
            return false;
        }

        private bool IsDot(char ch)
        {
            if (ch == '.') return true;
            return false;
        }

        private bool IsE(char ch)
        {
            if (ch == 'E') return true;
            return false;
        }

        private bool IsSign(char ch)
        {
            if (ch == '+' || ch == '-') return true;
            return false;
        }

        private string Identifier(string input, ref int index)
        {
            string output = "";
            if (IsChar(input[index]))
            {
                output += input[index++];
                while (index < input.Length && (IsChar(input[index]) || IsDigit(input[index])))
                {
                    output += input[index++];
                }
            }
            return output;
        }

        private string Number(string input, ref int index, ref bool Error, string output = "",bool E =false,bool Dot=false)
        {
            if (IsDigit(input[index]))
            {
                output += input[index++];
                while (index < input.Length && (IsDigit(input[index]) || (IsChar(input[index]) && !IsE(input[index])) || IsDot(input[index])))
                {
                    if (IsChar(input[index]) || (IsDot(input[index]) && Dot==true))
                    {
                        Error = true;
                    }
                    if (IsDot(input[index]))
                    { 
                        Dot = true;
                        if (index + 1 < input.Length && IsDigit(input[index + 1]))
                            ;
                        else
                        {
                            Error = true;
                        }
                    }
                    output += input[index++];
                }
                if(index < input.Length && IsE(input[index]))
                {
                    if (E) Error = true;
                    E = true;
                    output += input[index++];
                    if (index  < input.Length && IsDigit(input[index]) )
                    {
                        return Number(input, ref index, ref Error, output,E);
                    }
                    else if (index < input.Length && IsSign(input[index]) )
                    {
                        output += input[index++];
                        if (index < input.Length && IsDigit(input[index]))
                        {
                            return Number(input, ref index, ref Error, output,E);
                        }
                        else
                        {
                            Error = true;
                        }
                    }
                    else
                    {
                        Error = true;
                    }
                }
            }
            return output;
        }

        private string GetSpecialSymbol(string current)
        {
            foreach (var SpecialSymbol in special)
            {
                string Symbol = SpecialSymbol.Symbol;
                string Name = SpecialSymbol.Name;
                if (Symbol == current)
                {
                    return Name;
                }
            }
            return "";
        }

        private void AddScannerList(ref List<KeyValuePair<string, string>> ScannerData, string output, string result)
        {
            ScannerData.Add(new KeyValuePair<string, string>(output, result));
        }
    }
    
}
