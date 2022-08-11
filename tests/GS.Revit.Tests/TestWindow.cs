using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Frame = System.Windows.Controls.Frame;
using Grid = System.Windows.Controls.Grid;

public static class TestWindow
{
    /// <summary>
    /// Shows the window of specified type.
    /// </summary>
    /// <typeparam name="TWindow">The type of the window.</typeparam>
    /// <param name="args">The arguments to pass in constructor of TWindow.</param>
    public static void Show<TWindow>(params object[] args)
        where TWindow : Window
    {
        var testWindow = (TWindow)Activator.CreateInstance(typeof(TWindow), args);

        // Initiates the dispatcher thread shutdown when the window closes.
        testWindow.Closed += (s, e) => testWindow.Dispatcher.InvokeShutdown();
        testWindow.Show();

        // Makes the thread support message pumping.
        Dispatcher.Run();
        Dispatcher.CurrentDispatcher.InvokeShutdown();
    }

    /// <summary>
    /// Shows the window of specified type.
    /// </summary>
    /// <param name="args">The arguments to pass in constructor of TWindow.</param>
    /// <param name="viewModel">ViewModel for the window.</param>
    /// <typeparam name="TWindow">The type of the window.</typeparam>
    public static void Show<TWindow, TViewModel>(TViewModel viewModel, params object[] args)
        where TWindow : Window
    {
        var testWindow = (TWindow)Activator.CreateInstance(typeof(TWindow), args);
        testWindow.DataContext = viewModel;

        // Initiates the dispatcher thread shutdown when the window closes.
        testWindow.Closed += (s, e) => testWindow.Dispatcher.InvokeShutdown();
        testWindow.Show();

        // Makes the thread support message pumping.
        Dispatcher.Run();
        Dispatcher.CurrentDispatcher.InvokeShutdown();
    }

    /// <summary>
    /// Shows a window with the page of specified type in it.
    /// </summary>
    /// <typeparam name="TPage">The type of page to place into the window.</typeparam>
    /// <param name="args">The arguments to pass in constructor of TPage.</param>
    public static void ShowPage<TPage>(int width = 400, int height = 600, params object[] args)
        where TPage : Page
    {
        var testPage = (TPage)Activator.CreateInstance(typeof(TPage), args);

        var frame = new Frame
        {
            Content = testPage,
        };

        var grid = new Grid();
        grid.Children.Add(frame);

        var window = new Window
        {
            Title = testPage.Title,
            Content = grid,
            Width = width,
            Height = height,
        };

        // Initiates the dispatcher thread shutdown when the window closes.
        window.Closed += (s, e) => testPage.Dispatcher.InvokeShutdown();
        window.Show();

        // Makes the thread support message pumping.
        Dispatcher.Run();
        Dispatcher.CurrentDispatcher.InvokeShutdown();
    }
}