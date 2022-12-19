using System;

namespace Automat_Vanzari
{

    class Context
    {
        // Reference to current state
        private State currentState = null;

        // Constructor
        public Context(State nextState)
        { Transition(nextState); }

        // Transition: currentState -> nextState
        public void Transition(State nextState)
        {
            currentState = nextState;
            currentState.SetContext(this);
        }

        // State-specific behavior delegated to current state object
        public void InsertNickel()  // Request
        { currentState.NickelInserted(); }  // Handle

        public void InsertDime()
        { currentState.DimeInserted(); }

        public void InsertQuarter()
        { currentState.QuarterInserted(); }

        // Show / refresh UI
        public void ShowUI()
        {
            Console.Clear();
            Console.WriteLine("-------------------------- Vending Machine --------------------------");
            Console.WriteLine("Product price: 20c");
            Console.WriteLine("Accepted coins: Nickel (N / 5c), Dime (D / 10c), Quarter (Q / 25c)");
            Console.WriteLine("Type 'stop' or 'exit' to exit");
            Console.WriteLine();

            Console.Write("Current balance: ");
            currentState.ShowBalance();
            
            // Console.Write("Current state: ");
            // Console.WriteLine(currentState.GetType().Name);

            Console.WriteLine();
            Console.Write("Insert a coin: ");
        }
    }

    abstract class State
    {
        // Backreference to context
        // Used by States to transition the Context to other States
        protected Context currentContext;

        // Update backreference to Context
        public void SetContext(Context nextContext)
        { currentContext = nextContext; }
        
        public abstract void NickelInserted();
        public abstract void DimeInserted();
        public abstract void QuarterInserted();
        public abstract void ShowBalance();

        public void DispenseProduct()
        { 
            Console.WriteLine();
            Console.WriteLine("Product dispensed! :)");
            Console.WriteLine("Press any key to take product...");
            Console.ReadKey();
        }

        public void ReturnChange(string coinName)
        { 
            Console.WriteLine();
            Console.WriteLine($"{coinName} returned!");
            Console.WriteLine("Press any key to take change...");
            Console.ReadKey();
        }
    }

    class StateA : State   // Current balance: 0c
    {
        public override void ShowBalance()
        { Console.WriteLine("0c"); }

        public override void NickelInserted()  // + 5c
        {
            // Transition to B
            currentContext.Transition(new StateB());
        }

        public override void DimeInserted()  // + 10c
        {
            // Transition to C
            currentContext.Transition(new StateC());
        }

        public override void QuarterInserted()  // + 25c
        {
            // Dispense merchandise
            DispenseProduct();

            // Return NICKEL in change
            ReturnChange("Nickel");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    class StateB : State   // Current balance: 5c
    {
        public override void ShowBalance()
        { Console.WriteLine("5c"); }

        public override void NickelInserted()  // + 5c
        {
            // Transition to C
            currentContext.Transition(new StateC());
        }

        public override void DimeInserted()  // + 10c
        {
            // Transition to D
            currentContext.Transition(new StateD());
        }

        public override void QuarterInserted()  // + 25c
        {
            // Dispense merchandise
            DispenseProduct();

            // Return DIME in change
            ReturnChange("Dime");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    class StateC : State   // Current balance: 10c
    {
        public override void ShowBalance()
        { Console.WriteLine("10c"); }

        public override void NickelInserted()  // + 5c
        {
            // Transition to D
            currentContext.Transition(new StateD());
        }

        public override void DimeInserted()  // + 10c
        {
            // Dispense merchandise
            DispenseProduct();

            // Transition to A
            currentContext.Transition(new StateA());
        }

        public override void QuarterInserted()  // + 25c
        {
            // Dispense merchandise
            DispenseProduct();
            
            // Return NICKEL in change
            ReturnChange("Nickel");

            // Return DIME in change
            ReturnChange("Dime");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    class StateD : State   // Current balance: 15c
    {
        public override void ShowBalance()
        { Console.WriteLine("15c"); }

        public override void NickelInserted()  // + 5c
        {
            // Dispense merchandise
            DispenseProduct();

            // Transition to A
            currentContext.Transition(new StateA());
        }

        public override void DimeInserted()  // + 10c
        {
            // Dispense merchandise
            DispenseProduct();
        
            // Return NICKEL in change
            ReturnChange("Nickel");

            // Transition to A
            currentContext.Transition(new StateA());
        }

        public override void QuarterInserted()  // + 25c
        {
            // Dispense merchandise
            DispenseProduct();

            // Return NICKEL in change
            ReturnChange("Nickel");

            // Return DIME in change
            ReturnChange("Dime");

            // Transition to B
            currentContext.Transition(new StateB());
        }
    }
}