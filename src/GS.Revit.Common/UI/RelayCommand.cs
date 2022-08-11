using System;
using System.Windows.Input;

namespace GS.Revit.Common.UI
{
    /// <summary>
    /// General command to conveniently invoke any method.
    /// </summary>
    /// <seealso cref="ICommand" />
    public class RelayCommand : ICommand
    {
        private readonly Action commandAction;
        private readonly Predicate<object> executionPredicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="commandAction">Invoked when command is executed.</param>
        public RelayCommand(Action commandAction, Predicate<object> canExecute = null)
        {
            this.commandAction = commandAction;
            this.executionPredicate = canExecute;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return executionPredicate?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            commandAction?.Invoke();
        }
    }

    /// <summary>
    /// General command to conveniently invoke any method.
    /// </summary>
    /// <typeparam name="T">Type of the parameter passed to the command's 'Execute' method.</typeparam>
    /// <seealso cref="ICommand" />
    public class RelayCommand<T> : ICommand
    {
        private Action<T> commandAction;
        private readonly Predicate<object> executionPredicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="commandAction">Invoked when command is executed.</param>
        public RelayCommand(Action<T> commandAction, Predicate<object> canExecute = null)
        {
            this.commandAction = commandAction;
            this.executionPredicate = canExecute;
        }

        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        public bool CanExecute(object parameter)
        {
            return executionPredicate?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            commandAction?.Invoke((T)parameter);
        }
    }
}
