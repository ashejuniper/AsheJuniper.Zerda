namespace AsheJuniper.Zerda.Engine;

using Raylib_cs;

public sealed class ZerdaEngine
{
    private static ZerdaEngine? instance;
    private static readonly object padlock = new object();

    public static ZerdaEngine Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ZerdaEngine();
                }

                return instance;
            }
        }
    }

    public Window Window => Window.Instance;

    public ZerdaEngine(
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        Initialize(
            fullscreen,
            resizeable,
            undecorated
        );

        MainLoop();
    }

    public static ZerdaEngine Start(
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        instance = new ZerdaEngine(
            fullscreen,
            resizeable,
            undecorated
        );

        var frameRate = ZerdaEngine.Instance.Window.FrameRate;

        Console.WriteLine(
            "Frame rate: {0}",
            frameRate
        );

        return Instance;
    }

    internal Window CreateWindow(
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        Window.instance = new Window(
            this,
            fullscreen,
            resizeable,
            undecorated
        );

        return Window.instance;
    }

    private void Initialize(
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        Window.Start(
            this,
            fullscreen,
            resizeable,
            undecorated
        );
    }

    private void MainLoop()
    {
        while (!Window.Instance.ShouldExit)
        {
            UpdateApplicationState();

            Window.Render();
        }

        Raylib.CloseWindow();
    }

    private void UpdateApplicationState()
    {
        // TODO: Tick update logic goes here.
        // TODO: Call all OnTick event listeners.
    }
}
