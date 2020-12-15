using System;

namespace ConsoleApp3
{
    public class BinaryTree
    {
        private TreeNode root;

        public void Insert(int value)
        {
            if(root !=null)
            {
                root.Insert(value);
            }
            else
            {
                root = new TreeNode(value);
            }
        }
        public void InorderTraversal()
        {
            if(root !=null)
            {
                root.InOrderTraversal();
            }
        }
        public void PreOrderTraversal()
        {
            if (root != null)
            {
                root.PreOrderTraversal();
            }
        }
        public void PostOrderTraversal()
        {
            if (root != null)
            {
                root.PostOrderTraversal();
            }
        }
        //O log n
        public TreeNode Find(int value)
        {
            if (root != null)
            {
              return  root.Find(value);
            }
            else
            {
                return null;
            }
        }
        //O log n
        public TreeNode FindRecursive(int value)
        {
            if (root != null)
            {
                return root.FindRecursive(value);
            }
            else
            {
                return null;
            }
        }
    }
    public class TreeNode
    {
        private int data;

        public int Data
        {
            get { return data; }
        }
        public TreeNode (int value)
        {
            data = value;
        }

        private TreeNode leftnode;
        public TreeNode LeftNod
        {
            get { return leftnode; }
            set { leftnode = value; }
        }
        private TreeNode rightnode;
        public TreeNode RightNode
        {
            get { return rightnode; }
            set { rightnode = value; }
        }
        public TreeNode FindRecursive(int value)
        {
            if (value == data)
                return this;
            else if(value<data && leftnode !=null)
            {
                return LeftNod.FindRecursive(value);
            }
            else if(rightnode !=null)
            {
                return RightNode.FindRecursive(value);

            }
            else
            {
                return null;
            }
        }
        public TreeNode Find(int value)
        {
            TreeNode currentNode = this;
            while(currentNode !=null)
            {
                if(currentNode.data== value)
                {
                    return currentNode;
                }
                else if(value<currentNode.data)
                {
                    currentNode = currentNode.LeftNod;
                }
                else
                {
                    currentNode = currentNode.RightNode;
                }
            }
            return null;
        }

        public void Insert(int value)
        {
            if(value>=data)
            {
                if(rightnode== null)
                {
                    rightnode = new TreeNode(value);
                }
                else
                {
                    rightnode.Insert(value);
                }
            }
            else
            {
                if(leftnode==null)
                {
                    leftnode = new TreeNode(value)
                        ;
                }
                else
                {
                    leftnode.Insert(value);
                }
            }
        }

        public  void InOrderTraversal()
        {
         //left->root->right  
            // throw new NotImplementedException();
            if(leftnode!=null)
            {
                leftnode.InOrderTraversal();
            }
            Console.Write(data+" ");
            if(rightnode!=null)
            {
                rightnode.InOrderTraversal();
            }
        }
        public void PreOrderTraversal()
        {
            Console.Write(data + " ");
            //root->left->right
            // throw new NotImplementedException();
            if (leftnode != null)
            {
                leftnode.PreOrderTraversal();
            }
          
            if (rightnode != null)
            {
                rightnode.PreOrderTraversal();
            }
        }
        public void PostOrderTraversal()
        {
            Console.Write(data + " ");
            //root->right->left
            // throw new NotImplementedException();
            if (rightnode != null)
            {
                rightnode.PostOrderTraversal();
            }
            if (leftnode != null)
            {
                leftnode.PostOrderTraversal();
            }          
        }
    }
}
