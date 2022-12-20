using System;

namespace Automat_Vanzari
{
    /// <summary>
    /// <para> Context: </para>
    /// <para> References a concrete state </para>
    /// <para> Delegates all state-specific work to it, through the state interface </para>
    /// <para> Handles transitions and non-state-specific tasks </para>
    /// </summary>
    class Context
    {
        // Reference to current state
        private State currentState;

        // Constructor
        public Context(State nextState)
        { Transition(nextState); }

        /// <summary> Transition to nextState </summary>
        public void Transition(State nextState)
        {
            currentState = nextState;
            currentState.SetContext(this);
        }

        /// <summary> Shows / refreshes vending machine UI</summary>
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

        // ------------------------ State-specific behaviour ------------------------ //
        // Gets delegated to currentState

        /// <summary> Insert a nickel into the vending machine </summary>
        public void InsertNickel()            // Request
        { currentState.NickelInserted(); }    // Handle

        /// <summary> Insert a dime into the vending machine </summary>
        public void InsertDime()
        { currentState.DimeInserted(); }

        /// <summary> Insert a quarter into the vending machine </summary>
        public void InsertQuarter()
        { currentState.QuarterInserted(); }

        // ------------------------------------------------------------------------- //
    }

    /// <summary>
    /// <para> State interface: </para>
    /// <para> All states inherit from it </para>
    /// <para> Concrete states will implement methods defined here </para>
    /// </summary>
    abstract class State
    {
        // Backreference to context
        // Used by States to transition through Context to other States
        protected Context currentContext;

        /// <summary> Update backreference to Context after transition </summary>
        public void SetContext(Context nextContext)
        { currentContext = nextContext; }
        
        /// <summary> Insert a nickel </summary>
        public abstract void NickelInserted();

        /// <summary> Insert a dime </summary>
        public abstract void DimeInserted();

        /// <summary> Insert a quarter </summary>
        public abstract void QuarterInserted();

        /// <summary> Show current balance </summary>
        public abstract void ShowBalance();

        /// <summary> Dispense product, then wait for key press </summary>
        public void DispenseProduct()
        { 
            Console.WriteLine();
            Console.WriteLine("Product dispensed! :)");
            Console.WriteLine("Press any key to take product...");
            Console.ReadKey();
        }

        /// <summary> Return change, then wait for key press </summary>
        public void ReturnChange(string coinName)
        { 
            Console.WriteLine();
            Console.WriteLine($"{coinName} returned!");
            Console.WriteLine("Press any key to take change...");
            Console.ReadKey();
        }
    }

    /// <summary> Concrete state A: Current balance: 0c </summary>
    class StateA : State
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

            // Return nickel in change
            ReturnChange("Nickel");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    /// <summary> Concrete state B: Current balance: 5c </summary>
    class StateB : State
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

            // Return dime in change
            ReturnChange("Dime");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    /// <summary> Concrete state C: Current balance: 10c </summary>
    class StateC : State
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
            
            // Return nickel in change
            ReturnChange("Nickel");

            // Return dime in change
            ReturnChange("Dime");

            // Transition to A
            currentContext.Transition(new StateA());
        }
    }

    /// <summary> Concrete state D: Current balance: 15c </summary>
    class StateD : State
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
        
            // Return nickel in change
            ReturnChange("Nickel");

            // Transition to A
            currentContext.Transition(new StateA());
        }

        public override void QuarterInserted()  // + 25c
        {
            // Dispense merchandise
            DispenseProduct();

            // Return nickel in change
            ReturnChange("Nickel");

            // Return dime in change
            ReturnChange("Dime");

            // Transition to B
            currentContext.Transition(new StateB());
        }
    }
}