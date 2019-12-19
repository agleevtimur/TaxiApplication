using System;
using System.Collections.Generic;
using TaxiBotClassLibrary.InterCommands;

namespace TaxiBotClassLibrary
{
    static class StateMachine
    {
        public static void Run()
        {
            var commands = new List<ICommand>
            {
                new SetTime() as ICommand, // 0
                new SetStartPoint() as ICommand, // 1
                new SetDestination() as ICommand, // 2
                new SetAmount() as ICommand, // 3
                new GetFellows() as ICommand // 4
            };

            var states = new List<State<ICommand>>()
            {
                new State<ICommand>(), // 0 Time
                new State<ICommand>(), // 1 LocationFrom
                new State<ICommand>(), // 2 LocationTo
                new State<ICommand>(),// 3 Amount
                new State<ICommand>(),// 4 Fellows
                new State<ICommand>(true) // 5 Finish
            };

            void action(List<ICommand> func, List<State<ICommand>> statess)
            {
                statess[0].Transition = new Dictionary<ICommand, State<ICommand>>();// start

                for (int i = 1; i < statess.Count; i++)
                {
                    if (statess[i].IsLastState != true)//состояние finish не может сделать откат или принять какие то данные
                    {
                        statess[i].Transition = new Dictionary<ICommand, State<ICommand>>();
                    }
                    statess[i - 1].Transition.Add(func[i - 1], statess[i]);
                };
            }

            NDFAutomate<ICommand>.DefineNDFAutomate(states, commands, action);
        }

        public static void Revoke()
        {
            NDFAutomate<ICommand>.DropNDFAutomate();
        }
    }

    public static class NDFAutomate<T>
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
