namespace ConsoleApp4
{
     internal class Animal
    {
        
        public virtual void AnimalSound()
        {
            System.Console.WriteLine("Animal sound");
        }
    }

    class pig: Animal
    {
        public override void AnimalSound()
        {
            System.Console.WriteLine("poink poink");
        }
    }
    class dog : Animal
    {
        public override void AnimalSound()
        {
            System.Console.WriteLine("bark bark");
        }
    }
}