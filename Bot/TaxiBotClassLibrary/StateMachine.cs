using System;
using System.Collections.Generic;
using TaxiBotClassLibrary.InterCommands;

namespace TaxiBotClassLibrary
{
    static class StateMachine
    {
        public static void Run()
        {
            NDFAutomate<ICommand>.DefineNDFAutomate(Data<ICommand>.States, Data<ICommand>.Commands, Data<ICommand>.View);
        }

        public static void Revoke()
        {
            NDFAutomate<ICommand>.DropNDFAutomate();
        }
    }

    public class NDFAutomate<T>
    {
        public static List<State<T>> States { get; private set; }
        public static List<T> Functions { get; private set; }

        public static void DefineNDFAutomate(List<State<T>> states, List<T> functions,
            Action<List<T>, List<State<T>>> action)
        {
            States = states;
            Functions = functions;
            action(Functions, States);
            CurrentState = States[0];
        }

        public static void DropNDFAutomate()
        {
            States = null;
            Functions = null;
        }

        public static State<T> CurrentState { get; set; }
    }

    public class State<T>
    {
        public Dictionary<T, State<T>> Transition { get; set; }
        public bool IsLastState { get; }

        public string Name { get; }
        public State(string name, bool isLast = false)
        {
            Name = name;
            IsLastState = isLast;
        }

    }
}
