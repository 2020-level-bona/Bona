using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueCommandProvider : ICommandProvider
{
    Queue<ICommand> commands;

    public QueueCommandProvider(Queue<ICommand> commands) {
        this.commands = commands;
    }

    public ICommand Next() {
        if (commands.Count == 0)
            return null;
        return commands.Dequeue();
    }
}
