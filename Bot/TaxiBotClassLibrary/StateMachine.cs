using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using TaxiBotClassLibrary.InterCommands;

namespace TaxiBotClassLibrary
{
    static class StateMachine
    {
        public static void Run()
        {
            var commands = GetFunctions();
            
            var states = new List<State<InterCommand>>()
            {
                new State<InterCommand>("Time"), // 0
                new State<InterCommand>("LocationFrom"), // 1
                new State<InterCommand>("LocationTo"), // 2
                new State<InterCommand>("Amount"),// 3
                new State<InterCommand>("Finish", true) // 4
            };

            Action<List<InterCommand>, List<State<InterCommand>>> action = (func, statess) =>
            {
                statess[0].Transition = new Dictionary<InterCommand, State<InterCommand>>();// start

                for (int i = 1; i < statess.Count; i++)
                {
                    if (statess[i].IsLastState != true)//состояние finish не может сделать откат или принять какие то данные
                    {
                        statess[i].Transition = new Dictionary<InterCommand, State<InterCommand>>();
                    }
                    statess[i - 1].Transition.Add(func[i - 1], statess[i]);
                };
            };

            NDFAutomate<InterCommand>.DefineNDFAutomate(states, commands, action);
        }

        private static List<InterCommand> GetFunctions()
        {
            var list = new List<InterCommand>();
            list.Add(Activator.CreateInstance(typeof(SetTime)) as InterCommand);
            list.Add(Activator.CreateInstance(typeof(SetStartPoint)) as InterCommand);
            list.Add(Activator.CreateInstance(typeof(SetDestination)) as InterCommand);
            list.Add(Activator.CreateInstance(typeof(SetAmount)) as InterCommand);
            return list;
        }
    }

    public static class NDFAutomate<T>
    {
        public static List<State<T>> States { get; set; }
        public static List<T> Functions { get; set; }

        public static void DefineNDFAutomate(List<State<T>> states, List<T> functions,
            Action<List<T>, List<State<T>>> action)
        {
            States = states;
            Functions = functions;
            action(Functions, States);
        }
    }

    public class State<T>
    {
        public Dictionary<T, State<T>> Transition { get; set; }

        public string Name { get; set; }
        public bool IsLastState { get; set; }

        public State(string name, bool isLast = false)
        {
            IsLastState = isLast;
            Name = name;
        }

    }
}
