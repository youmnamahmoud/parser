using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scannerTokens
{
     public enum pt{Statements,Repeat_Statement, Assign_Statement, Read_Statement, Write_Statement, If, Then, Else, Repeat, Until, Read, Write,Exp, Simple_Exp, Term, Factor,Identifier,Number,Comp_Op, Assign_Op, Add_Op, Mul_Op}

    class Parser
    {
        enum s{If, Then, Else, End, Repeat, Until, Read, Write, EndFile, Error, Identifier, Number, Comment, ReservedWords, Char, Plus, Minus, Times, Equal, Division, GreaterThan, LessThan, LeftParentheses,RightParentheses, SemiColon, Assign, LessThanOrEqual, GreaterThanOrEqual, NotEqual}
        private List<KeyValuePair<string, string>> data;
        private bool done;
 
        public bool CreateParseTree(List<KeyValuePair<string, string>> SD, ref TreeNode ParserTreeRoot)
        {
            data = new List<KeyValuePair<string, string>>();
            foreach (var Data in SD)
            {
                if (Data.Value == s.Error.ToString())
                {
                    MessageBox.Show("Error in Scanner");
                    return false;
                }
                if (Data.Value != s.Comment.ToString())
                    data.Add(new KeyValuePair<string, string>(Data.Key, Data.Value));
            }
            ParserTreeRoot = Program();        
            return done;
        }

        private TreeNode Program()
        {
            done = true;
            TreeNode Node = null;
            int Index = 0;
            if (data.Count>1 && (!StatementSequence(ref Index, ref Node) || Index != data.Count - 1))
            {
                //MessageBox.Show("Error in Parser");
                done = false;
            }
            return Node;
        }
        // stmt-seq -> stmt {; stmt}
        private bool StatementSequence(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode StatementSequenceNode = null;
            int old = current;
            if (Statement(ref current, ref StatementSequenceNode))
            {
                CurrentNode = CreateNode(pt.Statements, StatementSequenceNode);// Create Node
                old = current;
            }
            else
                return false;
            while (true)
            {
                if (data[current++].Value == s.SemiColon.ToString())
                {
                    if (Statement(ref current, ref StatementSequenceNode))
                    {
                        CurrentNode = AddChildNode(CurrentNode, StatementSequenceNode);// Add child
                        old=current;
                    }
                    else
                        return false;
                }
                else
                {
                    current = old;
                    return true;
                }
            }

        }

        // stmt -> if-stmt | repeat-stmt | assign-stmt | read-stmt | write-stmt
        private bool Statement(ref int current, ref TreeNode CurrentNode)
        {
            int old = current;
            if (IfStatement(ref current, ref CurrentNode) || RepeatStatement(ref current, ref CurrentNode) || AssignStatement(ref current, ref CurrentNode) || ReadStatement(ref current, ref CurrentNode) || WriteStatement(ref current, ref CurrentNode))
                return true;
            current = old;
            return false;
        }

        // if-stmt -> if exp then stmt-seq [else stmt-seq] end
        private bool IfStatement(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode exp = null;
            TreeNode stmts = null;
            TreeNode elseStmts = null;

            int old = current;
            if ( data[current++].Key == s.If.ToString() && Expression(ref current, ref exp) &&
                data[current++].Key == s.Then.ToString() && StatementSequence(ref current, ref stmts) &&
                data[current++].Key == s.End.ToString() )
            {
                //Create Node
                CurrentNode = CreateNode(pt.If, exp);
                CurrentNode = AddChildNode(CurrentNode, CreateNode(pt.Then, stmts));

                return true;
            }
            current = old;
            if ( data[current++].Key == s.If.ToString() && Expression(ref current, ref exp) &&
                data[current++].Key == s.Then.ToString() && StatementSequence(ref current, ref stmts) &&
                data[current++].Key == s.Else.ToString() &&  StatementSequence(ref current, ref elseStmts) &&
                data[current++].Key == s.End.ToString() )
            {
                // Create Node
                CurrentNode = CreateNode(pt.If, exp);
                CurrentNode = AddChildNode(CurrentNode, CreateNode(pt.Then, stmts));
                CurrentNode = AddChildNode(CurrentNode, CreateNode(pt.Else, elseStmts));

                return true;
            }
            current = old;
            return false;
        }

        // repeat-stmt -> repeat stmt-seq until exp
        private bool RepeatStatement(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode stmtsNode = null;
            TreeNode exp = null;

            int old = current;
            if (data[current++].Key == s.Repeat.ToString() && StatementSequence(ref current, ref stmtsNode) &&
                data[current++].Key == s.Until.ToString() && Expression(ref current, ref exp) )
            {
                // Create Nodes
                CurrentNode = CreateNode(pt.Repeat_Statement, stmtsNode);
                CurrentNode = AddChildNode(CurrentNode, CreateNode(pt.Until,exp));
                return true;
            }
            current = old;
            return false;
        }

        // assign-stmt -> identifier := exp
        private bool AssignStatement(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode idNode = null;
            TreeNode assignOpNode = null;
            TreeNode exp = null;

            int old = current;
            if ( data[current++].Value == s.Identifier.ToString() &&
                data[current++].Value == s.Assign.ToString() && Expression(ref current, ref exp) )
            {
                //Create Node
                idNode = CreateNode(pt.Identifier, data[old].Key);
                assignOpNode = CreateNode(pt.Assign_Op, ":=");
                CurrentNode = CreateNode(pt.Assign_Statement, idNode);
                CurrentNode = AddChildNode(CurrentNode, assignOpNode);
                CurrentNode = AddChildNode(CurrentNode, exp);

                return true;
            }
            current = old;
            return false;
        }

        // read-stmt -> read identifier
        private bool ReadStatement(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode idNode = null;

            int old = current;
            if (data[current++].Key == s.Read.ToString() &&
                data[current++].Value == s.Identifier.ToString())
            {
                // Create Node
                idNode = CreateNode(pt.Identifier, data[current - 1].Key);
                CurrentNode = CreateNode(pt.Read_Statement, pt.Read);
                CurrentNode = AddChildNode(CurrentNode, idNode);

                return true;
            }
            current = old;
            return false;
        }

        // write-stmt -> write exp
        private bool WriteStatement(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode expNode = null;

            int old = current;
            if ( data[current++].Key == s.Write.ToString() &&
                Expression(ref current, ref expNode) )
            {
                // Create Node
                CurrentNode = CreateNode(pt.Write_Statement, pt.Write);
                CurrentNode = AddChildNode(CurrentNode, expNode);

                return true;
            }
            current = old;
            return false;
        }

        // exp -> simple-exp [comparison-op simple-exp]
        private bool Expression(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode simpleExpNode = null;
            TreeNode compNode = null;

            int old = current;
            if ( SimpleExpression(ref current , ref simpleExpNode) )
            {
                // Create Node
                CurrentNode = CreateNode(pt.Exp, simpleExpNode);

                old = current;
            }
            else
            {
                current = old;
                return false;
            }

            if (ComparisonOp(ref current , ref compNode))
            {
                if (SimpleExpression(ref current , ref simpleExpNode))
                {
                    // Add childs
                    CurrentNode = AddChildNode(CurrentNode, compNode);
                    CurrentNode = AddChildNode(CurrentNode, simpleExpNode);
                    old = current;
                }
                else
                {
                    current = old;
                    return false;
                }
            }
            current = old;
            return true;
        }

        // comparison-op -> < | =
        private bool ComparisonOp(ref int current, ref TreeNode CurrentNode)
        {
            int old = current;
            if (data[current].Value == s.LessThan.ToString() ||
                data[current].Value == s.GreaterThan.ToString() ||
                data[current].Value == s.LessThanOrEqual.ToString() ||
                data[current].Value == s.GreaterThanOrEqual.ToString() ||
                data[current].Value == s.Equal.ToString() ||
                data[current].Value == s.NotEqual.ToString() )
            {
                // Create Node
                CurrentNode = CreateNode(pt.Comp_Op, data[current].Key);

                current++;
                return true;
            }
            current = old;
            return false;
        }

        // simple-exp -> term { addop term }
        private bool SimpleExpression(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode termNode = null;
            TreeNode addNode = null;

            int old = current;
            if (Term(ref current, ref termNode))
            {
                // Create Node
                CurrentNode = CreateNode(pt.Simple_Exp, termNode);

                old = current;
            }
            else
            {
                current = old;
                return false;
            }

            while (true)
            {
                if (AddOp(ref current , ref addNode))
                {
                    if (Term(ref current, ref termNode))
                    {
                        // Add Childs
                        CurrentNode = AddChildNode(CurrentNode, addNode);
                        CurrentNode = AddChildNode(CurrentNode, termNode);

                        old = current;
                    }
                    else
                    {

                        current = old;
                        return false;
                    }
                }
                else
                {
                    current = old;
                    return true;
                }
            }
        }

        // addop -> + | -
        private bool AddOp(ref int current, ref TreeNode CurrentNode)
        {
            int old = current;
            if (data[current].Value == s.Plus.ToString() ||
                data[current].Value == s.Minus.ToString())
            {
                // Create Node
                CurrentNode = CreateNode(pt.Add_Op,data[current].Key);

                current++;
                return true;
            }
            current = old;
            return false;
        }

        // term -> factor { mulop factor }
        private bool Term(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode factorNode = null;
            TreeNode MulNode = null;

            int old = current;
            if ( Factor(ref current, ref factorNode) )
            {
                // Create Node
                CurrentNode = CreateNode(pt.Term, factorNode);

                old = current;
            }
            else
            {
                current = old;
                return false;
            }
            while (true)
            {
                if (multOp(ref current, ref MulNode))
                {
                    if (Factor(ref current , ref factorNode))
                    {
                        // Add Nodes to Term Node
                        CurrentNode = AddChildNode(CurrentNode, MulNode);
                        CurrentNode = AddChildNode(CurrentNode, factorNode);

                        old = current;
                    }
                    else
                    {

                        current = old;
                        return false;
                    }
                }
                else
                {
                    current = old;
                    return true;
                }
            }
        }

        // mulop -> * | /
        private bool multOp(ref int current, ref TreeNode CurrentNode)
        {
            int old = current;
            if (data[current].Value == s.Times.ToString())
            {
                // Create Node
                CurrentNode = CreateNode(pt.Mul_Op, data[current].Key);

                current++;
                return true;
            }
            current = old;
            return false;
        }

        // factor -> (exp) | number | identifier
        private bool Factor(ref int current, ref TreeNode CurrentNode)
        {
            TreeNode expNode = null;
            TreeNode numOrId = null;

            int old = current;
            if (data[current++].Value == s.LeftParentheses.ToString() && Expression(ref current, ref expNode) &&
                data[current++].Value == s.RightParentheses.ToString() )
            { 
                // Create Node
                CurrentNode = CreateNode(pt.Factor, expNode);

                return true;
            }
            current = old;
            if ( data[current].Value == s.Number.ToString() ||
                data[current].Value == s.Identifier.ToString() )
            {
                // Create Node
                numOrId = CreateNode(data[current].Value, data[current].Key);
                CurrentNode = CreateNode(pt.Factor, numOrId);

                current++;
                return true;
            }
            current = old;
            return false;
        }

        private TreeNode CreateNode(string nodeName,string value)
        {
            TreeNode CurrentNode = new TreeNode(nodeName);
            CurrentNode.Name = nodeName;
            CurrentNode.Nodes.Add(value);
            CurrentNode.Nodes[0].Name = value;

            return CurrentNode;
        }

        private TreeNode CreateNode(string nodeName, TreeNode value)
        {
            TreeNode CurrentNode = new TreeNode(nodeName);
            CurrentNode.Name = nodeName;
            CurrentNode.Nodes.Add(value);

            return CurrentNode;
        }
        private TreeNode CreateNode(pt nodeName)
        {
            TreeNode node = new TreeNode(nodeName.ToString());
            node.Name = nodeName.ToString();
            return node;
        }
        private TreeNode CreateNode(pt nodeName, TreeNode value)
        {
            TreeNode CurrentNode = new TreeNode(nodeName.ToString());
            CurrentNode.Name = nodeName.ToString();
            CurrentNode.Nodes.Add(value);

            return CurrentNode;
        }
        private TreeNode CreateNode(pt nodeName, pt value)
        {
            TreeNode CurrentNode = new TreeNode(nodeName.ToString());
            CurrentNode.Name = nodeName.ToString();
            CurrentNode.Nodes.Add(value.ToString());
            CurrentNode.Nodes[0].Name = value.ToString();

            return CurrentNode;
        }
        private TreeNode CreateNode(pt nodeName, string value)
        {
            TreeNode CurrentNode = new TreeNode(nodeName.ToString());
            CurrentNode.Name = nodeName.ToString();
            CurrentNode.Nodes.Add(value);
            CurrentNode.Nodes[0].Name = value;

            return CurrentNode;
        }
  
        private TreeNode AddChildNode(TreeNode Parent,TreeNode Child)
        {
            Parent.Nodes.Add(Child);
            return Parent;
        }
    }

    
}
