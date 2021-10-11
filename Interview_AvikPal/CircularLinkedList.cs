using System;
using System.Collections.Generic;
using System.Text;

namespace Interview_AvikPal
{
    public class CircularLinkedList
    {
        public class Node
        {
            public string data;
            public Node next;

            public Node(string d)
            {
                data = d;
                next = null;
            }
        }
        public Node head;

        // Constructor
        public CircularLinkedList()
        {
            head = null;
        }

        /* function to insert a new_node
          in a list in sorted way. Note that
          this function expects a pointer
          to head node as this can modify
          the head of the input linked list */
        public void sortedInsert(Node new_node)
        {
            Node current = head;

            // Case 1 : Linked List is empty
            if (current == null)
            {
                new_node.next = new_node;
                head = new_node;

            }

            // Case 2 : New node is to be inserted just before the head node
            else if (current.data.CompareTo(new_node.data) <0)
            {

                /* If value is smaller than
                    head's value then we need
                    to change next of last node */
                while (current.next != head)
                    current = current.next;

                current.next = new_node;
                new_node.next = head;
                head = new_node;
            }

            // Case 3 : New node is to be  inserted somewhere after the head
            else
            {

                /* Locate the node before
                the point of insertion */
                while (current.next != head &&
                    current.data.CompareTo(new_node.data) > 0)
                    current = current.next;

                new_node.next = current.next;
                current.next = new_node;
            }
        }

        // Utility method to print a linked list
        public void printList()
        {
            if (head != null)
            {
                Node temp = head;
                do
                {
                    Console.Write(temp.data + " ");
                    temp = temp.next;
                }
                while (temp != head);
            }
        }
    }
}
