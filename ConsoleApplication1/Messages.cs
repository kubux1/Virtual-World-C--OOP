using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class StackElement
    {
        public StackElement previous;
        public String text;

        public StackElement() {
            this.previous = null;
            this.text = "";
        }
    };

    class Messages : StackElement
    {
        StackElement top = new StackElement();

        public Messages()
        {
            this.top = null;
        }

        public void push(String text_oryg)
        {
            if (text_oryg != "")
            {
                StackElement newEl = new StackElement();
                newEl.previous = null;
                if (top != null)
                    newEl.previous = top;
                newEl.text = text_oryg;
                top = newEl;
            }
        }

        void popBack()
        {
            if (top != null)
            {
                StackElement tmp = top;
                top = top.previous;
            }
        }

       public String show(int messageNumber)
        {
            StackElement tmp = top;
            if (top == null)
            {
                return "";
            }
            else
            {
                for (int i = 0; i < messageNumber; i++)
                {
                    tmp = tmp.previous;
                }
                if (tmp != null)
                {
                    return tmp.text;
                }
                else return "";
            }
        }
    }
}
