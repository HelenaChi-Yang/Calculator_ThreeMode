using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BinaryExpressionTree
{
    /// <summary>
    /// Expression Tree
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 內部類別 Node
        /// </summary>
        public class Node
        {
            internal Operations Operation;
            internal Node LeftNode;
            internal Node RightNode;
        }

        /// <summary>
        /// 新增node節點
        /// </summary>
        /// <param name="sign">符號</param>
        /// <returns></returns>
        public Node NewNode(Operations operation)
        {
            Node tempNode = new Node();
            tempNode.Operation = operation;
            tempNode.LeftNode = null;
            tempNode.RightNode = null;
            return tempNode;
        }

        /// <summary>
        /// 建構二元樹
        ///４條件：(，數字，運算子，)
        ///當遇到 ( 直接進入 operatorStack
        ///當遇到數字直接進入 operandStack
        ///當遇到運算子，比前一個運算子優先級小，且不是 (，就做多個樹狀node，直到遇到優先級比當前小才停下來
        ///當遇到 ) ，全部pop出樹狀node，直到遇到 (
        /// </summary>
        /// <param name="infixOperations">中序式</param>
        public Node BuildExpressionTree(List<Operations> infixOperations)
        {
            //暫存運算子
            Stack<Operations> operatorStack = new Stack<Operations>();
            //暫存節點(運算元+樹)
            Stack<Node> nodeStack = new Stack<Node>();
            //node暫存
            Node root = new Node();
            Node leftNode = new Node();
            Node rightNode = new Node();
            foreach (Operations operation in infixOperations)
            {
                if (operation.GetType() == typeof(LeftParenthesis))
                {
                    operatorStack.Push(operation);
                }
                else if (operation.GetType() == typeof(Number))
                {
                    nodeStack.Push(NewNode(operation));
                }
                else if (operation.GetType().BaseType == typeof(Operator))
                {
                    while (operatorStack.Count != 0 && operation.Priority >= operatorStack.Peek().Priority && operatorStack.Peek().GetType() != typeof(LeftParenthesis))
                    {
                        //operatorStack最上面為root
                        root = NewNode(operatorStack.Peek());
                        operatorStack.Pop();
                        //NodeStack最上面為rightNode
                        rightNode = nodeStack.Peek();
                        nodeStack.Pop();
                        //NodeStack第二個為leftNode
                        leftNode = nodeStack.Peek();
                        nodeStack.Pop();
                        //root指向rightNode跟leftNode並塞回operatorStack
                        root.RightNode = rightNode;
                        root.LeftNode = leftNode;
                        nodeStack.Push(root);
                    }
                    //把當前operator塞進去
                    operatorStack.Push(operation);
                }
                else if (operation.GetType() == typeof(RightParenthesis))
                {
                    while (operatorStack.Count != 0 && operatorStack.Peek().GetType() != typeof(LeftParenthesis))
                    {
                        root = NewNode(operatorStack.Peek());
                        operatorStack.Pop();
                        rightNode = nodeStack.Peek();
                        nodeStack.Pop();
                        leftNode = nodeStack.Peek();
                        nodeStack.Pop();
                        root.RightNode = rightNode;
                        root.LeftNode = leftNode;
                        nodeStack.Push(root);
                    }
                    //刪掉 (
                    operatorStack.Pop();
                }
            }
            root = nodeStack.Pop();
            return root;
        }

        /// <summary>
        /// 後序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> PostorderTraversal(Node root, List<Operations> operations)
        {
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                PostorderTraversal(root.LeftNode, operations);
            }
            if (root.RightNode != null)
            {
                PostorderTraversal(root.RightNode, operations);
            }
            operations.Add(root.Operation);
            return operations;
        }

        /// <summary>
        /// 前序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> PreorderTraversal(Node root, List<Operations> operations)
        {
            operations.Add(root.Operation);
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                PreorderTraversal(root.LeftNode, operations);
            }
            if (root.RightNode != null)
            {
                PreorderTraversal(root.RightNode, operations);
            }
            return operations;
        }

        /// <summary>
        /// 中序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> InorderTraversal(Node root, List<Operations> operations)
        {
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                PreorderTraversal(root.LeftNode, operations);
            }
            operations.Add(root.Operation);
            if (root.RightNode != null)
            {
                PreorderTraversal(root.RightNode, operations);
            }
            return operations;
        }

        /// <summary>
        /// 後序式演算法輸出結果
        /// </summary>
        /// <param name="result">運算式</param>
        /// <returns></returns>
        public decimal PostfixResult(List<Operations> result)
        {
            //pop出來需要被計算的數字
            List<int> computeNumber = new List<int>();
            //postfix的stack紀錄
            Stack<decimal> PostfixStack = new Stack<decimal>();

            foreach (Operations operatorOrOperand in result)
            {
                if (operatorOrOperand is Operator)
                {
                    //目前運算子
                    Operator currentOperator = (Operator)operatorOrOperand;

                    //pop出最上層兩個運算元
                    decimal firstNumber = PostfixStack.Pop();
                    decimal secondNumber = PostfixStack.Pop();

                    //執行運算並回到stack
                    decimal tempResult = currentOperator.Calculate(secondNumber, firstNumber);
                    PostfixStack.Push(tempResult);
                }
                else
                {
                    //數字
                    PostfixStack.Push(Convert.ToDecimal(operatorOrOperand.Sign));
                }
            }
            decimal finalResult = PostfixStack.Pop();
            return finalResult;
        }
    }
}
