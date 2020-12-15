using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BinaryTree
{
    class BinaryTree
    {
        Treenode rootnode = null;

        public void Insert(int value)
        {
            if(rootnode==null)
            {
                rootnode = new Treenode(value);
            }
            else
            {
                rootnode.Insert(value);
            }
        }

        internal void InorderTraversal()
        {
            if(rootnode!=null)
            {
                rootnode.InorderTraversal();
            }
        }

        internal void Reverse()
        {
            if(rootnode !=null)
            {
               rootnode= rootnode.Reverse(rootnode);
            }
        }

    }
    public class Treenode
    {
        public int _data;
        public int Data
        {
            get { return _data; }
           set { _data = value; }
        }
        public Treenode(int Value)
        {
            _data = Value;
        }
        public Treenode leftnode;
        public Treenode LeftNode
        {
            get { return leftnode; }
            set { leftnode = value; }
        }
        public Treenode rightnode;
        public Treenode RightNode
        {
            get { return rightnode; }
            set { rightnode = value; }
        }

        public void Insert(int value)
        {
            if(value>=_data)
            {
                if (rightnode == null)
                {
                    rightnode = new Treenode(value);
                }
                else
                {
                    rightnode.Insert(value);
                }
            }
            else
            {
                if (leftnode == null)
                {
                    leftnode = new Treenode(value);
                }
                else
                {
                    leftnode.Insert(value);
                }
            }
            
        }
        public void InorderTraversal()
        {
            if(leftnode!=null)
            {
                leftnode.InorderTraversal();
            }
           
            Console.WriteLine(_data+" ");
            if(rightnode!=null)
            {
                rightnode.InorderTraversal();
            }         


        }
        public void PostOrderTraversal()
        {
            Console.Write(_data + " ");
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

        internal Treenode Reverse(Treenode rootnode)
        {
            
            if(rootnode !=null)
            {
                (rootnode.leftnode, rootnode.rightnode) = (Reverse(rootnode.rightnode), Reverse(rootnode.leftnode));
            }
            return rootnode;
          //if (rootnode== null)
          //  {
          //      return rootnode;
          //  }
          
          //      LeftNode = Reverse(rootnode.LeftNode);
          //      RightNode = Reverse(rootnode.RightNode);
          //     // Treenode temp = rootnode.;
          //      rootnode.LeftNode = RightNode;
          //      rootnode.RightNode = LeftNode;
          //  return rootnode;
        }
    }
}
