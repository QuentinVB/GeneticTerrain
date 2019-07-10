using System;

namespace Ast
{
    class AstTree
    {
        class Node
        {
            public int value;
            public Node left;
            public Node right;
        }

        class Tree
        {
            public Node insert(Node root, int v)
            {
                if (root == null)
                {
                    root = new Node();
                    root.value = v;
                }
                else if (v < root.value)
                {
                    root.left = insert(root.left, v);
                }
                else
                {
                    root.right = insert(root.right, v);
                }

                return root;

            }

            public void traverse(Node root)
            {
                if (root == null)
                {
                    return;
                }

                traverse(root.left);
                traverse(root.right);
            }
        }

        public static void AstTrees()
        {
            Node root = null;
            Tree bst = new Tree();
            int SIZE = 20;
            int[] a = new int[SIZE];

            Console.WriteLine("Generating random array with {0} values...", SIZE);

            Random random = new Random();

            for (int i = 0; i < SIZE; i++)
            {
                a[i] = random.Next(10000);
            }

            Console.WriteLine("Filling the tree with {0} nodes...", SIZE);

            for (int i = 0; i < SIZE; i++)
            {
                root = bst.insert(root, a[i]);
                Console.WriteLine(a[i]);
            }


            Console.WriteLine("Traversing all {0} nodes in tree...", SIZE);

            bst.traverse(root);

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}