using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class List_element
    {
        internal List_element previous;
        internal List_element next;
        protected internal Organism organism;

       public List_element()
        {
            this.previous = null;
            this.next = null;
        }
    };

   public class  Container : List_element
    {
           internal List_element head;
            List_element tail;

            public Container() {
                head = null;
                tail = null;
            }
    
   internal void insert_element(Organism new_organism)
        {
            List_element tmp = new List_element();
            if (head == null)
            {
                head = tmp;
                tail = tmp;
                tmp.organism = new_organism;
            }
            else
            {
                insert_secret(new_organism);
            }
        }

        private int insert_secret(Organism new_organism)
        {
            List_element tmp = new List_element();
            List_element q = new List_element();
            tmp = head;
            while (tmp.organism.getInitiative() > new_organism.getInitiative())
            {
                if (tmp.next == null)
                    break;
                else
                    tmp = tmp.next;
            }
            while (tmp.organism.getInitiative() == new_organism.getInitiative())
            {
                if (tmp.next == null)
                    break;
                else
                    tmp = tmp.next;
            }
            if (new_organism.getInitiative() <= tmp.organism.getInitiative())
            {
                // Wstawianie przed elementem
                if (tmp.next != null)
                {
                    q.next = tmp;
                    q.previous = tmp.previous;
                    tmp.previous.next = q;
                    tmp.previous = q;
                    q.organism = new_organism;
                    return 0;
                }
                // Wstawianie za elementem
                if (tmp.next == null)
                {
                    q.next = null;
                    tail.next = q;
                    q.previous = tail;
                    tail = q;
                    q.organism = new_organism;
                    return 0;
                }
            }

            if (new_organism.getInitiative() > tmp.organism.getInitiative())
            {
                if (tmp.previous == null)
                {
                    q.next = tmp;
                    q.previous = tmp.previous;
                    tmp.previous = q;
                    head = q;
                    q.organism = new_organism;
                    return 0;
                }
                if (tmp.previous != null)
                {
                    q.next = tmp;
                    q.previous = tmp.previous;
                    tmp.previous.next = q;
                    tmp.previous = q;
                    q.organism = new_organism;
                    return 0;
                }
            }
            return 0;
        }
        // Wyszukiwanie elementu do usunięcia
        private List_element search_element(Organism kill_organism)
        {
            List_element tmp = new List_element();
            if (head == null)
                return null;
            if (head != null)
            {
                tmp = head;
                while (tmp != null)
                {
                    if (tmp.organism == kill_organism)
                        return tmp;
                    tmp = tmp.next;
                }
            }
            return null; // jesli nie znaleziono
        }
   
       internal int delete_element(Organism kill_organism)
        {
            List_element tmp = new List_element();
            List_element q = new List_element();
            tmp = search_element(kill_organism);
            // usuwanie elementu z poczatku list, ktory nie jest tez ostatnim
            if (tmp == head && tmp != tail)
            {
                head = tmp.next;
                tmp.next.previous = null;
                return 0;
            }
            // usuwanie jedynego elemntu w liscie
            if (tmp == head && tmp == tail)
            {
                head = null;
                tail = null;
                return 0;
            }
            // usuwanie elementu w srodku
            if (tmp != head && tmp != tail)
            {
                tmp.previous.next = tmp.next;
                tmp.next.previous = tmp.previous;
                tmp = null;
                return 0;
            }
            //ususwanie ostatniego elementu ktory nie jest pierwszym
            if (tmp != head && tmp == tail)
            {
                tail = tmp.previous;
                tmp.previous.next = null;
                return 0;
            }
            return 0;
        }
    }
}
