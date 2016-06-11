using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class World
    {
        internal Container worldOrganisms = new Container();
        internal List_element tmp = new List_element();
        private Organism[][] organismsArray;
        internal Human_Control humanControl = new Human_Control();
        internal Messages messages = new Messages();
        private bool gameStatus_;
        private int height_;
        private int weight_;

        public World() { }

        public World(int height, int weight)
        {
            Randoms randomOrganism = new Randoms();
            this.gameStatus_ = true;
            this.height_ = height;
            this.weight_ = weight;
            organismsArray = new Organism[weight][];

            for (int i = 0; i < weight; i++)
            {
                organismsArray[i] = new Organism[height];
            }

            this.fillArray();
            new Human(this, worldOrganisms, organismsArray);

            for (int i = 0; i < 15; i++)
            {
                int organismNumber = randomOrganism.randOrganism();
                switch (organismNumber)
                {
                    case 0:
                        {
                            new Wolf(this, worldOrganisms, organismsArray);
                            break;
                        }
                    case 1:
                        {
                            new Sheep(this, worldOrganisms, organismsArray);
                            break;
                        }

                    case 2:
                        {
                            new Turtle(this, worldOrganisms, organismsArray);
                            break;
                        }

                    case 3:
                        {
                            new Grass(this, worldOrganisms, organismsArray);
                            break;
                        }
                    case 4:
                        {
                            new Dandelion(this, worldOrganisms, organismsArray);
                            break;
                        }
                    case 5:
                        {
                            new Berries(this, worldOrganisms, organismsArray);
                            break;
                        }
                    default:
                        break;
                }
            }
            this.paintWorld();
        }

        public void makeTour()
        {
            List_element tmp_dead = new List_element();
            this.tmp = worldOrganisms.head;
            while (this.tmp != null)
            {
                tmp_dead = this.tmp.next;
                this.tmp.organism.action(worldOrganisms, organismsArray);
                if (this.tmp == null)
                    this.tmp = tmp_dead;
                else
                    this.tmp = this.tmp.next;
            }

            this.paintWorld();
        }

        void fillArray()
        {

            for (int i = 0; i < this.weight_; i++)
            {
                for (int j = 0; j < this.height_; j++)
                {
                    this.organismsArray[i][j] = null;
                }
            }
        }

        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origRow + x, origCol + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        enum humanMoves { goUp = -1, goDown = 1, goLeft = -1, goRight = 1 };

        void paintWorld()
        {
            Console.Clear();
            char humanControls;
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            this.tmp = worldOrganisms.head;


            for (int i = 0; i < weight_; i++)
            {

                WriteAt("#", i, height_);
            }

            for (int i = 0; i < height_ + 1; i++)
            {

                WriteAt("#", weight_, i);
            }

            while (tmp != null)
            {
                tmp.organism.setOrganismColor();
                WriteAt(tmp.organism.getOrganismRepresentation(), tmp.organism.getPosX(), tmp.organism.getPosY());
                Console.ResetColor();
                tmp = tmp.next;
            }
            var organismsNames = new List<string> { "H - Human", "W - Wolf", "S - Sheep", "T - Turtle", "G - Grass", "D - Dandelion", "B - Berries", "E - Activate Human Power",
            "Q - Quit Game" };
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("Legend:", 40, 0);
            Console.ResetColor();
            paintMessages();
            int y = 0;
            foreach (var name in organismsNames)
            {
                WriteAt(name + " ", 40, 2 + y);
                y += 1;
            }
            humanControl.deleteCoordinates();
            humanControls = Console.ReadKey().KeyChar;
            controls(humanControls);  
            Console.WriteLine();
        }

        void controls(char humanControls)
        {
            if (humanControls == 'w')
            {
                humanControl.setMoveY((int)humanMoves.goUp);
            }
            if (humanControls == 's')
            {
                humanControl.setMoveY((int)humanMoves.goDown);
            }
            if (humanControls == 'a')
            {
                humanControl.setMoveX((int)humanMoves.goLeft);
            }
            if (humanControls == 'd')
            {
                humanControl.setMoveX((int)humanMoves.goRight);
            }
            if (humanControls == 'e')
            {
                humanControl.setHumanSuperPower();
                humanControls = Console.ReadKey().KeyChar;
                controls(humanControls);
            }
            if (humanControls == 'q')
            {
                gameStatus_ = false;
            }

        }

        void paintMessages()
        {
            WriteAt("Messages", 10, 32);
            int messageNumber = 0;
            int moveCoulmne = 0;
            while (messages.show(messageNumber) != "")
            {

                if (moveCoulmne == 22) //46
                    break;
                WriteAt(messages.show(messageNumber), 3, 55 - moveCoulmne);
                moveCoulmne++;
                messageNumber++;
            }
        }

        Container getContainer()
        {
            return this.worldOrganisms;
        }
        List_element getTmp()
        {
            return this.tmp;
        }

        Human_Control getHumanControl()
        {
            return this.humanControl;
        }

        public bool getGameStatus()
        {
            return gameStatus_;
        }
        public int getHeight()
        {
            return this.height_;
        }
        public int getWeight()
        {
            return this.weight_;
        }
        public void putMessage(String statement)
        {
            this.messages.push(statement);
        }
    }
}
