using System;
using System.Collections.Generic;
using TaxiBotClassLibrary.InterCommands;

namespace TaxiBotClassLibrary
{
    static class StateMachine
    {
        public static void Run()
        {
            void action(List<ICommand> func, List<State<ICommand>> states)
            {
                states[0].Transition = new Dictionary<ICommand, State<ICommand>>();// start

                for (int i = 1; i < states.Count; i++)
                {
                    if (states[i].IsLastState != true)//состояние finish не может сделать откат или принять какие то данные
                    {
                        states[i].Transition = new Dictionary<ICommand, State<ICommand>>();
                    }
                    states[i - 1].Transition.Add(func[i - 1], states[i]);
                };
            }

            NDFAutomate<ICommand>.DefineNDFAutomate(Data<ICommand>.States, Data<ICommand>.Commands, action);
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
            NDFAutomate<ICommand>.Functions = null;
            NDFAutomate<ICommand>.States = null;
            NDFAutomate<ICommand>.CurrentState = null;
        }
        public static State<T> CurrentState { get; set; }
    }

    public class State<T>
    {
        public Dictionary<T, State<T>> Transition { get; set; }
        public bool IsLastState { get; }
        public string Container { get; set; }

        public State( bool isLast = false)
        {
            IsLastState = isLast;
        }

    }
}
