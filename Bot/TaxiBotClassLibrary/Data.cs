using System;
using System.Collections.Generic;
using System.Text;
using TaxiBotClassLibrary.InterCommands;

namespace TaxiBotClassLibrary
{
    static class Data<T>
        where T : class
    {
        public static readonly List<T> Commands = new List<T>
            {
                new SetTime() as T, // 0
                new SetStartPoint() as T, // 1
                new SetDestination() as T, // 2
                new SetAmount() as T // 3
            };

        public static readonly List<State<T>> States = new List<State<T>>()
            {
                new State<T>(),// 0 start
                new State<T>(), // 1 Time
                new State<T>(), // 2 LocationFrom
                new State<T>(), // 3 LocationTo
                new State<T>(true) // 4 Awaiting
            };

        public static void View(List<ICommand> func, List<State<ICommand>> statess)
        {
            statess[0].Transition = new Dictionary<ICommand, State<ICommand>>();// time

            for (int i = 1; i < statess.Count; i++)
            {
                if (statess[i].IsLastState != true)
                {
                    statess[i].Transition = new Dictionary<ICommand, State<ICommand>>();
                }
                statess[i - 1].Transition.Add(func[i - 1], statess[i]);
            };
        }
    }
}
